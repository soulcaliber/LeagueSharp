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
    class Ashe : ChampionPlugin
    {
        static Ashe()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "Volley")
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_AsheVolley;
            }
        }

        private static void ProcessSpell_AsheVolley(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "Volley")
            {
                for (int i = -4; i < 5; i++)
                {
                    Vector3 endPos2 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), i * spellData.angle).To3D();
                    if (i != 0)
                    {
                        SpellDetector.CreateSpellData(hero, args.Start, endPos2, spellData, null, 0, false);
                    }
                }
            }
        }
    }
}
