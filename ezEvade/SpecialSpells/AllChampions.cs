using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade.SpecialSpells
{

    class AllChampions : ChampionPlugin
    {
        public static Dictionary<string, bool> pDict = new Dictionary<string, bool>();

        static AllChampions()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.isThreeWay && !pDict.ContainsKey("ProcessSpell_ProcessThreeWay"))
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_ThreeWay;
                pDict["ProcessSpell_ProcessThreeWay"] = true;
            }    

            if (spellData.hasEndExplosion && !pDict.ContainsKey("OnUpdate_EndExplosion"))
            {
                Game.OnUpdate += OnUpdate_EndExplosion;
                pDict["OnUpdate_EndExplosion"] = true;
            }
        }

        private void OnUpdate_EndExplosion(EventArgs args)
        {
            if (ObjectCache.menuCache.cache["CheckSpellCollision"].GetValue<bool>() == false)
            {
                return;
            }

            foreach (var entry in SpellDetector.detectedSpells)
            {
                var spell = entry.Value;
                if (spell.info.name.Contains("_exp") && spell.spellType == SpellType.Circular)
                {
                    spell.endPos = spell.GetCurrentSpellPosition();
                }
            }
        }

        private static void ProcessSpell_ThreeWay(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.isThreeWay)
            {
                Vector3 endPos2 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), spellData.angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos2, spellData, null, 0, false);

                Vector3 endPos3 = MathUtils.RotateVector(args.Start.To2D(), args.End.To2D(), -spellData.angle).To3D();
                SpellDetector.CreateSpellData(hero, args.Start, endPos3, spellData, null, 0, false);
            }
        }

    }
}
