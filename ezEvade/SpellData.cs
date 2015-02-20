using LeagueSharp;

namespace ezEvade
{
    public enum SpellType
    {
        Line,
        Circular,
        Cone
    }

    public class SpellData
    {
        public float Angle;
        public string CharName;
        public int Dangerlevel = 1;
        public bool DefaultOff = false;
        public float ExtraDelay = 0;
        public float ExtraDistance = 0;
        public float ExtraEndTime = 0;
        public bool HasEndExplosion = false;
        public bool IsThreeWay = false;
        public bool IsWall = false;
        public string MissileName = string.Empty;
        public string Name;
        public float ProjectileSpeed = float.MaxValue;
        public float Radius;
        public float Range;
        public bool RangeCap = false;
        public float SideRadius;
        public float SpellDelay = 250;
        public SpellSlot SpellKey = SpellSlot.Q;
        public string SpellName;
        public SpellType SpellType;
        public int Splits;
        public bool UsePackets = false;

        public SpellData()
        {
        }

        public SpellData(
            string charName,
            string spellName,
            string name,
            int range,
            int radius,
            int dangerlevel,
            SpellType spellType
            )
        {
            CharName = charName;
            SpellName = spellName;
            Name = name;
            Range = range;
            Radius = radius;
            Dangerlevel = dangerlevel;
            SpellType = spellType;
        }
    }
}