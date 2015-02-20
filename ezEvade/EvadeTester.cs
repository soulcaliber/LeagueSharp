using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace ezEvade
{
    internal class EvadeTester
    {
        private const bool TestingCollision = false;
        public static Menu Menu;
        public static Menu TestMenu;
        private static Vector2 _circleRenderPos;
        private static Vector2 _startWalkPos;
        private static float _startWalkTime;
        private static Vector2 _testCollisionPos;

        public EvadeTester(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnIssueOrder += Game_OnIssueOrder;
            Game.OnGameUpdate += Game_OnGameUpdate;

            Menu = mainMenu;

            TestMenu = new Menu("Test", "Test");
            TestMenu.AddItem(new MenuItem("TestWall", "TestWall").SetValue(true));
            Menu.AddSubMenu(TestMenu);

            Game_OnGameLoad();
        }

        private static float GameTime
        {
            get { return Game.ClockTime*1000; }
        }

        private static Obj_AI_Hero MyHero
        {
            get { return ObjectManager.Player; }
        }

        private static void Game_OnGameLoad()
        {
            Game.PrintChat("EvadeTester loaded");
            Menu.AddSubMenu(new Menu("Test", "Test"));

            Game.PrintChat("Ping:" + Game.Ping);
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (!(_startWalkTime > 0))
            {
                return;
            }

            if (GameTime - _startWalkTime > 500 && MyHero.IsMoving == false)
            {
                //Game.PrintChat("walkspeed: " + startWalkPos.Distance(myHero.ServerPosition.To2D()) / (gameTime - startWalkTime));
                _startWalkTime = 0;
            }
        }

        private static void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
            {
                return;
            }

            if (args.Order == GameObjectOrder.MoveTo)
            {
                if (TestingCollision)
                {
                    if (args.TargetPosition.To2D().Distance(_testCollisionPos) < 3)
                    {
                        //var path = myHero.GetPath();
                        //circleRenderPos

                        args.Process = false;
                    }
                }
            }

            if (args.Order == GameObjectOrder.MoveTo)
            {
                var heroPos = MyHero.ServerPosition.To2D();
                var pos = args.TargetPosition.To2D();
                var speed = MyHero.MoveSpeed;

                _startWalkPos = heroPos;
                _startWalkTime = GameTime;

                foreach (var entry in SpellDetector.Spells)
                {
                    var spell = entry.Value;
                    var walkDir = (pos - heroPos).Normalized();


                    var spellTime = (GameTime - spell.StartTime) - spell.Info.SpellDelay;
                    var spellPos = spell.StartPos + spell.Direction*spell.Info.ProjectileSpeed*(spellTime/1000);
                    //Game.PrintChat("aaaa" + spellTime);


                    bool isCollision;
                    var movingCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir*(speed - 25),
                        spell.Direction*(spell.Info.ProjectileSpeed - 200), MyHero.BoundingRadius,
                        EvadeHelper.GetSpellRadius(spell), out isCollision);
                    if (!isCollision)
                    {
                        continue;
                    }

                    //Game.PrintChat("aaaa" + spellPos.Distance(spell.endPos) / spell.info.projectileSpeed);
                    if (true) //spellPos.Distance(spell.endPos) / spell.info.projectileSpeed > movingCollisionTime)
                    {
                        Game.PrintChat("movingCollisionTime: " + movingCollisionTime);
                        _circleRenderPos = heroPos + walkDir*speed*movingCollisionTime;
                    }
                }
            }
        }

/*
        private void GetPath(Vector2 movePos)
        {
        }
*/

        private static void Drawing_OnDraw(EventArgs args)
        {
            Render.Circle.DrawCircle(
                new Vector3(MyHero.ServerPosition.X, MyHero.ServerPosition.Y, MyHero.ServerPosition.Z),
                MyHero.BoundingRadius, Color.White, 3);
            Render.Circle.DrawCircle(new Vector3(_circleRenderPos.X, _circleRenderPos.Y, MyHero.ServerPosition.Z),
                MyHero.BoundingRadius, Color.Red, 3);

            foreach (var entry in SpellDetector.DrawSpells)
            {
                var spell = entry.Value;
                if (spell.Info.SpellType != SpellType.Line)
                {
                    continue;
                }

                var spellPos = SpellDetector.GetCurrentSpellPosition(spell);

                Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, MyHero.Position.Z), spell.Info.Radius,
                    Color.White, 3);

                /*spellPos = spellPos + spell.direction * spell.info.projectileSpeed * (60 / 1000); //move the spellPos by 50 miliseconds forwards
                    spellPos = spellPos + spell.direction * 200; //move the spellPos by 50 units forwards        

                    Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);*/
            }

            if (TestMenu.Item("TestWall").GetValue<bool>())
            {
                EvadeHelper.GetBestPositionTest();
            }
        }
    }
}