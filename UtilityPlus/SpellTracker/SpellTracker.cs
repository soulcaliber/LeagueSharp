using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.SpellTracker
{
    class SpellTrackerInfo
    {
        public SpellSlot spellSlot;
        public float cooldownExpires = 0;
        public float totalCooldown = 0;
        public SpellCooldownInfo info;

        public SpellTrackerInfo(SpellCooldownInfo info, SpellSlot spellSlot, float cooldownExpires)
        {
            this.info = info;
            this.spellSlot = spellSlot;
            this.cooldownExpires = cooldownExpires;
        }
    }

    class TeleportInfo
    {
        public float startTime = 0;
        public float endTime = 0;
        public Vector3 position = Vector3.Zero;
        public bool isTurretTeleport = false;
        public bool isTeleporting = false;
        public bool isRecalling = false;
        public Obj_AI_Hero hero;

        public TeleportInfo(Obj_AI_Hero hero)
        {
            this.hero = hero;
        }
    }

    class Tracker
    {
        private static Menu menu;
        public static SpellSlot[] summonerSpellSlots = { SpellSlot.Summoner1, SpellSlot.Summoner2 };
        public static SpellSlot[] spellSlots = { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };

        public static Color backgroundColor = Color.FromArgb(255, 15, 130, 0);

        public static float allyOffsetY = 14;
        public static float enemyOffsetY = 16;

        static float recallBarX = Drawing.Width * 0.425f;
        static float recallBarY = Drawing.Height * 0.80f;
        static float recalBarWidth = Drawing.Width - 2 * recallBarX;

        public static Dictionary<int, TeleportInfo> teleportInfos = new Dictionary<int, TeleportInfo>();

        public static Dictionary<int, Dictionary<string, SpellTrackerInfo>> spellCooldowns =
            new Dictionary<int, Dictionary<string, SpellTrackerInfo>>();

        public Tracker(Menu mainMenu)
        {
            menu = mainMenu;

            Menu spellTrackerMenu = new Menu("Spell Tracker", "SpellTracker");

            spellTrackerMenu.AddItem(new MenuItem("TrackEnemyCooldown", "Track Enemies").SetValue(true));
            spellTrackerMenu.AddItem(new MenuItem("TrackAllyCooldown", "Track Allies").SetValue(true));
            spellTrackerMenu.AddItem(new MenuItem("TrackNoMana", "Track Mana Cost").SetValue(true));
            spellTrackerMenu.AddItem(new MenuItem("TrackEnemyRecalls", "Track Recalls").SetValue(true));

            menu.AddSubMenu(spellTrackerMenu);

            Obj_AI_Hero.OnProcessSpellCast += Game_OnProcessSpell;

            SummonerData.GetSummonerColor("test");

            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnTeleport += Game_OnTeleport;

            Obj_AI_Base.OnCreate += Game_OnCreateObject;

            LoadSpecialSpells();
        }

        private static void LoadSpecialSpells()
        {
            foreach (var hero in HeroManager.AllHeroes)
            {
                teleportInfos.Add(hero.NetworkId, new TeleportInfo(hero));

                Dictionary<string, SpellTrackerInfo> spellDictioniary = new Dictionary<string, SpellTrackerInfo>();
                spellCooldowns.Add(hero.NetworkId, spellDictioniary);

                foreach (var spell in SpellCooldownDatabase.spellCDDatabase)
                {
                    if (spell.spellSlot == SpellSlot.Summoner1)
                    {
                        var spellSlot = GetSummonerSlot(hero, spell.spellName);
                        if (spellSlot != SpellSlot.Unknown)
                        {
                            spellDictioniary.Add(spell.spellName.ToLower(),
                                new SpellTrackerInfo(spell, spellSlot, 0));
                        }
                    }
                    else
                    {
                        if (spell.charName == hero.ChampionName || spell.charName == "AllChampions")
                        {
                            var tSpell = hero.Spellbook.GetSpell(spell.spellSlot);
                            if (tSpell.Name.ToLower() == spell.spellName.ToLower())
                            {
                                spellDictioniary.Add(spell.spellName.ToLower(),
                                    new SpellTrackerInfo(spell, spell.spellSlot, 0));
                            }
                        }
                    }
                }
            }

        }

        private static SpellSlot GetSummonerSlot(Obj_AI_Hero hero, string SpellName)
        {
            var sum1 = hero.Spellbook.GetSpell(SpellSlot.Summoner1);
            if (sum1.Name.ToLower() == SpellName.ToLower())
            {
                return SpellSlot.Summoner1;
            }

            var sum2 = hero.Spellbook.GetSpell(SpellSlot.Summoner2);
            if (sum2.Name.ToLower() == SpellName.ToLower())
            {
                return SpellSlot.Summoner2;
            }

            return SpellSlot.Unknown;
        }

        private void Game_OnCreateObject(GameObject sender, EventArgs args)
        {
            if (sender.Type == GameObjectType.obj_GeneralParticleEmitter &&
                sender.Name.Contains("global_ss_teleport"))
            {
                foreach (var info in teleportInfos.Values)
                {
                    if (info.isTeleporting)
                    {
                        if (sender.Name.Contains("turret"))
                        {
                            info.isTurretTeleport = true;
                        }

                        info.position = sender.Position;

                        Draw.RenderObjects.Add(
                            new Draw.RenderText(info.hero.ChampionName, sender.Position.To2D(), 3500,
                            (sender.Name.Contains("blue") ? Color.SkyBlue : Color.Red)));

                        Draw.RenderObjects.Add(
                            new Draw.CooldownBar(sender.Position.To2D(), 3500, 20));
                    }
                }
            }
        }

        private void Game_OnTeleport(Obj_AI_Base sender, GameObjectTeleportEventArgs args)
        {
            var hero = sender as Obj_AI_Hero;
            if (hero != null)
            {
                var packet = Packet.S2C.Teleport.Decoded(sender, args);
                if (packet.Type == Packet.S2C.Teleport.Type.Recall)
                {
                    if (packet.Status == Packet.S2C.Teleport.Status.Start)
                    {
                        var info = teleportInfos[sender.NetworkId];
                        info.isRecalling = true;
                        info.startTime = HelperUtils.TickCount;

                        var totalRecallTime = 8000;

                        if (Game.MapId == GameMapId.CrystalScar)
                        {
                            totalRecallTime = 4500;
                        }

                        info.endTime = HelperUtils.TickCount + totalRecallTime;
                    }
                    else
                    {
                        var info = teleportInfos[sender.NetworkId];
                        info.isRecalling = false;
                    }
                }

                if (packet.Type == Packet.S2C.Teleport.Type.Teleport)
                {
                    var duration = 0;

                    if (packet.Status == Packet.S2C.Teleport.Status.Finish)
                    {
                        duration = 300;

                        var info = teleportInfos[sender.NetworkId];
                        if (info.isTurretTeleport)
                        {
                            duration = 240;
                            ConsolePrinter.Print(sender.Name);
                        }
                    }
                    else if (packet.Status == Packet.S2C.Teleport.Status.Abort)
                    {
                        duration = 200;
                    }

                    if (packet.Status == Packet.S2C.Teleport.Status.Start)
                    {
                        var info = teleportInfos[sender.NetworkId];
                        info.isTeleporting = true;
                        info.isTurretTeleport = false;
                        info.isRecalling = false;
                        info.startTime = HelperUtils.TickCount;
                    }
                    else
                    {
                        var info = teleportInfos[sender.NetworkId];
                        info.isTeleporting = false;
                        info.isTurretTeleport = false;
                    }

                    if (duration > 0)
                    {
                        SpellTrackerInfo spellInfo;
                        if (spellCooldowns[sender.NetworkId].TryGetValue("summonerteleport", out spellInfo))
                        {
                            spellInfo.cooldownExpires = HelperUtils.TickCount + duration * 1000;
                            spellInfo.totalCooldown = duration;
                        }
                    }
                }
            }
        }

        private void Game_OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            var hero = sender as Obj_AI_Hero;
            if (hero != null)
            {
                SpellTrackerInfo spellInfo;
                if (spellCooldowns[sender.NetworkId].TryGetValue(args.SData.Name.ToLower(), out spellInfo))
                {
                    var spellInst = hero.GetSpell(spellInfo.spellSlot);
                    spellInfo.cooldownExpires = HelperUtils.TickCount
                        + spellInfo.info.cooldownArray[spellInst.Level - 1] * 1000; // + 250
                }
            }
        }

        private void DrawRecallBars()
        {
            if (!menu.Item("TrackEnemyRecalls").GetValue<bool>())
            {
                return;
            }

            bool recallBarDrawn = false;

            foreach (var info in teleportInfos.Values.OrderBy(i => i.endTime))
            {
                if (info.isRecalling
                    && info.hero.IsEnemy
                    )
                {
                    var hero = info.hero;

                    var timeLeft = (HelperUtils.TickCount - info.startTime);
                    var totalRecallTime = 8000;

                    if (Game.MapId == GameMapId.CrystalScar)
                    {
                        totalRecallTime = 4500;
                    }

                    var percent = timeLeft < totalRecallTime ? 1f - (timeLeft / totalRecallTime) : 1f;

                    if (percent < 1f)
                    {
                        if (recallBarDrawn == false)
                        {
                            Drawing.DrawLine(new Vector2(recallBarX - 1, recallBarY - 1),
                            new Vector2(recallBarX + recalBarWidth + 1, recallBarY - 1), 12, Color.Black);

                            Drawing.DrawLine(new Vector2(recallBarX, recallBarY),
                            new Vector2(recallBarX + recalBarWidth, recallBarY), 10, Color.LightGray);

                            recallBarDrawn = true;
                        }

                        var textDimension = TextUtils.GetTextExtent(hero.ChampionName);
                        TextUtils.DrawText(recallBarX + recalBarWidth * percent - textDimension.Width / 2,
                            recallBarY - 20, Color.White, hero.ChampionName);

                        Drawing.DrawLine(new Vector2(recallBarX, recallBarY),
                            new Vector2(recallBarX + recalBarWidth * percent + 2, recallBarY), 10,
                            Color.FromArgb((int)(255 * (1f - percent)), Color.Black));
                    }
                }
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            DrawRecallBars();

            //var testTime = HelperUtils.TickCount;

            //for (int j = 0; j < 10;j++ )

            foreach (var hero in HeroManager.AllHeroes)
            {
                if (hero.IsMe ||
                    !hero.IsHPBarRendered
                    || (hero.IsAlly && !menu.Item("TrackAllyCooldown").GetValue<bool>())
                    || (hero.IsEnemy && !menu.Item("TrackEnemyCooldown").GetValue<bool>())
                    )
                {
                    continue;
                }

                var startX = hero.HPBarPosition.X - 13;
                var startY = hero.HPBarPosition.Y +
                    (hero.IsAlly ? allyOffsetY : enemyOffsetY);

                foreach (var slot in summonerSpellSlots)
                {
                    var spell = hero.Spellbook.GetSpell(slot);
                    var time = spell.CooldownExpires - Game.Time;
                    var totalCooldown = spell.Cooldown;

                    SpellTrackerInfo spellInfo;
                    if (spellCooldowns[hero.NetworkId].TryGetValue(spell.Name.ToLower(), out spellInfo))
                    {
                        time = (spellInfo.cooldownExpires - HelperUtils.TickCount) / 1000;
                        totalCooldown = spellInfo.totalCooldown > 0 ? spellInfo.totalCooldown : totalCooldown;
                    }

                    var percent = (time > 0 && Math.Abs(totalCooldown) > float.Epsilon)
                            ? 1f - (time / totalCooldown)
                            : 1f;

                    var spellState = hero.Spellbook.CanUseSpell(slot);

                    if (percent != 1f)
                    {
                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20, startY), 12, Color.Black);
                        Drawing.DrawLine(new Vector2(startX + 1, startY + 1), new Vector2(startX + 19, startY + 1), 10, Color.Gray);
                    }
                    else
                    {
                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20, startY), 12, Color.Green);
                    }


                    if (spellState != SpellState.NotLearned)
                    {
                        var color = Color.Orange;

                        if (slot == SpellSlot.Summoner1)
                        {
                            color = SummonerData.heroS1Color[hero.NetworkId];
                        }
                        else
                        {
                            color = SummonerData.heroS2Color[hero.NetworkId];
                        }

                        Drawing.DrawLine(new Vector2(startX + 1, startY + 1),
                                        new Vector2(startX + 1 + 18 * percent, startY + 1),
                                        10, color);
                    }

                    startY += 12;
                }

                startX += 23;
                startY -= 6;

                Drawing.DrawLine(new Vector2(startX - 2, startY - 1), new Vector2(startX + 105, startY - 1), 8, Color.Black);

                foreach (var slot in spellSlots)
                {
                    var spell = hero.Spellbook.GetSpell(slot);
                    var time = spell.CooldownExpires - Game.Time;
                    var totalCooldown = spell.Cooldown;

                    SpellTrackerInfo spellInfo;
                    if (spellCooldowns[hero.NetworkId].TryGetValue(spell.Name.ToLower(), out spellInfo))
                    {
                        time = (spellInfo.cooldownExpires - HelperUtils.TickCount) / 1000;
                        totalCooldown = spellInfo.totalCooldown > 0 ? spellInfo.totalCooldown : totalCooldown;
                    }

                    var percent = (time > 0 && Math.Abs(totalCooldown) > float.Epsilon)
                            ? 1f - (time / totalCooldown)
                            : 1f;

                    var spellState = hero.Spellbook.CanUseSpell(slot);

                    if (percent != 1f)
                    {
                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20, startY), 5, Color.Gray);
                    }

                    if (spellState != SpellState.NotLearned)
                    {
                        var color = Color.Yellow;

                        if (percent == 1f)
                        {
                            color = Color.Green;
                        }

                        /*if (spellState == SpellState.NoMana)
                        {
                            color = Color.Blue;
                        }

                        if (spellState == SpellState.Disabled)
                        {
                            color = Color.Black;
                        }*/

                        if (time > 0 && time < 2)
                        {
                            color = Color.Orange;
                        }

                        if (menu.Item("TrackNoMana").GetValue<bool>() &&
                            spell.ManaCost > hero.Mana)
                        {
                            color = Color.Pink;
                        }

                        Drawing.DrawLine(new Vector2(startX, startY), new Vector2(startX + 20 * percent, startY), 5, color);
                    }

                    startX = startX + 27;
                }
            }

            //Console.WriteLine("Time spent: " + (HelperUtils.TickCount - testTime));
        }
    }
}
