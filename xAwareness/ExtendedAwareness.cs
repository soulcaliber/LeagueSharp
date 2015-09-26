using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace xAwareness
{
    class PositionInfo
    {
        public Vector2 direction = Vector2.Zero;
        public Vector2 screenPosition = Vector2.Zero;
        public float distance = 0;
        public float screenCollisionDistance = 0;

        public PositionInfo()
        {

        }

        public PositionInfo(Vector2 screenPosition, Vector2 direction, float distance, float screenCollisionDistance)
        {

        }
    }

    class ExtendedAwareness
    {
        public static Menu menu;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static Dictionary<int, HeroTracker> heroTrackers = new Dictionary<int, HeroTracker>();

        public static Vector2 screenOffset = Vector2.Zero;
        public static Vector3 currentRandomScreen = Vector3.Zero;

        public static bool initializedOffset = false;

        public ExtendedAwareness()
        {
            LoadAssembly();
        }

        private void LoadAssembly()
        {
            try
            {
                Utility.DelayAction.Add(250, () =>
                {
                    if (LeagueSharp.Game.Mode == GameMode.Running)
                    {
                        Game_OnGameLoad(new EventArgs());
                    }
                    else
                    {
                        Game.OnStart += Game_OnGameLoad;
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Game_OnGameLoad(EventArgs args)
        {
            //menu = mainMenu;

            Menu positionAwarenessMenu = new Menu("xAwareness", "xAwareness", true);
            positionAwarenessMenu.AddItem(new MenuItem("ShowAllyPosition", "Show Ally").SetValue(false));
            positionAwarenessMenu.AddItem(new MenuItem("ShowEnemyPosition", "Show Enemy").SetValue(true));
            //positionAwarenessMenu.AddItem(new MenuItem("ShowFowPosition", "Show Fow Position").SetValue(true));

            Menu rangeMenu = new Menu("RangeSettings", "RangeSettings");
            rangeMenu.AddItem(new MenuItem("PositionAwarenessRange",
                "Max Awareness Range").SetValue(new Slider(6000, 0, 20000)));
            rangeMenu.AddItem(new MenuItem("InScreenAwarenessRange",
                "InScreen Awareness Range").SetValue(new Slider(0, 0, 250)));
            positionAwarenessMenu.AddSubMenu(rangeMenu);

            Menu iconMenu = new Menu("IconSettings", "IconSettings");
            iconMenu.AddItem(new MenuItem("ShowPathDirection", "Show Path Direction").SetValue(true));
            iconMenu.AddItem(new MenuItem("ShowTeamColor", "Show Team Color").SetValue(true));
            iconMenu.AddItem(new MenuItem("IconOpacity",
                "Icon Opacity").SetValue(new Slider(50, 0, 100)));
            iconMenu.AddItem(new MenuItem("IconScale",
                "Icon Scale").SetValue(new Slider(35, 0, 100)));
            iconMenu.AddItem(new MenuItem("MinIconScale",
                "Min Icon Scale").SetValue(new Slider(40, 0, 100)));

            iconMenu.AddItem(new MenuItem("IconSettingsDescription", "        --Some Options Require Reload--"));
            /*iconMenu.AddItem(new MenuItem("FowIconScale",
                "InScreen Icon Scale").SetValue(new Slider(75, 0, 100)));*/
            positionAwarenessMenu.AddSubMenu(iconMenu);

            //menu.AddSubMenu(positionAwarenessMenu);
            menu = positionAwarenessMenu;
            menu.AddToMainMenu();

            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Game_OnUpdate;
        }

        private void Game_OnUpdate(EventArgs args)
        {
            currentRandomScreen = Drawing.ScreenToWorld(new Vector2(0, 0));
            //Render.Circle.DrawCircle(GetWorldCenter().To3D().SetZ(0), 30, Color.White);
        }

        private void InitializeIcons()
        {
            foreach (var hero in HeroManager.AllHeroes)
            {
                if (!hero.IsMe)
                {
                    HeroTracker tracker = new HeroTracker(hero, ImageLoader.Load(hero.ChampionName));
                    heroTrackers.Add(hero.NetworkId, tracker);
                }
            }
        }

        private Vector2 WorldToScreen(Vector3 position)
        {
            Viewport viewPort = new Viewport(0, 0, Drawing.Width, Drawing.Height);

            Vector3 cursorMatrix;
            Vector3 source = position;
            Matrix view = Drawing.View * Drawing.Projection;


            Drawing.Direct3DDevice.Viewport.Project(ref source, ref view, out cursorMatrix);

            return cursorMatrix.To2D();
        }

        private void InitializeScreenOffset()
        {
            var topLeftWorld = Drawing.ScreenToWorld(new Vector2(500, 500));
            topLeftWorld.Z = 0;

            Vector3 testPosition = topLeftWorld;

            var lastScreenPos = Drawing.WorldToScreen(testPosition);

            testPosition.X += 5;
            testPosition.Y += 5;

            var screenPos = Drawing.WorldToScreen(testPosition);
            var screenSign = new Vector2(Math.Sign(screenPos.X - lastScreenPos.X),
                                        Math.Sign(screenPos.Y - lastScreenPos.Y));

            for (int i = 0; i < Math.Max(Drawing.Width, Drawing.Height) / 5; i++)
            {
                testPosition.X += screenSign.X * Math.Sign((Drawing.Width / 2) - screenPos.X) * 5;
                testPosition.Y += screenSign.Y * Math.Sign((Drawing.Height / 2) - screenPos.Y) * 5;

                lastScreenPos = screenPos;
                screenPos = Drawing.WorldToScreen(testPosition);
            }

            screenOffset = new Vector2(testPosition.X - topLeftWorld.X, testPosition.Y - topLeftWorld.Y);
        }

        public static Vector2 GetWorldCenter()
        {
            var topLeftWorld = currentRandomScreen;

            return topLeftWorld.To2D() + screenOffset;
        }
                
        public static PositionInfo GetScreenPosition(Vector3 position3D)
        {
            var position = position3D.To2D();

            var worldCenter = GetWorldCenter();
            var screenCenter = Drawing.WorldToScreen(worldCenter.To3D().SetZ(0));

            if (position3D.IsOnScreen())
            {
                var screenPosition = Drawing.WorldToScreen(position3D);

                return new PositionInfo
                {
                screenPosition = screenPosition,
                direction = (screenPosition - screenCenter).Normalized(),
                distance = worldCenter.Distance(position),
                screenCollisionDistance = worldCenter.Distance(position)
                };
            }

            var worldDir = (position - worldCenter).Normalized();
            var worldClosePosition = worldCenter + worldDir * 100;

            var screenClosePosition = Drawing.WorldToScreen(worldClosePosition.To3D().SetZ(0));

            var dir = (screenClosePosition - screenCenter).Normalized();
            var screenFarPosition = screenCenter + dir * (Math.Max(Drawing.Width, Drawing.Height) + 100);

            var ray = new Ray(screenFarPosition.To3D().SetZ(0), -dir.To3D().SetZ(0));

            var boundingBox = new BoundingBox(new Vector3(0, 0, -1),
                new Vector3(Drawing.Width, Drawing.Height, 1));

            float dist;
            var hasIntersection = ray.Intersects(ref boundingBox, out dist);

            if (hasIntersection)
            {
                var rayDirection = dir;
                var distance = worldCenter.Distance(position);
                var finalScreenPos = screenFarPosition - dir * (dist);

                return new PositionInfo
                {
                    screenPosition = position3D.IsOnScreen() ?
                            Drawing.WorldToScreen(position3D) : finalScreenPos,
                    direction = rayDirection,
                    distance = distance,
                    screenCollisionDistance = dist
                };
            }

            //Console.WriteLine("no intersect");

            return null;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (initializedOffset == false)
            {
                InitializeScreenOffset();
                InitializeIcons();
                initializedOffset = true;
            }
        }
    }
}
