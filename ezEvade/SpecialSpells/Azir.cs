using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade.SpecialSpells
{
    class Azir : ChampionPlugin
    {
        static Azir()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "AzirQWrapper")
            {
                var hero = HeroManager.Enemies.FirstOrDefault(h => h.ChampionName == "Azir");
                if (hero == null)
                {
                    return;
                }

                var info = new ObjectTrackerInfo(hero)
                {
                    Name = "AzirQSoldier",
                    OwnerNetworkID = hero.NetworkId
                };

                ObjectTracker.objTracker.Add(hero.NetworkId, info);
                GameObject.OnCreate += (obj, args) => OnCreateObj_AzirSoldier(obj, args, hero);
                GameObject.OnDelete += (obj, args) => OnDeleteObj_AzirSoldier(obj, args, hero);
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_AzirSoldier;
            }
        }

        private static void OnCreateObj_AzirSoldier(GameObject obj, EventArgs args, Obj_AI_Hero hero)
        {
            if (obj.Name == "AzirSoldier")
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in ObjectTracker.objTracker)
                {
                    var info = entry.Value;
                    if (info.Name == "AzirQSoldier")
                    {
                        info.usePosition = false;
                        info.objList.Add(obj.NetworkId, obj);
                        
                        Utility.DelayAction.Add(8900, () =>
                        {
                            if (info.objList.ContainsKey(obj.NetworkId))
                                info.objList.Remove(obj.NetworkId);
                        });
                    }
                }
            }
        }

        private static void OnDeleteObj_AzirSoldier(GameObject obj, EventArgs args, Obj_AI_Hero hero)
        {
            if (obj.Name == "AzirSoldier")
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in ObjectTracker.objTracker)
                {
                    var info = entry.Value;
                    if (info.Name == "AzirQSoldier")
                    {
                        if (info.objList.ContainsKey(obj.NetworkId))
                            info.objList.Remove(obj.NetworkId);
                    }
                }
            }
        }

        private static void ProcessSpell_AzirSoldier(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "AzirQWrapper")
            {
                foreach (KeyValuePair<int, ObjectTrackerInfo> entry in ObjectTracker.objTracker)
                {
                    var info = entry.Value;
                    if (info.Name == "AzirQSoldier")
                    {
                        foreach (var objEntry in info.objList)
                        {
                            var soldier = objEntry.Value;
                            if (soldier == null || !soldier.IsValid || soldier.IsDead)
                            {
                                continue;
                            }

                            SpellDetector.CreateSpellData(hero, soldier.Position, args.End, spellData);
                        }
                    }
                }

                specialSpellArgs.noProcess = true;
            }
        }
    }
}
