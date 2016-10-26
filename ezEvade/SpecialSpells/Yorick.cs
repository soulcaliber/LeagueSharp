using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace ezEvade.SpecialSpells
{
    class Yorick : ChampionPlugin
    {
        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "YorickE")
            {
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "YorickE")
            {
                var end = args.End;
                var start = args.Start;
                var direction = (end - start).Normalized();

                if (start.Distance(end) > spellData.range)
                    end = start + (end - start).Normalized() * spellData.range;

                var spellStart = end.Extend(hero.ServerPosition, 100);
                var spellEnd = spellStart + direction * 1;

                SpellDetector.CreateSpellData(hero, spellStart, spellEnd, spellData);
                specialSpellArgs.noProcess = true;
            }
        }
    }
}
