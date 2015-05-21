using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class EvadeHelper
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

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
            public float closestDistance = float.MaxValue;
            public float intersectionTime = float.MaxValue;
            public bool rejectPosition = false;
            public float posDistToChamps = float.MaxValue;
            public bool hasComfortZone = true;
            public Obj_AI_Base target = null;

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
                this.timestamp = Evade.GetTickCount();
            }

            public PositionInfo(
                Vector2 position,
                bool isDangerousPos,
                float distanceToMouse)
            {
                this.position = position;
                this.isDangerousPos = isDangerousPos;
                this.distanceToMouse = distanceToMouse;
                this.timestamp = Evade.GetTickCount();
            }
        }

        public static PositionInfo InitPositionInfo(Vector2 pos, float extraDelayBuffer, float extraEvadeDistance, Vector2 lastMovePos, Spell lowestEvadeTimeSpell) //clean this shit up
        {
            var extraDist = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraCPADistance").GetValue<Slider>().Value;

            var posInfo = CanHeroWalkToPos(pos, myHero.MoveSpeed, extraDelayBuffer + Game.Ping, extraDist);
            //posInfo.isDangerousPos = CheckDangerousPos(pos, 6);
            posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;// ? 1 : 0;            
            posInfo.closestDistance = posInfo.distanceToMouse; //GetMovementBlockPositionValue(pos, lastMovePos);
            posInfo.intersectionTime = GetIntersectDistance(lowestEvadeTimeSpell, myHero.ServerPosition.To2D(), pos);
            posInfo.distanceToMouse = pos.Distance(lastMovePos);
            posInfo.posDistToChamps = GetDistanceToChampions(pos);

            if (Evade.menu.SubMenu("MiscSettings").SubMenu("FastEvade").Item("RejectMinDistance").GetValue<Slider>().Value > 0
            && Evade.menu.SubMenu("MiscSettings").SubMenu("FastEvade").Item("RejectMinDistance").GetValue<Slider>().Value >
                posInfo.closestDistance) //reject closestdistance
            {
                posInfo.rejectPosition = true;
            }

            if (Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("MinComfortZone").GetValue<Slider>().Value >
                posInfo.posDistToChamps)
            {
                posInfo.hasComfortZone = false;
            }

            return posInfo;
        }

        public static bool PlayerInSkillShot(Spell spell)
        {
            return InSkillShot(spell, myHero.ServerPosition.To2D(), myHero.BoundingRadius);
        }

        public static bool InSkillShot(Spell spell, Vector2 position, float radius, bool predictCollision = true)
        {
            if (spell.info.spellType == SpellType.Line)
            {
                Vector2 spellPos = spell.GetCurrentSpellPosition(); //leave little space at back of skillshot
                Vector2 spellEndPos = predictCollision ? spell.GetSpellEndPosition() : spell.endPos;

                if (spell.info.projectileSpeed == float.MaxValue
                    && Evade.GetTickCount() - spell.startTime > spell.info.spellDelay)
                {
                    return false;
                }

                var projection = position.ProjectOn(spellPos, spellEndPos);

                /*if (projection.SegmentPoint.Distance(spellEndPos) < 100) //Check Skillshot endpoints
                {
                    //unfinished
                }*/

                return projection.SegmentPoint.Distance(position) <= spell.GetSpellRadius() + radius;
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                return position.Distance(spell.endPos) <= spell.GetSpellRadius() + radius;
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

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;

            if (Evade.menu.SubMenu("MiscSettings").Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 150;
                posRadius = 25;
            }

            List<PositionInfo> posTable = new List<PositionInfo>();

            List<Vector2> fastestPositions = GetFastestPositions();

            Spell lowestEvadeTimeSpell;
            var lowestEvadeTime = GetLowestEvadeTime(out lowestEvadeTimeSpell);

            foreach (var pos in fastestPositions) //add the fastest positions into list of candidates
            {
                posTable.Add(InitPositionInfo(pos, extraDelayBuffer, extraEvadeDistance, lastMovePos, lowestEvadeTimeSpell));
            }

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


                    posTable.Add(InitPositionInfo(pos, extraDelayBuffer, extraEvadeDistance, lastMovePos, lowestEvadeTimeSpell));


                    if (pos.IsWall())
                    {
                        //Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float)25, Color.White, 3);
                    }
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

            bool fastEvadeMode = false;

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;

            if (Evade.menu.Item("CalculateWindupDelay").GetValue<bool>())
            {
                var extraWindupDelay = Evade.lastWindupTime - Evade.GetTickCount();
                if (extraWindupDelay > 0)
                {
                    extraDelayBuffer += (int)extraWindupDelay;
                }
            }

            if (Evade.menu.SubMenu("MiscSettings").Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 150;
                posRadius = 25;
            }

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();

            CalculateEvadeTime();

            Spell lowestEvadeTimeSpell;
            var lowestEvadeTime = GetLowestEvadeTime(out lowestEvadeTimeSpell);

            List<Vector2> fastestPositions = GetFastestPositions();

            foreach (var pos in fastestPositions) //add the fastest positions into list of candidates
            {
                posTable.Add(InitPositionInfo(pos, extraDelayBuffer, extraEvadeDistance, lastMovePos, lowestEvadeTimeSpell));
            }

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


                    posTable.Add(InitPositionInfo(pos, extraDelayBuffer, extraEvadeDistance, lastMovePos, lowestEvadeTimeSpell));
                }
            }

            IOrderedEnumerable<PositionInfo> sortedPosTable;

            if (SpellDetector.spells.Count() == 1
                && Evade.menu.SubMenu("MiscSettings").SubMenu("FastEvade").Item("FastEvadeActivationTime").GetValue<Slider>().Value > 0
                && Evade.menu.SubMenu("MiscSettings").SubMenu("FastEvade").Item("FastEvadeActivationTime").GetValue<Slider>().Value + Game.Ping + extraDelayBuffer >
                lowestEvadeTime)
            {
                sortedPosTable =
                posTable.OrderBy(p => p.intersectionTime)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount);

                fastEvadeMode = true;
                //Game.PrintChat("fast evade: " + lowestEvadeTime);
            }
            else
            {
                sortedPosTable =
                posTable.OrderBy(p => p.rejectPosition)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
                        .ThenByDescending(p => p.hasComfortZone)
                    //.ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);
            }


            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPathCollision(myHero, posInfo.position) == false)
                {
                    if (fastEvadeMode)
                    {
                        posInfo.position = GetExtendedSafePosition(myHero.ServerPosition.To2D(), posInfo.position, extraEvadeDistance);
                        return CanHeroWalkToPos(posInfo.position, myHero.MoveSpeed, Game.Ping, 10);
                    }

                    if (PositionInfoStillValid(posInfo))
                    {

                        if (CheckDangerousPos(posInfo.position, extraEvadeDistance))
                        {
                            posInfo.position = GetExtendedSafePosition(myHero.ServerPosition.To2D(), posInfo.position, extraEvadeDistance);
                        }

                        //posInfo.position = GetExtendedSafePosition(myHero.ServerPosition.To2D(), posInfo.position, extraEvadeDistance);
                        return posInfo;
                    }
                }
            }

            return SetAllUndodgeable();
        }

        public static PositionInfo GetBestPositionMovementBlock(Vector2 movePos)
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraEvadeDistance = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraAvoidDistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = movePos;//Game.CursorPos.To2D(); //movePos

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

                    bool isDangerousPos = CheckDangerousPos(pos, 6) || CheckMovePath(pos);
                    var dist = pos.Distance(lastMovePos);

                    //if (pos.Distance(myHero.Position.To2D()) < 100)
                    //    dist = 0;

                    var posInfo = new PositionInfo(pos, isDangerousPos, dist);
                    //posInfo.intersectionTime = GetMovementBlockPositionValue(pos, movePos);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? HasExtraAvoidDistance(pos, extraEvadeDistance) : false;
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);
            //.ThenBy(p => p.intersectionTime);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPathCollision(myHero, posInfo.position) == false)
                    return posInfo;
            }
            return null;
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

                    bool isDangerousPos = CheckDangerousPos(pos, 6);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, isDangerousPos, dist);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;

                    posInfo.posDistToChamps = GetDistanceToChampions(pos);

                    if (Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("MinComfortZone").GetValue<Slider>().Value >
                        posInfo.posDistToChamps)
                    {
                        posInfo.hasComfortZone = false;
                    }

                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenByDescending(p => p.hasComfortZone)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPointCollision(myHero, posInfo.position) == false)
                    return posInfo;
            }

            return null;
        }

        public static PositionInfo GetBestPositionDash(EvadeSpellData spell)
        {
            int posChecked = 0;
            int maxPosToCheck = 100;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = 100;// Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;
            var extraDist = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraCPADistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();
            List<int> spellList = GetSpellList();

            int minDistance = 50; //Math.Min(spell.range, minDistance)
            int maxDistance = int.MaxValue;

            if (spell.fixedRange)
            {
                minDistance = maxDistance = (int)spell.range;
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

                    var posInfo = CanHeroWalkToPos(pos, spell.speed, extraDelayBuffer + Game.Ping, extraDist);
                    posInfo.isDangerousPos = CheckDangerousPos(pos, 6);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? CheckDangerousPos(pos, extraEvadeDistance) : false;// ? 1 : 0;                    
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posInfo.spellList = spellList;

                    posInfo.posDistToChamps = GetDistanceToChampions(pos);

                    if (Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("MinComfortZone").GetValue<Slider>().Value >
                        posInfo.posDistToChamps)
                    {
                        posInfo.hasComfortZone = false;
                    }

                    posTable.Add(posInfo);
                }

                if (curRadius >= maxDistance)
                    break;
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
                        .ThenByDescending(p => p.hasComfortZone)
                        .ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

            foreach (var posInfo in sortedPosTable)
            {
                if (CheckPathCollision(myHero, posInfo.position) == false)
                {
                    if (PositionInfoStillValid(posInfo, spell.speed))
                    {
                        if (posInfo != null && Evade.lastPosInfo != null
                            && Evade.lastPosInfo.posDangerLevel > posInfo.posDangerLevel)
                        {
                            return posInfo;
                        }
                    }
                }
            }

            return null;
        }

        public static PositionInfo GetBestPositionTargetedDash(EvadeSpellData spell)
        {
            var extraDelayBuffer = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = 100;// Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;
            var extraDist = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraCPADistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();
            List<int> spellList = GetSpellList();

            int minDistance = 50; //Math.Min(spell.range, minDistance)
            int maxDistance = int.MaxValue;

            if (spell.fixedRange)
            {
                minDistance = maxDistance = (int)spell.range;
            }

            List<Obj_AI_Base> collisionCandidates = new List<Obj_AI_Base>();

            if (spell.spellTargets.Contains(SpellTargets.Targetables))
            {
                foreach (var obj in ObjectManager.Get<Obj_AI_Base>()
                    .Where(h => !h.IsMe && h.IsValidTarget(spell.range, false)))
                {
                    collisionCandidates.Add(obj);
                }
            }
            else
            {
                List<Obj_AI_Hero> heroList = new List<Obj_AI_Hero>();

                if (spell.spellTargets.Contains(SpellTargets.EnemyChampions)
                    && spell.spellTargets.Contains(SpellTargets.AllyChampions))
                {
                    heroList = HeroManager.AllHeroes;
                }
                else if (spell.spellTargets.Contains(SpellTargets.EnemyChampions))
                {
                    heroList = HeroManager.Enemies;
                }
                else if (spell.spellTargets.Contains(SpellTargets.AllyChampions))
                {
                    heroList = HeroManager.Allies;
                }


                foreach (var hero in heroList.Where(h => !h.IsMe && h.IsValidTarget(spell.range)))
                {
                    collisionCandidates.Add(hero);
                }

                List<Obj_AI_Base> minionList = new List<Obj_AI_Base>();

                if (spell.spellTargets.Contains(SpellTargets.EnemyMinions)
                    && spell.spellTargets.Contains(SpellTargets.AllyMinions))
                {
                    minionList = MinionManager.GetMinions(spell.range, MinionTypes.All, MinionTeam.All);
                }
                else if (spell.spellTargets.Contains(SpellTargets.EnemyMinions))
                {
                    minionList = MinionManager.GetMinions(spell.range, MinionTypes.All, MinionTeam.Enemy);
                }
                else if (spell.spellTargets.Contains(SpellTargets.AllyMinions))
                {
                    minionList = MinionManager.GetMinions(spell.range, MinionTypes.All, MinionTeam.Ally);
                }

                foreach (var minion in minionList.Where(h => h.IsValidTarget(spell.range)))
                {
                    collisionCandidates.Add(minion);
                }
            }

            foreach (var candidate in collisionCandidates)
            {
                var pos = candidate.ServerPosition.To2D();

                PositionInfo posInfo;

                if (spell.spellName == "YasuoDashWrapper")
                {
                    bool hasDashBuff = false;

                    foreach (var buff in candidate.Buffs)
                    {
                        if (buff.Name == "YasuoDashWrapper")
                        {
                            hasDashBuff = true;
                            break;
                        }
                    }

                    if (hasDashBuff)
                        continue;
                }

                if (spell.fixedRange)
                {
                    var dir = (pos - heroPoint).Normalized();
                    pos = heroPoint + dir * spell.range;
                }

                if (spell.evadeType == EvadeType.Dash)
                {
                    posInfo = CanHeroWalkToPos(pos, spell.speed, extraDelayBuffer + Game.Ping, extraDist);
                    posInfo.isDangerousPos = CheckDangerousPos(pos, 6);
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posInfo.spellList = spellList;
                }
                else
                {
                    bool isDangerousPos = CheckDangerousPos(pos, 6);
                    var dist = pos.Distance(lastMovePos);

                    posInfo = new PositionInfo(pos, isDangerousPos, dist);
                }




                /*
                posInfo.posDistToChamps = GetDistanceToChampions(pos);

                if (Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("MinComfortZone").GetValue<Slider>().Value >
                    posInfo.posDistToChamps)
                {
                    posInfo.hasComfortZone = false;
                }*/
                posInfo.target = candidate;
                posTable.Add(posInfo);
            }

            if (spell.evadeType == EvadeType.Dash)
            {
                var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
                    //.ThenByDescending(p => p.hasComfortZone)
                    //.ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

                var first = sortedPosTable.FirstOrDefault();
                if (first != null && Evade.lastPosInfo != null && first.isDangerousPos == false
                    && Evade.lastPosInfo.posDangerLevel > first.posDangerLevel)
                {
                    return first;
                }
            }
            else
            {
                var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                    //.ThenByDescending(p => p.hasComfortZone)
                    //.ThenBy(p => p.hasExtraDistance)
                        .ThenBy(p => p.distanceToMouse);

                var first = sortedPosTable.FirstOrDefault();
                if (first != null && Evade.lastPosInfo != null && first.isDangerousPos == false
                    && Evade.lastPosInfo.posDangerLevel > first.posDangerLevel)
                {
                    return first;
                }
            }

            return null;

        }

        public static List<Vector2> GetSurroundingPositions(int maxPosToCheck = 150, int posRadius = 25)
        {
            List<Vector2> positions = new List<Vector2>();

            int posChecked = 0;
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
                    var pos = new Vector2((float)Math.Floor(heroPoint.X + curRadius * Math.Cos(cRadians)),
                                          (float)Math.Floor(heroPoint.Y + curRadius * Math.Sin(cRadians)));

                    positions.Add(pos);
                }
            }

            return positions;
        }

        public static float GetMovementBlockPositionValue(Vector2 pos, Vector2 movePos)
        {
            float value = 0;// pos.Distance(movePos);

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                var spellPos = spell.GetCurrentSpellPosition(true, Game.Ping);
                var extraDist = 100 + spell.GetSpellRadius();

                value -= Math.Max(0, -(10 * ((float)0.8 * extraDist) / pos.Distance(spell.GetSpellProjection(pos))) + extraDist);
            }

            return value;
        }

        public static bool PositionInfoStillValid(PositionInfo posInfo, float moveSpeed = 0)
        {
            return true; //too buggy

            if (moveSpeed == 0)
                moveSpeed = myHero.MoveSpeed;

            var canDodge = true;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells) //final check
            {
                Spell spell = entry.Value;

                if (posInfo.undodgeableSpells.Contains(entry.Key))
                {
                    continue;
                }

                float timeElapsed = Evade.GetTickCount() - posInfo.timestamp;
                if (spell.info.spellType == SpellType.Line)
                {
                    if (spell.info.projectileSpeed != float.MaxValue && posInfo.closestDistance < spell.info.projectileSpeed * timeElapsed / 1000)
                    {
                        canDodge = false;
                        break;
                    }
                }
                else if (spell.info.spellType == SpellType.Circular)
                {
                    if (posInfo.closestDistance < moveSpeed * timeElapsed / 1000)
                    {
                        canDodge = false;
                        break;
                    }
                }
            }

            return canDodge;
        }

        public static List<Vector2> GetExtendedPositions(Vector2 from, Vector2 to, float extendDistance)
        {
            Vector2 direction = (to - from).Normalized();
            List<Vector2> positions = new List<Vector2>();
            float sectorDistance = 50;

            for (float i = sectorDistance; i < extendDistance; i += sectorDistance)
            {
                Vector2 pos = to + direction * i;

                positions.Add(pos);
            }

            return positions;
        }

        public static Vector2 GetExtendedSafePosition(Vector2 from, Vector2 to, float extendDistance)
        {
            Vector2 direction = (to - from).Normalized();
            Vector2 lastPosition = to;
            float sectorDistance = 50;

            for (float i = sectorDistance; i < extendDistance; i += sectorDistance)
            {
                Vector2 pos = to + direction * i;

                if (CheckDangerousPos(pos, 6) || CheckPathCollision(myHero, pos))
                {
                    return lastPosition;
                }

                lastPosition = pos;
            }

            return lastPosition;
        }

        public static void CalculateEvadeTime()
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                float evadeTime, spellHitTime;
                spell.CanHeroEvade(myHero, out evadeTime, out spellHitTime);

                spell.spellHitTime = spellHitTime;
                spell.evadeTime = evadeTime;
            }
        }

        public static float GetLowestEvadeTime(out Spell lowestSpell)
        {
            float lowest = float.MaxValue;
            lowestSpell = null;

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (spell.spellHitTime != float.MinValue)
                {
                    //Game.PrintChat("spellhittime: " + spell.spellHitTime);
                    lowest = Math.Min(lowest, (spell.spellHitTime - spell.evadeTime));
                    lowestSpell = spell;
                }
            }

            return lowest;
        }

        public static Vector2 GetFastestPosition(Spell spell)
        {
            var heroPos = myHero.ServerPosition.To2D();

            if (spell.info.spellType == SpellType.Line)
            {
                var projection = heroPos.ProjectOn(spell.startPos, spell.endPos).SegmentPoint;
                return projection.Extend(heroPos, spell.GetSpellRadius() + myHero.BoundingRadius + 10);
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                return spell.endPos.Extend(heroPos, spell.GetSpellRadius() + myHero.BoundingRadius + 10);
            }

            return Vector2.Zero;
        }

        public static List<Vector2> GetFastestPositions()
        {
            List<Vector2> positions = new List<Vector2>();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                var pos = GetFastestPosition(spell);


                if (pos != Vector2.Zero)
                {
                    positions.Add(pos);
                }

            }

            return positions;
        }

        public static float CompareFastestPosition(Spell spell, Vector2 start, Vector2 movePos)
        {
            var fastestPos = GetFastestPosition(spell);
            var moveDir = (movePos - start).Normalized();
            var fastestDir = (GetFastestPosition(spell) - start).Normalized();

            return moveDir.AngleBetween(fastestDir); // * (180 / ((float)Math.PI));
        }

        public static float GetIntersectDistance(Spell spell, Vector2 start, Vector2 end)
        {
            if (spell == null)
                return float.MaxValue;

            Vector3 start3D = new Vector3(start.X, start.Y, 0);
            Vector2 walkDir = (end - start);
            Vector3 walkDir3D = new Vector3(walkDir.X, walkDir.Y, 0);

            Ray heroPath = new Ray(start3D, walkDir3D);

            if (spell.info.spellType == SpellType.Line)
            {
                Vector2 intersection;
                bool hasIntersection = LineIntersectLinearSpellEx(start, end, spell, out intersection);
                if (hasIntersection)
                {
                    return start.Distance(intersection);
                }
            }
            else if (spell.info.spellType == SpellType.Circular)
            {

            }

            return float.MaxValue;
        }

        public static bool CheckDangerousPos(Vector2 pos, float extraBuffer) //, List<int> dodgeTable)
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (pos.IsUnderTurret())
                {
                    return true;
                }

                if (InSkillShot(spell, pos, myHero.BoundingRadius + extraBuffer))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasExtraAvoidDistance(Vector2 pos, float extraBuffer)
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (spell.info.spellType == SpellType.Line)
                {
                    if (InSkillShot(spell, pos, myHero.BoundingRadius + extraBuffer))
                    {
                        return true;
                    }
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

                if (InSkillShot(spell, pos, myHero.BoundingRadius + extraBuffer))
                {
                    dangerlevel += spell.GetSpellDangerLevel();
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

        public static PositionInfo CanHeroWalkToPos(Vector2 pos, float speed, float delay, float extraDist, bool useServerPosition = true)
        {
            int posDangerLevel = 0;
            int posDangerCount = 0;
            float closestDistance = float.MaxValue;
            List<int> dodgeableSpells = new List<int>();
            List<int> undodgeableSpells = new List<int>();

            Vector2 heroPos = myHero.ServerPosition.To2D();

            if (useServerPosition == false)
            {
                heroPos = myHero.Position.To2D();
            }

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                closestDistance = Math.Min(closestDistance, GetClosestDistanceApproach(spell, pos, speed, delay, heroPos, extraDist));
                //GetIntersectTime(spell, myHero.ServerPosition.To2D(), pos);
                //Math.Min(closestDistance, GetClosestDistanceApproach(spell, pos, myHero.MoveSpeed, delay, myHero.ServerPosition.To2D()));

                if (InSkillShot(spell, pos, myHero.BoundingRadius + 6)
                    || PredictSpellCollision(spell, pos, speed, delay, heroPos, extraDist)
                    || pos.IsUnderTurret())
                {
                    posDangerLevel = Math.Max(posDangerLevel, spell.GetSpellDangerLevel());
                    posDangerCount += spell.GetSpellDangerLevel();
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
                closestDistance,
                dodgeableSpells,
                undodgeableSpells);
        }

        public static float GetClosestDistanceApproach(Spell spell, Vector2 pos, float speed, float delay, Vector2 heroPos, float extraDist)
        {
            var walkDir = (pos - heroPos).Normalized();
            var zVector = new Vector2(0, 0);

            if (spell.info.spellType == SpellType.Line)
            {

                var spellPos = spell.GetCurrentSpellPosition(true, delay);
                var spellEndPos = spell.GetSpellEndPosition();

                Vector2 cSpellPos;
                Vector2 cHeroPos;
                var cpa = MathUtilsCPA.CPAPointsEx(heroPos, walkDir * speed, spellPos, spell.direction * spell.info.projectileSpeed, pos, spellEndPos, out cSpellPos, out cHeroPos);

                var spellPos2 = spell.GetCurrentSpellPosition(true, 0);

                Vector2 cSpellPos2;
                Vector2 cHeroPos2;
                var cpa2 = MathUtilsCPA.CPAPointsEx(heroPos, walkDir * speed, spellPos2, spell.direction * spell.info.projectileSpeed, pos, spellEndPos, out cSpellPos2, out cHeroPos2);

                var cHeroPosProjection = cHeroPos.ProjectOn(cSpellPos2, cSpellPos); //from predicted 

                var checkDist = myHero.BoundingRadius + spell.GetSpellRadius() + extraDist;

                if (cHeroPosProjection.IsOnSegment)
                {
                    if (cHeroPosProjection.SegmentPoint.Distance(cHeroPos) <= checkDist)
                    {
                        return 0;
                    }
                }

                if (cpa <= checkDist
                    || cpa2 <= checkDist)
                {
                    return 0;
                }

                return cpa - (myHero.BoundingRadius + spell.GetSpellRadius() + extraDist);
                //return MathUtils.ClosestTimeOfApproach(heroPos, walkDir * speed, spellPos, spell.direction * spell.info.projectileSpeed);
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                var spellHitTime = Math.Max(0, spell.endTime - Evade.GetTickCount());  //extraDelay
                var walkRange = heroPos.Distance(pos);
                var predictedRange = speed * (spellHitTime / 1000);
                var tHeroPos = heroPos + walkDir * Math.Min(predictedRange, walkRange); //Hero predicted pos

                return Math.Max(0, tHeroPos.Distance(spell.endPos) - (spell.GetSpellRadius() + myHero.BoundingRadius + extraDist)); //+ dodgeBuffer
            }

            return 1;
        }

        public static bool PredictSpellCollision(Spell spell, Vector2 pos, float speed, float delay, Vector2 heroPos, float extraDist)
        {
            var walkDir = (pos - heroPos).Normalized();
            var zVector = new Vector2(0, 0);

            //heroPos = GetRealHeroPos();
            /*
            if (Evade.menu.SubMenu("MiscSettings").Item("CalculateHeroPos").GetValue<bool>())
                heroPos = GetRealHeroPos(); //testing*/

            /*if (!myHero.IsMoving)
                walkDir = zVector;*/

            if (spell.info.spellType == SpellType.Line)
            {
                return GetClosestDistanceApproach(spell, pos, speed, delay, heroPos, extraDist) == 0;
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                var spellHitTime = Math.Max(0, spell.endTime - Evade.GetTickCount());  //extraDelay
                var walkRange = heroPos.Distance(pos);
                var predictedRange = speed * (spellHitTime / 1000);
                var tHeroPos = heroPos + walkDir * Math.Min(predictedRange, walkRange); //Hero predicted pos

                return tHeroPos.Distance(spell.endPos) <= spell.GetSpellRadius() + myHero.BoundingRadius; //+ dodgeBuffer
            }
            else if (spell.info.spellType == SpellType.Cone)
            {
                var spellHitTime = Math.Max(0, spell.endTime - Evade.GetTickCount());  //extraDelay
                var walkRange = heroPos.Distance(pos);
                var predictedRange = speed * (spellHitTime / 1000);
                var tHeroPos = heroPos + walkDir * Math.Min(predictedRange, walkRange); //Hero predicted pos

                return InSkillShot(spell, tHeroPos, myHero.BoundingRadius);
            }

            return false;
        }

        public static Vector2 GetRealHeroPos(float delay = 0)
        {
            var path = myHero.Path;
            if (path.Length < 1)
            {
                return myHero.ServerPosition.To2D();
            }

            //if (!myHero.IsMoving)
            //    return myHero.Position.To2D();

            var serverPos = myHero.ServerPosition.To2D();
            var heroPos = myHero.Position.To2D();

            var walkDir = (path[0].To2D() - heroPos).Normalized();
            var realPos = heroPos + walkDir * myHero.MoveSpeed * (delay / 1000);

            return realPos;
        }

        public static float GetDistanceToChampions(Vector2 pos)
        {
            float minDist = float.MaxValue;

            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (hero.Team != myHero.Team && !hero.IsDead)
                {
                    var heroPos = hero.ServerPosition.To2D();
                    var dist = heroPos.Distance(pos);

                    minDist = Math.Min(minDist, dist);
                }
            }

            return minDist;
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

        public static PositionInfo CompareLastMovePos(PositionInfo newPosInfo)
        {          
            PositionInfo posInfo = null;
            var path = myHero.Path;
            if (path.Length > 0)
            {
                var movePos = path[path.Length - 1].To2D();
                posInfo = EvadeHelper.CanHeroWalkToPos(movePos, myHero.MoveSpeed, 0, 0, false);
            }
            else
            {
                posInfo = EvadeHelper.CanHeroWalkToPos(myHero.ServerPosition.To2D(), myHero.MoveSpeed, 0, 0, false);
            }

            if (posInfo.posDangerCount < newPosInfo.posDangerCount)
            {
                return posInfo;
            }

            return newPosInfo;
        }

        public static int GetHighestDetectedSpellID()
        {
            int highest = 0;

            foreach (var spell in SpellDetector.spells)
            {
                highest = Math.Max(highest, spell.Key);
            }

            return highest;
        }

        public static int GetHighestSpellID(PositionInfo posInfo)
        {
            if (posInfo == null)
                return 0;

            int highest = 0;

            foreach (var spellID in posInfo.undodgeableSpells)
            {
                highest = Math.Max(highest, spellID);
            }

            foreach (var spellID in posInfo.dodgeableSpells)
            {
                highest = Math.Max(highest, spellID);
            }

            return highest;
        }

        public static PositionInfo SetAllDodgeable()
        {
            List<int> dodgeableSpells = new List<int>();
            List<int> undodgeableSpells = new List<int>();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                dodgeableSpells.Add(entry.Key);
            }

            return new PositionInfo(
                myHero.Position.To2D(),
                0,
                0,
                true,
                0,
                dodgeableSpells,
                undodgeableSpells);
        }

        public static PositionInfo SetAllUndodgeable()
        {
            List<int> dodgeableSpells = new List<int>();
            List<int> undodgeableSpells = new List<int>();

            var posDangerLevel = 0;
            var posDangerCount = 0;

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                undodgeableSpells.Add(entry.Key);

                var spellDangerLevel = spell.GetSpellDangerLevel();

                posDangerLevel = Math.Max(posDangerLevel, spellDangerLevel);
                posDangerCount += spellDangerLevel;
            }

            return new PositionInfo(
                myHero.Position.To2D(),
                posDangerLevel,
                posDangerCount,
                true,
                0,
                dodgeableSpells,
                undodgeableSpells);
        }

        public static bool LineIntersectLinearSpell(Vector2 a, Vector2 b, Spell spell)
        {
            var myBoundingRadius = myHero.BoundingRadius;
            var spellDir = spell.direction;
            var pSpellDir = spell.direction.Perpendicular();
            var spellRadius = spell.GetSpellRadius();
            var spellPos = spell.GetCurrentSpellPosition() - spellDir * myBoundingRadius; //leave some space at back of spell
            var endPos = spell.GetSpellEndPosition() + spellDir * myBoundingRadius; //leave some space at the front of spell

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

        public static bool LineIntersectLinearSpellEx(Vector2 a, Vector2 b, Spell spell, out Vector2 intersection) //edited
        {
            var myBoundingRadius = myHero.BoundingRadius;
            var spellDir = spell.direction;
            var pSpellDir = spell.direction.Perpendicular();
            var spellRadius = spell.GetSpellRadius();
            var spellPos = spell.GetCurrentSpellPosition() - spellDir * myBoundingRadius; //leave some space at back of spell
            var endPos = spell.GetSpellEndPosition() + spellDir * myBoundingRadius; //leave some space at the front of spell

            var startRightPos = spellPos + pSpellDir * (spellRadius + myBoundingRadius);
            var startLeftPos = spellPos - pSpellDir * (spellRadius + myBoundingRadius);
            var endRightPos = endPos + pSpellDir * (spellRadius + myBoundingRadius);
            var endLeftPos = endPos - pSpellDir * (spellRadius + myBoundingRadius);

            List<Geometry.IntersectionResult> intersects = new List<Geometry.IntersectionResult>();
            Vector2 heroPos = myHero.ServerPosition.To2D();

            intersects.Add(a.Intersection(b, startRightPos, startLeftPos));
            intersects.Add(a.Intersection(b, endRightPos, endLeftPos));
            intersects.Add(a.Intersection(b, startRightPos, endRightPos));
            intersects.Add(a.Intersection(b, startLeftPos, endLeftPos));

            var sortedIntersects = intersects.Where(i => i.Intersects).OrderBy(i => i.Point.Distance(heroPos)); //Get first intersection

            if (sortedIntersects.Count() > 0)
            {
                intersection = sortedIntersects.First().Point;
                return true;
            }

            intersection = Vector2.Zero;
            return false;
        }

        public static bool CheckMovePath(Vector2 movePos, float extraDelay = 0)
        {
            /*if (EvadeSpell.lastSpellEvadeCommand.evadeSpellData != null)
            {
                var evadeSpell = EvadeSpell.lastSpellEvadeCommand.evadeSpellData;
                float evadeTime = Game.Ping;

                if (EvadeSpell.lastSpellEvadeCommand.evadeSpellData.evadeType == EvadeType.Dash)
                    evadeTime += evadeSpell.spellDelay + myHero.ServerPosition.To2D().Distance(movePos) / (evadeSpell.speed / 1000);
                else if (EvadeSpell.lastSpellEvadeCommand.evadeSpellData.evadeType == EvadeType.Blink)
                    evadeTime += evadeSpell.spellDelay;

                if (Evade.GetTickCount() - EvadeSpell.lastSpellEvadeCommand.timestamp < evadeTime)
                {

                    Game.PrintChat("in" + CheckMoveToDirection(EvadeSpell.lastSpellEvadeCommand.targetPosition, movePos));
                    return CheckMoveToDirection(EvadeSpell.lastSpellEvadeCommand.targetPosition, movePos);
                }
            }*/


            var path = myHero.GetPath(movePos.To3D());
            Vector2 lastPoint = Vector2.Zero;

            foreach (Vector3 point in path)
            {
                var point2D = point.To2D();
                if (lastPoint != Vector2.Zero && CheckMoveToDirection(lastPoint, point2D, extraDelay))
                {
                    return true;
                }

                lastPoint = point2D;
            }

            return false;
        }

        public static bool CheckMoveToDirection(Vector2 from, Vector2 movePos, float extraDelay = 0)
        {
            var heroPoint = from;
            var dir = (movePos - heroPoint).Normalized();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (!PlayerInSkillShot(spell))
                {
                    Vector2 spellPos = spell.GetCurrentSpellPosition();

                    if (spell.info.spellType == SpellType.Line)
                    {
                        //PredictSpellCollision(spell, movePos, myHero.MoveSpeed, 65, from, 30);
                        /*if (Evade.menu.Item("FasterCrossing").GetValue<bool>())
                        {
                            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
                            if (PredictSpellCollision(spell, movePos, myHero.MoveSpeed, extraDelayBuffer, from, 30))
                                return true;
                        }
                        else
                        {
                            if (LineIntersectLinearSpell(heroPoint, movePos, spell))
                                return true;
                        }*/
                        if (LineIntersectLinearSpell(heroPoint, movePos, spell))
                            return true;

                    }
                    else if (spell.info.spellType == SpellType.Circular)
                    {
                        /*bool isCollision = false;
                        var collisionTime = MathUtils.GetCollisionTime(heroPoint, spell.endPos, dir * myHero.MoveSpeed, new Vector2(0, 0), 1,
                            spell.GetSpellRadius() + myHero.BoundingRadius, out isCollision);
                        if (collisionTime > 0)
                        {
                            if (!(collisionTime >= heroPoint.Distance(movePos) / myHero.MoveSpeed)) //collision occurs when hero is moving to the destination
                            {
                                return true;
                            }
                        }*/

                        var cpa = MathUtilsCPA.CPAPointsEx(heroPoint, dir * myHero.MoveSpeed, spell.endPos, new Vector2(0, 0), movePos, spell.endPos);
                        if (cpa < myHero.BoundingRadius + spell.GetSpellRadius())
                        {
                            return true;
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
