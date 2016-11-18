using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace ezEvade.SpecialSpells
{
    class Taric : ChampionPlugin
    {
        static Taric()
        {
            // taric E rework
            // todo: fix for multiple tarics on same team (one for all)
        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "TaricE")
            {
                Game.OnUpdate += Game_OnUpdate;
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            var taric = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Taric");
            if (taric != null && taric.CheckTeam())
            {
                foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == taric.NetworkId))
                {
                    spell.Value.startPos = taric.ServerPosition.To2D();
                    spell.Value.endPos = taric.ServerPosition.To2D() + spell.Value.direction * spell.Value.info.range;
                }
            }

            var partner = HeroManager.AllHeroes.FirstOrDefault(x => x.HasBuff("taricwleashactive"));
            if (partner != null && taric.CheckTeam())
            {
                foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == partner.NetworkId))
                {
                    spell.Value.startPos = partner.ServerPosition.To2D();
                    spell.Value.endPos = partner.ServerPosition.To2D() + spell.Value.direction * spell.Value.info.range;
                }
            }
        }

        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "TaricE")
            {
                var partner = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName != "Taric" && x.HasBuff("taricwleashactive"));
                if (partner != null && partner.CheckTeam())
                {
                    var start = partner.ServerPosition.To2D();
                    var direction = (args.End.To2D() - start).Normalized();
                    var end = start + direction * spellData.range;

                    SpellDetector.CreateSpellData(partner, start.To3D(), end.To3D(), spellData);
                }
            }
        }
    }
}
