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

        public static bool PlayerInSkillShot(Spell spell)
        {
            return myHero.ServerPosition.To2D().InSkillShot(spell, myHero.BoundingRadius);
        }

        public static PositionInfo InitPositionInfo(Vector2 pos, float extraDelayBuffer, float extraEvadeDistance, Vector2 lastMovePos, Spell lowestEvadeTimeSpell) //clean this shit up
        {
            var extraDist = Evade.menu.Item("ExtraCPADistance").GetValue<Slider>().Value;

            var posInfo = CanHeroWalkToPos(pos, myHero.MoveSpeed, extraDelayBuffer + Game.Ping, extraDist);
            posInfo.isDangerousPos = pos.CheckDangerousPos(6);
            posInfo.hasExtraDistance = extraEvadeDistance > 0 ? pos.CheckDangerousPos(extraEvadeDistance) : false;// ? 1 : 0;            
            posInfo.closestDistance = posInfo.distanceToMouse; //GetMovementBlockPositionValue(pos, lastMovePos);
            posInfo.intersectionTime = GetIntersectDistance(lowestEvadeTimeSpell, myHero.ServerPosition.To2D(), pos);
            posInfo.distanceToMouse = pos.Distance(lastMovePos);
            posInfo.posDistToChamps = pos.GetDistanceToChampions();

            if (Evade.menu.Item("RejectMinDistance").GetValue<Slider>().Value > 0
            && Evade.menu.Item("RejectMinDistance").GetValue<Slider>().Value >
                posInfo.closestDistance) //reject closestdistance
            {
                posInfo.rejectPosition = true;
            }

            if (Evade.menu.Item("MinComfortZone").GetValue<Slider>().Value >
                posInfo.posDistToChamps)
            {
                posInfo.hasComfortZone = false;
            }

            return posInfo;
        }

        public static IOrderedEnumerable<PositionInfo> GetBestPositionTest()
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = Evade.menu.Item("ExtraEvadeDistance").GetValue<Slider>().Value;

            if (Evade.menu.Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 150;
                posRadius = 25;
            }

            List<PositionInfo> posTable = new List<PositionInfo>();

            List<Vector2> fastestPositions = GetFastestPositions();

            Spell lowestEvadeTimeSpell;
            var lowestEvadeTime = SpellDetector.GetLowestEvadeTime(out lowestEvadeTimeSpell);

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

            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = Evade.menu.Item("ExtraEvadeDistance").GetValue<Slider>().Value;

            if (Evade.menu.Item("CalculateWindupDelay").GetValue<bool>())
            {
                var extraWindupDelay = Evade.lastWindupTime - Evade.TickCount;
                if (extraWindupDelay > 0)
                {
                    extraDelayBuffer += (int)extraWindupDelay;
                }
            }

            extraDelayBuffer += (int)(Evade.avgCalculationTime);

            if (Evade.menu.Item("HigherPrecision").GetValue<bool>())
            {
                maxPosToCheck = 150;
                posRadius = 25;
            }

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();

            CalculateEvadeTime();

            Spell lowestEvadeTimeSpell;
            var lowestEvadeTime = SpellDetector.GetLowestEvadeTime(out lowestEvadeTimeSpell);

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

            /*if (Evade.menu.Item("FastestEvadeMode").GetValue<bool>())
            {
                sortedPosTable = posTable.OrderBy(p => p.intersectionTime);
                fastEvadeMode = true;
            }*/
            if (SpellDetector.spells.Count() == 1
                && Evade.menu.Item("FastEvadeActivationTime").GetValue<Slider>().Value > 0
                && Evade.menu.Item("FastEvadeActivationTime").GetValue<Slider>().Value + Game.Ping + extraDelayBuffer >
                lowestEvadeTime)
            {
                sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.intersectionTime)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount);

                fastEvadeMode = true;
                //Console.WriteLine("fast evade: " + lowestEvadeTime);
            }
            else
            {
                sortedPosTable =
                posTable.OrderBy(p => p.rejectPosition)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
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
                        return CanHeroWalkToPos(posInfo.position, myHero.MoveSpeed, Game.Ping, 0);
                    }

                    if (PositionInfoStillValid(posInfo))
                    {

                        if (posInfo.position.CheckDangerousPos(extraEvadeDistance))
                        {
                            posInfo.position = GetExtendedSafePosition(myHero.ServerPosition.To2D(), posInfo.position, extraEvadeDistance);
                        }

                        //posInfo.position = GetExtendedSafePosition(myHero.ServerPosition.To2D(), posInfo.position, extraEvadeDistance);
                        return posInfo;
                    }
                }
            }

            return PositionInfo.SetAllUndodgeable();
        }

        public static PositionInfo GetBestPositionMovementBlock(Vector2 movePos)
        {
            int posChecked = 0;
            int maxPosToCheck = 50;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraEvadeDistance = Evade.menu.Item("ExtraAvoidDistance").GetValue<Slider>().Value;

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

                    bool isDangerousPos = pos.CheckDangerousPos(6) || CheckMovePath(pos);
                    var dist = pos.Distance(lastMovePos);

                    //if (pos.Distance(myHero.Position.To2D()) < 100)
                    //    dist = 0;

                    var posInfo = new PositionInfo(pos, isDangerousPos, dist);
                    //posInfo.intersectionTime = GetMovementBlockPositionValue(pos, movePos);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? pos.HasExtraAvoidDistance(extraEvadeDistance) : false;
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

                    bool isDangerousPos = pos.CheckDangerousPos(6);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, isDangerousPos, dist);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? pos.CheckDangerousPos(extraEvadeDistance) : false;

                    posInfo.posDistToChamps = pos.GetDistanceToChampions();

                    if (Evade.menu.Item("MinComfortZone").GetValue<Slider>().Value < posInfo.posDistToChamps)
                    {
                        posTable.Add(posInfo);
                    }
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

            return null;
        }

        public static PositionInfo GetBestPositionDash(EvadeSpellData spell)
        {
            int posChecked = 0;
            int maxPosToCheck = 100;
            int posRadius = 50;
            int radiusIndex = 0;

            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = 100;// Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;
            var extraDist = Evade.menu.Item("ExtraCPADistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();
            List<int> spellList = SpellDetector.GetSpellList();

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
                    posInfo.isDangerousPos = pos.CheckDangerousPos(6);
                    posInfo.hasExtraDistance = extraEvadeDistance > 0 ? pos.CheckDangerousPos(extraEvadeDistance) : false;// ? 1 : 0;                    
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posInfo.spellList = spellList;

                    posInfo.posDistToChamps = pos.GetDistanceToChampions();

                    posTable.Add(posInfo);
                }

                if (curRadius >= maxDistance)
                    break;
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
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
            /*if (spell.spellDelay > 0)
            {
                if (CheckWindupTime(spell.spellDelay))
                {
                    return null;
                }
            }*/

            var extraDelayBuffer = Evade.menu.Item("ExtraPingBuffer").GetValue<Slider>().Value;
            var extraEvadeDistance = 100;// Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraEvadeDistance").GetValue<Slider>().Value;
            var extraDist = Evade.menu.Item("ExtraCPADistance").GetValue<Slider>().Value;

            Vector2 heroPoint = myHero.ServerPosition.To2D();
            Vector2 lastMovePos = Game.CursorPos.To2D();

            List<PositionInfo> posTable = new List<PositionInfo>();
            List<int> spellList = SpellDetector.GetSpellList();

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
                    if (!obj.IsValid<Obj_AI_Turret>())
                    {
                        collisionCandidates.Add(obj);
                    }
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

                if (spell.behindTarget)
                {
                    var dir = (pos - heroPoint).Normalized();
                    pos = pos + dir * (candidate.BoundingRadius + myHero.BoundingRadius);
                }

                if (spell.infrontTarget)
                {
                    var dir = (pos - heroPoint).Normalized();
                    pos = pos - dir * (candidate.BoundingRadius + myHero.BoundingRadius);
                }

                if (spell.fixedRange)
                {
                    var dir = (pos - heroPoint).Normalized();
                    pos = heroPoint + dir * spell.range;
                }

                if (spell.evadeType == EvadeType.Dash)
                {
                    posInfo = CanHeroWalkToPos(pos, spell.speed, extraDelayBuffer + Game.Ping, extraDist);
                    posInfo.isDangerousPos = pos.CheckDangerousPos(6);
                    posInfo.distanceToMouse = pos.Distance(lastMovePos);
                    posInfo.spellList = spellList;
                }
                else
                {
                    bool isDangerousPos = pos.CheckDangerousPos(6);
                    var dist = pos.Distance(lastMovePos);

                    posInfo = new PositionInfo(pos, isDangerousPos, dist);
                }

                posInfo.target = candidate;
                posTable.Add(posInfo);
            }

            if (spell.evadeType == EvadeType.Dash)
            {
                var sortedPosTable =
                posTable.OrderBy(p => p.isDangerousPos)
                        .ThenBy(p => p.posDangerLevel)
                        .ThenBy(p => p.posDangerCount)
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

        public static bool CheckWindupTime(float windupTime)
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                var hitTime = spell.GetSpellHitTime(myHero.ServerPosition.To2D());
                if (hitTime < windupTime)
                {
                    return true;
                }
            }

            return false;
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

            /*if (moveSpeed == 0)
                moveSpeed = myHero.MoveSpeed;

            var canDodge = true;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells) //final check
            {
                Spell spell = entry.Value;

                if (posInfo.undodgeableSpells.Contains(entry.Key))
                {
                    continue;
                }

                float timeElapsed = Evade.GetTickCount - posInfo.timestamp;
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

            return canDodge;*/
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

                if (pos.CheckDangerousPos(6) || CheckPathCollision(myHero, pos))
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

        public static float GetCombinedIntersectionDistance(Vector2 movePos)
        {
            var heroPoint = myHero.ServerPosition.To2D();
            float sumIntersectDist = 0;

            foreach (Spell spell in SpellDetector.spells.Values)
            {
                var intersectDist = GetIntersectDistance(spell, heroPoint, movePos);
                sumIntersectDist += intersectDist * spell.GetSpellDangerLevel();
            }

            return sumIntersectDist;
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
                bool hasIntersection = spell.LineIntersectLinearSpellEx(start, end, out intersection);
                if (hasIntersection)
                {
                    return start.Distance(intersection);
                }
            }
            else if (spell.info.spellType == SpellType.Circular)
            {
                if (end.InSkillShot(spell, myHero.BoundingRadius) == false)
                {
                    Vector2 intersection1, intersection2;
                    MathUtils.FindLineCircleIntersections(spell.endPos.X, spell.endPos.Y, spell.GetSpellRadius(), start, end, out intersection1, out intersection2);

                    if (intersection1.X != float.NaN && MathUtils.isPointOnLineSegment(intersection1, start, end))
                    {
                        return start.Distance(intersection1);
                    }
                    else if (intersection2.X != float.NaN && MathUtils.isPointOnLineSegment(intersection2, start, end))
                    {
                        return start.Distance(intersection2);
                    }
                }
            }

            return float.MaxValue;
        }

        public static PositionInfo CanHeroWalkToPos(Vector2 pos, float speed, float delay, float extraDist, bool useServerPosition = true)
        {
            int posDangerLevel = 0;
            int posDangerCount = 0;
            float closestDistance = float.MaxValue;
            List<int> dodgeableSpells = new List<int>();
            List<int> undodgeableSpells = new List<int>();

            Vector2 heroPos = myHero.ServerPosition.To2D();

            var minComfortDistance = Evade.menu.Item("MinComfortZone").GetValue<Slider>().Value;

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

                if (pos.InSkillShot(spell, myHero.BoundingRadius + 6)
                    || PredictSpellCollision(spell, pos, speed, delay, heroPos, extraDist)
                    || pos.IsUnderTurret()
                    || (spell.info.spellType != SpellType.Line && pos.isNearEnemy(minComfortDistance)))
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

            heroPos = heroPos - walkDir * speed * ((float)Game.Ping) / 1000;

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
                var spellHitTime = Math.Max(0, spell.endTime - Evade.TickCount);  //extraDelay
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
                var spellHitTime = Math.Max(0, spell.endTime - Evade.TickCount);  //extraDelay
                var walkRange = heroPos.Distance(pos);
                var predictedRange = speed * (spellHitTime / 1000);
                var tHeroPos = heroPos + walkDir * Math.Min(predictedRange, walkRange); //Hero predicted pos

                return tHeroPos.Distance(spell.endPos) <= spell.GetSpellRadius() + myHero.BoundingRadius; //+ dodgeBuffer
            }
            else if (spell.info.spellType == SpellType.Cone)
            {
                var spellHitTime = Math.Max(0, spell.endTime - Evade.TickCount);  //extraDelay
                var walkRange = heroPos.Distance(pos);
                var predictedRange = speed * (spellHitTime / 1000);
                var tHeroPos = heroPos + walkDir * Math.Min(predictedRange, walkRange); //Hero predicted pos

                return tHeroPos.InSkillShot(spell, myHero.BoundingRadius);
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

                if (Evade.GetTickCount - EvadeSpell.lastSpellEvadeCommand.timestamp < evadeTime)
                {

                    Console.WriteLine("in" + CheckMoveToDirection(EvadeSpell.lastSpellEvadeCommand.targetPosition, movePos));
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
            var dir = (movePos - from).Normalized();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (!from.InSkillShot(spell, myHero.BoundingRadius))
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
                        if (spell.LineIntersectLinearSpell(from, movePos))
                            return true;

                    }
                    else if (spell.info.spellType == SpellType.Circular)
                    {
                        var cpa = MathUtilsCPA.CPAPointsEx(from, dir * myHero.MoveSpeed, spell.endPos, new Vector2(0, 0), movePos, spell.endPos);
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
