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
    class Fizz : ChampionPlugin
    {
        static Fizz()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "FizzMarinerDoom")
            {
                Obj_AI_Minion.OnCreate += (obj, args) => OnCreateObj_FizzMarinerDoom(obj, args, spellData);
                Obj_AI_Minion.OnDelete += (obj, args) => OnDeleteObj_FizzMarinerDoom(obj, args, spellData);
            }

            if (spellData.spellName == "FizzPiercingStrike")
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_FizzPiercingStrike;
            }
        }

        private static void ProcessSpell_FizzPiercingStrike(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "FizzPiercingStrike")
            {
                if (args.Target != null && args.Target.IsMe)
                {
                    SpellDetector.CreateSpellData(hero, args.Start, args.End, spellData, null, 0);
                }

                specialSpellArgs.noProcess = true;
            }
        }

        private static void OnDeleteObj_FizzMarinerDoom(GameObject obj, EventArgs args, SpellData spellData)
        {
            //need to track where bait is attached to

            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != ObjectManager.Player.Team &&
                missile.SData.Name == "FizzMarinerDoomMissile")
            {
                SpellDetector.CreateSpellData(missile.SpellCaster, missile.StartPosition, missile.EndPosition,
                spellData, null, 1000, true, SpellType.Circular, false, 350);
            }
        }

        private static void OnCreateObj_FizzMarinerDoom(GameObject obj, EventArgs args, SpellData spellData)
        {
            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

            if (missile.SpellCaster != null && missile.SpellCaster.Team != ObjectManager.Player.Team &&
                missile.SData.Name == "FizzMarinerDoomMissile")
            {
                SpellDetector.CreateSpellData(missile.SpellCaster, missile.StartPosition, missile.EndPosition,
                spellData, null, 500, true, SpellType.Circular, false, spellData.secondaryRadius);

                /*foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                {
                    var spell = entry.Value;

                    if (spell.info.spellName == "FizzMarinerDoom" &&
                        spell.spellObject != null && spell.spellObject.NetworkId == missile.NetworkId)
                    {
                        if (spell.spellType == SpellType.Circular)
                        {                            
                            spell.spellObject = null;
                        }
                    }
                }*/
            }
        }
    }
}
