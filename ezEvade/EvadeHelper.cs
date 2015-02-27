using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class EvadeHelper
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static float gameTime { get { return Game.ClockTime * 1000; } }

        public class PositionInfo
        {
            public int posDangerLevel = 0;
            public int posDangerCount = 0;
            public bool isDangerousPos = false;
            public float distanceToMouse = 0;
            public List<int> dodgeableSpells = new List<int>();
            public List<int> undodgeableSpells = new List<int>();
            public List<int> spellList = new List<int>();
            public Vector2 position;
            public float timestamp;
            public bool hasExtraDistance = false;

            public PositionInfo(
                Vector2 position,
                int posDangerLevel,
                int posDangerCount,
                bool isDangerousPos,
                float distanceToMouse,
                List<int> dodgeableSpells,
                List<int> undodgeableSpells)
            {
                this.position = position;
                this.posDangerLevel = posDangerLevel;
                this.posDangerCount = posDangerCount;
                this.isDangerousPos = isDangerousPos;
                this.distanceToMouse = distanceToMouse;
                this.dodgeableSpells = dodgeableSpells;
                this.undodgeableSpells = undodgeableSpells;
                this.timestamp = gameTime;
            }

            public PositionInfo(
                Vector2 position,
                bool isDangerousPos,
                float distanceToMouse)
            {
                this.position = position;
                this.isDangerousPos = isDangerousPos;
                this.distanceToMouse = distanceToMouse;
                this.timestamp = gameTime;
            }
        }

        public static int GetSpellDangerLevel(Spell spell)
        {
            var dangerStr = Evade.menu.SubMenu("Spells").SubMenu(spell.info.charName + spell.info.spellName + "Settings")
                .Item(spell.info.spellName + "DangerLevel").GetValue<StringList>().SelectedValue;

            var dangerlevel = 1;

            switch (dangerStr)
            {
                case "Low":
                    dangerlevel = 1;
                    break;
                case "High":
                    dangerlevel = 3;
                    break;
                case "Extreme":
                    dangerlevel = 4;
                    break;
                default:
                    dangerlevel = 2;
                    break;
            }

            return dangerlevel;
        }

        public static string GetSpellDangerString(Spell spell)
        {
            switch (GetSpellDangerLevel(spell))
            {
                case 1:
                    return "Low";
                case 3:
                    return "High";
                case 4:
                    return "Extreme";
                default:
                    return "Normal";
            }
        }

        public static float GetSpellRadius(Spell spell)
        {
            var radius = Evade.menu.SubMenu("Spells").SubMenu(spell.info.charName + spell.info.spellName + "Settings")
                .Item(spell.info.spellName + "SpellRadius").GetValue<Slider>().Value;
            var extraRadius = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraSpellRadius").GetValue<Slider>().Value;

            return (float) (radius + extraRadius);
        }

        public static bool playerInSkillShot(Spell spell)
        {
            return inSkillShot(spell, myHero.ServerPosition.To2D(), myHero.BoundingRadius);
        }

        public static bool inSkillShot(Spell spell, Vector2 position, float radius)
        {
            if (spell.info.spellType == SpellType.Line)
            {
                Vector2 spellPos = SpellDetector.GetCurrentSpellPosition(spell); //leave little space at back of skillshot

                if (spell.info.projectileSpeed == float.MaxValue
                    && gameTime - spell.startTime > spell.info.spellDelay)
                {
                    return false;
                }

                var projection = position.ProjectOn(spellPos, spell.endPos);

                if (projection.SegmentPoint.Distance(spell.endPos) < 100) //Check Skillshot endpoints
                {
                    //unfinished
                }

                return projection.SegmentPoint.Distance(position) <= GetSpellRadius(spell) + radius;
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                return position.Distance(spell.endPos) <= GetSpellRadius(spell) + radius;
            }
            else if (spell.info.spellType == SpellType.Cone)
            {

            }
            return false;
        }

        public static IOrderedEnumerable<PositionInfo> GetBestPositionTest()
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraDelay").GetValue<Slider>().Value;

            if (Evade.menu.SubMenu("MiscSettings").Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 100;
                posRadius = 25;
            }

            List<PositionInfo> posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                int curRadius = radiusIndex * (2 * posRadius);
                int curCircleChecks = (int)Math.Ceiling((2 * Math.PI * (double)curRadius) / (2 * (double)posRadius));

                for (int i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2 * Math.PI / (curCircleChecks - 1)) * i; //check decimals
                    var pos = new Vector2((float)Math.Floor(heroPoint.X + curRadius * Math.Cos(cRadians)), (float)Math.Floor(heroPoint.Y + curRadius * Math.Sin(cRadians)));

                    var posInfo = canHeroWalkToPos(pos, myHero.MoveSpeed, extraDelayBuffer + Game.Ping);
                    posInfo.isDangerousPos = CheckDangerousPos(pos, 6);
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posTable.Add(posInfo);

                    /*
                    if (posDangerLevel > 0)
                    {
                        Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float) posRadius, Color.White, 3);
                    }*/

                    
                    var path = myHero.GetPath(pos.To3D());

                    //Render.Circle.DrawCircle(path[path.Length - 1], (float)posRadius, Color.White, 3);
                    //Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float)posRadius, Color.White, 3);

                    //var posOnScreen = Drawing.WorldToScreen(path[path.Length - 1]);
                    //Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + path.Length);
                }
            }

            var sortedPosTable = posTable.OrderBy(p => p.isDangerousPos).ThenBy(p => p.posDangerLevel).ThenBy(p => p.posDangerCount).ThenBy(p => p.distanceToMouse);

            return sortedPosTable;
        }

        public static PositionInfo GetBestPosition()
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraDelay").GetValue<Slider>().Value;
            var extraEvadeDistance = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;

            if (Evade.menu.SubMenu("MiscSettings").Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 150;
                posRadius = 25;
            }

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                int curRadius = radiusIndex * (2 * posRadius);
                int curCircleChecks = (int)Math.Ceiling((2 * Math.PI * (double)curRadius) / (2 * (double)posRadius));

                for (int i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2 * Math.PI / (curCircleChecks - 1)) * i; //check decimals
                    var pos = new Vector2((float)Math.Floor(heroPoint.X + curRadius * Math.Cos(cRadians)), (float)Math.Floor(heroPoint.Y + curRadius * Math.Sin(cRadians)));

                    var posInfo = canHeroWalkToPos(pos, myHero.MoveSpeed, extraDelayBuffer + Game.Ping);
                    posInfo.isDangerousPos = CheckDangerousPos(pos, 6);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;// ? 1 : 0;                    
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posTable.Add(posInfo);
                }
            }

            //testing
            var sortedPosTable = 
                posTable.OrderBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
                        .ThenBy(p => p.isDangerousPos)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPathCollision(myHero, posInfo.position) == false)
                    return posInfo;
            }

            return sortedPosTable.First();
        }

        public static PositionInfo GetBestPositionMovementBlock(Vector2 movePos)
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraEvadeDistance = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraAvoidDistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = movePos; // Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                int curRadius = radiusIndex * (2 * posRadius);
                int curCircleChecks = (int)Math.Ceiling((2 * Math.PI * (double)curRadius) / (2 * (double)posRadius));

                for (int i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2 * Math.PI / (curCircleChecks - 1)) * i; //check decimals
                    var pos = new Vector2((float)Math.Floor(heroPoint.X + curRadius * Math.Cos(cRadians)), (float)Math.Floor(heroPoint.Y + curRadius * Math.Sin(cRadians)));

                    bool isDangerousPos = CheckDangerousPos(pos, 6) || checkMoveToDirection(pos);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, isDangerousPos, dist);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable = 
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPathCollision(myHero, posInfo.position) == false)
                    return posInfo;                
            }

            return sortedPosTable.First();
        }

        public static PositionInfo GetBestPositionBlink()
        {
            int posChecked = 0;
            int maxPosToCheck = 100;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraEvadeDistance = 100;//Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraAvoidDistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                int curRadius = radiusIndex * (2 * posRadius);
                int curCircleChecks = (int)Math.Ceiling((2 * Math.PI * (double)curRadius) / (2 * (double)posRadius));

                for (int i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2 * Math.PI / (curCircleChecks - 1)) * i; //check decimals
                    var pos = new Vector2((float)Math.Floor(heroPoint.X + curRadius * Math.Cos(cRadians)), (float)Math.Floor(heroPoint.Y + curRadius * Math.Sin(cRadians)));

                    bool isDangerousPos = CheckDangerousPos(pos, 6) || checkMoveToDirection(pos);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, isDangerousPos, dist);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPointCollision(myHero, posInfo.position) == false)
                    return posInfo;
            }

            return sortedPosTable.First();
        }

        public static PositionInfo GetBestPositionDash(EvadeSpellData spell)
        {
            int posChecked = 0;
            int maxPosToCheck = 100;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraDelay").GetValue<Slider>().Value;
            var extraEvadeDistance = 100;// Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();
            List<int> spellList = GetSpellList();

            int minDistance = 50; //Math.Min(spell.range, minDistance)
            int maxDistance = int.MaxValue;

            if (spell.fixedRange)
            {
                minDistance = maxDistance = (int) spell.range;
            }

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                int curRadius = radiusIndex * (2 * posRadius) + (minDistance - 2 * posRadius);
                int curCircleChecks = (int)Math.Ceiling((2 * Math.PI * (double)curRadius) / (2 * (double)posRadius));

                for (int i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2 * Math.PI / (curCircleChecks - 1)) * i; //check decimals
                    var pos = new Vector2((float)Math.Floor(heroPoint.X + curRadius * Math.Cos(cRadians)), (float)Math.Floor(heroPoint.Y + curRadius * Math.Sin(cRadians)));

                    var posInfo = canHeroWalkToPos(pos, spell.speed, extraDelayBuffer + Game.Ping);
                    posInfo.isDangerousPos = CheckDangerousPos(pos, 6);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;// ? 1 : 0;                    
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posInfo.spellList = spellList;
                    posTable.Add(posInfo);
                }

                if (curRadius >= maxDistance)
                {
                    break;
                }
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPointCollision(myHero, posInfo.position) == false)
                    return posInfo;
            }

            return sortedPosTable.First();
        }

        public static bool CheckDangerousPos(Vector2 pos, float extraBuffer) //, List<int> dodgeTable)
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (inSkillShot(spell, pos, myHero.BoundingRadius + extraBuffer))
                {
                    return true;
                }
            }
            return false;
        }

        public static int CheckPosDangerLevel(Vector2 pos, float extraBuffer) 
        {
            var dangerlevel = 0;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (inSkillShot(spell, pos, myHero.BoundingRadius + extraBuffer))
                {
                    dangerlevel += GetSpellDangerLevel(spell);
                }
            }
            return dangerlevel;
        }

        public static List<int> GetSpellList()
        {
            List<int> spellList = new List<int>();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                spellList.Add(spell.spellID);
            }

            return spellList;
        }

        public static PositionInfo canHeroWalkToPos(Vector2 pos, float speed, float delay)
        {
            int posDangerLevel = 0;
            int posDangerCount = 0;
            List<int> dodgeableSpells = new List<int>();
            List<int> undodgeableSpells = new List<int>();
                      

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (GetSpellCollisionTimeToPos(spell, pos, speed, delay, myHero.ServerPosition.To2D()))
                {
                    posDangerLevel = Math.Max(posDangerLevel, GetSpellDangerLevel(spell));
                    posDangerCount += GetSpellDangerLevel(spell);
                    undodgeableSpells.Add(spell.spellID);
                }
                else
                {
                    dodgeableSpells.Add(spell.spellID);
                }                
            }

            return new PositionInfo(
                pos,
                posDangerLevel,
                posDangerCount,
                posDangerCount > 0,
                0,
                dodgeableSpells,
                undodgeableSpells);            
        }

        public static bool GetSpellCollisionTimeToPos(Spell spell, Vector2 pos, float speed, float delay, Vector2 heroPos)
        {
            var walkDir = (pos - heroPos).Normalized();
            var zVector = new Vector2(0, 0);

            /*
            if (Evade.menu.SubMenu("MiscSettings").Item("CalculateHeroPos").GetValue<bool>())
                heroPos = GetRealHeroPos(); //testing*/

            /*if (!myHero.IsMoving)
                walkDir = zVector;*/

            if (spell.info.spellType == SpellType.Line)
            {
                //zVector

                if (spell.info.projectileSpeed == float.MaxValue)
                {

                }

                var spellPos = SpellDetector.GetCurrentSpellPosition(spell);

                if (spell.spellObject == null) //rewrite this
                {
                    float spellTime = (gameTime - spell.startTime) - spell.info.spellDelay;
                    spellPos = spell.startPos + spell.direction * spell.info.projectileSpeed * (spellTime / 1000);
                }

                //Using triple checks
                //Check if skillshot will hit pos if hero is standing still
                bool isCollision = false;

                float standingCollisionTime = MathUtils.GetCollisionTime(pos, spellPos, zVector, spell.direction * spell.info.projectileSpeed, myHero.BoundingRadius, GetSpellRadius(spell), out isCollision);
                if (isCollision && standingCollisionTime > 0)
                {
                    if (spellPos.Distance(spell.endPos) / spell.info.projectileSpeed > standingCollisionTime)
                        return true; //if collision happens when the skillshot is in flight
                }


                //Check if skillshot will hit hero if hero is moving
                float movingCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir * speed, spell.direction * spell.info.projectileSpeed, myHero.BoundingRadius, GetSpellRadius(spell) + 5, out isCollision);
                if (isCollision && movingCollisionTime > 0)
                {
                    if (spellPos.Distance(spell.endPos) / spell.info.projectileSpeed > movingCollisionTime)
                        return true; //if collision happens when the skillshot is in flight
                }


                //Check if skillshot will hit hero if hero is moving and the skillshot is moved forwards by 50 units                

                spellPos = spellPos + spell.direction * spell.info.projectileSpeed * (delay / 1000); //move the spellPos by 50 miliseconds forwards
                spellPos = spellPos + spell.direction * 50; //move the spellPos by 50 units forwards              
                
                var finalExtraDelay = (50/spell.info.projectileSpeed) + (delay/1000);

                float extraCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir * speed, spell.direction * spell.info.projectileSpeed, myHero.BoundingRadius, GetSpellRadius(spell) + 5, out isCollision);
                if (isCollision && extraCollisionTime > -finalExtraDelay)
                {
                    if (spellPos.Distance(spell.endPos) / spell.info.projectileSpeed > extraCollisionTime)
                        return true; //if collision happens when the skillshot is in flight
                    else
                        return false;
                }
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                var spellHitTime = Math.Max(0, spell.endTime - gameTime);  //extraDelay
                var walkRange = heroPos.Distance(pos);
                var predictedRange = speed * (spellHitTime / 1000);
                var tHeroPos = heroPos + walkDir * Math.Min(predictedRange, walkRange); //Hero predicted pos

                return tHeroPos.Distance(spell.endPos) <= GetSpellRadius(spell) + myHero.BoundingRadius; //+ dodgeBuffer
            }
            else if (spell.info.spellType == SpellType.Cone)
            {
                var spellHitTime = Math.Max(0, spell.endTime - gameTime);  //extraDelay
                var tHeroPos = heroPos + walkDir * speed * (spellHitTime / 1000);

                return inSkillShot(spell, tHeroPos, myHero.BoundingRadius);
            }

            return false;
        }

        public static Vector2 GetRealHeroPos()
        {
            var path = myHero.Path;
            if (path.Length < 1)
            {
                return myHero.ServerPosition.To2D();
            }

            var serverPos = myHero.ServerPosition.To2D();
            var heroPos = myHero.Position.To2D();

            var walkDir = (path[path.Length - 1].To2D() - serverPos).Normalized();
            var realPos = heroPos + walkDir * myHero.MoveSpeed * ((float)Game.Ping / 2000);

            return realPos;
        }

        public static bool CheckPathCollision(Obj_AI_Base unit, Vector2 movePos)
        {
            var path = unit.GetPath(movePos.To3D());

            if (path.Length > 0)
            {
                if (movePos.Distance(path[path.Length - 1].To2D()) > 5 || path.Length > 2)
                {                    
                    return true;
                }
            }           

            return false;
        }

        public static bool CheckPointCollision(Obj_AI_Base unit, Vector2 movePos)
        {
            var path = unit.GetPath(movePos.To3D());
            
            if (path.Length > 0)
            {
                if (movePos.Distance(path[path.Length - 1].To2D()) > 5)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool isSamePosInfo(PositionInfo posInfo1, PositionInfo posInfo2)
        {
            return new HashSet<int>(posInfo1.spellList).SetEquals(posInfo2.spellList);
        }

        public static bool LineIntersectLinearSpell(Vector2 a, Vector2 b, Spell spell)
        {
            var myBoundingRadius = myHero.BoundingRadius;
            var spellDir = spell.direction;
            var pSpellDir = spell.direction.Perpendicular();
            var spellRadius = GetSpellRadius(spell);
            var spellPos = SpellDetector.GetCurrentSpellPosition(spell) - spellDir * myBoundingRadius; //leave some space at back of spell
            var endPos = spell.endPos + spellDir * myBoundingRadius; //leave some space at the front of spell

            var startRightPos = spellPos + pSpellDir * (spellRadius + myBoundingRadius);
            var startLeftPos = spellPos - pSpellDir * (spellRadius + myBoundingRadius);
            var endRightPos = endPos + pSpellDir * (spellRadius + myBoundingRadius);
            var endLeftPos = endPos - pSpellDir * (spellRadius + myBoundingRadius);

            bool int1 = MathUtils.CheckLineIntersection(a, b, startRightPos, startLeftPos);
            bool int2 = MathUtils.CheckLineIntersection(a, b, endRightPos, endLeftPos);
            bool int3 = MathUtils.CheckLineIntersection(a, b, startRightPos, endRightPos);
            bool int4 = MathUtils.CheckLineIntersection(a, b, startLeftPos, endLeftPos);

            if (int1 || int2 || int3 || int4)
            {
               return true;
            }

            return false;
        }

        public static bool checkMoveToDirection(Vector2 movePos)
        {
            var heroPoint = myHero.ServerPosition.To2D();
            var dir = (movePos - heroPoint).Normalized();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (!playerInSkillShot(spell))
                {
                    Vector2 spellPos = SpellDetector.GetCurrentSpellPosition(spell);                    

                    if (spellPos.Distance(heroPoint) > 500 + GetSpellRadius(spell))
                    {
                        return GetSpellCollisionTimeToPos(spell, movePos, myHero.MoveSpeed, 0, myHero.ServerPosition.To2D());
                    }
                    
                    if (spell.info.spellType == SpellType.Line)
                    {
                        if (LineIntersectLinearSpell(heroPoint, movePos, spell))
                            return true;
                    }
                    else if (spell.info.spellType == SpellType.Circular)
                    {
                        bool isCollision = false;
                        var collisionTime = MathUtils.GetCollisionTime(heroPoint, spell.endPos, dir * myHero.MoveSpeed, new Vector2(0, 0), 1, 
                            GetSpellRadius(spell) + myHero.BoundingRadius, out isCollision);
                        if (collisionTime > 0)
                        {
                            if (!(collisionTime >= heroPoint.Distance(movePos) / myHero.MoveSpeed)) //collision occurs when hero is moving to the destination
                            {
                                return true;
                            }
                        }
                    }
                    else if (spell.info.spellType == SpellType.Cone)
                    {

                    }
                }
            }

            return false;
        }

    }
}
