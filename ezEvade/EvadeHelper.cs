using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace ezEvade
{
    internal class EvadeHelper
    {
        private static Obj_AI_Hero MyHero
        {
            get { return ObjectManager.Player; }
        }

        private static float GameTime
        {
            get { return Game.ClockTime*1000; }
        }

        public static float GetSpellRadius(Spell spell)
        {
            return spell.Info.Radius;
        }

        public static bool PlayerInSkillShot(Spell spell)
        {
            return InSkillShot(spell, MyHero.ServerPosition.To2D(), MyHero.BoundingRadius);
        }

        public static bool InSkillShot(Spell spell, Vector2 position, float radius)
        {
            switch (spell.Info.SpellType)
            {
                case SpellType.Line:
                    var spellPos = SpellDetector.GetCurrentSpellPosition(spell);
                    // Leave little space at back of skillshot

                    if (spell.Info.ProjectileSpeed == float.MaxValue
                        && GameTime - spell.StartTime > spell.Info.SpellDelay)
                    {
                        return false;
                    }

                    var projection = position.ProjectOn(spellPos, spell.EndPos);

                    if (projection.SegmentPoint.Distance(spell.EndPos) < 100) // Check Skillshot endpoints
                    {
                    }

                    return projection.SegmentPoint.Distance(position) <= GetSpellRadius(spell) + radius;
                case SpellType.Circular:
                    return position.Distance(spell.EndPos) <= GetSpellRadius(spell) + radius;
                case SpellType.Cone:
                    break;
            }
            return false;
        }

        public static PositionInfo GetBestPositionTest()
        {
            var posChecked = 0;
            const int maxPosToCheck = 100;
            const int posRadius = 25;
            var radiusIndex = 0;

            var heroPoint = MyHero.ServerPosition.To2D();
            //var lastMovePos = Game.CursorPos.To2D();

            var posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                var curRadius = radiusIndex*(2*posRadius);
                var curCircleChecks = (int) Math.Ceiling((2*Math.PI*curRadius)/(2*(double) posRadius));

                for (var i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2*Math.PI/(curCircleChecks - 1))*i; // Check decimals
                    var pos = new Vector2((float) Math.Floor(heroPoint.X + curRadius*Math.Cos(cRadians)),
                        (float) Math.Floor(heroPoint.Y + curRadius*Math.Sin(cRadians)));

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

                    var path = MyHero.GetPath(pos.To3D());

                    //Render.Circle.DrawCircle(path[path.Length - 1], (float)posRadius, Color.White, 3);

                    var posOnScreen = Drawing.WorldToScreen(path[path.Length - 1]);
                    Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, string.Empty + path.Length);
                }
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.IsDangerousPos)
                    .ThenBy(p => p.PosDangerLevel)
                    .ThenBy(p => p.PosDangerCount)
                    .ThenBy(p => p.Dist);

            return sortedPosTable.First();
        }

        public static PositionInfo GetBestPosition()
        {
            var posChecked = 0;
            const int maxPosToCheck = 50;
            const int posRadius = 50;
            var radiusIndex = 0;

            var heroPoint = MyHero.ServerPosition.To2D();
            var lastMovePos = Game.CursorPos.To2D();

            var posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                var curRadius = radiusIndex*(2*posRadius);
                var curCircleChecks = (int) Math.Ceiling((2*Math.PI*curRadius)/(2*(double) posRadius));

                for (var i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2*Math.PI/(curCircleChecks - 1))*i; // Check decimals
                    var pos = new Vector2((float) Math.Floor(heroPoint.X + curRadius*Math.Cos(cRadians)),
                        (float) Math.Floor(heroPoint.Y + curRadius*Math.Sin(cRadians)));

                    var ret = CanHeroWalkToPos(pos, MyHero.MoveSpeed, 60 + Game.Ping);
                    var posDangerLevel = ret.Item1;
                    var posDangerCount = ret.Item2;
                    var dodgeableSpells = ret.Item3;

                    var isDangerousPos = CheckDangerousPos(pos, 6);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, posDangerLevel, posDangerCount, isDangerousPos, dist,
                        dodgeableSpells);
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable =
                posTable.OrderBy(p => p.IsDangerousPos)
                    .ThenBy(p => p.PosDangerLevel)
                    .ThenBy(p => p.PosDangerCount)
                    .ThenBy(p => p.Dist);

            foreach (
                var posInfo in sortedPosTable.Where(posInfo => CheckPathCollision(MyHero, posInfo.Position) == false))
            {
                return posInfo;
            }

            return sortedPosTable.First();
        }

        public static PositionInfo GetBestPositionMovementBlock(Vector2 movePos)
        {
            var posChecked = 0;
            const int maxPosToCheck = 50;
            const int posRadius = 50;
            var radiusIndex = 0;

            var heroPoint = MyHero.ServerPosition.To2D();
            var lastMovePos = movePos; // Game.CursorPos.To2D();

            var posTable = new List<PositionInfo>();

            while (posChecked < maxPosToCheck)
            {
                radiusIndex++;

                var curRadius = radiusIndex*(2*posRadius);
                var curCircleChecks = (int) Math.Ceiling((2*Math.PI*curRadius)/(2*(double) posRadius));

                for (var i = 1; i < curCircleChecks; i++)
                {
                    posChecked++;
                    var cRadians = (2*Math.PI/(curCircleChecks - 1))*i; // Check decimals
                    var pos = new Vector2((float) Math.Floor(heroPoint.X + curRadius*Math.Cos(cRadians)),
                        (float) Math.Floor(heroPoint.Y + curRadius*Math.Sin(cRadians)));

                    var isDangerousPos = CheckDangerousPos(pos, 6) || CheckMoveToDirection(pos);
                    var dist = pos.Distance(lastMovePos);

                    var posInfo = new PositionInfo(pos, 0, 0, isDangerousPos, dist, null);
                    posTable.Add(posInfo);
                }
            }

            var sortedPosTable = posTable.OrderBy(p => p.IsDangerousPos).ThenBy(p => p.Dist);

            foreach (
                var posInfo in sortedPosTable.Where(posInfo => CheckPathCollision(MyHero, posInfo.Position) == false))
            {
                return posInfo;
            }

            return sortedPosTable.First();
        }

        public static bool CheckDangerousPos(Vector2 pos, float extraBuffer) //, List<int> dodgeTable)
        {
            return
                SpellDetector.Spells.Select(entry => entry.Value)
                    .Any(spell => InSkillShot(spell, pos, MyHero.BoundingRadius + extraBuffer));
        }

        public static Tuple<int, int, List<int>> CanHeroWalkToPos(Vector2 pos, float speed, float delay)
        {
            var posDangerLevel = 0;
            var posDangerCount = 0;
            var dodgeableSpells = new List<int>();

            foreach (var spell in SpellDetector.Spells.Select(entry => entry.Value))
            {
                if (GetSpellCollisionTimeToPos(spell, pos, speed, delay, MyHero.ServerPosition.To2D()))
                {
                    posDangerLevel = Math.Max(posDangerLevel, spell.Info.Dangerlevel);
                    posDangerCount += spell.Info.Dangerlevel;
                }
                else
                {
                    dodgeableSpells.Add(spell.SpellId);
                }
            }

            return Tuple.Create(posDangerLevel, posDangerCount, dodgeableSpells);
        }

        public static bool GetSpellCollisionTimeToPos(Spell spell, Vector2 pos, float speed, float delay,
            Vector2 heroPos)
        {
            var walkDir = (pos - heroPos).Normalized();
            var zVector = new Vector2(0, 0);


            /*if (!myHero.IsMoving)
                walkDir = zVector;*/

            switch (spell.Info.SpellType)
            {
                case SpellType.Line:
                    //zVector

                    if (spell.Info.ProjectileSpeed == float.MaxValue)
                    {
                    }

                    var spellPos = SpellDetector.GetCurrentSpellPosition(spell);

                    if (spell.SpellObject == null) // Rewrite this
                    {
                        var spellTime = (GameTime - spell.StartTime) - spell.Info.SpellDelay;
                        spellPos = spell.StartPos + spell.Direction*spell.Info.ProjectileSpeed*(spellTime/1000);
                    }

                    // Using triple checks
                    // Check if skillshot will hit pos if hero is standing still
                    bool isCollision;

                    var standingCollisionTime = MathUtils.GetCollisionTime(pos, spellPos, zVector,
                        spell.Direction*spell.Info.ProjectileSpeed, MyHero.BoundingRadius, GetSpellRadius(spell),
                        out isCollision);
                    if (isCollision && standingCollisionTime > 0)
                    {
                        if (spellPos.Distance(spell.EndPos)/spell.Info.ProjectileSpeed > standingCollisionTime)
                        {
                            return true; // If collision happens when the skillshot is in flight
                        }
                    }


                    // Check if skillshot will hit hero if hero is moving
                    var movingCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir*speed,
                        spell.Direction*spell.Info.ProjectileSpeed, MyHero.BoundingRadius, GetSpellRadius(spell),
                        out isCollision);
                    if (isCollision && movingCollisionTime > 0)
                    {
                        if (spellPos.Distance(spell.EndPos)/spell.Info.ProjectileSpeed > movingCollisionTime)
                        {
                            return true; // If collision happens when the skillshot is in flight
                        }
                    }


                    // Check if skillshot will hit hero if hero is moving and the skillshot is moved forwards by 50 units                

                    spellPos = spellPos + spell.Direction*spell.Info.ProjectileSpeed*(delay/1000);
                    // Move the spellPos by 50 miliseconds forwards
                    spellPos = spellPos + spell.Direction*50; //move the spellPos by 50 units forwards                

                    //Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);

                    if (heroPos.Distance(spellPos) <= GetSpellRadius(spell) + 10 + MyHero.BoundingRadius)
                    {
                        //Game.PrintChat("already hit");
                        //return true;
                    }

                    var extraCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir*speed,
                        spell.Direction*spell.Info.ProjectileSpeed, MyHero.BoundingRadius, GetSpellRadius(spell),
                        out isCollision);
                    if (isCollision && extraCollisionTime > 0)
                    {
                        return spellPos.Distance(spell.EndPos)/spell.Info.ProjectileSpeed > extraCollisionTime;
                    }
                    break;
                case SpellType.Circular:
                {
                    var spellHitTime = Math.Max(0, spell.EndTime - GameTime); // ExtraDelay
                    var tHeroPos = heroPos + walkDir*speed*(spellHitTime/1000); // Hero predicted pos

                    return tHeroPos.Distance(spell.EndPos) <= GetSpellRadius(spell) + MyHero.BoundingRadius;
                    // + dodgeBuffer
                }
                case SpellType.Cone:
                {
                    var spellHitTime = Math.Max(0, spell.EndTime - GameTime); // ExtraDelay
                    var tHeroPos = heroPos + walkDir*speed*(spellHitTime/1000);

                    return InSkillShot(spell, tHeroPos, MyHero.BoundingRadius);
                }
            }

            return false;
        }

        public static bool CheckPathCollision(Obj_AI_Base unit, Vector2 movePos)
        {
            var path = unit.GetPath(movePos.To3D());

            if (path.Length <= 0)
            {
                return false;
            }

            return movePos.Distance(path[path.Length - 1].To2D()) > 5 || path.Length > 2;
        }

        public static bool LineIntersectLinearSpell(Vector2 a, Vector2 b, Spell spell)
        {
            var myBoundingRadius = MyHero.BoundingRadius;
            var spellDir = spell.Direction;
            var pSpellDir = spell.Direction.Perpendicular();
            var spellRadius = GetSpellRadius(spell);
            var spellPos = SpellDetector.GetCurrentSpellPosition(spell) - spellDir*myBoundingRadius;
            // Leave some space at back of spell
            var endPos = spell.EndPos + spellDir*myBoundingRadius; // Leave some space at the front of spell

            var startRightPos = spellPos + pSpellDir*(spellRadius + myBoundingRadius);
            var startLeftPos = spellPos - pSpellDir*(spellRadius + myBoundingRadius);
            var endRightPos = endPos + pSpellDir*(spellRadius + myBoundingRadius);
            var endLeftPos = endPos - pSpellDir*(spellRadius + myBoundingRadius);

            var int1 = MathUtils.CheckLineIntersection(a, b, startRightPos, startLeftPos);
            var int2 = MathUtils.CheckLineIntersection(a, b, endRightPos, endLeftPos);
            var int3 = MathUtils.CheckLineIntersection(a, b, startRightPos, endRightPos);
            var int4 = MathUtils.CheckLineIntersection(a, b, startLeftPos, endLeftPos);

            return int1 || int2 || int3 || int4;
        }

        public static bool CheckMoveToDirection(Vector2 movePos)
        {
            var heroPoint = MyHero.ServerPosition.To2D();
            var dir = (movePos - heroPoint).Normalized();

            foreach (var entry in SpellDetector.Spells)
            {
                var spell = entry.Value;
                if (PlayerInSkillShot(spell))
                {
                    continue;
                }

                var spellPos = SpellDetector.GetCurrentSpellPosition(spell);

                if (spellPos.Distance(heroPoint) > 500 + spell.Info.Radius)
                {
                    return GetSpellCollisionTimeToPos(spell, movePos, MyHero.MoveSpeed, 0,
                        MyHero.ServerPosition.To2D());
                }

                switch (spell.Info.SpellType)
                {
                    case SpellType.Line:
                        if (LineIntersectLinearSpell(heroPoint, movePos, spell))
                            return true;
                        break;
                    case SpellType.Circular:
                        bool isCollision;
                        var collisionTime = MathUtils.GetCollisionTime(heroPoint, spell.EndPos, dir*MyHero.MoveSpeed,
                            new Vector2(0, 0), 1, GetSpellRadius(spell) + MyHero.BoundingRadius, out isCollision);
                        if (collisionTime > 0)
                        {
                            if (collisionTime >= heroPoint.Distance(movePos)/MyHero.MoveSpeed)
                                // Collision occurs when hero is moving to the destination
                            {
                                return true;
                            }
                        }
                        break;
                    case SpellType.Cone:
                        break;
                }
            }

            return false;
        }

        public class PositionInfo
        {
            public float Dist;
            public List<int> DodgeableSpells;
            public bool IsDangerousPos;
            public int PosDangerCount;
            public int PosDangerLevel;
            public Vector2 Position;
            public float TimeStamp;

            public PositionInfo(
                Vector2 position,
                int posDangerLevel,
                int posDangerCount,
                bool isDangerousPos,
                float dist,
                List<int> dodgeableSpells)
            {
                Position = position;
                PosDangerLevel = posDangerLevel;
                PosDangerCount = posDangerCount;
                IsDangerousPos = isDangerousPos;
                Dist = dist;
                DodgeableSpells = dodgeableSpells;
                TimeStamp = GameTime;
            }
        }
    }
}