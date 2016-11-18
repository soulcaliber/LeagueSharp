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
            // todo: fix for multiple sions on same team (e.g one for all)
        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "SionR")
            {
                Game.OnUpdate += Game_OnUpdate;
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "SionR")
            {
                spellData.projectileSpeed = hero.MoveSpeed;
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            var sion = HeroManager.AllHeroes.FirstOrDefault(x => x.ChampionName == "Sion");
            if (sion != null && sion.CheckTeam() && sion.HasBuff("SionR"))
            {
                foreach (var spell in SpellDetector.detectedSpells.Where(x => x.Value.heroID == sion.NetworkId && x.Value.info.spellName == "SionR"))
                {
                    var facingPos = sion.ServerPosition.To2D() + sion.Direction.To2D().Perpendicular();
                    var endPos = sion.ServerPosition.To2D() + (facingPos - sion.ServerPosition.To2D()).Normalized() * 450;

                    spell.Value.startPos = sion.ServerPosition.To2D();
                    spell.Value.endPos = endPos;

                    if (EvadeUtils.TickCount - spell.Value.startTime >= 1000)
                    {
                        SpellDetector.CreateSpellData(sion, sion.ServerPosition, endPos.To3D(), spell.Value.info, null, 0, false, SpellType.Line, false);
                        spell.Value.startTime = EvadeUtils.TickCount;
                        break;
                    }
                }
            }
        }
    }
}
