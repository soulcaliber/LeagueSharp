using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.SpellTracker
{
    class SpellDurationInfo
    {
        public string charName;
        public string spellName;
        public float duration;

        public SpellDurationInfo()
        {

        }
    }

    class SpellDurationDatabase
    {
        public static List<SpellDurationInfo> spellDatabase = new List<SpellDurationInfo>();

        static SpellDurationDatabase()
        {
            spellDatabase.Add(new SpellDurationInfo
            {
                charName = "Ezreal",
                spellName = "ezrealmysticshot",
                duration = 4000,
            });
        }
    }
}
