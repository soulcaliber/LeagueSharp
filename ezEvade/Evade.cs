using System;
using System.Linq;
using System.Threading;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    internal class Evade
    {
        public static SpellDetector SpellDetector;
        private static SpellDrawer _spellDrawer;
        //private static EvadeTester _evadeTester;
        private static SpellSlot _lastSpellCast;
        public static float LastTickCount;
        public static bool IsDodging;
        public static bool DodgeOnlyDangerous;
        public static EvadeHelper.PositionInfo LastPosInfo;
        public static EvadeCommand LastEvadeCommand = new EvadeCommand {IsProcessed = true};
        public static Menu Menu;

        public Evade()
        {
            CustomEvents.Game.OnGameLoad += delegate
            {
                var onGameLoad = new Thread(Game_OnGameLoad);
                onGameLoad.Start();
            };
            Obj_AI_Base.OnIssueOrder += Game_OnIssueOrder;
            Spellbook.OnCastSpell += Game_OnCastSpell;
            Game.OnGameUpdate += Game_OnGameUpdate;
            Game.OnGameSendPacket += Game_OnSendPacket;

            SpellDetector.OnCreateSpell += SpellDetector_OnCreateSpell;
        }

        private static Obj_AI_Hero MyHero
        {
            get { return ObjectManager.Player; }
        }

        private static float GameTime
        {
            get { return Game.ClockTime*1000; }
        }

        private static void Game_OnGameLoad()
        {
            Game.PrintChat(
                "<font color=\"#66CCFF\" >Yomie's </font><font color=\"#CCFFFF\" >ezEvade </font><font color=\"#66CCFF\" >loaded</font>");

            Menu = new Menu("ezEvade", "ezEvade", true);

            var mainMenu = new Menu("Main", "Main");
            mainMenu.AddItem(new MenuItem("DodgeSkillShots", "Dodge SkillShots").SetValue(true));
            mainMenu.AddItem(new MenuItem("DodgeDangerous", "Dodge Only Dangerous").SetValue(false));
            Menu.AddSubMenu(mainMenu);

            var keyMenu = new Menu("KeySettings", "KeySettings");
            keyMenu.AddItem(new MenuItem("DodgeDangerous", "Enable Dodge Only Dangerous Keys").SetValue(false));
            keyMenu.AddItem(
                new MenuItem("DodgeDangerousKey", "Dodge Only Dangerous Key").SetValue(new KeyBind(32, KeyBindType.Press)));
            keyMenu.AddItem(
                new MenuItem("DodgeDangerousKey2", "Dodge Only Dangerous Key 2").SetValue(new KeyBind('V',
                    KeyBindType.Press)));
            Menu.AddSubMenu(keyMenu);

            Menu.AddToMainMenu();

            SpellDetector = new SpellDetector(Menu);
            _spellDrawer = new SpellDrawer(Menu);
            //evadeTester = new EvadeTester(menu);
        }

        private static void Game_OnSendPacket(GamePacketEventArgs args)
        {
            // Check if the packet sent is a spell cast
            if (args.PacketData[0] != 104)
            {
                return;
            }

            if (!IsDodging)
            {
                return;
            }

            if (
                SpellDetector.WindupSpells.Select(entry => entry.Value)
                    .Any(spellData => spellData.SpellKey == _lastSpellCast))
                // Check if it's a spell that we should block
            {
                args.Process = false;
            }
        }

        private static void Game_OnCastSpell(Spellbook hero, SpellbookCastSpellEventArgs args)
        {
            if (!hero.Owner.IsMe)
            {
                return;
            }

            _lastSpellCast = args.Slot;
        }

        private static void Game_OnIssueOrder(Obj_AI_Base hero, GameObjectIssueOrderEventArgs args)
        {
            if (!hero.IsMe)
            {
                return;
            }

            if (args.Order == GameObjectOrder.MoveTo)
            {
                if (LastEvadeCommand.IsProcessed == false)
                {
                    if (LastEvadeCommand.Order == EvadeOrderCommand.MoveTo
                        && LastEvadeCommand.TargetPosition.Distance(args.TargetPosition.To2D()) < 3)
                    {
                        LastEvadeCommand.IsProcessed = true;
                        return;
                    }
                }

                // Movement block code goes in here
                if (IsDodging)
                {
                    args.Process = false; // Block the command
                }
                else
                {
                    var movePos = args.TargetPosition.To2D();
                    if (!EvadeHelper.CheckMoveToDirection(movePos))
                    {
                        return;
                    }

                    args.Process = false; // Block the command

                    var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                    if (posInfo != null)
                    {
                        Evade_MoveTo(posInfo.Position);
                    }
                }
            }
            else // Need more logic
            {
                if (IsDodging)
                {
                    args.Process = false; // Block the command
                }
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (MyHero.IsDead)
            {
                return;
            }

            if (GameTime - LastTickCount > 50) //Tick limiter
            {
                DodgeSkillShots();
                LastTickCount = GameTime;
            }

            CheckDodgeOnlyDangerous();
        }

        private static void DodgeSkillShots()
        {
            if (Menu.SubMenu("Main").Item("DodgeSkillShots").GetValue<bool>() == false)
            {
                return;
            }

            var playerInDanger = EvadeHelper.CheckDangerousPos(MyHero.ServerPosition.To2D(), 0);

            if (playerInDanger && LastPosInfo != null && LastPosInfo.DodgeableSpells.Count > 0)
            {
                IsDodging = true;
            }
            else
            {
                IsDodging = false;
            }

            if (IsDodging)
            {
                // Checking for Null Reference
                if (LastPosInfo == null)
                {
                    return;
                }

                var lastBestPosition = LastPosInfo.Position;

                Evade_MoveTo(lastBestPosition);

                if (lastBestPosition.Distance(MyHero.ServerPosition.To2D()) < 3) // A bit faulty
                {
                    //isDodging = false;
                }
            }
            else
            {
                // Check if hero will walk into a skillshot
                var path = MyHero.Path;
                if (path.Length <= 0)
                {
                    return;
                }

                var movePos = path[path.Length - 1].To2D();
                if (!EvadeHelper.CheckMoveToDirection(movePos))
                {
                    return;
                }

                var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
                if (posInfo != null)
                {
                    Evade_MoveTo(posInfo.Position);
                }
            }
        }

        public static void Evade_MoveTo(Vector2 movePos)
        {
            LastEvadeCommand = new EvadeCommand
            {
                Order = EvadeOrderCommand.MoveTo,
                TargetPosition = movePos,
                TimeStamp = GameTime,
                IsProcessed = false
            };
            MyHero.IssueOrder(GameObjectOrder.MoveTo, movePos.To3D());
        }

        public static bool IsDodgeDangerousEnabled()
        {
            if (Menu.SubMenu("Main").Item("DodgeDangerous").GetValue<bool>())
            {
                return true;
            }

            if (!Menu.SubMenu("KeySettings").Item("DodgeDangerous").GetValue<bool>())
            {
                return false;
            }

            return Menu.SubMenu("KeySettings").Item("DodgeDangerousKey").GetValue<KeyBind>().Active
                   || Menu.SubMenu("KeySettings").Item("DodgeDangerousKey2").GetValue<KeyBind>().Active;
        }

        public static void CheckDodgeOnlyDangerous() //Dodge only dangerous event
        {
            var bDodgeOnlyDangerous = IsDodgeDangerousEnabled();
            if (DodgeOnlyDangerous == false && bDodgeOnlyDangerous)
            {
                SpellDetector.RemoveNonDangerousSpells();
                DodgeOnlyDangerous = true;
            }
            else
            {
                DodgeOnlyDangerous = bDodgeOnlyDangerous;
            }
        }

        private static void SpellDetector_OnCreateSpell(Spell newSpell)
        {
            var posInfo = EvadeHelper.GetBestPosition();
            LastPosInfo = posInfo;

            //Game.PrintChat("SkillsDodged: " + lastPosInfo.dodgeableSpells.Count + " DangerLevel: " + lastPosInfo.posDangerLevel);

            DodgeSkillShots();
        }

        public static void CheckMovingIntoDanger(Vector2 movePos)
        {
            var intersect = EvadeHelper.CheckMoveToDirection(movePos);
            if (!intersect)
            {
                return;
            }

            var posInfo = EvadeHelper.GetBestPositionMovementBlock(movePos);
            if (posInfo != null)
            {
                // Check if there is solution
                MyHero.IssueOrder(GameObjectOrder.MoveTo, posInfo.Position.To3D());
            }
        }
    }
}