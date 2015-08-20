using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.TowerRange
{
    class RangeIndicator
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static Dictionary<int, Obj_AI_Turret> turretCache = new Dictionary<int, Obj_AI_Turret>();
        private static Dictionary<int, AttackableUnit> turretTarget = new Dictionary<int, AttackableUnit>();

        private static Menu menu;

        public RangeIndicator(Menu mainMenu)
        {
            menu = mainMenu;

            Menu towerMenu = new Menu("TowerRange", "TowerRange");
            towerMenu.AddItem(new MenuItem("DrawEnemyTowerRange", "Draw Enemy Tower Range").SetValue(true));
            towerMenu.AddItem(new MenuItem("DrawAllyTowerRange", "Draw Ally Tower Range").SetValue(true));

            menu.AddSubMenu(towerMenu);

            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnTarget += Turret_OnTarget;

            InitializeCache();
        }

        private void Turret_OnTarget(Obj_AI_Base sender, Obj_AI_BaseTargetEventArgs args)
        {
            var turret = sender as Obj_AI_Turret;
            if (turret != null && turretCache.ContainsKey(sender.NetworkId))
            {
                turretTarget[sender.NetworkId] = args.Target;
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            var turretRange = 800 + myHero.BoundingRadius;

            foreach (var entry in turretCache)
            {
                var turret = entry.Value;

                var circlePadding = 20;

                if (turret == null || !turret.IsValid || turret.IsDead)
                {
                    Utility.DelayAction.Add(1, () => turretCache.Remove(entry.Key));
                    continue;
                }

                if (turret.IsEnemy && menu.Item("DrawEnemyTowerRange").GetValue<bool>() == false)
                {
                    continue;
                }

                if (turret.IsAlly && menu.Item("DrawAllyTowerRange").GetValue<bool>() == false)
                {
                    continue;
                }

                if (turret.TotalAttackDamage > 800)
                {
                    //turretRange = 1400 + myHero.BoundingRadius;
                    continue;
                }

                var distToTurret = myHero.ServerPosition.Distance(turret.Position);
                if (distToTurret < turretRange + 500)
                {
                    var tTarget = turretTarget[turret.NetworkId];
                    if (tTarget.IsValidTarget(float.MaxValue, false))
                    {
                        if (tTarget is Obj_AI_Hero)
                        {
                            Render.Circle.DrawCircle(tTarget.Position, tTarget.BoundingRadius + circlePadding,
                            Color.FromArgb(255, 255, 0, 0), 20);
                        }
                        else
                        {
                            Render.Circle.DrawCircle(tTarget.Position, tTarget.BoundingRadius + circlePadding,
                            Color.FromArgb(255, 0, 255, 0), 10);
                        }
                    }

                    if (tTarget != null && (tTarget.IsMe || (turret.IsAlly && tTarget is Obj_AI_Hero)))
                    {
                        Render.Circle.DrawCircle(turret.Position, turretRange,
                            Color.FromArgb(255, 255, 0, 0), 20);
                    }
                    else
                    {
                        var alpha = distToTurret > turretRange ? (turretRange + 500 - distToTurret) / 2 : 250;
                        Render.Circle.DrawCircle(turret.Position, turretRange,
                            Color.FromArgb((int)alpha, 0, 255, 0), 10);
                    }
                }

            }
        }

        private static void InitializeCache()
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Turret>())
            {
                if (!turretCache.ContainsKey(obj.NetworkId))
                {
                    turretCache.Add(obj.NetworkId, obj);
                    turretTarget.Add(obj.NetworkId, null);
                }
            }
        }
    }
}
