using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace xAwareness
{
    class HeroTracker
    {
        private Obj_AI_Hero hero;
        private Vector3 lastLocation;
        private PositionInfo lastPosInfo = new PositionInfo();
        private Vector2 screenPosition;
        private Vector2 imgScale;
        private float imageWidth;
        private TeleportInfo teleportInfo;
        private float leaveVisiblityTime = 0;
        private Vector2 leaveVisiblityPosition = Vector2.Zero;
        private bool leaveVisiblityOnScreen = true;
        private float renderFOWTime = 0;
        private Vector2 pathPosition = Vector2.Zero;

        public static float layerCount = 0;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public HeroTracker(Obj_AI_Hero hero, Bitmap bmp)
        {
            this.hero = hero;
            teleportInfo = TeleportTracker.teleportInfos[hero.NetworkId];

            InitArrowImage();
            InitIconImage(bmp);                        
            InitCircleImage();

            lastLocation = hero.Position;

            Game.OnUpdate += Game_OnTick;
            Obj_AI_Base.OnLeaveVisiblityClient += Hero_OnLeaveVisibility;
            MissileClient.OnCreate += OnCreate_MissileClient;
            //Obj_AI_Base.OnProcessSpellCast += Hero_OnProcessSpell;
            //Drawing.OnDraw += Game_OnDraw;
        }

        private void InitArrowImage()
        {
            if (ExtendedAwareness.menu.Item("ShowPathDirection").GetValue<bool>() == false)
            {
                return;
            }

            //------------Arrow Image
            var arrowImage = new Render.Sprite(
                hero.IsAlly ? ImageLoader.arrowAllyImg : ImageLoader.arrowEnemyImg,
                new Vector2(0, 0));

            arrowImage.Scale = new Vector2(0.5f, 0.5f);
            arrowImage.VisibleCondition = sender =>
                       visiblecond1();

            arrowImage.PositionUpdate = delegate
            {
                try
                {
                    if (hero.IsMoving && hero.Path.Count() > 0)
                    {
                        var dir = (hero.Path.Last().To2D() - hero.Position.To2D()).Normalized();
                        var angle = (float)(-Math.Atan2(dir.Y, dir.X) - (Math.PI / 180) * 45);

                        arrowImage.Rotation = angle;
                    }

                    arrowImage.Scale = imgScale;
                    return screenPosition + new Vector2(imageWidth / 2, imageWidth / 2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return Vector2.Zero;
            };
            arrowImage.Add(HeroTracker.layerCount+=0.1f);
        }

        private void InitIconImage(Bitmap bmp)
        {
            //-------------------Icon Image-----------------------------//

            var image = new Render.Sprite(bmp, new Vector2(0, 0));
            //image.GrayScale();

            image.Scale = new Vector2(0.5f, 0.5f);
            image.VisibleCondition = sender =>
                    visiblecond2();

            image.PositionUpdate = delegate
            {
                try
                {
                    Vector2 v2 = lastPosInfo.screenPosition;

                    var menuScale = ExtendedAwareness.menu.Item("IconScale").GetValue<Slider>().Value;
                    var minScale = ExtendedAwareness.menu.Item("MinIconScale").GetValue<Slider>().Value;

                    float scale = Math.Max((minScale / 100.0f), 1 - (lastPosInfo.distance - 2000) / 5000);
                    //scale = scale > (maxScale / 100.0f) ? (maxScale / 100.0f) : scale;

                    /*if (!hero.IsVisible)
                    {
                        scale = fowScale / 100.0f;
                    }*/

                    scale = 2 * (menuScale / 100.0f) * scale;

                    image.Scale = new Vector2(scale, scale);

                    v2.X -= image.Width / 2f;
                    v2.Y -= image.Height / 2f;


                    float totalLength = (float)Math.Sqrt(2) * (image.Width / 2f);
                    float extraLength = totalLength - image.Width / 2f;

                    if (v2.X > Drawing.Width / 2)
                    {
                        v2.X = v2.X + image.Width + extraLength > Drawing.Width ?
                            v2.X - ((v2.X + image.Width + extraLength) - Drawing.Width) : v2.X;
                    }
                    else
                    {
                        v2.X = v2.X - extraLength < 0 ? v2.X - v2.X + extraLength : v2.X;
                    }

                    if (v2.Y > Drawing.Height / 2)
                    {
                        v2.Y = v2.Y + image.Height + extraLength > Drawing.Height ?
                            v2.Y - ((v2.Y + image.Height + extraLength) - Drawing.Height) : v2.Y;
                    }
                    else
                    {
                        v2.Y = v2.Y - extraLength < 0 ? v2.Y - v2.Y + extraLength : v2.Y;
                    }


                    imgScale = image.Scale;
                    screenPosition = v2;
                    imageWidth = image.Width;
                    return v2;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return Vector2.Zero;
            };
            image.Add(HeroTracker.layerCount+=0.1f);
        }

        private void InitCircleImage()
        {
            if (ExtendedAwareness.menu.Item("ShowTeamColor").GetValue<bool>() == false)
            {
                return;
            }

            //----------Circle Image
            var circleImage = new Render.Sprite(
                hero.IsAlly ? ImageLoader.circleAllyImg : ImageLoader.circleEnemyImg,
                new Vector2(0, 0));

            circleImage.Scale = new Vector2(0.5f, 0.5f);
            circleImage.VisibleCondition = sender =>
                       visiblecond2();

            circleImage.PositionUpdate = delegate
            {
                try
                {
                    circleImage.Scale = imgScale;
                    return screenPosition;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return Vector2.Zero;
            };
            circleImage.Add(HeroTracker.layerCount+=0.1f);
        }

        private bool visiblecond1()
        {
            try
            {
                return !(hero.IsAlly && ExtendedAwareness.menu.Item("ShowAllyPosition").GetValue<bool>() == false)
                    && !(hero.IsEnemy && ExtendedAwareness.menu.Item("ShowEnemyPosition").GetValue<bool>() == false)
                    && lastPosInfo.distance < ExtendedAwareness.menu.Item("PositionAwarenessRange").GetValue<Slider>().Value
                    && hero.IsMoving
                    && hero.IsVisible && !hero.IsDead
                    && !hero.Position.IsOnScreen();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        private bool visiblecond2()
        {
            try
            {
                return (!(hero.IsAlly && ExtendedAwareness.menu.Item("ShowAllyPosition").GetValue<bool>() == false)
                    && !(hero.IsEnemy && ExtendedAwareness.menu.Item("ShowEnemyPosition").GetValue<bool>() == false)
                    && lastPosInfo.distance < ExtendedAwareness.menu.Item("PositionAwarenessRange").GetValue<Slider>().Value
                    && !hero.IsDead
                    && ShouldShowIconFow())
                    || (teleportInfo.isTeleporting && teleportInfo.position != Vector3.Zero);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        private void Game_OnDraw(EventArgs args)
        {
            var path = WaypointTracker.paths[hero.NetworkId];
            var lastPos = hero.ServerPosition.To2D();

            foreach (var pos in path)
            {
                Drawing.DrawLine(Drawing.WorldToScreen(lastPos.To3D()),
                    Drawing.WorldToScreen(pos.To3D()), 3, Color.Red);
                lastPos = pos;
            }
        }

        private void Hero_OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (hero.IsVisible)
            {
                return;
            }

            if (args.Target != null && args.Target.NetworkId == hero.NetworkId)
            {
                renderFOWTime = HelperUtils.TickCount + 1000;
                leaveVisiblityPosition = args.End.To2D();
                return;
            }
        }

        private void OnCreate_MissileClient(GameObject sender, EventArgs args)
        {
            if (sender == null || hero.IsVisible)
            {
                return;
            }

            var missile = sender as MissileClient;
            if (missile != null)
            {
                if (missile.SpellCaster != null && missile.SpellCaster.NetworkId == hero.NetworkId)
                {
                    if (!leaveVisiblityOnScreen)
                    {
                        renderFOWTime = HelperUtils.TickCount + 1000;
                        leaveVisiblityPosition = hero.ServerPosition.To2D();
                        return;
                    }
                }

                /*if (missile.Target != null && missile.Target.NetworkId == hero.NetworkId)
                {
                    renderFOWTime = HelperUtils.TickCount + 1000;
                    leaveVisiblityPosition = missile.EndPosition.To2D();
                    return;
                }*/
            }
        }

        private void Hero_OnLeaveVisibility(AttackableUnit sender, EventArgs args)
        {
            if (sender.NetworkId == hero.NetworkId)
            {
                leaveVisiblityTime = HelperUtils.TickCount;
                leaveVisiblityPosition = hero.Position.To2D();
                leaveVisiblityOnScreen = hero.Position.IsOnScreen();
            }
        }

        private void Game_OnTick(EventArgs args)
        {
            var position = hero.Position;

            if (teleportInfo.isTeleporting && teleportInfo.position != Vector3.Zero)
            {
                position = teleportInfo.position;
            }
            else if (!hero.IsVisible)
            {
                pathPosition = WaypointTracker.GetHeroCurrentPosition(hero);
                if (pathPosition != Vector2.Zero)
                {
                    position = pathPosition.To3D().SetZ(hero.ServerPosition.Z);
                }
                else
                {
                    position = hero.ServerPosition;
                }
            }

            var posInfo = ExtendedAwareness.GetScreenPosition(position);
            if (posInfo != null)
            {
                lastPosInfo = posInfo;
            }

            /*if (!hero.IsVisible && hero.ServerPosition.To2D().Distance(leaveVisiblityPosition) > 5)
            {
                renderFOWTime = HelperUtils.TickCount + 1000;
                leaveVisiblityPosition = hero.ServerPosition.To2D();
            }*/
        }

        private bool ShouldShowIconFow()
        {
            return renderFOWTime - HelperUtils.TickCount > 0
                || (hero.IsVisible && !IsOnScreen(hero.Position))
                || (!hero.IsVisible && pathPosition != Vector2.Zero && !leaveVisiblityOnScreen);
        }

        public bool IsOnScreen(Vector3 position)
        {
            float extraPadding = ExtendedAwareness.menu.Item("InScreenAwarenessRange").GetValue<Slider>().Value;
            var pos = Drawing.WorldToScreen(position);
            return pos.X > 0 + extraPadding && pos.X <= Drawing.Width - extraPadding
                && pos.Y > 0 + extraPadding && pos.Y <= Drawing.Height - extraPadding;
        }
    }
}
