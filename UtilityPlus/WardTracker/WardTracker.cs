using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.WardTracker
{
    class WardTrackerInfo
    {
        public WardData wardData;
        public Vector3 position;
        public Obj_AI_Base wardObject;
        public float timestamp;
        public float endTime;
        public bool unknownDuration;
        public Vector2 startPos;
        public Vector2 endPos;

        public WardTrackerInfo(
            WardData wardData,
            Vector3 position,
            Obj_AI_Base wardObject,
            bool fromMissile = false,
            float timestamp = 0)
        {
            this.wardData = wardData;
            this.position = position;
            this.wardObject = wardObject;
            this.unknownDuration = fromMissile;
            this.timestamp = timestamp == 0 ? HelperUtils.TickCount : timestamp;
            this.endTime = this.timestamp + wardData.duration;
        }
    }

    class Tracker
    {
        public static Menu menu;

        public static List<WardTrackerInfo> wards = new List<WardTrackerInfo>();
        public static float lastCheckExpiredWards = 0;

        public static System.Drawing.Size tStrSize = TextUtils.GetTextExtent("00:00");
        public static System.Drawing.Size utStrSize = TextUtils.GetTextExtent("?? 00:00 ??");
        public static System.Drawing.Size wardStrSize = TextUtils.GetTextExtent("Ward");

        public Tracker(Menu mainMenu)
        {
            menu = mainMenu;

            Menu wardTrackerMenu = new Menu("Ward Tracker", "WardTracker");

            wardTrackerMenu.AddItem(new MenuItem("TrackEnemyWards", "Track Wards").SetValue(true));

            menu.AddSubMenu(wardTrackerMenu);

            Obj_AI_Hero.OnProcessSpellCast += Game_OnProcessSpell;
            Obj_AI_Base.OnCreate += Game_OnCreateObj;
            Obj_AI_Base.OnDelete += Game_OnDeleteObj;

            Game.OnUpdate += Game_OnUpdate;

            Drawing.OnDraw += Drawing_OnDraw;

            InitWards();
        }

        private void InitWards()
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (obj != null && obj.IsValid && !obj.IsDead)
                {
                    Game_OnCreateObj(obj, null);
                }
            }
        }

        private void Game_OnDeleteObj(GameObject sender, EventArgs args)
        {
            if (!menu.Item("TrackEnemyWards").GetValue<bool>())
            {
                return;
            }

            if (sender.Type == GameObjectType.obj_AI_Minion)
            {
                foreach (var ward in wards)
                {
                    if (ward.wardObject != null && ward.wardObject.NetworkId == sender.NetworkId)
                    {
                        DelayAction.Add(0, () => wards.Remove(ward));
                    }
                }
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if (!menu.Item("TrackEnemyWards").GetValue<bool>())
            {
                return;
            }

            if (HelperUtils.TickCount > lastCheckExpiredWards)
            {
                CheckExpiredWards();
                lastCheckExpiredWards = HelperUtils.TickCount + 500;
            }

        }

        private void CheckExpiredWards()
        {
            foreach (var ward in wards)
            {
                if (HelperUtils.TickCount > ward.endTime)
                {
                    DelayAction.Add(0, () => wards.Remove(ward));
                }

                if (ward.wardObject != null && (ward.wardObject.IsDead || !ward.wardObject.IsValid))
                {
                    DelayAction.Add(0, () => wards.Remove(ward));
                }
            }
        }

        private void Game_OnProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {          

            if (!menu.Item("TrackEnemyWards").GetValue<bool>())
            {
                return;
            }

            if (hero.IsEnemy && hero.IsValid<Obj_AI_Hero>())
            {
                WardData wardData;
                if (WardDatabase.wardspellNames.TryGetValue(args.SData.Name.ToLower(), out wardData))
                {
                    var pos = args.End.To2D();

                    DelayAction.Add(50, () =>
                    {
                        foreach (var ward in wards)
                        {
                            if (ward.position.To2D().Distance(pos) < 50
                                && HelperUtils.TickCount - ward.timestamp < 100)
                            {
                                return;
                            }
                        }

                        WardTrackerInfo newWard = new WardTrackerInfo(
                            wardData,
                            pos.To3D(),
                            null
                            );

                        newWard.startPos = args.Start.To2D();
                        newWard.endPos = args.End.To2D();

                        wards.Add(newWard);
                    });
                }
            }
        }

        private void Game_OnCreateObj(GameObject sender, EventArgs args)
        {
            /*if (sender.Name.ToLower().Contains("minion")
                || sender.Name.ToLower().Contains("turret"))
            {
                return;
            }

            if (sender.IsValid<MissileClient>())
            {
                var tMissile = sender as MissileClient;
                if (tMissile.SpellCaster.Type != GameObjectType.obj_AI_Hero)
                {
                    return;
                }
            }

            ConsolePrinter.Print(sender.Type + " : " + sender.Name);*/
                        
            if (!menu.Item("TrackEnemyWards").GetValue<bool>())
            {
                return;
            }

            //Visible ward placement
            var obj = sender as Obj_AI_Minion;
            WardData wardData;

            if (obj != null && obj.IsEnemy
                && WardDatabase.wardObjNames.TryGetValue(obj.CharData.BaseSkinName.ToLower(), out wardData))
            {
                var timestamp = HelperUtils.TickCount - (obj.MaxMana - obj.Mana) * 1000;

                WardTrackerInfo newWard = new WardTrackerInfo(
                            wardData,
                            obj.Position,
                            obj,
                            !obj.IsVisible && args == null,
                            timestamp
                            );

                wards.Add(newWard);

                DelayAction.Add(500, () =>
                {
                    if (newWard.wardObject != null && newWard.wardObject.IsValid && !newWard.wardObject.IsDead)
                    {
                        timestamp = HelperUtils.TickCount - (obj.MaxMana - obj.Mana) * 1000;

                        newWard.timestamp = timestamp;

                        foreach (var ward in wards)
                        {
                            if (ward.wardObject == null)
                            {
                                //Check for Process Spell wards
                                if (ward.position.Distance(sender.Position) < 550
                                        && Math.Abs(ward.timestamp - timestamp) < 2000)
                                {
                                    DelayAction.Add(0, () => wards.Remove(ward));
                                    break;
                                }
                            }
                        }
                    }
                });

            }

            //FOW placement
            var missile = sender as MissileClient;

            if (missile != null && missile.SpellCaster.IsEnemy)
            {
                if (missile.SData.Name.ToLower() == "itemplacementmissile")// && !missile.SpellCaster.IsVisible)
                {
                    var dir = (missile.EndPosition.To2D() - missile.StartPosition.To2D()).Normalized();
                    var pos = missile.StartPosition.To2D() + dir * 500;

                    foreach (var ward in wards) //check for visible
                    {
                        if (ward.position.To2D().Distance(pos) < 750
                            && HelperUtils.TickCount - ward.timestamp < 50)
                        {
                            return;
                        }
                    }

                    DelayAction.Add(100, () =>
                    {
                        foreach (var ward in wards) //check for OnProcessSpell
                        {
                            if (ward.position.To2D().Distance(pos) < 750
                                && HelperUtils.TickCount - ward.timestamp < 125)
                            {
                                return;
                            }
                        }

                        WardTrackerInfo newWard = new WardTrackerInfo(
                            WardDatabase.missileWardData,
                            pos.To3D(),
                            null,
                            true
                            );

                        newWard.startPos = missile.StartPosition.To2D();
                        newWard.endPos = missile.EndPosition.To2D();

                        wards.Add(newWard);
                    });
                }
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (!menu.Item("TrackEnemyWards").GetValue<bool>())
            {
                return;
            }

            foreach (var ward in wards)
            {
                var wardPos = ward.wardObject != null ? ward.wardObject.Position : ward.position;

                if (wardPos.IsOnScreen() && ward.endTime > HelperUtils.TickCount)
                {
                    var wardScreenPos = Drawing.WorldToScreen(wardPos);
                    var timeStr = TextUtils.FormatTime((ward.endTime - HelperUtils.TickCount) / 1000f);
                    var tSize = tStrSize;

                    if (ward.unknownDuration)
                    {
                        timeStr = "?? " + timeStr + " ??";
                        tSize = utStrSize;
                    }


                    if (timeStr != null)
                    {
                        TextUtils.DrawText(wardScreenPos.X - wardStrSize.Width / 2, wardScreenPos.Y, Color.White, "Ward");
                        TextUtils.DrawText(wardScreenPos.X - tSize.Width / 2, wardScreenPos.Y + wardStrSize.Height, Color.White, timeStr);
                    }

                }
            }
        }
    }
}
