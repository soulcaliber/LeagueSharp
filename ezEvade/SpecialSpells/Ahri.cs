using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace ezEvade.SpecialSpells
{
    class Ahri : ChampionPlugin
    {
        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "AhriOrbofDeception2")
            {
                var hero = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Ahri");
                if (hero != null && hero.CheckTeam())
                {
                    Game.OnUpdate += (args) => Game_OnUpdate(args, hero);
                }
            }
        }

        private void Game_OnUpdate(EventArgs args, Obj_AI_Hero hero)
        {
            foreach (
                var spell in
                    SpellDetector.detectedSpells.Where(
                        s =>
                            s.Value.heroID == hero.NetworkId &&
                            s.Value.info.spellName.ToLower() == "ahriorbofdeception2"))
            {
                spell.Value.endPos = hero.ServerPosition.To2D();
            }           
        }
    }
}
