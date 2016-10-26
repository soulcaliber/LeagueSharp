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
    class Twitch : ChampionPlugin
    {
        static Twitch()
        {
        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "TwitchSprayandPrayAttack")
            {
                SpellDetector.OnProcessSpecialSpell += ProcessSpell_TwitchSprayandPrayAttack;
            }
        }

        private void ProcessSpell_TwitchSprayandPrayAttack(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "TwitchSprayandPrayAttack")
            {
                if (args.Target != null)
                {
                    var start = hero.ServerPosition;
                    var end = hero.ServerPosition + (args.Target.Position - hero.ServerPosition) * spellData.range;

                    var data = (SpellData) spellData.Clone();
                    data.spellDelay = hero.AttackCastDelay * 1000;

                    SpellDetector.CreateSpellData(hero, start, end, data);
                }
            }
        }
    }
}
