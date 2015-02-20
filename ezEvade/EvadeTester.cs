using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class EvadeTester
    {
        public static Menu menu;
        public static Menu testMenu;

        private static float gameTime { get { return Game.ClockTime * 1000; } }
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        private static Vector2 circleRenderPos;

        private static Vector2 startWalkPos;
        private static float startWalkTime = 0;

        private static Vector2 testCollisionPos;
        private static bool testingCollision = false;

        public EvadeTester(Menu mainMenu)
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
            Game.OnGameUpdate += Game_OnGameUpdate;

            menu = mainMenu;

            testMenu = new Menu("Test", "Test");
            testMenu.AddItem(new MenuItem("TestWall", "TestWall").SetValue(true));
            menu.AddSubMenu(testMenu);

            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            Game.PrintChat("EvadeTester loaded");
            menu.AddSubMenu(new Menu("Test", "Test"));

            Game.PrintChat("Ping:" + Game.Ping);
            
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (startWalkTime > 0)
            {
                if (gameTime - startWalkTime > 500 && myHero.IsMoving == false)
                {
                    //Game.PrintChat("walkspeed: " + startWalkPos.Distance(myHero.ServerPosition.To2D()) / (gameTime - startWalkTime));
                    startWalkTime = 0;
                }
            }
        }

        private void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
                return;

            if (args.Order == GameObjectOrder.MoveTo)
            {
                if (testingCollision)
                {
                    if (args.TargetPosition.To2D().Distance(testCollisionPos) < 3)
                    {
                        //var path = myHero.GetPath();
                        //circleRenderPos

                        args.Process = false;
                    }
                }
            }

            if (args.Order == GameObjectOrder.MoveTo)
            {
                Vector2 heroPos = myHero.ServerPosition.To2D();
                Vector2 pos = args.TargetPosition.To2D();
                float speed = myHero.MoveSpeed;

                startWalkPos = heroPos;
                startWalkTime = gameTime;

                foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                {
                    Spell spell = entry.Value;
                    var spellPos = SpellDetector.GetCurrentSpellPosition(spell);
                    var walkDir = (pos - heroPos).Normalized();

                    
                        float spellTime = (gameTime - spell.startTime) - spell.info.spellDelay;
                        spellPos = spell.startPos + spell.direction * spell.info.projectileSpeed * (spellTime / 1000);
                        //Game.PrintChat("aaaa" + spellTime);
                    

                    bool isCollision = false;
                    float movingCollisionTime = MathUtils.GetCollisionTime(heroPos, spellPos, walkDir * (speed - 25), spell.direction * (spell.info.projectileSpeed - 200), myHero.BoundingRadius, EvadeHelper.GetSpellRadius(spell), out isCollision);
                    if (isCollision)
                    {
                        //Game.PrintChat("aaaa" + spellPos.Distance(spell.endPos) / spell.info.projectileSpeed);
                        if (true)//spellPos.Distance(spell.endPos) / spell.info.projectileSpeed > movingCollisionTime)
                        {
                            Game.PrintChat("movingCollisionTime: " + movingCollisionTime);
                            circleRenderPos = heroPos + walkDir * speed * movingCollisionTime;                            
                        }
                            
                    }
                }
            }
        }

        private void GetPath(Vector2 movePos)
        {

        }

        private void Drawing_OnDraw(EventArgs args)
        {
            Render.Circle.DrawCircle(new Vector3(myHero.ServerPosition.X, myHero.ServerPosition.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.White, 3);
            Render.Circle.DrawCircle(new Vector3(circleRenderPos.X, circleRenderPos.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.Red, 3);

            foreach (KeyValuePair<int, Spell> entry in SpellDetector.drawSpells)
            {
                Spell spell = entry.Value;

                if (spell.info.spellType == SpellType.Line)
                {
                    Vector2 spellPos = SpellDetector.GetCurrentSpellPosition(spell);

                    Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);
                                        
                    /*spellPos = spellPos + spell.direction * spell.info.projectileSpeed * (60 / 1000); //move the spellPos by 50 miliseconds forwards
                    spellPos = spellPos + spell.direction * 200; //move the spellPos by 50 units forwards        

                    Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), spell.info.radius, Color.White, 3);*/
                }
                

            }

            if (testMenu.Item("TestWall").GetValue<bool>())
                EvadeHelper.GetBestPositionTest();
        }
    }    
}
