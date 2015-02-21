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
            public int posDangerLevel;
            public int posDangerCount;
            public bool isDangerousPos;
            public float dist;
            public List<int> dodgeableSpells;
            public Vector2 position;
            public float timestamp;

            public PositionInfo(
                Vector2 position,
                int posDangerLevel,
                int posDangerCount,
                bool isDangerousPos,
                float dist,
                List<int> dodgeableSpells)
            {
                this.position = position;
                this.posDangerLevel = posDangerLevel;
                this.posDangerCount = posDangerCount;
                this.isDangerousPos = isDangerousPos;
                this.dist = dist;
                this.dodgeableSpells = dodgeableSpells;
                this.timestamp = gameTime;
            }
        }

        public static int GetSpellDangerLevel(Spell spell)
        {
            var dangerStr = Evade.menu.SubMenu("Spells").SubMenu(spell.info.charName + spell.info.spellName + "Settings").Item("DangerLevel").GetValue<StringList>().SelectedValue;

            var dangerlevel = 1;

            switch (dangerStr)
            {
                case "Low":
                    dangerlevel = 0;
                    break;
                case "High":
                    dangerlevel = 2;
                    break;
                case "Extreme":
                    dangerlevel = 3;
                    break;
                default:
                    dangerlevel = 1;
                    break;
            }

            return dangerlevel;
        }

        public static float GetSpellRadius(Spell spell)
        {
            var radius = Evade.menu.SubMenu("Spells").SubMenu(spell.info.charName + spell.info.spellName + "Settings")
                .Item("SpellRadius").GetValue<Slider>().Value;
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

        public static PositionInfo GetBestPositionTest()
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

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

                    /*var ret = canHeroWalkToPos(pos, myHero.MoveSpeed, 250);
                    int posDangerLevel = ret.Item1;
                    int posDangerCount = ret.Item2;
                    List<int> dodgeableSpells = ret.Item3;

                    if (posDangerLevel > 0)
                    {
                        Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float) posRadius, Color.White, 3);
                    }

                    bool isDangerousPos = CheckDangerousPos(pos, 0);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, posDangerLevel, posDangerCount, isDangerousPos, dist, dodgeableSpells);
                    posTable.Add(posInfo);*/

                    var path = myHero.GetPath(pos.To3D());

                    //Render.Circle.DrawCircle(path[path.Length - 1], (float)posRadius, Color.White, 3);
                    Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float)posRadius, Color.White, 3);

                    var posOnScreen = Drawing.WorldToScreen(path[path.Length - 1]);
                    Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + path.Length);
                }
            }

            var sortedPosTable = posTable.OrderBy(p => p.isDangerousPos).ThenBy(p => p.posDangerLevel).ThenBy(p => p.posDangerCount).ThenBy(p => p.dist);

            return sortedPosTable.First();
        }

        public static PositionInfo GetBestPosition()
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraDelay").GetValue<Slider>().Value;

            if (Evade.menu.SubMenu("MiscSettings").Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 100;
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

                    var ret = canHeroWalkToPos(pos, myHero.MoveSpeed, 60 + Game.Ping);
                    int posDangerLevel = ret.Item1;
                    int posDangerCount = ret.Item2;
                    List<int> dodgeableSpells = ret.Item3;

                    bool isDangerousPos = CheckDangerousPos(pos, 6);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, posDangerLevel, posDangerCount, isDangerousPos, dist, dodgeableSpells);
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable = posTable.OrderBy(p => p.isDangerousPos).ThenBy(p => p.posDangerLevel).ThenBy(p => p.posDangerCount).ThenBy(p => p.dist);

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

                    var posInfo = new PositionInfo(pos, 0, 0, isDangerousPos, dist, null);
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable = posTable.OrderBy(p => p.isDangerousPos).ThenBy(p => p.dist);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPathCollision(myHero, posInfo.position) == false)
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

        public static Tuple<int, int, List<int>> canHeroWalkToPos(Vector2 pos, float speed, float delay)
        {
            int posDangerLevel = 0;
            int posDangerCount = 0;
            List<int> dodgeableSpells = new List<int>();                      

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (GetSpellCollisionTimeToPos(spell, pos, speed, delay, myHero.ServerPosition.To2D()))
                {
                    posDangerLevel = Math.Max(posDangerLevel, GetSpellDangerLevel(spell));
                    posDangerCount += GetSpellDangerLevel(spell);
                }
                else
                {
                    dodgeableSpells.Add(spell.spellID);
                }
            }

            return Tuple.Create(posDangerLevel, posDangerCount, dodgeableSpells);
        }

        public static bool GetSpellCollisionTimeToPos(Spell spell, Vector2 pos, float speed, float delay, Vector2 heroPos)
        {
            var walkDir = (pos - heroPos).Normalized();
            var zVector = new Vector2(0, 0);


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
                                

                //Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);

                if (heroPos.Distance(spellPos) <= GetSpellRadius(spell) + 10 + myHero.BoundingRadius)
                {
                    //Game.PrintChat("already hit");
                    //return true;
                }

                float extraCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir * speed, spell.direction * spell.info.projectileSpeed, myHero.BoundingRadius, GetSpellRadius(spell) + 5, out isCollision);
                if (isCollision && extraCollisionTime > 0)
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
                var tHeroPos = heroPos + walkDir * speed * (spellHitTime / 1000); //Hero predicted pos

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
                        var collisionTime = MathUtils.GetCollisionTime(heroPoint, spell.endPos, dir * myHero.MoveSpeed, new Vector2(0, 0), 1, GetSpellRadius(spell) + myHero.BoundingRadius, out isCollision);
                        if (collisionTime > 0)
                        {
                            if (collisionTime >= heroPoint.Distance(movePos) / myHero.MoveSpeed) //collision occurs when hero is moving to the destination
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
