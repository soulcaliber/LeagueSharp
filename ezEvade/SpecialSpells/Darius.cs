using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace ezEvade.SpecialSpells
{
    class Darius : ChampionPlugin
    {
        static Darius()
        {
            // todo: fix for multiple darius' on same team (one for all)
        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "DariusCleave")
            {
                Game.OnUpdate += Game_OnUpdate;
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            var darius = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Darius");
            if (darius != null && darius.CheckTeam())
            {
                foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == darius.NetworkId))
                {
                    if (spell.Value.info.spellName == "DariusCleave")
                    {
                        spell.Value.startPos = darius.ServerPosition.To2D();
                        spell.Value.endPos = darius.ServerPosition.To2D() + spell.Value.direction * spell.Value.info.range;                   
                    }
                }
            }
        }

        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "DariusCleave")
            {
                //SpellDetector.CreateSpellData(hero, start.To3D(), end.To3D(), spellData);
            }
        }
    }
}
