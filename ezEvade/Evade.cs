using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{

    internal class Evade
    {
        public static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static SpellDetector spellDetector;
        private static SpellDrawer spellDrawer;
        private static EvadeTester evadeTester;
        private static PingTester pingTester;
        private static EvadeSpell evadeSpell;

        public static SpellSlot lastSpellCast;
        public static float lastSpellCastTime = 0;

        public static float lastWindupTime = 0;

        public static float lastTickCount = 0;
        public static float lastStopEvadeTime = 0;

        public static Vector3 lastMovementBlockPos = Vector3.Zero;
        public static float lastMovementBlockTime = 0;

        public static DateTime assemblyLoadTime = DateTime.Now;

        public static bool isDodging = false;
        public static bool dodgeOnlyDangerous = false;

        public static bool hasGameEnded = false;
        public static bool isChanneling = false;
        public static Vector2 channelPosition = Vector2.Zero;

        public static EvadeHelper.PositionInfo lastPosInfo;
        public static EvadeCommand lastEvadeCommand = new EvadeCommand { isProcessed = true, timestamp = GetTickCount() };

        public static EvadeCommand lastBlockedUserMoveTo = new EvadeCommand { isProcessed = true, timestamp = GetTickCount() };
        public static float lastDodgingEndTime = 0;

        public static Menu menu;

        public static int CastSpellPacketID = 83;


        public Evade()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private void Game_OnGameLoad(EventArgs args)
        {
            Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
            Spellbook.OnCastSpell += Game_OnCastSpell;
            Game.OnUpdate += Game_OnGameUpdate;
            Game.OnSendPacket += Game_OnSendPacket;
            Game.OnEnd += Game_OnGameEnd;
            SpellDetector.OnProcessDetectedSpells += SpellDetector_OnProcessDetectedSpells;

            Game.PrintChat("<font color=\"#66CCFF\" >Yomie's </font><font color=\"#CCFFFF\" >ezEvade</font> - " +
               "<font color=\"#FFFFFF\" >Version " + Assembly.GetExecutingAssembly().GetName().Version + "</font>");

            menu = new Menu("ezEvade", "ezEvade", true);

            Menu mainMenu = new Menu("Main", "Main");
            mainMenu.AddItem(new MenuItem("DodgeSkillShots", "Dodge SkillShots").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
            mainMenu.AddItem(new MenuItem("UseEvadeSpells", "Use Evade Spells").SetValue(true));
            mainMenu.AddItem(new MenuItem("DodgeDangerous", "Dodge Only Dangerous").SetValue(false));
            mainMenu.AddItem(new MenuItem("DodgeFOWSpells", "Dodge FOW SkillShots").SetValue(true));
            mainMenu.AddItem(new MenuItem("DodgeCircularSpells", "Dodge Circular SkillShots").SetValue(true));
            menu.AddSubMenu(mainMenu);

            //var keyBind = mainMenu.Item("DodgeSkillShots").GetValue<KeyBind>();
            //mainMenu.Item("DodgeSkillShots").SetValue(new KeyBind(keyBind.Key, KeyBindType.Toggle, true));

            spellDetector = new SpellDetector(menu);
            evadeSpell = new EvadeSpell(menu);

            Menu keyMenu = new Menu("Key Settings", "KeySettings");
            keyMenu.AddItem(new MenuItem("DodgeDangerousKeyEnabled", "Enable Dodge Only Dangerous Keys").SetValue(false));
            keyMenu.AddItem(new MenuItem("DodgeDangerousKey", "Dodge Only Dangerous Key").SetValue(new KeyBind(32, KeyBindType.Press)));
            keyMenu.AddItem(new MenuItem("DodgeDangerousKey2", "Dodge Only Dangerous Key 2").SetValue(new KeyBind('V', KeyBindType.Press)));
            menu.AddSubMenu(keyMenu);

            Menu miscMenu = new Menu("Misc Settings", "MiscSettings");
            miscMenu.AddItem(new MenuItem("HigherPrecision", "Enhanced Dodge Precision").SetValue(true));
            miscMenu.AddItem(new MenuItem("RecalculatePosition", "Recalculate Path").SetValue(true));
            miscMenu.AddItem(new MenuItem("ContinueMovement", "Continue Last Movement").SetValue(true));
            miscMenu.AddItem(new MenuItem("CalculateWindupDelay", "Calculate Windup Delay").SetValue(true));
            miscMenu.AddItem(new MenuItem("LoadPingTester", "Load Ping Tester").SetValue(true));
            //miscMenu.AddItem(new MenuItem("CalculateHeroPos", "Calculate Hero Position").SetValue(false));

            Menu limiterMenu = new Menu("Limiter", "Limiter");
            limiterMenu.AddItem(new MenuItem("TickLimiter", "Tick Limiter").SetValue(new Slider(50, 0, 200)));
            miscMenu.AddSubMenu(limiterMenu);

            Menu fastEvadeMenu = new Menu("Fast Evade", "FastEvade");
            fastEvadeMenu.AddItem(new MenuItem("FastEvadeActivationTime", "FastEvade Activation Time").SetValue(new Slider(200, 0, 500)));
            fastEvadeMenu.AddItem(new MenuItem("SpellActivationTime", "Spell Activation Time").SetValue(new Slider(100, 0, 500)));
            fastEvadeMenu.AddItem(new MenuItem("RejectMinDistance", "Collision Distance Buffer").SetValue(new Slider(10, 0, 100)));

            miscMenu.AddSubMenu(fastEvadeMenu);

            /*Menu evadeSpellSettingsMenu = new Menu("Evade Spell", "EvadeSpellMisc");
            evadeSpellSettingsMenu.AddItem(new MenuItem("EvadeSpellActivationTime", "Evade Spell Activation Time").SetValue(new Slider(150, 0, 500)));

            miscMenu.AddSubMenu(evadeSpellSettingsMenu);*/

            Menu bufferMenu = new Menu("Extra Buffers", "ExtraBuffers");
            bufferMenu.AddItem(new MenuItem("ExtraPingBuffer", "Extra Ping Buffer").SetValue(new Slider(65, 0, 200)));
            bufferMenu.AddItem(new MenuItem("ExtraCPADistance", "Extra Collision Distance").SetValue(new Slider(10, 0, 150)));
            bufferMenu.AddItem(new MenuItem("ExtraSpellRadius", "Extra Spell Radius").SetValue(new Slider(0, 0, 100)));
            bufferMenu.AddItem(new MenuItem("ExtraEvadeDistance", "Extra Evade Distance").SetValue(new Slider(100, 0, 200)));
            bufferMenu.AddItem(new MenuItem("ExtraAvoidDistance", "Extra Avoid Distance").SetValue(new Slider(0, 0, 300)));

            bufferMenu.AddItem(new MenuItem("MinComfortZone", "Minimum Comfort Zone").SetValue(new Slider(400, 0, 1000)));

            miscMenu.AddSubMenu(bufferMenu);
            menu.AddSubMenu(miscMenu);

            menu.AddToMainMenu();

            spellDrawer = new SpellDrawer(menu);

            if (menu.Item("LoadPingTester").GetValue<bool>())
            {
                pingTester = new PingTester(menu);
            }

            SetCastSpellPacketID();

            //evadeTester = new EvadeTester(menu);
        }

        public static float GetTickCount()
        {
            return (float)DateTime.Now.Subtract(assemblyLoadTime).TotalMilliseconds; //Game.ClockTime * 1000;
        }

        public static void SetCastSpellPacketID()
        {
            if (Game.Version.StartsWith("5.7"))
            {
                CastSpellPacketID = 233;
            }
            else if (Game.Version.StartsWith("5.6"))
            {
                CastSpellPacketID = 161;
            }
            else if (Game.Version.StartsWith("5.5"))
            {
                CastSpellPacketID = 83;
            }
            else if (Game.Version.StartsWith("5.4"))
            {
                CastSpellPacketID = 228;
            }
        }

        private void Game_OnGameEnd(GameEndEventArgs args)
        {
            hasGameEnded = true;
        }

        private void Game_OnSendPacket(GamePacketEventArgs args)
        {
            if (!Situation.ShouldDodge())
                return;

            // Check if the packet sent is a spell cast

            //Game.PrintChat("" + args.GetPacketId());


            if (args != null && args.GetPacketId() == CastSpellPacketID)
            {
                //Game.PrintChat("" + Game.Version);

                if (isDodging && SpellDetector.spells.Count() > 0)
                {
                    foreach (KeyValuePair<String, SpellData> entry in SpellDetector.windupSpells)
                    {
                        SpellData spellData = entry.Value;

                        if (spellData.spellKey == lastSpellCast) //check if it's a spell that we should block
                        {
                            args.Process = false;
                            return;
                        }
                    }
                }
            }
        }

        private void Game_OnCastSpell(Spellbook spellbook, SpellbookCastSpellEventArgs args)
        {
            if (!spellbook.Owner.IsMe)
                return;

            var sData = spellbook.GetSpell(args.Slot);
            string name;

            if (SpellDetector.channeledSpells.TryGetValue(sData.Name, out name))
            {
                //Evade.isChanneling = true;
                //Evade.channelPosition = myHero.ServerPosition.To2D();
                lastStopEvadeTime = GetTickCount() + Game.Ping + 100;
            }

            if (EvadeSpell.lastSpellEvadeCommand != null && EvadeSpell.lastSpellEvadeCommand.timestamp + Game.Ping + 100 > GetTickCount())
            {
                args.Process = false;
            }

            lastSpellCast = args.Slot;
            lastSpellCastTime = GetTickCount();
        }

        private void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
                return;

            if (!Situation.ShouldDodge())
                return;

            if (args.Order == GameObjectOrder.MoveTo)
            {
                //movement block code goes in here
                if (isDodging && SpellDetector.spells.Count() > 0)
                {
                    CheckHeroInDanger();

                    lastBlockedUserMoveTo = new EvadeCommand
                    {
                        order = EvadeOrderCommand.MoveTo,
                        targetPosition = args.TargetPosition.To2D(),
                        timestamp = GetTickCount(),
                        isProcessed = false,
                    };

                    args.Process = false; //Block the command
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    var extraDelay = Evade.menu.SubMenu("MiscSettings").SubMenu("ExtraBuffers").Item("ExtraPingBuffer").GetValue<Slider>().Value;
                    if (EvadeHelper.CheckMovePath(movePos, Game.Ping + extraDelay))
                    {
                        args.Process = false; //Block the command

                        if (GetTickCount() - lastMovementBlockTime < 250 && lastMovementBlockPos.Distance(args.TargetPosition) < 100)
                        {
                            return;
                        }

                        lastMovementBlockPos = args.TargetPosition;
                        lastMovementBlockTime = GetTickCount();

                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            EvadeCommand.MoveTo(posInfo.position);
                        }
                        return;
                    }
                }
            }
            else //need more logic
            {
                if (isDodging)
                {
                    args.Process = false; //Block the command
                }
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            try
            {
                if (Evade.GetTickCount() < lastTickCount)
                {
                    Game.PrintChat("Time Error");
                    //lastTickCount = Evade.GetTickCount();                   
                }

                CheckHeroInDanger();
                ContinueLastBlockedCommand();

                if (isChanneling && channelPosition.Distance(myHero.ServerPosition.To2D()) > 50)
                {
                    isChanneling = false;
                }

                var limitDelay = Evade.menu.Item("TickLimiter").GetValue<Slider>().Value; //Tick limiter                
                if (Evade.GetTickCount() - lastTickCount > limitDelay
                    && Evade.GetTickCount() > lastStopEvadeTime)
                {
                    DodgeSkillShots(); //walking
                    EvadeSpell.UseEvadeSpell(); //using spells
                    lastTickCount = Evade.GetTickCount();
                }

                CheckDodgeOnlyDangerous();
            }
            catch (Exception e)
            {
                Game.PrintChat(e.StackTrace);
            }
        }

        private void ContinueLastBlockedCommand()
        {
            if (menu.SubMenu("MiscSettings").Item("ContinueMovement").GetValue<bool>())
            {
                if (GetTickCount() - lastDodgingEndTime > 50 && GetTickCount() - lastDodgingEndTime < 500)
                {
                    if (lastBlockedUserMoveTo.isProcessed == false && GetTickCount() - lastBlockedUserMoveTo.timestamp < 500)
                    {
                        //Game.PrintChat("Continue Movement");
                        myHero.IssueOrder(GameObjectOrder.MoveTo, lastBlockedUserMoveTo.targetPosition.To3D());
                        lastBlockedUserMoveTo.isProcessed = true;
                    }
                }
            }
        }

        private void CheckHeroInDanger()
        {
            bool playerInDanger = false;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (lastPosInfo != null && lastPosInfo.dodgeableSpells.Contains(spell.spellID) &&
                    EvadeHelper.InSkillShot(spell, myHero.ServerPosition.To2D(), myHero.BoundingRadius))
                {
                    playerInDanger = true;
                    break;
                }
            }

            if (isDodging && !playerInDanger)
            {
                lastDodgingEndTime = GetTickCount();
            }

            if (isDodging == false && !Situation.ShouldDodge())
                return;

            isDodging = playerInDanger;
        }

        private void DodgeSkillShots()
        {
            if (!Situation.ShouldDodge())
            {
                isDodging = false;
                return;
            }

            /*
            if (isDodging && playerInDanger == false) //serverpos test
            {
                myHero.IssueOrder(GameObjectOrder.HoldPosition, myHero, false);
            }*/

            if (isDodging)
            {
                if (lastPosInfo != null)
                {
                    /*foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
                    {
                        Spell spell = entry.Value;

                        Game.PrintChat("" + (int)(GetTickCount()-spell.startTime));
                    }*/


                    Vector2 lastBestPosition = lastPosInfo.position;

                    if (menu.SubMenu("MiscSettings").Item("RecalculatePosition").GetValue<bool>())//recheck path
                    {
                        var path = myHero.Path;
                        if (path.Length > 0)
                        {
                            var movePos = path[path.Length - 1].To2D();

                            if (movePos.Distance(lastPosInfo.position) < 5) //more strict checking
                            {
                                var posInfo = EvadeHelper.CanHeroWalkToPos(movePos, myHero.MoveSpeed, 0, 0, false);
                                if (EvadeHelper.isSamePosInfo(posInfo, lastPosInfo) && posInfo.posDangerCount > lastPosInfo.posDangerCount)
                                {
                                    var newPosInfo = EvadeHelper.GetBestPosition();
                                    if (newPosInfo.posDangerCount < posInfo.posDangerCount)
                                    {
                                        lastPosInfo = newPosInfo;
                                        CheckHeroInDanger();
                                    }
                                    else if (EvadeSpell.PreferEvadeSpell())
                                    {
                                        lastPosInfo = EvadeHelper.SetAllUndodgeable();
                                        EvadeSpell.UseEvadeSpell(); //using spells)
                                    }
                                }
                            }
                        }
                    }

                    EvadeCommand.MoveTo(lastBestPosition);
                }
            }
            else //if not dodging
            {
                //Check if hero will walk into a skillshot
                var path = myHero.Path;
                if (path.Length > 0)
                {
                    var movePos = path[path.Length - 1].To2D();

                    if (EvadeHelper.CheckMovePath(movePos))
                    {
                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            EvadeCommand.MoveTo(posInfo.position);
                        }
                        return;
                    }
                }
            }
        }

        public static bool isDodgeDangerousEnabled()
        {
            if (menu.SubMenu("Main").Item("DodgeDangerous").GetValue<bool>() == true)
            {
                return true;
            }

            if (menu.SubMenu("KeySettings").Item("DodgeDangerousKeyEnabled").GetValue<bool>() == true)
            {
                if (menu.SubMenu("KeySettings").Item("DodgeDangerousKey").GetValue<KeyBind>().Active == true
                || menu.SubMenu("KeySettings").Item("DodgeDangerousKey2").GetValue<KeyBind>().Active == true)
                    return true;
            }

            return false;
        }

        public static void CheckDodgeOnlyDangerous() //Dodge only dangerous event
        {
            bool bDodgeOnlyDangerous = isDodgeDangerousEnabled();

            if (dodgeOnlyDangerous == false && bDodgeOnlyDangerous)
            {
                spellDetector.RemoveNonDangerousSpells();
                dodgeOnlyDangerous = true;
            }
            else
            {
                dodgeOnlyDangerous = bDodgeOnlyDangerous;
            }
        }

        private void SpellDetector_OnProcessDetectedSpells()
        {
            if (EvadeHelper.CheckDangerousPos(myHero.ServerPosition.To2D(), 0))
            {
                if (Evade.menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<KeyBind>().Active == false
                    || EvadeSpell.PreferEvadeSpell())
                {
                    lastPosInfo = EvadeHelper.SetAllUndodgeable();
                    EvadeSpell.UseEvadeSpell(); //using spells
                }
                else
                {
                    var posInfo = EvadeHelper.GetBestPosition();

                    /*if (EvadeHelper.GetHighestDetectedSpellID() > EvadeHelper.GetHighestSpellID(posInfo))
                    {
                        return;
                    }*/

                    if (lastPosInfo != null && posInfo != null && lastPosInfo.posDangerCount < posInfo.posDangerCount)
                    {
                        return;
                    }

                    lastPosInfo = posInfo;

                    CheckHeroInDanger();
                    DodgeSkillShots(); //walking
                    EvadeSpell.UseEvadeSpell(); //using spells
                }
            }
            else
            {
                lastPosInfo = EvadeHelper.SetAllDodgeable();
            }


            //Game.PrintChat("SkillsDodged: " + lastPosInfo.dodgeableSpells.Count + " DangerLevel: " + lastPosInfo.undodgeableSpells.Count);            
        }
    }
}
