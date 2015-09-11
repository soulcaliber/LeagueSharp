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
    public class PositionInfo
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public int posDangerLevel = 0;
        public int posDangerCount = 0;
        public bool isDangerousPos = false;
        public float distanceToMouse = 0;
        public List<int> dodgeableSpells = new List<int>();
        public List<int> undodgeableSpells = new List<int>();
        public List<int> spellList = new List<int>();
        public Vector2 position;
        public float timestamp;
        public float endTime = 0;
        public bool hasExtraDistance = false;
        public float closestDistance = float.MaxValue;
        public float intersectionTime = float.MaxValue;
        public bool rejectPosition = false;
        public float posDistToChamps = float.MaxValue;
        public bool hasComfortZone = true;
        public Obj_AI_Base target = null;
        public bool recalculatedPath = false;
        public float speed = 0;

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
            this.timestamp = EvadeUtils.TickCount;
        }

        public PositionInfo(
            Vector2 position,
            bool isDangerousPos,
            float distanceToMouse)
        {
            this.position = position;
            this.isDangerousPos = isDangerousPos;
            this.distanceToMouse = distanceToMouse;
            this.timestamp = EvadeUtils.TickCount;
        }

        public static PositionInfo SetAllDodgeable()
        {
            return SetAllDodgeable(myHero.Position.To2D());
        }

        public static PositionInfo SetAllDodgeable(Vector2 position)
        {
            List<int> dodgeableSpells = new List<int>();
            List<int> undodgeableSpells = new List<int>();

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;
                dodgeableSpells.Add(entry.Key);
            }

            return new PositionInfo(
                position,
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

                var spellDangerLevel = spell.dangerlevel;

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
    }

    public static class PositionInfoExtensions
    {
        public static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static int GetHighestSpellID(this PositionInfo posInfo)
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

        public static bool isSamePosInfo(this PositionInfo posInfo1, PositionInfo posInfo2)
        {
            return new HashSet<int>(posInfo1.spellList).SetEquals(posInfo2.spellList);
        }

        public static bool isBetterMovePos(this PositionInfo newPosInfo)
        {
            PositionInfo posInfo = null;
            var path = myHero.Path;
            if (path.Length > 0)
            {
                var movePos = path[path.Length - 1].To2D();
                posInfo = EvadeHelper.CanHeroWalkToPos(movePos, ObjectCache.myHeroCache.moveSpeed, 0, 0, false);
            }
            else
            {
                posInfo = EvadeHelper.CanHeroWalkToPos(ObjectCache.myHeroCache.serverPos2D, ObjectCache.myHeroCache.moveSpeed, 0, 0, false);
            }

            if (posInfo.posDangerCount < newPosInfo.posDangerCount)
            {
                return false;
            }

            return true;
        }

        public static PositionInfo CompareLastMovePos(this PositionInfo newPosInfo)
        {
            PositionInfo posInfo = null;
            var path = myHero.Path;
            if (path.Length > 0)
            {
                var movePos = path[path.Length - 1].To2D();
                posInfo = EvadeHelper.CanHeroWalkToPos(movePos, ObjectCache.myHeroCache.moveSpeed, 0, 0, false);
            }
            else
            {
                posInfo = EvadeHelper.CanHeroWalkToPos(ObjectCache.myHeroCache.serverPos2D, ObjectCache.myHeroCache.moveSpeed, 0, 0, false);
            }

            if (posInfo.posDangerCount < newPosInfo.posDangerCount)
            {
                return posInfo;
            }

            return newPosInfo;
        }
    }


}
