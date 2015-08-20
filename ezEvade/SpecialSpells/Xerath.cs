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
    class Xerath : ChampionPlugin
    {
        static Xerath()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "xeratharcanopulse2")
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_XerathArcanopulse2;
            }
        }

        private static void ProcessSpell_XerathArcanopulse2(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (args.SData.Name == "XerathArcanopulseChargeUp")// || spellData.spellName == "xeratharcanopulse2")
            {
                var castTime = -1 * (hero.Spellbook.CastTime - Game.Time) * 1000;

                if (castTime > 0)
                {
                    var dir = (args.End.To2D() - args.Start.To2D()).Normalized();
                    var endPos = args.Start.To2D() + dir * Math.Min(spellData.range, 750 + castTime / 2);
                    SpellDetector.CreateSpellData(hero, args.Start, endPos.To3D(), spellData);
                }

                specialSpellArgs.noProcess = true;
            }
        }
    }
}
