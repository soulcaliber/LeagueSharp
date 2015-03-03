using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class SpecialSpells
    {

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static Dictionary<string, bool> pDict = new Dictionary<string, bool>();

        public static void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.isThreeWay && !pDict.ContainsKey("ProcessSpell_ProcessThreeWay"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ProcessThreeWay;
                pDict["ProcessSpell_ProcessThreeWay"] = true;
            }

            if (spellData.spellName == "ZiggsQ" && !pDict.ContainsKey("ProcessSpell_ProcessZiggsQ"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ProcessZiggsQ;
                pDict["ProcessSpell_ProcessZiggsQ"] = true;                
            }
        }

        private static void ProcessSpell_ProcessThreeWay(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData)
        {
            if (spellData.isThreeWay)
            {
                Vector3 endPos2 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), spellData.angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos2, spellData, null);

                Vector3 endPos3 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), -spellData.angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos3, spellData, null);
            }
        }

        private static void ProcessSpell_ProcessZiggsQ(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData)
        {
            if (spellData.spellName == "ZiggsQ")
            {
                var startPos = hero.ServerPosition.To2D();
                var endPos = args.End.To2D();
                var dir = (endPos - startPos).Normalized();

                if (endPos.Distance(startPos) > 850)
                {
                    endPos = startPos + dir * 850;
                }

                SpellDetector.CreateSpellData(hero, args.Start, endPos.To3D(), spellData, null);

                var endPos2 = endPos + dir * 0.4f * startPos.Distance(endPos);
                SpellDetector.CreateSpellData(hero, args.Start, endPos2.To3D(), spellData, null, 250);

                var endPos3 = endPos2 + dir * 0.6f * endPos.Distance(endPos2);
                SpellDetector.CreateSpellData(hero, args.Start, endPos3.To3D(), spellData, null, 800);
            }
        }
        
    }
}
