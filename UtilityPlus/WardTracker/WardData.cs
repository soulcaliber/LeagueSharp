using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityPlus.WardTracker
{
    class WardData
    {
        public float duration = 60 * 1000;
        public string objectName = "visionward";
        public float range = 1100;
        public string spellName = "visionward";

        public WardData()
        {

        }
    }

    class WardDatabase
    {
        public static List<WardData> wardDatabase = new List<WardData>();
        public static Dictionary<string, WardData> wardspellNames = new Dictionary<string, WardData>();
        public static Dictionary<string, WardData> wardObjNames = new Dictionary<string, WardData>();
        public static WardData missileWardData;

        static WardDatabase()
        {
            LoadWardDatabase();
            LoadWardDictionary();
        }

        private static void LoadWardDictionary()
        {
            foreach (var ward in wardDatabase)
            {
                var spellName = ward.spellName.ToLower();
                if (!wardspellNames.ContainsKey(spellName))
                {
                    wardspellNames.Add(spellName, ward);
                }

                var objName = ward.objectName.ToLower();
                if (!wardObjNames.ContainsKey(objName))
                {
                    wardObjNames.Add(objName, ward);
                }
            }
        }

        private static void LoadWardDatabase()
        {
            //Trinkets:
            wardDatabase.Add(
            new WardData
            {
                duration = 1 * 60 * 1000,
                objectName = "YellowTrinket",
                range = 1100,
                spellName = "TrinketTotemLvl1",
            });

            wardDatabase.Add(
            new WardData
            {
                duration = 2 * 60 * 1000,
                objectName = "YellowTrinketUpgrade",
                range = 1100,
                spellName = "TrinketTotemLvl2",
            });

            wardDatabase.Add(
            new WardData
            {
                duration = 3 * 60 * 1000,
                objectName = "SightWard",
                range = 1100,
                spellName = "TrinketTotemLvl3",
            });

            //Ward items and normal wards:
            wardDatabase.Add(
            new WardData
            {
                duration = 3 * 60 * 1000,
                objectName = "SightWard",
                range = 1100,
                spellName = "SightWard",
            });

            wardDatabase.Add(
            new WardData
            {
                duration = 3 * 60 * 1000,
                objectName = "SightWard",
                range = 1100,
                spellName = "ItemGhostWard",
            });

            missileWardData =
            new WardData
            {
                duration = 3 * 60 * 1000,
                objectName = "MissileWard",
                range = 1100,
                spellName = "MissileWard",
            };
        }
    }
}
