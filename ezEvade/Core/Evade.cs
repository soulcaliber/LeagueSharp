using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

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

        private static Stopwatch stopWatch = Stopwatch.StartNew();

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

        public static PositionInfo lastPosInfo;
        public static EvadeCommand lastEvadeCommand = new EvadeCommand { isProcessed = true, timestamp = TickCount };

        public static EvadeCommand lastBlockedUserMoveTo = new EvadeCommand { isProcessed = true, timestamp = TickCount };
        public static float lastDodgingEndTime = 0;

        public static Menu menu;

        public static int CastSpellPacketID = 83;

        public static float sumCalculationTime = 0;
        public static float numCalculationTime = 0;
        public static float avgCalculationTime = 0;


        public Evade()
        {
            LoadAssembly();
        }

        private void LoadAssembly()
        {
            DelayAction.Add(1, () =>
            {
                if (LeagueSharp.Game.Mode == GameMode.Running)
                {
                    DelayAction.Add(350, () => { Game_OnGameLoad(new EventArgs()); });
                }
                else
                {
                    Game.OnStart += Game_OnGameLoad;
                }
            });
        }

        private void Game_OnGameLoad(EventArgs args)
        {
            try
            {
                Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
                Spellbook.OnCastSpell += Game_OnCastSpell;
                Game.OnUpdate += Game_OnGameUpdate;
                //Game.OnSendPacket += Game_OnSendPacket;
                Game.OnEnd += Game_OnGameEnd;
                SpellDetector.OnProcessDetectedSpells += SpellDetector_OnProcessDetectedSpells;
                Orbwalking.BeforeAttack += Orbwalking_BeforeAttack;

                /*Console.WriteLine("<font color=\"#66CCFF\" >Yomie's </font><font color=\"#CCFFFF\" >ezEvade</font> - " +
                   "<font color=\"#FFFFFF\" >Version " + Assembly.GetExecutingAssembly().GetName().Version + "</font>");
                */

                menu = new Menu("ezEvade", "ezEvade", true);

                Menu mainMenu = new Menu("Main", "Main");
                mainMenu.AddItem(new MenuItem("DodgeSkillShots", "Dodge SkillShots").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
                mainMenu.AddItem(new MenuItem("ActivateEvadeSpells", "Use Evade Spells").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
                mainMenu.AddItem(new MenuItem("DodgeDangerous", "Dodge Only Dangerous").SetValue(false));
                mainMenu.AddItem(new MenuItem("DodgeFOWSpells", "Dodge FOW SkillShots").SetValue(true));
                mainMenu.AddItem(new MenuItem("DodgeCircularSpells", "Dodge Circular SkillShots").SetValue(true));
                //mainMenu.AddItem(new MenuItem("FastestEvadeMode", "Fastest Evade Mode").SetValue(false));
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
                miscMenu.AddItem(new MenuItem("HigherPrecision", "Enhanced Dodge Precision").SetValue(false));
                miscMenu.AddItem(new MenuItem("RecalculatePosition", "Recalculate Path").SetValue(false));
                miscMenu.AddItem(new MenuItem("ContinueMovement", "Continue Last Movement").SetValue(false));
                miscMenu.AddItem(new MenuItem("CalculateWindupDelay", "Calculate Windup Delay").SetValue(true));
                miscMenu.AddItem(new MenuItem("CheckSpellCollision", "Check Spell Collision").SetValue(false));
                miscMenu.AddItem(new MenuItem("PreventDodgingUnderTower", "Prevent Dodging Under Tower").SetValue(false));
                miscMenu.AddItem(new MenuItem("PreventDodgingNearEnemy", "Prevent Dodging Near Enemies").SetValue(true));
                miscMenu.AddItem(new MenuItem("AdvancedSpellDetection", "Advanced Spell Detection").SetValue(false));
                //miscMenu.AddItem(new MenuItem("AllowCrossing", "Allow Crossing").SetValue(false));
                miscMenu.AddItem(new MenuItem("LoadPingTester", "Load Ping Tester").SetValue(true));
                //miscMenu.AddItem(new MenuItem("CalculateHeroPos", "Calculate Hero Position").SetValue(false));

                Menu evadeModeMenu = new Menu("Mode", "EvadeModeSettings");
                evadeModeMenu.AddItem(new MenuItem("EvadeMode", "Evade Mode")
                    .SetValue(new StringList(new[] { "Smooth", "Fastest" }, 0)));
                miscMenu.AddSubMenu(evadeModeMenu);

                Menu limiterMenu = new Menu("Humanizer", "Limiter");
                limiterMenu.AddItem(new MenuItem("TickLimiter", "Tick Limiter").SetValue(new Slider(50, 0, 200)));
                limiterMenu.AddItem(new MenuItem("SpellDetectionTime", "Spell Detection Time").SetValue(new Slider(0, 0, 1000)));
                limiterMenu.AddItem(new MenuItem("ReactionTime", "Reaction Time").SetValue(new Slider(0, 0, 1000)));
                limiterMenu.AddItem(new MenuItem("DodgeInterval", "Dodge Interval").SetValue(new Slider(0, 0, 2000)));
                miscMenu.AddSubMenu(limiterMenu);                

                Menu fastEvadeMenu = new Menu("Fast Evade", "FastEvade");
                fastEvadeMenu.AddItem(new MenuItem("FastEvadeActivationTime", "FastEvade Activation Time").SetValue(new Slider(65, 0, 500)));
                fastEvadeMenu.AddItem(new MenuItem("SpellActivationTime", "Spell Activation Time").SetValue(new Slider(200, 0, 1000)));
                fastEvadeMenu.AddItem(new MenuItem("RejectMinDistance", "Collision Distance Buffer").SetValue(new Slider(10, 0, 100)));

                miscMenu.AddSubMenu(fastEvadeMenu);

                /*Menu evadeSpellSettingsMenu = new Menu("Evade Spell", "EvadeSpellMisc");
                evadeSpellSettingsMenu.AddItem(new MenuItem("EvadeSpellActivationTime", "Evade Spell Activation Time").SetValue(new Slider(150, 0, 500)));

                miscMenu.AddSubMenu(evadeSpellSettingsMenu);*/

                Menu bufferMenu = new Menu("Extra Buffers", "ExtraBuffers");
                bufferMenu.AddItem(new MenuItem("ExtraPingBuffer", "Extra Ping Buffer").SetValue(new Slider(65, 0, 200)));
                bufferMenu.AddItem(new MenuItem("ExtraCPADistance", "Extra Collision Distance").SetValue(new Slider(10, 0, 150)));
                bufferMenu.AddItem(new MenuItem("ExtraSpellRadius", "Extra Spell Radius").SetValue(new Slider(0, 0, 100)));
                bufferMenu.AddItem(new MenuItem("ExtraEvadeDistance", "Extra Evade Distance").SetValue(new Slider(100, 0, 300)));
                bufferMenu.AddItem(new MenuItem("ExtraAvoidDistance", "Extra Avoid Distance").SetValue(new Slider(0, 0, 300)));

                bufferMenu.AddItem(new MenuItem("MinComfortZone", "Min Distance to Champion").SetValue(new Slider(400, 0, 1000)));

                miscMenu.AddSubMenu(bufferMenu);

                Menu resetMenu = new Menu("Reset Config", "ResetConfig");
                resetMenu.AddItem(new MenuItem("ResetConfig", "Reset Config").SetValue(false));
                resetMenu.AddItem(new MenuItem("ResetConfig197", "Set Patch Config").SetValue(true));

                miscMenu.AddSubMenu(resetMenu);

                menu.AddSubMenu(miscMenu);
                menu.AddToMainMenu();

                spellDrawer = new SpellDrawer(menu);

                if (menu.Item("LoadPingTester").GetValue<bool>())
                {
                    pingTester = new PingTester(menu);
                }

                var initCache = ObjectCache.myHeroCache;

                //evadeTester = new EvadeTester(menu);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static float TickCount
        {
            get
            {
                return stopWatch.ElapsedMilliseconds;
            }
        }

        public static void ResetConfig()
        {
            menu.Item("DodgeSkillShots").SetValue(new KeyBind('K', KeyBindType.Toggle, true));
            menu.Item("ActivateEvadeSpells").SetValue(new KeyBind('K', KeyBindType.Toggle, true));
            menu.Item("DodgeDangerous").SetValue(false);
            menu.Item("DodgeFOWSpells").SetValue(true);
            menu.Item("DodgeCircularSpells").SetValue(true);

            menu.Item("DodgeDangerousKeyEnabled").SetValue(false);
            menu.Item("DodgeDangerousKey").SetValue(new KeyBind(32, KeyBindType.Press));
            menu.Item("DodgeDangerousKey2").SetValue(new KeyBind('V', KeyBindType.Press));

            menu.Item("HigherPrecision").SetValue(false);
            menu.Item("RecalculatePosition").SetValue(false);
            menu.Item("ContinueMovement").SetValue(false);
            menu.Item("CalculateWindupDelay").SetValue(true);
            menu.Item("CheckSpellCollision").SetValue(false);
            menu.Item("PreventDodgingUnderTower").SetValue(false);
            menu.Item("PreventDodgingNearEnemy").SetValue(true);
            menu.Item("AdvancedSpellDetection").SetValue(false);
            menu.Item("LoadPingTester").SetValue(true);

            menu.Item("EvadeMode").SetValue(new StringList(new[] { "Smooth", "Fast" }, 0));

            menu.Item("TickLimiter").SetValue(new Slider(50, 0, 200));
            menu.Item("SpellDetectionTime").SetValue(new Slider(0, 0, 1000));
            menu.Item("ReactionTime").SetValue(new Slider(0, 0, 1000));
            menu.Item("DodgeInterval").SetValue(new Slider(0, 0, 2000));

            menu.Item("FastEvadeActivationTime").SetValue(new Slider(65, 0, 500));
            menu.Item("SpellActivationTime").SetValue(new Slider(200, 0, 1000));
            menu.Item("RejectMinDistance").SetValue(new Slider(10, 0, 100));

            menu.Item("ExtraPingBuffer").SetValue(new Slider(65, 0, 200));
            menu.Item("ExtraCPADistance").SetValue(new Slider(10, 0, 150));
            menu.Item("ExtraSpellRadius").SetValue(new Slider(0, 0, 100));
            menu.Item("ExtraEvadeDistance").SetValue(new Slider(100, 0, 300));
            menu.Item("ExtraAvoidDistance").SetValue(new Slider(0, 0, 300));
            menu.Item("MinComfortZone").SetValue(new Slider(400, 0, 1000));
        }

        public static void SetPatchConfig()
        {
            menu.Item("ReactionTime").SetValue(new Slider(0, 0, 1000));
            menu.Item("ExtraAvoidDistance").SetValue(new Slider(0, 0, 300));
        }

        private void Game_OnGameEnd(GameEndEventArgs args)
        {
            hasGameEnded = true;
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
                //Evade.channelPosition = ObjectCache.myHeroCache.serverPos2D;
                lastStopEvadeTime = TickCount + ObjectCache.gamePing + 100;
            }

            if (EvadeSpell.lastSpellEvadeCommand != null && EvadeSpell.lastSpellEvadeCommand.timestamp + ObjectCache.gamePing + 150 > TickCount)
            {
                args.Process = false;
            }
            
            lastSpellCast = args.Slot;
            lastSpellCastTime = TickCount;

            //moved from processPacket

            if (Situation.ShouldDodge())
            {
                if (isDodging && SpellDetector.spells.Count() > 0)
                {
                    foreach (KeyValuePair<String, SpellData> entry in SpellDetector.windupSpells)
                    {
                        SpellData spellData = entry.Value;

                        if (spellData.spellKey == args.Slot) //check if it's a spell that we should block
                        {
                            args.Process = false;
                            return;
                        }
                    }

                    foreach (var evadeSpell in EvadeSpell.evadeSpells)
                    {
                        if (evadeSpell.isItem == false && evadeSpell.spellKey == args.Slot)
                        {
                            lastPosInfo = PositionInfo.SetAllUndodgeable();
                            return;
                        }
                    }
                }
            }
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
                        timestamp = TickCount,
                        isProcessed = false,
                    };

                    args.Process = false; //Block the command
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    var extraDelay = ObjectCache.menuCache.cache["ExtraPingBuffer"].GetValue<Slider>().Value;
                    if (EvadeHelper.CheckMovePath(movePos, ObjectCache.gamePing + extraDelay))
                    {
                        lastBlockedUserMoveTo = new EvadeCommand
                        {
                            order = EvadeOrderCommand.MoveTo,
                            targetPosition = args.TargetPosition.To2D(),
                            timestamp = TickCount,
                            isProcessed = false,
                        };

                        args.Process = false; //Block the command

                        if (TickCount - lastMovementBlockTime < 250 && lastMovementBlockPos.Distance(args.TargetPosition) < 100)
                        {
                            return;
                        }

                        lastMovementBlockPos = args.TargetPosition;
                        lastMovementBlockTime = TickCount;

                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            EvadeCommand.MoveTo(posInfo.position);                            
                        }
                        return;
                    }
                    else
                    {
                        lastBlockedUserMoveTo.isProcessed = true;
                    }
                }
            }
            else //need more logic
            {
                if (isDodging)
                {
                    args.Process = false; //Block the command
                }
                else
                {
                    if (args.Order == GameObjectOrder.AttackUnit)
                    {
                        var target = args.Target;
                        if (target != null && target.IsValid<Obj_AI_Base>())
                        {
                            var baseTarget = target as Obj_AI_Base;
                            if (ObjectCache.myHeroCache.serverPos2D.Distance(baseTarget.ServerPosition.To2D()) >
                                myHero.AttackRange + ObjectCache.myHeroCache.boundingRadius + baseTarget.BoundingRadius)
                            {
                                var movePos = args.TargetPosition.To2D();
                                var extraDelay = ObjectCache.menuCache.cache["ExtraPingBuffer"].GetValue<Slider>().Value;
                                if (EvadeHelper.CheckMovePath(movePos, ObjectCache.gamePing + extraDelay))
                                {
                                    args.Process = false; //Block the command
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Orbwalking_BeforeAttack(Orbwalking.BeforeAttackEventArgs args)
        {
            if (isDodging)
            {
                args.Process = false; //Block orbwalking
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            try
            {
                CheckHeroInDanger();

                if (isChanneling && channelPosition.Distance(ObjectCache.myHeroCache.serverPos2D) > 50)
                {
                    isChanneling = false;
                }

                if (ObjectCache.menuCache.cache["ResetConfig"].GetValue<bool>())
                {
                    ResetConfig();
                    menu.Item("ResetConfig").SetValue(false);
                }

                if (ObjectCache.menuCache.cache["ResetConfig197"].GetValue<bool>())
                {
                    SetPatchConfig();
                    menu.Item("ResetConfig197").SetValue(false);
                }

                var limitDelay = ObjectCache.menuCache.cache["TickLimiter"].GetValue<Slider>().Value; //Tick limiter                
                if (Evade.TickCount - lastTickCount > limitDelay
                    && Evade.TickCount > lastStopEvadeTime)
                {
                    DodgeSkillShots(); //walking
                    EvadeSpell.UseEvadeSpell(); //using spells
                    ContinueLastBlockedCommand();
                    lastTickCount = Evade.TickCount;
                }

                CheckDodgeOnlyDangerous();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ContinueLastBlockedCommand()
        {
            if (ObjectCache.menuCache.cache["ContinueMovement"].GetValue<bool>())
            {
                var movePos = lastBlockedUserMoveTo.targetPosition;
                var extraDelay = ObjectCache.menuCache.cache["ExtraPingBuffer"].GetValue<Slider>().Value;

                if (isDodging == false && lastBlockedUserMoveTo.isProcessed == false
                    && TickCount - lastEvadeCommand.timestamp > ObjectCache.gamePing + extraDelay
                    && TickCount - lastBlockedUserMoveTo.timestamp < 1500)
                {                    
                    if (!EvadeHelper.CheckMovePath(movePos, ObjectCache.gamePing + extraDelay))
                    {
                        //Console.WriteLine("Continue Movement");
                        //myHero.IssueOrder(GameObjectOrder.MoveTo, movePos.To3D());
                        EvadeCommand.MoveTo(movePos);
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
                    ObjectCache.myHeroCache.serverPos2D.InSkillShot(spell, ObjectCache.myHeroCache.boundingRadius))
                {
                    playerInDanger = true;
                    break;
                }
            }

            if (isDodging && !playerInDanger)
            {
                lastDodgingEndTime = TickCount;
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

                        Console.WriteLine("" + (int)(TickCount-spell.startTime));
                    }*/


                    Vector2 lastBestPosition = lastPosInfo.position;

                    if (ObjectCache.menuCache.cache["RecalculatePosition"].GetValue<bool>())//recheck path
                    {
                        var dodgeInterval = ObjectCache.menuCache.cache["DodgeInterval"].GetValue<Slider>().Value;
                        if (lastPosInfo != null && !lastPosInfo.recalculatedPath &&
                            dodgeInterval <= TickCount - lastPosInfo.timestamp)
                        {
                            var path = myHero.Path;
                            if (path.Length > 0)
                            {
                                var movePos = path[path.Length - 1].To2D();

                                if (movePos.Distance(lastPosInfo.position) < 5) //more strict checking
                                {
                                    var posInfo = EvadeHelper.CanHeroWalkToPos(movePos, ObjectCache.myHeroCache.moveSpeed, 0, 0, false);
                                    if (posInfo.isSamePosInfo(lastPosInfo) &&
                                        posInfo.posDangerCount > lastPosInfo.posDangerCount)
                                    {
                                        var newPosInfo = EvadeHelper.GetBestPosition();
                                        if (newPosInfo.posDangerCount < posInfo.posDangerCount)
                                        {
                                            lastPosInfo = newPosInfo;
                                            CheckHeroInDanger();
                                        }
                                        else if (EvadeSpell.PreferEvadeSpell())
                                        {
                                            lastPosInfo = PositionInfo.SetAllUndodgeable();
                                        }
                                        else
                                        {
                                            lastPosInfo.recalculatedPath = true;
                                        }
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
                //return;
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
            if (ObjectCache.menuCache.cache["DodgeDangerous"].GetValue<bool>() == true)
            {
                return true;
            }

            if (ObjectCache.menuCache.cache["DodgeDangerousKeyEnabled"].GetValue<bool>() == true)
            {
                if (ObjectCache.menuCache.cache["DodgeDangerousKey"].GetValue<KeyBind>().Active == true
                || ObjectCache.menuCache.cache["DodgeDangerousKey2"].GetValue<KeyBind>().Active == true)
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

        public static void SetAllUndodgeable()
        {
            lastPosInfo = PositionInfo.SetAllUndodgeable();
        }

        private void SpellDetector_OnProcessDetectedSpells()
        {
            if (ObjectCache.menuCache.cache["DodgeSkillShots"].GetValue<KeyBind>().Active == false)
            {
                lastPosInfo = PositionInfo.SetAllUndodgeable();
                EvadeSpell.UseEvadeSpell();
                return;
            }

            if (ObjectCache.myHeroCache.serverPos2D.CheckDangerousPos(0))
            {
                if (EvadeSpell.PreferEvadeSpell())
                {
                    lastPosInfo = PositionInfo.SetAllUndodgeable();
                }
                else
                {
                    var calculationTimer = TickCount;

                    var posInfo = EvadeHelper.GetBestPosition();

                    var caculationTime = TickCount - calculationTimer;

                    if (numCalculationTime > 0)
                    {
                        sumCalculationTime += caculationTime;
                        avgCalculationTime = sumCalculationTime / numCalculationTime;
                    }
                    numCalculationTime += 1;

                    //Console.WriteLine("CalculationTime: " + caculationTime);
                    
                    /*if (EvadeHelper.GetHighestDetectedSpellID() > EvadeHelper.GetHighestSpellID(posInfo))
                    {
                        return;
                    }*/
                    if (posInfo != null)
                    {
                        lastPosInfo = posInfo.CompareLastMovePos();
                    }

                    CheckHeroInDanger();
                    DodgeSkillShots(); //walking
                    EvadeSpell.UseEvadeSpell(); //using spells
                }
            }
            else
            {
                lastPosInfo = PositionInfo.SetAllDodgeable();
            }


            //Console.WriteLine("SkillsDodged: " + lastPosInfo.dodgeableSpells.Count + " DangerLevel: " + lastPosInfo.undodgeableSpells.Count);            
        }
    }
}
