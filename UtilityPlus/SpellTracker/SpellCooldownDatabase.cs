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
    class SpellCooldownInfo
    {
        public string charName;
        public string spellName;
        public SpellSlot spellSlot;
        public float[] cooldownArray;

        public SpellCooldownInfo()
        {

        }
    }

    class SpellCooldownDatabase
    {
        public static List<SpellCooldownInfo> spellCDDatabase = new List<SpellCooldownInfo>();

        static SpellCooldownDatabase()
        {
            spellCDDatabase.Add(new SpellCooldownInfo
            {
                charName = "AllChampions",
                spellName = "summonerteleport",
                spellSlot = SpellSlot.Summoner1,
                cooldownArray = new float[] { 300, 300, 300, 300, 300},
            });

            /*spellCDDatabase.Add(new SpellCooldownInfo
            {
                charName = "Ezreal",
                spellName = "ezrealmysticshot",
                spellSlot = SpellSlot.Q,
                cooldownArray = new float[] { 4, 4, 4, 4, 4 },
            });*/
        }
    }
}
