using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{

    internal class Evade
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static float gameTime { get { return Game.ClockTime * 1000; } }

        public static SpellDetector spellDetector;
        private static SpellDrawer spellDrawer;
        private static EvadeTester evadeTester;

        private static SpellSlot lastSpellCast;

        public static float lastTickCount;

        public static bool isDodging = false;
        public static bool dodgeOnlyDangerous = false;

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
            Game.PrintChat("<font color=\"#66CCFF\" >Yomie's </font><font color=\"#CCFFFF\" >ezEvade </font><font color=\"#66CCFF\" >loaded</font>");

            menu = new Menu("ezEvade", "ezEvade", true);

            Menu mainMenu = new Menu("Main", "Main");
            mainMenu.AddItem(new MenuItem("DodgeSkillShots", "Dodge SkillShots").SetValue(true));
            mainMenu.AddItem(new MenuItem("DodgeDangerous", "Dodge Only Dangerous").SetValue(false));
            menu.AddSubMenu(mainMenu);

            Menu keyMenu = new Menu("KeySettings", "KeySettings");
            keyMenu.AddItem(new MenuItem("DodgeDangerous", "Enable Dodge Only Dangerous Keys").SetValue(false));
            keyMenu.AddItem(new MenuItem("DodgeDangerousKey", "Dodge Only Dangerous Key").SetValue(new KeyBind(32, KeyBindType.Press)));
            keyMenu.AddItem(new MenuItem("DodgeDangerousKey2", "Dodge Only Dangerous Key 2").SetValue(new KeyBind('V', KeyBindType.Press)));
            menu.AddSubMenu(keyMenu);

            menu.AddToMainMenu();

            spellDetector = new SpellDetector(menu);
            spellDrawer = new SpellDrawer(menu);
            //evadeTester = new EvadeTester(menu);
        }

        private void Game_OnSendPacket(GamePacketEventArgs args)
        {
            // Check if the packet sent is a spell cast
            if (args.PacketData[0] == 104)
            {
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

            if (args.Order == GameObjectOrder.MoveTo)
            {
                if (lastEvadeCommand.isProcessed == false)
                {
                    if (lastEvadeCommand.order == EvadeOrderCommand.MoveTo
                        && lastEvadeCommand.targetPosition.Distance(args.TargetPosition.To2D()) < 3)
                    {
                        lastEvadeCommand.isProcessed = true;
                        return;
                    }
                }

                //movement block code goes in here
                if (isDodging)
                {
                    args.Process = false; //Block the command
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    if (EvadeHelper.checkMoveToDirection(movePos))
                    {
                        args.Process = false; //Block the command

                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            Evade_MoveTo(posInfo.position);
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
            if (myHero.IsDead)
                return;

            if (gameTime - lastTickCount > 50) //Tick limiter
            {
                DodgeSkillShots();
                lastTickCount = gameTime;
            }

            CheckDodgeOnlyDangerous();
        }

        private void DodgeSkillShots()
        {
            if (menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<bool>() == false)
            {
                return;
            }

            bool playerInDanger = EvadeHelper.CheckDangerousPos(myHero.ServerPosition.To2D(), 0);

            if (playerInDanger && lastPosInfo != null && lastPosInfo.dodgeableSpells.Count > 0)
            {
                isDodging = true;
            }
            else
            {
                isDodging = false;
            }

            if (isDodging)
            {
                Vector2 lastBestPosition = lastPosInfo.position;

                Evade_MoveTo(lastBestPosition);

                if (lastBestPosition.Distance(myHero.ServerPosition.To2D()) < 3) //a bit faulty
                {
                    //isDodging = false;
                }
            }
            else
            {
                //Check if hero will walk into a skillshot
                var path = myHero.Path;
                if (path.Length > 0)
                {
                    var movePos = path[path.Length - 1].To2D();

                    if (EvadeHelper.checkMoveToDirection(movePos))
                    {
                        var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                        if (posInfo != null)
                        {
                            Evade_MoveTo(posInfo.position);
                        }
                        return;
                    }
                }
            }
        }

        public static void Evade_MoveTo(Vector2 movePos)
        {
            lastEvadeCommand = new EvadeCommand
            {
                order = EvadeOrderCommand.MoveTo,
                targetPosition = movePos,
                timestamp = gameTime,
                isProcessed = false
            };
            myHero.IssueOrder(GameObjectOrder.MoveTo, movePos.To3D());
        }

        public static bool isDodgeDangerousEnabled()
        {
            if (menu.SubMenu("Main").Item("DodgeDangerous").GetValue<bool>() == true)
            {
                return true;
            }

            if (menu.SubMenu("KeySettings").Item("DodgeDangerous").GetValue<bool>() == true)
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

            DodgeSkillShots();
        }

        public static void CheckMovingIntoDanger(Vector2 movePos)
        {
            bool intersect = EvadeHelper.checkMoveToDirection(movePos);
            if (intersect)
            {
                var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                if (posInfo != null)
                { //check if there is solution
                    myHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.position.To3D());
                }
            }
        }

    }
}
