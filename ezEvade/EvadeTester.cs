using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

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

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        private static Vector2 circleRenderPos;

        private static Vector2 startWalkPos;
        private static float startWalkTime = 0;

        private static Vector2 testCollisionPos;
        private static bool testingCollision = false;

        private static IOrderedEnumerable<EvadeHelper.PositionInfo> sortedBestPos;

        private static float lastGameTimerStart = 0;
        private static float lastTickCountTimerStart = 0;
        private static float lastWatchTimerStart = 0;

        private static float lastGameTimerTick = 0;
        private static float lastTickCountTimerTick = 0;
        private static float lastWatchTimerTick = 0;

        private static float getGameTimer { get { return Game.Time * 1000; } }
        private static float getTickCountTimer { get { return (float) DateTime.Now.TimeOfDay.TotalMilliseconds; } }
        private static float getWatchTimer { get { return Evade.GetTickCount(); } }

        private static float lastTimerCheck = 0;

        public EvadeTester(Menu mainMenu)
        {
            lastGameTimerStart = getGameTimer;
            lastTickCountTimerStart = getTickCountTimer;
            lastWatchTimerStart = getWatchTimer;

            lastTimerCheck = getTickCountTimer;

            Drawing.OnDraw += Drawing_OnDraw;
            //Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
            Game.OnGameUpdate += Game_OnGameUpdate;

            Obj_SpellMissile.OnCreate += SpellMissile_OnCreate;
            //Obj_AI_Hero.OnProcessSpellCast += Game_ProcessSpell;

            //GameObject.OnFloatPropertyChange += GameObject_OnFloatPropertyChange;
            //GameObject.OnIntegerPropertyChange += GameObject_OnIntegerPropertyChange;
            //Game.OnGameNotifyEvent += Game_OnGameNotifyEvent;

            SpellDetector.OnCreateSpell += SpellDetector_OnCreateSpell;

            menu = mainMenu;

            testMenu = new Menu("Test", "Test");
            testMenu.AddItem(new MenuItem("TestWall", "TestWall").SetValue(true));
            testMenu.AddItem(new MenuItem("TestPath", "TestPath").SetValue(true));
            testMenu.AddItem(new MenuItem("DrawHeroPos", "DrawHeroPos").SetValue(true));
            menu.AddSubMenu(testMenu);

            Game_OnGameLoad();
        }

        private void Game_OnGameLoad()
        {
            Game.PrintChat("EvadeTester loaded");
            menu.AddSubMenu(new Menu("Test", "Test"));

            Game.PrintChat("Ping:" + Game.Ping);

        }

        private void SpellDetector_OnCreateSpell(Spell newSpell)
        {
            var pos1 = newSpell.startPos;//SpellDetector.GetCurrentSpellPosition(newSpell);
            //Utility.DelayAction.Add(250, () => CompareSpellLocation2(newSpell));
                        
            sortedBestPos = EvadeHelper.GetBestPositionTest();
            circleRenderPos = Evade.lastPosInfo.position;
        }

        private void SpellMissile_OnCreate(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<Obj_SpellMissile>())
                return;

            Obj_SpellMissile missile = (Obj_SpellMissile)obj;
            Game.PrintChat("Offset: " + missile.SData.ParticleStartOffset);
            Game.PrintChat("Missile Speed " + missile.SData.MissileSpeed);
            Game.PrintChat("LineWidth " + missile.SData.LineWidth);
            circleRenderPos = missile.SData.ParticleStartOffset.To2D();
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            Game.PrintChat("" + args.SData.Name);
            circleRenderPos = args.SData.ParticleStartOffset.To2D();
        }

        private void CompareSpellLocation(Spell spell, Vector2 pos, float time)
        {
            var pos2 = SpellDetector.GetCurrentSpellPosition(spell);
            if (spell.spellObject != null)
            {
                Game.PrintChat("Compare: " + (pos2.Distance(pos)) / (Evade.GetTickCount() - time));
            }

        }

        private void CompareSpellLocation2(Spell spell)
        {
            var pos1 = SpellDetector.GetCurrentSpellPosition(spell);
            var timeNow = Evade.GetTickCount();

            if (spell.spellObject != null)
            {
                Game.PrintChat("start distance: " + (spell.startPos.Distance(pos1)));
            }

            Utility.DelayAction.Add(250, () => CompareSpellLocation(spell, pos1, timeNow));
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (startWalkTime > 0)
            {
                if (Evade.GetTickCount() - startWalkTime > 500 && myHero.IsMoving == false)
                {
                    //Game.PrintChat("walkspeed: " + startWalkPos.Distance(myHero.ServerPosition.To2D()) / (Evade.GetTickCount() - startWalkTime));
                    startWalkTime = 0;
                }
            }
        }

        private void Game_OnGameNotifyEvent(GameNotifyEventArgs args)
        {
            Game.PrintChat(""+args.EventId);
        }

        private void GameObject_OnFloatPropertyChange(GameObject obj, GameObjectFloatPropertyChangeEventArgs args)
        {

                if (args.Property != "mExp" && args.Property != "mGold" && args.Property != "mGoldTotal" )
                {
                    Game.PrintChat(args.Property + ": " + args.NewValue);
                }
                    
            
        }

        private void GameObject_OnIntegerPropertyChange(GameObject obj, GameObjectIntegerPropertyChangeEventArgs args)
        {
            if (!obj.IsMe)
            {
                if (args.Property != "mExp" && args.Property != "mGold" && args.Property != "mGoldTotal")
                {
                    Game.PrintChat("Int" + args.Property + ": " + args.NewValue);
                }

            }
        }

        private void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
                return;

            if (args.Order == GameObjectOrder.HoldPosition)
            {
                var path = myHero.Path;
                var heroPoint = myHero.ServerPosition.To2D();


                if (path.Length > 0)
                {
                    var movePos = path[path.Length - 1].To2D();
                    var walkDir = (movePos - heroPoint).Normalized();

                    //circleRenderPos = EvadeHelper.GetRealHeroPos();
                    //heroPoint;// +walkDir * myHero.MoveSpeed * (((float)Game.Ping) / 1000);
                }
            }

            /*
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
            }*/

            if (args.Order == GameObjectOrder.MoveTo)
            {

                Vector2 heroPos = myHero.ServerPosition.To2D();
                Vector2 pos = args.TargetPosition.To2D();
                float speed = myHero.MoveSpeed;

                startWalkPos = heroPos;
                startWalkTime = Evade.GetTickCount();

                foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                {
                    Spell spell = entry.Value;
                    var spellPos = SpellDetector.GetCurrentSpellPosition(spell);
                    var walkDir = (pos - heroPos).Normalized();


                    float spellTime = (Evade.GetTickCount() - spell.startTime) - spell.info.spellDelay;
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
                            //circleRenderPos = heroPos + walkDir * speed * movingCollisionTime;
                        }

                    }
                }
            }
        }

        private void GetPath(Vector2 movePos)
        {

        }

        private void PrintTimers()
        {
            Drawing.DrawText(10, 10, Color.White, "Timer1 Freq: " + (getGameTimer - lastGameTimerTick));
            Drawing.DrawText(10, 30, Color.White, "Timer2 Freq: " + (getTickCountTimer - lastTickCountTimerTick));
            Drawing.DrawText(10, 50, Color.White, "Timer3 Freq: " + ((1000L*1000L*1000L) / Stopwatch.Frequency));//(getWatchTimer - lastWatchTimerTick));

            if (getTickCountTimer - lastTimerCheck > 1000)
            {
                Game.PrintChat("" + ((getGameTimer - lastGameTimerStart) - (getTickCountTimer - lastTickCountTimerStart)));
                lastTimerCheck = getTickCountTimer;
            }

            Drawing.DrawText(10, 70, Color.White, "Timer1 Freq: " + (getGameTimer - lastGameTimerStart));
            Drawing.DrawText(10, 90, Color.White, "Timer2 Freq: " + (getTickCountTimer - lastTickCountTimerStart));
            Drawing.DrawText(10, 100, Color.White, "Timer3 Freq: " + (getWatchTimer - lastWatchTimerStart));



            lastGameTimerTick = getGameTimer;
            lastTickCountTimerTick = getTickCountTimer;
            lastWatchTimerTick = getWatchTimer;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            //PrintTimers();

            //EvadeHelper.GetBestPositionTest();

            var path = myHero.Path;

            if (path.Length > 0)
            {
                var heroPos2 = path[path.Length - 1].To2D();//EvadeHelper.GetRealHeroPos();

                Render.Circle.DrawCircle(new Vector3(heroPos2.X, heroPos2.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.White, 3);
                //Render.Circle.DrawCircle(new Vector3(myHero.ServerPosition.X, myHero.ServerPosition.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.White, 3);
                Render.Circle.DrawCircle(new Vector3(circleRenderPos.X, circleRenderPos.Y, myHero.ServerPosition.Z), 10, Color.Red, 3);
            }
              
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

            if (testMenu.Item("DrawHeroPos").GetValue<bool>())
            {

                Render.Circle.DrawCircle(new Vector3(myHero.ServerPosition.X, myHero.ServerPosition.Y, myHero.ServerPosition.Z), myHero.BoundingRadius, Color.White, 3);
            }

            if (testMenu.Item("TestPath").GetValue<bool>())
            {
                var tPath = myHero.GetPath(Game.CursorPos);
                Vector2 lastPoint = Vector2.Zero;

                foreach (Vector3 point in tPath)
                {
                    var point2D = point.To2D();
                    Render.Circle.DrawCircle(new Vector3(point.X, point.Y, point.Z), myHero.BoundingRadius, Color.Violet, 3);

                    lastPoint = point2D;
                }
            }

            if (testMenu.Item("TestPath").GetValue<bool>())
            {
                var tPath = myHero.GetPath(Game.CursorPos);
                Vector2 lastPoint = Vector2.Zero;

                foreach (Vector3 point in tPath)
                {
                    var point2D = point.To2D();
                    Render.Circle.DrawCircle(new Vector3(point.X, point.Y, point.Z), myHero.BoundingRadius, Color.Violet, 3);

                    lastPoint = point2D;
                }
            }

            if (testMenu.Item("TestWall").GetValue<bool>())
            {
                foreach (var posInfo in sortedBestPos)
                {
                    var posOnScreen = Drawing.WorldToScreen(posInfo.position.To3D());
                    //Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + (int)posInfo.closestDistance);
                    
                    if (!posInfo.rejectPosition)
                    {                       
                        Drawing.DrawText(posOnScreen.X, posOnScreen.Y, Color.Aqua, "" + (int)posInfo.closestDistance);
                    }                    

                    /*if (posInfo.posDangerCount <= 0)
                    {
                        var pos = posInfo.position;
                        Render.Circle.DrawCircle(new Vector3(pos.X, pos.Y, myHero.Position.Z), (float)25, Color.White, 3);
                    }*/

                }
            }

        }
    }
}
