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
    public static class Situation
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        static Situation()
        {

        }

        public static bool ShouldDodge()
        {
            if(Evade.menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<KeyBind>().Active == false
                || Evade.isChanneling
                || myHero.IsDead
                || myHero.IsInvulnerable
                || myHero.IsImmovable
                || (myHero.ChampionName == "Sion" && myHero.HasBuff("SionR"))
                || (myHero.ChampionName == "KogMaw" && myHero.HasBuff("kogmawicathiansurprise"))
                //|| HasSpellShield(myHero)
                || myHero.IsDashing()
                || Evade.hasGameEnded == true
                )
            {
                //has spellshield - sivir, noc, morgana
                //vlad pool
                //tryndamere r?
                //kayle ult buff?
                //hourglass
                //invulnerable
                //rooted
                //sion ult -> tenacity = 100?
                //stunned
                //elise e
                //zilean ulted
                //isdashing

                return false;
            }

            

              return true;
        }

        //from Evade by Esk0r
        public static bool HasSpellShield(Obj_AI_Hero unit)
        {
            if (ObjectManager.Player.HasBuffOfType(BuffType.SpellShield))
            {
                return true;
            }

            if (ObjectManager.Player.HasBuffOfType(BuffType.SpellImmunity))
            {
                return true;
            }

            //Sivir E
            if (unit.LastCastedSpellName() == "SivirE" && (Evade.GetTickCount() - Evade.lastSpellCastTime) < 300)
            {
                return true;
            }

            //Morganas E
            if (unit.LastCastedSpellName() == "BlackShield" && (Evade.GetTickCount() - Evade.lastSpellCastTime) < 300)
            {
                return true;
            }

            //Nocturnes E
            if (unit.LastCastedSpellName() == "NocturneShit" && (Evade.GetTickCount() - Evade.lastSpellCastTime) < 300)
            {
                return true;
            }

            return false;
        }

        public static bool ShouldUseEvadeSpell()
        {
            if (Evade.menu.SubMenu("Main").Item("UseEvadeSpells").GetValue<bool>() == false
                || Evade.isChanneling
                || myHero.IsDead
                || myHero.IsInvulnerable
                || Evade.hasGameEnded == true
                )
            {
                return false;
            }

            return true;
        }
    }
}
