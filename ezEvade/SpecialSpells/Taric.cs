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

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "TaricE")
            {
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "TaricE")
            {
                var sdata = new SpellData();
                sdata.charName = "Taric";
                sdata.name = "TaricE2";
                sdata.spellName = "TaricE";
                sdata.range = 750;
                sdata.radius = 100;
                sdata.fixedRange = true;
                sdata.spellDelay = 1000;
                sdata.projectileSpeed = int.MaxValue;
                sdata.dangerlevel = 3;

                var partner = HeroManager.Enemies.FirstOrDefault(x => x.HasBuff("taricwleashactive"));
                if (partner != null && partner.ChampionName != "Taric")
                {
                    var start = partner.ServerPosition.To2D();
                    var direction = (args.End.To2D() - start).Normalized();
                    var end = start + direction * spellData.range;

                    SpellDetector.CreateSpellData(partner, start.To3D(), end.To3D(), sdata);
                }
            }
        }
    }
}
