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
    public enum EvadeOrderCommand
    {
        None,
        MoveTo,
        CastSpell
    }

    class EvadeCommand
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static float gameTime { get { return Game.ClockTime * 1000; } }

        public EvadeOrderCommand order;
        public SpellSlot spellKey;
        public Vector2 targetPosition;
        public Obj_AI_Base target;
        public float timestamp;
        public bool isProcessed;

        public EvadeCommand()
        {
            this.timestamp = gameTime;
            this.isProcessed = false;
        }
    }
}
