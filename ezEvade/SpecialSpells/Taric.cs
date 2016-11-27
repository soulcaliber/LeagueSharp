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
                var hero = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Taric");
                if (hero != null)
                {
                    Game.OnUpdate += (args) => Game_OnUpdate(args, hero);
                    SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
                }
            }
        }

        private void Game_OnUpdate(EventArgs args, Obj_AI_Hero hero)
        {
            if (hero != null && hero.CheckTeam())
            {
                foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == hero.NetworkId))
                {
                    if (spell.Value.info.spellName.ToLower() == "tarice")
                    {
                        spell.Value.startPos = hero.ServerPosition.To2D();
                        spell.Value.endPos = hero.ServerPosition.To2D() + spell.Value.direction * spell.Value.info.range;
                    }
                }

                var partner = HeroManager.AllHeroes.FirstOrDefault(x => x.HasBuff("taricwleashactive"));
                if (partner != null && partner.CheckTeam())
                {
                    foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == partner.NetworkId))
                    {
                        if (spell.Value.info.spellName.ToLower() == "tarice")
                        {
                            spell.Value.startPos = partner.ServerPosition.To2D();
                            spell.Value.endPos = partner.ServerPosition.To2D() + spell.Value.direction * spell.Value.info.range;
                        }
                    }
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
