using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    public static class Position
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static int CheckPosDangerLevel(this Vector2 pos, float extraBuffer)
        {
            var dangerlevel = 0;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (pos.InSkillShot(spell, myHero.BoundingRadius + extraBuffer))
                {
                    dangerlevel += spell.GetSpellDangerLevel();
                }
            }
            return dangerlevel;
        }

        public static bool InSkillShot(this Vector2 position, Spell spell, float radius, bool predictCollision = true)
        {
            if (spell.info.spellType == SpellType.Line)
            {
                Vector2 spellPos = spell.GetCurrentSpellPosition(); //leave little space at back of skillshot
                Vector2 spellEndPos = predictCollision ? spell.GetSpellEndPosition() : spell.endPos;

                /*if (spell.info.projectileSpeed == float.MaxValue
                    && Evade.GetTickCount - spell.startTime > spell.info.spellDelay)
                {
                    return false;
                }*/

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

        public static float GetDistanceToChampions(this Vector2 pos)
        {
            float minDist = float.MaxValue;

            foreach (var hero in HeroManager.Enemies)
            {
                if (hero != null && hero.IsValid && !hero.IsDead && hero.IsVisible)
                {
                    var heroPos = hero.ServerPosition.To2D();
                    var dist = heroPos.Distance(pos);

                    minDist = Math.Min(minDist, dist);
                }
            }

            return minDist;
        }

        public static bool HasExtraAvoidDistance(this Vector2 pos, float extraBuffer)
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (spell.info.spellType == SpellType.Line)
                {
                    if (pos.InSkillShot(spell, myHero.BoundingRadius + extraBuffer))
                    {
                        return true;
                    }
                }
            }
            return false;
        }  

        public static bool CheckDangerousPos(this Vector2 pos, float extraBuffer)
        {
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (pos.IsUnderTurret())
                {
                    return true;
                }

                if (pos.InSkillShot(spell, myHero.BoundingRadius + extraBuffer))
                {
                    return true;
                }
            }
            return false;
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
    }
}
