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
    class Sion : ChampionPlugin
    {
        static Sion()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            /*if (spellData.spellName == "SionE")
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_SionE;
            }*/
        }

        private static void ProcessSpell_SionE(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData,
            SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "SionE")
            {
                var objList = new List<Obj_AI_Minion>();
                foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
                {
                    if (obj != null && obj.IsValid && !obj.IsDead && obj.IsAlly)
                    {
                        objList.Add(obj);
                    }
                }

                objList.OrderBy(o => o.Distance(hero.ServerPosition));

                var spellStart = args.Start.To2D();
                var dir = (args.End.To2D() - spellStart).Normalized();
                var spellEnd = spellStart + dir * spellData.range;

                foreach (var obj in objList)
                {
                    var objProjection = obj.ServerPosition.To2D().ProjectOn(spellStart, spellEnd);

                    if (objProjection.IsOnSegment && objProjection.SegmentPoint.Distance(obj.ServerPosition.To2D()) < obj.BoundingRadius + spellData.radius)
                    {
                        //sth happens
                    }
                }


                //specialSpellArgs.noProcess = true;
            }
        }
    }
}
