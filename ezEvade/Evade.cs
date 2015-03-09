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
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static SpellDetector spellDetector;
        private static SpellDrawer spellDrawer;
        private static EvadeTester evadeTester;
        private static EvadeSpell evadeSpell;

        private static SpellSlot lastSpellCast;

        public static float lastTickCount;

        private static bool isDodging = false;        
        public static bool dodgeOnlyDangerous = false;

        public static bool isChanneling = false;
        public static Vector2 channelPosition = Vector2.Zero;

        public static EvadeHelper.PositionInfo lastPosInfo;
        public static EvadeCommand lastEvadeCommand = new EvadeCommand { isProcessed = true };

        public static Menu menu;


        public Evade()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            Obj_AI_Hero.OnIssueOrder += Game_OnIssueOrder;
            Spellbook.OnCastSpell += Game_OnCastSpell;
            Game.OnGameUpdate += Game_OnGameUpdate;
            Game.OnGameSendPacket += Game_OnSendPacket;

            SpellDetector.OnCreateSpell += SpellDetector_OnCreateSpell;
        }

        private void Game_OnGameLoad(EventArgs args)
        {
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
            //miscMenu.AddItem(new MenuItem("CalculateHeroPos", "Calculate Hero Position").SetValue(false));

            Menu fastEvadeMenu = new Menu("Fast Evade", "FastEvade");
            fastEvadeMenu.AddItem(new MenuItem("FastEvadeActivationTime", "Fast Evade Time").SetValue(new Slider(300, 0, 500)));
            fastEvadeMenu.AddItem(new MenuItem("RejectMinDistance", "Collision Distance Buffer").SetValue(new Slider(40, 0, 100)));

            miscMenu.AddSubMenu(fastEvadeMenu);

            Menu bufferMenu = new Menu("Extra Buffers", "ExtraBuffers");
            bufferMenu.AddItem(new MenuItem("ExtraPingBuffer", "Extra Ping Buffer").SetValue(new Slider(10, 0, 150)));
            bufferMenu.AddItem(new MenuItem("ExtraCPADistance", "Extra Collision Distance").SetValue(new Slider(30, 0, 150)));
            bufferMenu.AddItem(new MenuItem("ExtraSpellRadius", "Extra Spell Radius").SetValue(new Slider(0, 0, 100)));
            bufferMenu.AddItem(new MenuItem("ExtraEvadeDistance", "Extra Evade Distance").SetValue(new Slider(20, 0, 100)));
            bufferMenu.AddItem(new MenuItem("ExtraAvoidDistance", "Extra Avoid Distance").SetValue(new Slider(0, 0, 100)));

            bufferMenu.AddItem(new MenuItem("MinComfortZone", "Minimum Comfort Zone").SetValue(new Slider(400, 0, 1000)));

            miscMenu.AddSubMenu(bufferMenu);
            menu.AddSubMenu(miscMenu);

            menu.AddToMainMenu();

            spellDrawer = new SpellDrawer(menu);
            //evadeTester = new EvadeTester(menu);
        }

        public static float GetTickCount()
        {
            return (float)DateTime.Now.TimeOfDay.TotalMilliseconds; //Game.ClockTime * 1000;
        }

        private void Game_OnSendPacket(GamePacketEventArgs args)
        {
            if (isChanneling || menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<KeyBind>().Active == false)
                return;

            // Check if the packet sent is a spell cast

            if (args != null && args.GetPacketId() == 228) //if(args.PacketData[0] == 228)            
            {                
                //Game.PrintChat("" + args.GetPacketId());
                
                if (isDodging)
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

        private void Game_OnCastSpell(Spellbook hero, SpellbookCastSpellEventArgs args)
        {
            if (!hero.Owner.IsMe)
                return;

            lastSpellCast = args.Slot;
        }

        private void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
                return;

            if (isChanneling || menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<KeyBind>().Active == false)
                return;

            if (args.Order == GameObjectOrder.MoveTo)
            {

                //movement block code goes in here
                if (isDodging)
                {
                    args.Process = false; //Block the command
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    if (EvadeHelper.checkMovePath(movePos))
                    {
                        args.Process = false; //Block the command

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
            CheckHeroInDanger();

            if (isChanneling && channelPosition.Distance(myHero.ServerPosition.To2D()) > 50)
            {
                isChanneling = false;
            }

            if (Evade.GetTickCount() - lastTickCount > 50) //Tick limiter
            {
                DodgeSkillShots(); //walking
                //EvadeSpell.UseEvadeSpell(); //using spells
                lastTickCount = Evade.GetTickCount();
            }

            CheckDodgeOnlyDangerous();
        }

        private void CheckHeroInDanger()
        {
            bool playerInDanger = false;
            foreach (KeyValuePair<int, Spell> entry in SpellDetector.spells)
            {
                Spell spell = entry.Value;

                if (lastPosInfo != null && lastPosInfo.dodgeableSpells.Contains(spell.spellID) &&
                    EvadeHelper.inSkillShot(spell, myHero.ServerPosition.To2D(), myHero.BoundingRadius))
                {
                    playerInDanger = true;
                    break;
                }
            }

            isDodging = playerInDanger;
        }

        private void DodgeSkillShots()
        {
            if (myHero.IsDead || isChanneling || menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<KeyBind>().Active == false)
            {
                isDodging = false;
                return;
            }

            /*
            if (isDodging && playerInDanger == false) //serverpos test
            {
                myHero.IssueOrder(GameObjectOrder.HoldPosition, myHero, false);
            }*/

            /*
            if (menu.SubMenu("KeySettings").Item("DodgeDangerousKey").GetValue<KeyBind>().Active == true)
            {
                VirtualMouse.VirtualClick(VirtualCommand.RightClick, myHero.ServerPosition);
            }*/

            if (isDodging)
            {
                Vector2 lastBestPosition = lastPosInfo.position;

                if (lastBestPosition.Distance(myHero.ServerPosition.To2D()) < 3) //a bit faulty
                {
                    //isDodging = false;
                }

                if (menu.SubMenu("MiscSettings").Item("RecalculatePosition").GetValue<bool>() && lastPosInfo != null)//recheck path
                {
                    var path = myHero.Path;
                    if (path.Length > 0)
                    {
                        var movePos = path[path.Length - 1].To2D();

                        if (movePos.Distance(lastPosInfo.position) < 5) //more strict checking
                        {
                            var posInfo = EvadeHelper.canHeroWalkToPos(movePos, myHero.MoveSpeed, 0, 0);
                            if (EvadeHelper.isSamePosInfo(posInfo, lastPosInfo) && posInfo.posDangerCount > lastPosInfo.posDangerCount)
                            {
                                lastPosInfo = EvadeHelper.GetBestPosition();
                            }
                        }
                    }
                }

                EvadeCommand.MoveTo(lastBestPosition);
            }
            else //if not dodging
            {
                //Check if hero will walk into a skillshot
                var path = myHero.Path;
                if (path.Length > 0)
                {
                    var movePos = path[path.Length - 1].To2D();

                    if (EvadeHelper.checkMovePath(movePos))
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

        private void SpellDetector_OnCreateSpell(Spell newSpell)
        {
            var posInfo = EvadeHelper.GetBestPosition();
            lastPosInfo = posInfo;

            //Game.PrintChat("SkillsDodged: " + lastPosInfo.dodgeableSpells.Count + " DangerLevel: " + lastPosInfo.posDangerLevel);

            DodgeSkillShots(); //walking
            EvadeSpell.UseEvadeSpell(); //using spells

        }

        public static void CheckMovingIntoDanger(Vector2 movePos)
        {
            bool intersect = EvadeHelper.checkMovePath(movePos);
            if (intersect)
            {
                var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                if (posInfo != null) //check if there is solution
                {
                    myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D());
                }
            }
        }       

    }
}
