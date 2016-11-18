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
                Game.OnUpdate += Game_OnUpdate;
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            var ahri = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Ahri");
            if (ahri != null && ahri.CheckTeam())
            {
                foreach (
                    var spell in
                        SpellDetector.detectedSpells.Where(
                            s =>
                                s.Value.heroID == ahri.NetworkId &&
                                s.Value.info.spellName.ToLower() == "ahriorbofdeception2"))
                {
                    spell.Value.endPos = ahri.ServerPosition.To2D();
                }
            }
        }
    }
}
