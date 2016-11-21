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
        private static SpellTester spellTester;
        private static AutoSetPing autoSetPing;

        public static SpellSlot lastSpellCast;
        public static float lastSpellCastTime = 0;

        public static float lastWindupTime = 0;

        public static float lastTickCount = 0;
        public static float lastStopEvadeTime = 0;

        public static Vector3 lastMovementBlockPos = Vector3.Zero;
        public static float lastMovementBlockTime = 0;

        public static float lastEvadeOrderTime = 0;
        public static float lastIssueOrderGameTime = 0;
        public static float lastIssueOrderTime = 0;
        public static GameObjectIssueOrderEventArgs lastIssueOrderArgs = null;

        public static Vector2 lastMoveToPosition = Vector2.Zero;
        public static Vector2 lastMoveToServerPos = Vector2.Zero;
        public static Vector2 lastStopPosition = Vector2.Zero;

        public static DateTime assemblyLoadTime = DateTime.Now;

        public static bool isDodging = false;
        public static bool dodgeOnlyDangerous = false;

        public static bool devModeOn = false;
        public static bool hasGameEnded = false;
        public static bool isChanneling = false;
        public static Vector2 channelPosition = Vector2.Zero;

        public static PositionInfo lastPosInfo;

        public static EvadeCommand lastEvadeCommand = new EvadeCommand { isProcessed = true, timestamp = EvadeUtils.TickCount };

        public static EvadeCommand lastBlockedUserMoveTo = new EvadeCommand { isProcessed = true, timestamp = EvadeUtils.TickCount };
        public static float lastDodgingEndTime = 0;

        public static Menu menu;

        public static float sumCalculationTime = 0;
        public static float numCalculationTime = 0;
        public static float avgCalculationTime = 0;

        public Evade()
        {
            LoadAssembly();
        }

        private void LoadAssembly()
        {
            DelayAction.Add(0, () =>
            {
                if (Game.Mode == GameMode.Running)
                {
                    Game_OnGameLoad(new EventArgs());
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
                //devModeOn = true;

                Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
                Spellbook.OnCastSpell += Game_OnCastSpell;
                Game.OnUpdate += Game_OnGameUpdate;

                Obj_AI_Hero.OnProcessSpellCast += Game_OnProcessSpell;

                Game.OnEnd += Game_OnGameEnd;
                SpellDetector.OnProcessDetectedSpells += SpellDetector_OnProcessDetectedSpells;
                Orbwalking.BeforeAttack += Orbwalking_BeforeAttack;


                Game.PrintChat(devModeOn ? "<b>ezEvade: Developer Mode On</b>" : "<b>ezEvade: Loaded!</b>");

                menu = new Menu("ezEvade", "ezEvade", true);

                Menu mainMenu = new Menu("Main", "Main");
                mainMenu.AddItem(new MenuItem("DodgeSkillShots", "Dodge SkillShots").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
                mainMenu.Item("DodgeSkillShots").Permashow();
                mainMenu.AddItem(new MenuItem("ActivateEvadeSpells", "Use Evade Spells").SetValue(new KeyBind('K', KeyBindType.Toggle, true)));
                mainMenu.Item("ActivateEvadeSpells").Permashow();
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
                keyMenu.AddItem(new MenuItem("DodgeOnlyOnComboKeyEnabled", "Enable Dodge Only On Combo Key").SetValue(false));
                keyMenu.AddItem(new MenuItem("DodgeComboKey", "Dodge Only Combo Key").SetValue(new KeyBind(32, KeyBindType.Press)));
                keyMenu.AddItem(new MenuItem("DontDodgeKeyEnabled", "Enable Don't Dodge Key").SetValue(false));
                keyMenu.AddItem(new MenuItem("DontDodgeKey", "Don't Dodge Key").SetValue(new KeyBind('Z', KeyBindType.Press)));
                menu.AddSubMenu(keyMenu);

                Menu miscMenu = new Menu("Misc Settings", "MiscSettings");
                miscMenu.AddItem(new MenuItem("HigherPrecision", "Enhanced Dodge Precision").SetValue(false));
                miscMenu.AddItem(new MenuItem("RecalculatePosition", "Recalculate Path").SetValue(true));
                miscMenu.AddItem(new MenuItem("ContinueMovement", "Continue Last Movement").SetValue(true));
                miscMenu.AddItem(new MenuItem("CalculateWindupDelay", "Calculate Windup Delay").SetValue(true));
                miscMenu.AddItem(new MenuItem("CheckSpellCollision", "Check Spell Collision").SetValue(false));
                miscMenu.AddItem(new MenuItem("PreventDodgingUnderTower", "Prevent Dodging Under Tower").SetValue(false));
                miscMenu.AddItem(new MenuItem("PreventDodgingNearEnemy", "Prevent Dodging Near Enemies").SetValue(true));
                miscMenu.AddItem(new MenuItem("AdvancedSpellDetection", "Advanced Spell Detection").SetValue(false));
                miscMenu.AddItem(new MenuItem("ClickRemove", "Allow Left Click Removal")
                    .SetValue(true).SetTooltip("Left Click to Remove Circular Spells and Globals"));
                //miscMenu.AddItem(new MenuItem("AllowCrossing", "Allow Crossing").SetValue(false));
                //miscMenu.AddItem(new MenuItem("CalculateHeroPos", "Calculate Hero Position").SetValue(false));
                miscMenu.AddItem(new MenuItem("EvadeMode", "Evade Profile")
                    .SetValue(new StringList(new[] {"Smooth", "Very Smooth", "Fastest", "Hawk", "Kurisu", "GuessWho"}, 0)));
                miscMenu.Item("EvadeMode").ValueChanged += OnEvadeModeChange;
                miscMenu.AddItem(new MenuItem("ResetConfig", "Reset Evade Config").SetValue(false));

                Menu limiterMenu = new Menu("Humanizer", "Limiter");
                limiterMenu.AddItem(new MenuItem("ClickOnlyOnce", "Click Only Once").SetValue(true));
                limiterMenu.AddItem(new MenuItem("EnableEvadeDistance", "Extended Evade").SetValue(false));            
                limiterMenu.AddItem(new MenuItem("TickLimiter", "Tick Limiter").SetValue(new Slider(100, 0, 500)));
                limiterMenu.AddItem(new MenuItem("SpellDetectionTime", "Spell Detection Time").SetValue(new Slider(0, 0, 1000)));
                limiterMenu.AddItem(new MenuItem("ReactionTime", "Reaction Time").SetValue(new Slider(0, 0, 500)));
                limiterMenu.AddItem(new MenuItem("DodgeInterval", "Dodge Interval").SetValue(new Slider(0, 0, 2000)));
                                
                miscMenu.AddSubMenu(limiterMenu);

                Menu fastEvadeMenu = new Menu("Fast Evade", "FastEvade");
                fastEvadeMenu.AddItem(new MenuItem("FastMovementBlock", "Fast Movement Block")).SetValue(false);
                fastEvadeMenu.AddItem(new MenuItem("FastEvadeActivationTime", "FastEvade Activation Time").SetValue(new Slider(65, 0, 500)));
                fastEvadeMenu.AddItem(new MenuItem("SpellActivationTime", "Spell Activation Time").SetValue(new Slider(400, 0, 1000)));
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
                bufferMenu.AddItem(new MenuItem("ExtraAvoidDistance", "Extra Avoid Distance").SetValue(new Slider(50, 0, 300)));

                bufferMenu.AddItem(new MenuItem("MinComfortZone", "Min Distance to Champion").SetValue(new Slider(550, 0, 1000)));

                miscMenu.AddSubMenu(bufferMenu);

                Menu loadTestMenu = new Menu("Tests", "LoadTests");

                loadTestMenu.AddItem(new MenuItem("LoadPingTester", "Load Ping Tester").SetValue(false));
                loadTestMenu.AddItem(new MenuItem("LoadSpellTester", "Load Spell Tester").SetValue(false));
                loadTestMenu.Item("LoadPingTester").ValueChanged += OnLoadPingTesterChange;
                loadTestMenu.Item("LoadSpellTester").ValueChanged += OnLoadSpellTesterChange;

                miscMenu.AddSubMenu(loadTestMenu);

                menu.AddSubMenu(miscMenu);
                menu.AddToMainMenu();

                spellDrawer = new SpellDrawer(menu);

                //autoSetPing = new AutoSetPing(menu);

                var initCache = ObjectCache.myHeroCache;

                if (devModeOn)
                {
                    Utility.DelayAction.Add(100, () => loadTestMenu.Item("LoadSpellTester").SetValue(true));
                }

                Console.WriteLine("ezEvade Loaded");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ResetConfig(bool kappa = true)
        {
            menu.Item("DodgeSkillShots").SetValue(new KeyBind('K', KeyBindType.Toggle, true));
            menu.Item("ActivateEvadeSpells").SetValue(new KeyBind('K', KeyBindType.Toggle, true));
            menu.Item("DodgeDangerous").SetValue(false);
            menu.Item("DodgeFOWSpells").SetValue(true);
            menu.Item("DodgeCircularSpells").SetValue(true);

            menu.Item("HigherPrecision").SetValue(false);
            menu.Item("RecalculatePosition").SetValue(true);
            menu.Item("ContinueMovement").SetValue(true);
            menu.Item("CalculateWindupDelay").SetValue(true);
            menu.Item("CheckSpellCollision").SetValue(false);
            menu.Item("PreventDodgingUnderTower").SetValue(false);
            menu.Item("PreventDodgingNearEnemy").SetValue(true);
            menu.Item("AdvancedSpellDetection").SetValue(false);
            menu.Item("LoadPingTester").SetValue(true);

            menu.Item("ClickOnlyOnce").SetValue(true);
            menu.Item("EnableEvadeDistance").SetValue(false);
            menu.Item("TickLimiter").SetValue(new Slider(100, 0, 500));
            menu.Item("SpellDetectionTime").SetValue(new Slider(0, 0, 1000));
            menu.Item("ReactionTime").SetValue(new Slider(0, 0, 500));
            menu.Item("DodgeInterval").SetValue(new Slider(0, 0, 2000));

            menu.Item("FastMovementBlock").SetValue(false);
            menu.Item("FastEvadeActivationTime").SetValue(new Slider(65, 0, 500));
            menu.Item("SpellActivationTime").SetValue(new Slider(400, 0, 1000));
            menu.Item("RejectMinDistance").SetValue(new Slider(10, 0, 100));

            menu.Item("ExtraPingBuffer").SetValue(new Slider(65, 0, 200));
            menu.Item("ExtraCPADistance").SetValue(new Slider(10, 0, 150));
            menu.Item("ExtraSpellRadius").SetValue(new Slider(0, 0, 100));
            menu.Item("ExtraEvadeDistance").SetValue(new Slider(100, 0, 300));
            menu.Item("ExtraAvoidDistance").SetValue(new Slider(50, 0, 300));
            menu.Item("MinComfortZone").SetValue(new Slider(550, 0, 1000));

            // drawings
            menu.Item("DrawSkillShots").SetValue(true);
            menu.Item("ShowStatus").SetValue(true);
            menu.Item("DrawSpellPos").SetValue(false);
            menu.Item("DrawEvadePosition").SetValue(false);

            if (kappa)
            {
                // profiles
                menu.Item("EvadeMode")
                    .SetValue(new StringList(new[] { "Smooth", "Very Smooth", "Fastest", "Hawk", "Kurisu", "GuessWho" }, 0));

                // keys
                menu.Item("DodgeDangerousKeyEnabled").SetValue(false);
                menu.Item("DodgeDangerousKey").SetValue(new KeyBind(32, KeyBindType.Press));
                menu.Item("DodgeDangerousKey2").SetValue(new KeyBind('V', KeyBindType.Press));
                menu.Item("DodgeOnlyOnComboKeyEnabled").SetValue(false);
                menu.Item("DodgeComboKey").SetValue(new KeyBind(32, KeyBindType.Press));
                menu.Item("DontDodgeKeyEnabled").SetValue(false);
                menu.Item("DontDodgeKey").SetValue(new KeyBind('Z', KeyBindType.Press));
            }
        }

        private void OnEvadeModeChange(object sender, OnValueChangeEventArgs e)
        {
            var mode = e.GetNewValue<StringList>().SelectedValue;

            if (mode == "Fastest")
            {
                ResetConfig(false);
                menu.Item("FastMovementBlock").SetValue(true);
                menu.Item("FastEvadeActivationTime").SetValue(new Slider(120, 0, 500));
                menu.Item("RejectMinDistance").SetValue(new Slider(25, 0, 100));
                menu.Item("ExtraCPADistance").SetValue(new Slider(25, 0, 150));
                menu.Item("ExtraPingBuffer").SetValue(new Slider(80, 0, 200));
            }
            else if (mode == "Very Smooth")
            {
                ResetConfig(false);
                menu.Item("FastEvadeActivationTime").SetValue(new Slider(0, 0, 500));
                menu.Item("RejectMinDistance").SetValue(new Slider(0, 0, 100));
                menu.Item("ExtraCPADistance").SetValue(new Slider(0, 0, 150));
                menu.Item("ExtraPingBuffer").SetValue(new Slider(40, 0, 200));
                menu.Item("AdvancedSpellDetection").SetValue(true);
            }
            else if (mode == "Smooth")
            {
                ResetConfig(false);
                menu.Item("FastMovementBlock").SetValue(true);
                menu.Item("FastEvadeActivationTime").SetValue(new Slider(65, 0, 500));
                menu.Item("RejectMinDistance").SetValue(new Slider(10, 0, 100));
                menu.Item("ExtraCPADistance").SetValue(new Slider(10, 0, 150));
                menu.Item("ExtraPingBuffer").SetValue(new Slider(65, 0, 200));
            }

            else if (mode == "Hawk")
            {
                ResetConfig(false);
                menu.Item("DodgeDangerous").SetValue(false);
                menu.Item("DodgeFOWSpells").SetValue(false);
                menu.Item("DodgeCircularSpells").SetValue(false);
                menu.Item("DodgeDangerousKeyEnabled").SetValue(true);
                menu.Item("HigherPrecision").SetValue(true);
                menu.Item("RecalculatePosition").SetValue(true);
                menu.Item("ContinueMovement").SetValue(true);
                menu.Item("CalculateWindupDelay").SetValue(true);
                menu.Item("CheckSpellCollision").SetValue(true);
                menu.Item("PreventDodgingUnderTower").SetValue(false);
                menu.Item("PreventDodgingNearEnemy").SetValue(true);
                menu.Item("AdvancedSpellDetection").SetValue(true);
                menu.Item("ClickOnlyOnce").SetValue(true);
                menu.Item("EnableEvadeDistance").SetValue(true);
                menu.Item("TickLimiter").SetValue(new Slider(200, 0, 500));
                menu.Item("SpellDetectionTime").SetValue(new Slider(375, 0, 1000));
                menu.Item("ReactionTime").SetValue(new Slider(285, 0, 500));
                menu.Item("DodgeInterval").SetValue(new Slider(235, 0, 2000));
                menu.Item("FastEvadeActivationTime").SetValue(new Slider(0, 0, 500));
                menu.Item("SpellActivationTime").SetValue(new Slider(200, 0, 1000));
                menu.Item("RejectMinDistance").SetValue(new Slider(0, 0, 100));
                menu.Item("ExtraPingBuffer").SetValue(new Slider(65, 0, 200));
                menu.Item("ExtraCPADistance").SetValue(new Slider(0, 0, 150));
                menu.Item("ExtraSpellRadius").SetValue(new Slider(0, 0, 100));
                menu.Item("ExtraEvadeDistance").SetValue(new Slider(200, 0, 300));
                menu.Item("ExtraAvoidDistance").SetValue(new Slider(200, 0, 300));
                menu.Item("MinComfortZone").SetValue(new Slider(550, 0, 1000));
            }

            else if (mode == "Kurisu")
            {
                ResetConfig(false);
                menu.Item("DodgeDangerous").SetValue(false);
                menu.Item("DodgeFOWSpells").SetValue(false);
                menu.Item("DodgeCircularSpells").SetValue(true);
                menu.Item("DodgeDangerousKeyEnabled").SetValue(true);
                menu.Item("HigherPrecision").SetValue(false);
                menu.Item("RecalculatePosition").SetValue(true);
                menu.Item("ContinueMovement").SetValue(true);
                menu.Item("CalculateWindupDelay").SetValue(true);
                menu.Item("CheckSpellCollision").SetValue(true);
                menu.Item("FastMovementBlock").SetValue(true);
                menu.Item("PreventDodgingUnderTower").SetValue(true);
                menu.Item("PreventDodgingNearEnemy").SetValue(true);
                menu.Item("AdvancedSpellDetection").SetValue(false);
                menu.Item("ClickOnlyOnce").SetValue(true);
                menu.Item("EnableEvadeDistance").SetValue(false);
                menu.Item("TickLimiter").SetValue(new Slider(100, 0, 500));
                menu.Item("SpellDetectionTime").SetValue(new Slider(0, 0, 1000));
                menu.Item("ReactionTime").SetValue(new Slider(0, 0, 500));
                menu.Item("DodgeInterval").SetValue(new Slider(0, 0, 2000));
                menu.Item("FastEvadeActivationTime").SetValue(new Slider(60, 0, 500));
                menu.Item("SpellActivationTime").SetValue(new Slider(400, 0, 1000));
                menu.Item("RejectMinDistance").SetValue(new Slider(10, 0, 100));
                menu.Item("ExtraPingBuffer").SetValue(new Slider(65, 0, 200));
                menu.Item("ExtraCPADistance").SetValue(new Slider(10, 0, 150));
                menu.Item("ExtraSpellRadius").SetValue(new Slider(0, 0, 100));
                menu.Item("ExtraEvadeDistance").SetValue(new Slider(165, 0, 300));
                menu.Item("ExtraAvoidDistance").SetValue(new Slider(60, 0, 300));
                menu.Item("MinComfortZone").SetValue(new Slider(420, 0, 1000));
            }
            
            else if (mode == "GuessWho")
            {
                ResetConfig(false);
                menu.Item("DodgeDangerousKeyEnabled").SetValue(true);
                menu.Item("DodgeDangerousKey2").SetValue(new KeyBind(109, KeyBindType.Press));
                menu.Item("HigherPrecision").SetValue(true);
                menu.Item("CheckSpellCollision").SetValue(true);
                menu.Item("PreventDodgingUnderTower").SetValue(true);
                menu.Item("ShowStatus").SetValue(false);
                menu.Item("DrawSpellPos").SetValue(true);
            }
        }

        private void OnLoadPingTesterChange(object sender, OnValueChangeEventArgs e)
        {
            e.Process = false;

            if (pingTester == null)
            {
                pingTester = new PingTester();
            }
        }

        private void OnLoadSpellTesterChange(object sender, OnValueChangeEventArgs e)
        {
            e.Process = false;

            if (spellTester == null)
            {
                spellTester = new SpellTester();
            }
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
                lastStopEvadeTime = EvadeUtils.TickCount + ObjectCache.gamePing + 100;
            }

            //block spell commmands if evade spell just used
            if (EvadeSpell.lastSpellEvadeCommand != null && 
                EvadeSpell.lastSpellEvadeCommand.timestamp + ObjectCache.gamePing + 150 > EvadeUtils.TickCount)
            {
                args.Process = false;
            }

            lastSpellCast = args.Slot;
            lastSpellCastTime = EvadeUtils.TickCount;

            //moved from processPacket

            /*if (args.Slot == SpellSlot.Recall)
            {
                lastStopPosition = myHero.ServerPosition.To2D();
            }*/

            if (Situation.ShouldDodge())
            {
                if (isDodging && SpellDetector.spells.Any())
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
                }
            }

            foreach (var evadeSpell in EvadeSpell.evadeSpells)
            {
                if (evadeSpell.isItem == false &&  evadeSpell.spellKey == args.Slot && evadeSpell.untargetable == false)
                {
                    if (evadeSpell.evadeType == EvadeType.Blink)
                    {
                        var dir = (args.StartPosition.To2D() - myHero.ServerPosition.To2D()).Normalized();

                        var end = myHero.ServerPosition.To2D() + dir * myHero.ServerPosition.To2D().Distance(Game.CursorPos.To2D());
                        if (evadeSpell.fixedRange || end.Distance(myHero.ServerPosition.To2D()) > evadeSpell.range)
                        {
                            end = myHero.ServerPosition.To2D() + dir * evadeSpell.range;
                        }

                        var posInfo = EvadeHelper.CanHeroWalkToPos(end, evadeSpell.speed, ObjectCache.gamePing, 0);
                        if (posInfo.posDangerCount < 1)
                        {
                            if (lastPosInfo != null)                              
                                lastPosInfo = posInfo;

                            if (lastPosInfo == null)
                                lastPosInfo = posInfo;

                            if (isDodging || EvadeUtils.TickCount < lastDodgingEndTime + 250)
                            {
                                EvadeCommand.MoveTo(Game.CursorPos.To2D());
                                lastStopEvadeTime = EvadeUtils.TickCount + ObjectCache.gamePing + 100;
                            }
                            return;
                        }
                    }

                    if (evadeSpell.evadeType == EvadeType.Dash)
                    {
                        var dashPos = args.StartPosition.To2D(); 

                        if (args.Target != null)
                        {
                            dashPos = args.Target.Position.To2D();
                        }

                        if (evadeSpell.fixedRange || dashPos.Distance(myHero.ServerPosition.To2D()) > evadeSpell.range)
                        {
                            var dir = (dashPos - myHero.ServerPosition.To2D()).Normalized();
                            dashPos = myHero.ServerPosition.To2D() + dir * evadeSpell.range;
                        }

                        var posInfo = EvadeHelper.CanHeroWalkToPos(dashPos, evadeSpell.speed, ObjectCache.gamePing, 0);
                        if (posInfo.posDangerLevel > 0)
                        {
                            args.Process = false;
                        }
                        else
                        {
                            if (isDodging || EvadeUtils.TickCount < lastDodgingEndTime + 500)
                            {
                                EvadeCommand.MoveTo(Game.CursorPos.To2D());
                                lastStopEvadeTime = EvadeUtils.TickCount + ObjectCache.gamePing + 100;
                            }

                            return;
                        }
                    }

                    lastPosInfo = PositionInfo.SetAllUndodgeable();
                    return;
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
                if (isDodging && SpellDetector.spells.Any())
                {
                    CheckHeroInDanger();

                    lastBlockedUserMoveTo = new EvadeCommand
                    {
                        order = EvadeOrderCommand.MoveTo,
                        targetPosition = args.TargetPosition.To2D(),
                        timestamp = EvadeUtils.TickCount,
                        isProcessed = false,
                    };

                    args.Process = false;
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    var extraDelay = ObjectCache.menuCache.cache["ExtraPingBuffer"].GetValue<Slider>().Value;
                
                    if (EvadeHelper.CheckMovePath(movePos, ObjectCache.gamePing + extraDelay))
                    {
                        /*if (ObjectCache.menuCache.cache["AllowCrossing"].GetValue<bool>())
                        {
                            var extraDelayBuffer = ObjectCache.menuCache.cache["ExtraPingBuffer"]
                                .GetValue<Slider>().Value + 30;
                            var extraDist = ObjectCache.menuCache.cache["ExtraCPADistance"]
                                .GetValue<Slider>().Value + 10;

                            var tPosInfo = EvadeHelper.CanHeroWalkToPos(movePos, ObjectCache.myHeroCache.moveSpeed, extraDelayBuffer + ObjectCache.gamePing, extraDist);

                            if (tPosInfo.posDangerLevel == 0)
                            {
                                lastPosInfo = tPosInfo;
                                return;
                            }
                        }*/

                        lastBlockedUserMoveTo = new EvadeCommand
                        {
                            order = EvadeOrderCommand.MoveTo,
                            targetPosition = args.TargetPosition.To2D(),
                            timestamp = EvadeUtils.TickCount,
                            isProcessed = false,
                        };

                        args.Process = false; //Block the command

                        if (EvadeUtils.TickCount - lastMovementBlockTime < 500 && lastMovementBlockPos.Distance(args.TargetPosition) < 100)
                        {
                            return;
                        }

                        lastMovementBlockPos = args.TargetPosition;
                        lastMovementBlockTime = EvadeUtils.TickCount;

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

            if (args.Process == true)
            {
                lastIssueOrderGameTime = Game.Time * 1000;
                lastIssueOrderTime = EvadeUtils.TickCount;
                lastIssueOrderArgs = args;

                if (args.Order == GameObjectOrder.MoveTo)
                {
                    lastMoveToPosition = args.TargetPosition.To2D();
                    lastMoveToServerPos = myHero.ServerPosition.To2D();
                }

                if (args.Order == GameObjectOrder.Stop)
                {
                    lastStopPosition = myHero.ServerPosition.To2D();
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

        private void Game_OnProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            if (!hero.IsMe)
            {
                return;
            }

            if (hero.NetworkId != myHero.NetworkId)
            {
                Console.WriteLine("IS NOT ME");
            }

            /*if (args.SData.Name.Contains("Recall"))
            {
                var distance = lastStopPosition.Distance(args.Start.To2D());
                float moveTime = 1000 * distance / myHero.MoveSpeed;

                Console.WriteLine("Extra dist: " + distance + " Extra Delay: " + moveTime);
            }*/

            string name;
            if (SpellDetector.channeledSpells.TryGetValue(args.SData.Name, out name))
            {
                Evade.isChanneling = true;
                Evade.channelPosition = myHero.ServerPosition.To2D();
            }

            if (ObjectCache.menuCache.cache["CalculateWindupDelay"].GetValue<bool>())
            {
                var castTime = (hero.Spellbook.CastTime - Game.Time) * 1000;

                if (castTime > 0 && !Orbwalking.IsAutoAttack(args.SData.Name)
                    && Math.Abs(castTime - myHero.AttackCastDelay * 1000) > 1)
                {
                    Evade.lastWindupTime = EvadeUtils.TickCount + castTime - Game.Ping / 2;

                    if (Evade.isDodging)
                    {
                        SpellDetector_OnProcessDetectedSpells(); //reprocess
                    }
                }
            }


        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            try
            {
                ObjectCache.myHeroCache.UpdateInfo();
                CheckHeroInDanger();

                if (isChanneling && channelPosition.Distance(ObjectCache.myHeroCache.serverPos2D) > 50
                    && !myHero.IsChannelingImportantSpell())
                {
                    isChanneling = false;
                }

                if (ObjectCache.menuCache.cache["ResetConfig"].GetValue<bool>())
                {
                    ResetConfig();
                    menu.Item("ResetConfig").SetValue(false);
                }

                var limitDelay = ObjectCache.menuCache.cache["TickLimiter"].GetValue<Slider>().Value; //Tick limiter                
                if (EvadeHelper.fastEvadeMode || EvadeUtils.TickCount - lastTickCount > limitDelay&& EvadeUtils.TickCount > lastStopEvadeTime)
                {
                    DodgeSkillShots(); //walking           
                    ContinueLastBlockedCommand();
                    lastTickCount = EvadeUtils.TickCount;
                }

                EvadeSpell.UseEvadeSpell(); //using spells
                CheckDodgeOnlyDangerous();
                RecalculatePath();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void RecalculatePath()
        {
            if (ObjectCache.menuCache.cache["RecalculatePosition"].GetValue<bool>() && isDodging)//recheck path
            {
                if (lastPosInfo != null && !lastPosInfo.recalculatedPath)
                {
                    var path = myHero.Path;
                    if (path.Length > 0)
                    {
                        var movePos = path.Last().To2D();

                        if (movePos.Distance(lastPosInfo.position) < 5) //more strict checking
                        {
                            var posInfo = EvadeHelper.CanHeroWalkToPos(movePos, ObjectCache.myHeroCache.moveSpeed, 0, 0, false);
                            if (posInfo.posDangerCount > lastPosInfo.posDangerCount)
                            {
                                lastPosInfo.recalculatedPath = true;

                                if (EvadeSpell.PreferEvadeSpell())
                                {
                                    lastPosInfo = PositionInfo.SetAllUndodgeable();
                                }
                                else
                                {
                                    var newPosInfo = EvadeHelper.GetBestPosition();
                                    if (newPosInfo.posDangerCount < posInfo.posDangerCount)
                                    {
                                        lastPosInfo = newPosInfo;
                                        CheckHeroInDanger();
                                        DodgeSkillShots();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ContinueLastBlockedCommand()
        {
            if (ObjectCache.menuCache.cache["ContinueMovement"].GetValue<bool>()
                && Situation.ShouldDodge())
            {
                var movePos = lastBlockedUserMoveTo.targetPosition;
                var extraDelay = ObjectCache.menuCache.cache["ExtraPingBuffer"].GetValue<Slider>().Value;

                if (isDodging == false && lastBlockedUserMoveTo.isProcessed == false
                    && EvadeUtils.TickCount - lastEvadeCommand.timestamp > ObjectCache.gamePing + extraDelay
                    && EvadeUtils.TickCount - lastBlockedUserMoveTo.timestamp < 1500)
                {
                    movePos = movePos + (movePos - ObjectCache.myHeroCache.serverPos2D).Normalized()
                        * EvadeUtils.random.NextFloat(1, 65);

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

                if (lastPosInfo != null && lastPosInfo.dodgeableSpells.Contains(spell.spellID))
                {
                    if (myHero.ServerPosition.To2D().InSkillShot(spell, ObjectCache.myHeroCache.boundingRadius))
                    {
                        playerInDanger = true;
                        break;
                    }

                    if (ObjectCache.menuCache.cache["EnableEvadeDistance"].GetValue<bool>() &&
                        EvadeUtils.TickCount < lastPosInfo.endTime)
                    {
                        playerInDanger = true;
                        break;
                    }
                }
            }

            if (isDodging && !playerInDanger)
            {
                lastDodgingEndTime = EvadeUtils.TickCount;
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

                    if (ObjectCache.menuCache.cache["ClickOnlyOnce"].GetValue<bool>() == false
                        || !(myHero.Path.Count() > 0 && lastPosInfo.position.Distance(myHero.Path.Last().To2D()) < 5))
                    //|| lastPosInfo.timestamp > lastEvadeOrderTime)
                    {
                        EvadeCommand.MoveTo(lastBestPosition);
                        lastEvadeOrderTime = EvadeUtils.TickCount;
                    }
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
                        /*if (ObjectCache.menuCache.cache["AllowCrossing"].GetValue<bool>())
                        {
                            var extraDelayBuffer = ObjectCache.menuCache.cache["ExtraPingBuffer"]
                                .GetValue<Slider>().Value + 30;
                            var extraDist = ObjectCache.menuCache.cache["ExtraCPADistance"]
                                .GetValue<Slider>().Value + 10;

                            var tPosInfo = EvadeHelper.CanHeroWalkToPos(movePos, ObjectCache.myHeroCache.moveSpeed, extraDelayBuffer + ObjectCache.gamePing, extraDist);

                            if (tPosInfo.posDangerLevel == 0)
                            {
                                lastPosInfo = tPosInfo;
                                return;
                            }
                        }*/

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

        public void CheckLastMoveTo()
        {
            if (EvadeHelper.fastEvadeMode || ObjectCache.menuCache.cache["FastMovementBlock"].GetValue<bool>())
            {
                if (isDodging == false)
                {
                    if (lastIssueOrderArgs != null && lastIssueOrderArgs.Order == GameObjectOrder.MoveTo)
                    {
                        if (Game.Time * 1000 - lastIssueOrderGameTime < 500)
                        {
                            Game_OnIssueOrder(myHero, lastIssueOrderArgs);
                            lastIssueOrderArgs = null;
                        }
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
            ObjectCache.myHeroCache.UpdateInfo();

            if (ObjectCache.menuCache.cache["DodgeSkillShots"].GetValue<KeyBind>().Active == false)
            {
                lastPosInfo = PositionInfo.SetAllUndodgeable();
                EvadeSpell.UseEvadeSpell();
                return;
            }

            if (ObjectCache.myHeroCache.serverPos2D.CheckDangerousPos(0)
                || ObjectCache.myHeroCache.serverPos2DExtra.CheckDangerousPos(0))
            {
                if (EvadeSpell.PreferEvadeSpell())
                {
                    lastPosInfo = PositionInfo.SetAllUndodgeable();
                }
                else
                {

                    var posInfo = EvadeHelper.GetBestPosition();

                    var calculationTimer = EvadeUtils.TickCount;
                    var caculationTime = EvadeUtils.TickCount - calculationTimer;

                    //computing time
                    /*if (numCalculationTime > 0)
                    {
                        sumCalculationTime += caculationTime;
                        avgCalculationTime = sumCalculationTime / numCalculationTime;
                    }
                    numCalculationTime += 1;*/

                    //Console.WriteLine("CalculationTime: " + caculationTime);

                    /*if (EvadeHelper.GetHighestDetectedSpellID() > EvadeHelper.GetHighestSpellID(posInfo))
                    {
                        return;
                    }*/
                    if (posInfo != null)
                    {
                        lastPosInfo = posInfo.CompareLastMovePos();

                        var travelTime = ObjectCache.myHeroCache.serverPos2DPing.Distance(lastPosInfo.position) / myHero.MoveSpeed;

                        lastPosInfo.endTime = EvadeUtils.TickCount + travelTime * 1000 - 100;
                    }

                    CheckHeroInDanger();
                    DodgeSkillShots(); //walking
                    CheckLastMoveTo();
                    EvadeSpell.UseEvadeSpell(); //using spells
                }
            }
            else
            {
                lastPosInfo = PositionInfo.SetAllDodgeable();
                CheckLastMoveTo();
            }


            //Console.WriteLine("SkillsDodged: " + lastPosInfo.dodgeableSpells.Count + " DangerLevel: " + lastPosInfo.undodgeableSpells.Count);            
        }
    }
}
