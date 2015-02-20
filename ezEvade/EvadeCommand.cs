using LeagueSharp;
using SharpDX;

namespace ezEvade
{
    public enum EvadeOrderCommand
    {
        None,
        MoveTo,
        CastSpell
    }

    internal class EvadeCommand
    {
        public bool IsProcessed;
        public EvadeOrderCommand Order;
        public SpellSlot SpellKey;
        public Obj_AI_Base Target;
        public Vector2 TargetPosition;
        public float TimeStamp;

        public EvadeCommand()
        {
            TimeStamp = GameTime;
            IsProcessed = false;
        }

/*
        private static Obj_AI_Hero MyHero
        {
            get { return ObjectManager.Player; }
        }
*/

        private static float GameTime
        {
            get { return Game.ClockTime*1000; }
        }
    }
}