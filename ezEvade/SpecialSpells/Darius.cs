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
                var hero = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Darius");
                if (hero != null && hero.CheckTeam())
                {
                    Game.OnUpdate += (args) => Game_OnUpdate(args, hero);
                }
            }
        }

        private void Game_OnUpdate(EventArgs args, Obj_AI_Hero hero)
        {
            foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == hero.NetworkId))
            {
                if (spell.Value.info.spellName == "DariusCleave")
                {
                    spell.Value.startPos = hero.ServerPosition.To2D();
                    spell.Value.endPos = hero.ServerPosition.To2D() + spell.Value.direction * spell.Value.info.range;                   
                }
            }           
        }
    }
}
