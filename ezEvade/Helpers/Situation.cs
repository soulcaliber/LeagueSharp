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

        public static bool isNearEnemy(this Vector2 pos, float distance, bool alreadyNear = true)
        {
            if (ObjectCache.menuCache.cache["PreventDodgingNearEnemy"].GetValue<bool>())
            {
                var curDistToEnemies = ObjectCache.myHeroCache.serverPos2D.GetDistanceToChampions();
                var posDistToEnemies = pos.GetDistanceToChampions();
                
                if (curDistToEnemies < distance)
                {
                    if (curDistToEnemies > posDistToEnemies)
                    {
                        return true;
                    }
                }
                else
                {
                    if (posDistToEnemies < distance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
                
        public static bool IsUnderTurret(this Vector2 pos, bool checkEnemy = true)
        {
            if (!ObjectCache.menuCache.cache["PreventDodgingUnderTower"].GetValue<bool>())
            {
                return false;
            }

            var turretRange = 875 + ObjectCache.myHeroCache.boundingRadius;

            foreach (var entry in ObjectCache.turrets)
            {
                var turret = entry.Value;
                if (turret == null || !turret.IsValid || turret.IsDead)
                {
                    Utility.DelayAction.Add(1, () => ObjectCache.turrets.Remove(entry.Key));
                    continue;
                }

                if (checkEnemy && turret.IsAlly)
                {
                    continue;
                }

                var distToTurret = pos.Distance(turret.Position.To2D());
                if (distToTurret <= turretRange)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ShouldDodge()
        {
            if (ObjectCache.menuCache.cache["DodgeSkillShots"].GetValue<KeyBind>().Active == false
                || CommonChecks()
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

        public static bool ShouldUseEvadeSpell()
        {
            if (ObjectCache.menuCache.cache["ActivateEvadeSpells"].GetValue<KeyBind>().Active == false
                || CommonChecks()
                || Evade.lastWindupTime - EvadeUtils.TickCount > 0
                )
            {
                return false;
            }

            return true;
        }

        public static bool CommonChecks()
        {
            return

                Evade.isChanneling
                || myHero.IsDead
                || myHero.IsInvulnerable
                || myHero.IsTargetable == false
                || HasSpellShield(myHero)
                || ChampionSpecificChecks()
                || myHero.IsDashing()
                || Evade.hasGameEnded == true;
        }

        public static bool ChampionSpecificChecks()
        {
            return (myHero.ChampionName == "Sion" && myHero.HasBuff("SionR"))
                ;

            //Untargetable
            //|| (myHero.ChampionName == "KogMaw" && myHero.HasBuff("kogmawicathiansurprise"))
            //|| (myHero.ChampionName == "Karthus" && myHero.HasBuff("KarthusDeathDefiedBuff"))

            //Invulnerable
            //|| myHero.HasBuff("kalistarallyspelllock"); 
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
            if (unit.LastCastedSpellName() == "SivirE" && (EvadeUtils.TickCount - Evade.lastSpellCastTime) < 300)
            {
                return true;
            }

            //Morganas E
            if (unit.LastCastedSpellName() == "BlackShield" && (EvadeUtils.TickCount - Evade.lastSpellCastTime) < 300)
            {
                return true;
            }

            //Nocturnes E
            if (unit.LastCastedSpellName() == "NocturneShit" && (EvadeUtils.TickCount - Evade.lastSpellCastTime) < 300)
            {
                return true;
            }

            return false;
        }

    }
}
