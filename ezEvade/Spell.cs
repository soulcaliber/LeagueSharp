using LeagueSharp;
using SharpDX;

namespace ezEvade
{
    public class Spell
    {
        public Vector2 Direction;
        public Vector2 EndPos;
        public float EndTime;
        public int HeroId;
        public SpellData Info;
        public int ProjectileId;
        public int SpellId;
        public GameObject SpellObject;
        public Vector2 StartPos;
        public float StartTime;
    }
}