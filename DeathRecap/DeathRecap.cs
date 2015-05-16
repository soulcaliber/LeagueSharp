using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace DeathRecap
{
    /*public enum DamageTypeEx
    {
        Magical = 163,
        Physical = 70,
        True = 78,
        Mixed = 0,
    }*/

    class DamageInfo
    {
        public string sourceName;
        public int sourceID;
        public float damageValue;
        public DamageType type;
        public float timestamp;

        public DamageInfo(int sourceID, string sourceName, float damageValue, DamageType type)
        {
            this.sourceID = sourceID;
            this.sourceName = sourceName;
            this.damageValue = damageValue;
            this.type = type;

            this.timestamp = Utils.TickCount;
        }
    }

    class TotalDamageInfo
    {
        public string sourceName;
        public int sourceID;

        public float magicalDamage = 0;
        public float physicalDamage = 0;
        public float trueDamage = 0;
        public float mixedDamage = 0;
        public float totalDamage = 0;
        public int comboCount = 0;

        /*public int magicalDamagePercent = 0;
        public int physicalDamagePercent = 0;
        public int trueDamagePercent = 0;
        public int mixedDamagePercent = 0;
        public int totalDamagePercent = 0;*/

        public TotalDamageInfo(int sourceID, string sourceName)
        {
            this.sourceID = sourceID;
            this.sourceName = sourceName;
        }

        public void ResetDamage()
        {
            magicalDamage = 0;
            physicalDamage = 0;
            trueDamage = 0;
            mixedDamage = 0;
            totalDamage = 0;
            comboCount = 0;
        }
    }

    class DeathRecap
    {
        public static Dictionary<int, Obj_AI_Base> objCache = new Dictionary<int, Obj_AI_Base>();
        public static List<DamageInfo> dmgCache = new List<DamageInfo>();
        public static Dictionary<string, TotalDamageInfo> finalDamage = new Dictionary<string, TotalDamageInfo>();
        public static List<TotalDamageInfo> orderedFinalDamage = new List<TotalDamageInfo>();

        public static Dictionary<int, Obj_AI_Hero> heroCache = new Dictionary<int, Obj_AI_Hero>();

        public static List<DamageInfo> myDmgCache = new List<DamageInfo>();
        public static Dictionary<int, TotalDamageInfo> myFinalDamage = new Dictionary<int, TotalDamageInfo>();
        public static List<TotalDamageInfo> orderedMyFinalDamage = new List<TotalDamageInfo>();

        public static TotalDamageInfo totalDamageTaken = new TotalDamageInfo(myHero.NetworkId, myHero.ChampionName);
        public static TotalDamageInfo totalDamageDealt = new TotalDamageInfo(myHero.NetworkId, myHero.ChampionName);

        public static int DamageTypeMagical = 163;
        public static int DamageTypePhysical = 78; //real value is 70
        public static int DamageTypeTrue = 78;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        private static Menu menu;

        public DeathRecap()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private void Game_OnGameLoad(EventArgs args)
        {
            menu = new Menu("DeathRecap", "DeathRecap", true);
            menu.AddItem(new MenuItem("ShowRecap", "Show Recap").SetValue(false));
            menu.AddItem(new MenuItem("ShowRecapPress", "Show Recap Key").SetValue(new KeyBind('J', KeyBindType.Press)));
            menu.AddItem(new MenuItem("ShowWhenDead", "Show Only When Dead").SetValue(true));
            menu.AddItem(new MenuItem("RecalculateOnDamage", "Recalculate On Damage").SetValue(true));
            menu.AddItem(new MenuItem("ShowDamageText", "Show Damage Text").SetValue(true));
            menu.AddItem(new MenuItem("TimeFrame", "Data Time Frame (Seconds)").SetValue(new Slider(30, 1, 300)));

            /*Menu damageTypeMenu = new Menu("DamageType", "DamageType");
            damageTypeMenu.AddItem(new MenuItem("TypePhysical", "Type Physical").SetValue(new Slider(70, 0, 255)));
            damageTypeMenu.AddItem(new MenuItem("TypeMagical", "Type Magical").SetValue(new Slider(163, 0, 255)));
            damageTypeMenu.AddItem(new MenuItem("TypeTrue", "Type True").SetValue(new Slider(78, 0, 255)));            
            menu.AddSubMenu(damageTypeMenu);*/

            menu.AddToMainMenu();

            AttackableUnit.OnDamage += Game_OnDamage;
            GameObject.OnCreate += GameObject_OnCreate;
            GameObject.OnDelete += GameObject_OnDelete;

            Drawing.OnDraw += Drawing_OnDraw;

            Obj_AI_Base.OnPlayAnimation += Hero_OnPlayAnimation;

            InitializeCache();
        }

        private static void InitializeCache()
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Base>())
            {
                if (!objCache.ContainsKey(obj.NetworkId))
                    objCache.Add(obj.NetworkId, obj);
            }

            foreach (var hero in HeroManager.Enemies)
            {
                if (!heroCache.ContainsKey(hero.NetworkId))
                    heroCache.Add(hero.NetworkId, hero);
            }
        }

        private static void IncrementTotalDamage(DamageInfo damageInfo, ref TotalDamageInfo totalDamage)
        {
            if (damageInfo.type == DamageType.Magical)
            {
                totalDamage.magicalDamage += damageInfo.damageValue;
            }
            else if (damageInfo.type == DamageType.Physical)
            {
                totalDamage.physicalDamage += damageInfo.damageValue;
            }
            else if (damageInfo.type == DamageType.True)
            {
                totalDamage.trueDamage += damageInfo.damageValue;
            }
            else
            {
                totalDamage.mixedDamage += damageInfo.damageValue;
            }

            totalDamage.totalDamage += damageInfo.damageValue;
            totalDamage.comboCount += 1;
        }

        private static void Hero_OnPlayAnimation(Obj_AI_Base obj, GameObjectPlayAnimationEventArgs args)
        {
            if (!obj.IsMe)
                return;

            /*for (int i = 1; i < 10; i++)
            {
                dmgCache.Add(new DamageInfo(i, "SomeDude" + i, 1, (DamageType)70));
                dmgCache.Add(new DamageInfo(i, "SomeDude" + i, 2, (DamageType)163));
                dmgCache.Add(new DamageInfo(i, "SomeDude" + i, 2, (DamageType)78));
            }

            myDmgCache.Add(new DamageInfo(10, "SomeDude", 1, (DamageType)70));
            myDmgCache.Add(new DamageInfo(10, "SomeDude", 2, (DamageType)163));
            myDmgCache.Add(new DamageInfo(10, "SomeDude", 2, (DamageType)78));
            RecalculateFinalDamage();*/

            if (!args.Animation.ToLower().Contains("death"))
                return;

            RecalculateFinalDamage(true);
        }

        private static void RecalculateFinalDamage(bool clearCache = false)
        {
            CleanDamageCache();
            totalDamageTaken.ResetDamage();
            totalDamageDealt.ResetDamage();

            finalDamage = new Dictionary<string, TotalDamageInfo>();
            foreach (var damageInfo in dmgCache)
            {
                TotalDamageInfo totalDamage;
                if (finalDamage.TryGetValue(damageInfo.sourceName, out totalDamage))
                {
                    IncrementTotalDamage(damageInfo, ref totalDamage);
                }
                else
                {
                    totalDamage = new TotalDamageInfo(damageInfo.sourceID, damageInfo.sourceName);
                    IncrementTotalDamage(damageInfo, ref totalDamage);
                    finalDamage.Add(damageInfo.sourceName, totalDamage);

                }
                IncrementTotalDamage(damageInfo, ref totalDamageTaken);
            }

            myFinalDamage = new Dictionary<int, TotalDamageInfo>();
            foreach (var damageInfo in myDmgCache)
            {
                TotalDamageInfo totalDamage;
                if (myFinalDamage.TryGetValue(damageInfo.sourceID, out totalDamage))
                {
                    IncrementTotalDamage(damageInfo, ref totalDamage);
                }
                else
                {
                    totalDamage = new TotalDamageInfo(damageInfo.sourceID, damageInfo.sourceName);
                    IncrementTotalDamage(damageInfo, ref totalDamage);
                    myFinalDamage.Add(damageInfo.sourceID, totalDamage);

                }
                IncrementTotalDamage(damageInfo, ref totalDamageDealt);
            }

            orderedFinalDamage = finalDamage.Values.OrderByDescending(value => value.totalDamage).ToList();
            orderedMyFinalDamage = myFinalDamage.Values.OrderByDescending(value => value.totalDamage).ToList();

            if (clearCache)
            {
                Utility.DelayAction.Add(5000, () => dmgCache.Clear());
                Utility.DelayAction.Add(5000, () => myDmgCache.Clear());
            }
        }

        private void DrawHpBar(float dmg, float targetMaxHp, string dmgTypeText, Color color,
            ref int hpPixelStart, ref string hpText, int hpBarPadding, int height, int hpBarHeight, int hpPixelMax, int hpBarLength)
        {
            if (hpPixelStart < hpPixelMax)
            {
                int percentHp = (int)(100 * dmg / targetMaxHp);
                string text = dmgTypeText + (int)dmg + " (" + percentHp + "%) ";
                int hpPixels = (int)((Math.Min(percentHp, 100) / 100f) * (hpBarLength - hpBarPadding * 2));

                if (hpPixels > 0)
                {
                    Drawing.DrawLine(hpPixelStart, height + hpBarPadding,
                        Math.Min(hpPixelMax, hpPixelStart + hpPixels), height + hpBarPadding, hpBarHeight - hpBarPadding * 2, color);
                }
                hpText += text;
                hpPixelStart += hpPixels;
            }
        }

        private static string GetDamageText(TotalDamageInfo totalDamageInfo, float targetMaxHp)
        {
            string text = "";
                        
            if (totalDamageInfo.physicalDamage > 0)
            {
                text += GetDamageSubText("Physical: ", totalDamageInfo.physicalDamage, targetMaxHp);
            }

            if (totalDamageInfo.magicalDamage > 0)
            {
                text += GetDamageSubText("Magical: ", totalDamageInfo.magicalDamage, targetMaxHp);
            }

            if (totalDamageInfo.mixedDamage > 0)
            {
                text += GetDamageSubText("Mixed: ", totalDamageInfo.mixedDamage, targetMaxHp);
            }

            if (totalDamageInfo.trueDamage > 0)
            {
                text += GetDamageSubText("True: ", totalDamageInfo.trueDamage, targetMaxHp);
            }

            return text;
        }

        private static string GetDamageSubText(string typeStr, float dmg, float targetMaxHp)
        {
            int percentHp = (int)(100 * dmg / targetMaxHp);
            return typeStr + " " + percentHp + "% ";
        }

        private void Drawing_OnDraw(EventArgs args)
        {

            if (menu.Item("ShowWhenDead").GetValue<bool>() && !myHero.IsDead
                && menu.Item("ShowRecapPress").GetValue<KeyBind>().Active == false)
            {
                return;
            }

            if (menu.Item("ShowRecap").GetValue<bool>() == false
                && menu.Item("ShowRecapPress").GetValue<KeyBind>().Active == false)
            {
                return;
            }

            int padding = 15;
            int hStart = (Drawing.Width / 2) - 500 + padding;
            int lineHeight = 15;
            int hpBarHeight = 50;
            int hpBarPadding = 2;
            int boxHeight = 525;
            int vStart = 200;

            if (Drawing.Height <= 1080)
            {
                vStart = (int)(vStart * ((float)Drawing.Height / 1080)); //150
                boxHeight = (int)(boxHeight * ((float)Drawing.Height / 1080));//Drawing.Height - 400;
                hpBarHeight = (int)(hpBarHeight * ((float)Drawing.Height / 1080));//25;
            }

            int height = vStart;
            int maxHeight = height + boxHeight;
            int damageInfoHeight = hpBarHeight + 20;


            Drawing.DrawLine(hStart - padding, height - padding,
                hStart + 1000 + padding, height - padding,
                boxHeight + padding * 2, Color.FromArgb(28, 28, 28));
            Drawing.DrawLine(hStart, height, hStart + 1000, height, boxHeight, Color.Black);

            Drawing.DrawText(hStart, height, Color.White, "Damage Taken: " + (int)totalDamageTaken.totalDamage
                + " (" + (int)(100 * totalDamageTaken.totalDamage / myHero.MaxHealth) + "%)");

            if (menu.Item("ShowDamageText").GetValue<bool>())
            {
                height += lineHeight;
                Drawing.DrawText(hStart, height, Color.White, GetDamageText(totalDamageTaken, myHero.MaxHealth));
            }
            
            height += 30;

            foreach (var totalDamageInfo in orderedFinalDamage)
            {
                string nameText = totalDamageInfo.sourceName + ": " + (int)totalDamageInfo.totalDamage
                    + " (" + (int)(100 * totalDamageInfo.totalDamage / myHero.MaxHealth) + "%) "
                    + totalDamageInfo.comboCount + "x";
                Drawing.DrawText(hStart, height, Color.White, nameText);

                int hpBarLength = 400;

                int hpPixelStart = hStart + hpBarPadding;
                int hpPixelMax = hStart + hpBarLength - hpBarPadding * 2;
                float targetMaxHp = myHero.MaxHealth;
                string hpText = "";

                height += lineHeight * 2;
                Drawing.DrawLine(hStart, height, hStart + hpBarLength, height, hpBarHeight, Color.FromArgb(51, 153, 255));
                Drawing.DrawLine(hStart + hpBarPadding, height + hpBarPadding,
                    hStart + hpBarLength - hpBarPadding, height + hpBarPadding, hpBarHeight - hpBarPadding * 2, Color.Gray);

                if (totalDamageInfo.physicalDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.physicalDamage, targetMaxHp, "Physical: ", Color.Red,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (totalDamageInfo.magicalDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.magicalDamage, targetMaxHp, "Magical: ", Color.Purple,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (totalDamageInfo.mixedDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.mixedDamage, targetMaxHp, "Mixed: ", Color.Red,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (totalDamageInfo.trueDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.trueDamage, targetMaxHp, "True: ", Color.White,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (menu.Item("ShowDamageText").GetValue<bool>())
                {
                    Drawing.DrawText(hStart, height - lineHeight, Color.White, hpText);
                }

                height += damageInfoHeight;

                if (height + damageInfoHeight > maxHeight)
                    break;
            }

            hStart += 500;
            height = vStart;

            Drawing.DrawText(hStart, height, Color.White, "Damage Dealt: " + (int)totalDamageDealt.totalDamage
                + " (" + (int)(100 * totalDamageDealt.totalDamage / myHero.MaxHealth) + "%)");

            if (menu.Item("ShowDamageText").GetValue<bool>())
            {
                height += lineHeight;
                Drawing.DrawText(hStart, height, Color.White, GetDamageText(totalDamageDealt, myHero.MaxHealth));
            }

            height += 30;

            foreach (var totalDamageInfo in orderedMyFinalDamage)
            {
                Obj_AI_Hero target;

                if (!heroCache.TryGetValue(totalDamageInfo.sourceID, out target))
                {
                    continue;
                }

                string nameText = totalDamageInfo.sourceName + ": " + (int)totalDamageInfo.totalDamage
                    + " (" + (int)(100 * totalDamageInfo.totalDamage / target.MaxHealth) + "%) "
                    + totalDamageInfo.comboCount + "x";
                Drawing.DrawText(hStart, height, Color.White, nameText);

                int hpBarLength = 400;

                int hpPixelStart = hStart + hpBarPadding;
                int hpPixelMax = hStart + hpBarLength - hpBarPadding * 2;
                float targetMaxHp = target.MaxHealth;
                string hpText = "";

                height += lineHeight * 2;
                Drawing.DrawLine(hStart, height, hStart + hpBarLength, height, hpBarHeight, Color.FromArgb(51, 153, 255));
                Drawing.DrawLine(hStart + hpBarPadding, height + hpBarPadding,
                    hStart + hpBarLength - hpBarPadding, height + hpBarPadding, hpBarHeight - hpBarPadding * 2, Color.Gray);


                if (totalDamageInfo.physicalDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.physicalDamage, targetMaxHp, "Physical: ", Color.Red,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (totalDamageInfo.magicalDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.magicalDamage, targetMaxHp, "Magical: ", Color.Purple,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (totalDamageInfo.mixedDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.mixedDamage, targetMaxHp, "Mixed: ", Color.Red,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (totalDamageInfo.trueDamage > 0)
                {
                    DrawHpBar(totalDamageInfo.trueDamage, targetMaxHp, "True: ", Color.White,
                        ref hpPixelStart, ref hpText, hpBarPadding, height, hpBarHeight, hpPixelMax, hpBarLength);
                }

                if (menu.Item("ShowDamageText").GetValue<bool>())
                {
                    Drawing.DrawText(hStart, height - lineHeight, Color.White, hpText);
                }

                height += damageInfoHeight;

                if (height + damageInfoHeight > maxHeight)
                    break;
            }
        }

        private static void GameObject_OnCreate(GameObject obj, EventArgs args)
        {
            if (obj.IsValid<Obj_AI_Base>())
            {
                Obj_AI_Base aiObj = obj as Obj_AI_Base;

                if (!objCache.ContainsKey(aiObj.NetworkId))
                    objCache.Add(aiObj.NetworkId, aiObj);
            }
        }

        private static void GameObject_OnDelete(GameObject obj, EventArgs args)
        {
            if (obj.IsValid<Obj_AI_Base>())
            {
                Obj_AI_Base aiObj = obj as Obj_AI_Base;
                objCache.Remove(aiObj.NetworkId);
            }
        }

        private static void Game_OnDamage(AttackableUnit unit, AttackableUnitDamageEventArgs args)
        {

            if (unit.IsMe)
            {
                if (args.HitType == DamageHitType.Dodge
                    || args.HitType == DamageHitType.Invulnerable
                    || args.HitType == DamageHitType.Miss)
                {
                    return;
                }

                if (DamageTypePhysical != 70 && (int)args.Type == 70)
                {
                    DamageTypePhysical = 70;
                }

                CleanDamageCache();

                Obj_AI_Base source;

                if (objCache.TryGetValue(args.SourceNetworkId, out source))
                {
                    if (source.IsValid<Obj_AI_Hero>())
                    {
                        var hero = source as Obj_AI_Hero;

                        dmgCache.Add(new DamageInfo(args.SourceNetworkId, hero.ChampionName, args.Damage, args.Type));
                    }
                    else
                    {
                        dmgCache.Add(new DamageInfo(args.SourceNetworkId, source.BaseSkinName, args.Damage, args.Type));
                    }

                    //Console.WriteLine(target.ChampionName + " : " + args.Damage);
                    //Console.WriteLine("DamageType: " + args.Type);

                    if (menu.Item("RecalculateOnDamage").GetValue<bool>())
                    {
                        RecalculateFinalDamage();
                    }
                }
            }
            else if (args.SourceNetworkId == myHero.NetworkId) //if myHero is attacking another hero
            {
                Obj_AI_Hero target;

                if (heroCache.TryGetValue(args.TargetNetworkId, out target))
                {
                    myDmgCache.Add(new DamageInfo(args.TargetNetworkId, target.ChampionName, args.Damage, args.Type));
                                        
                    if (menu.Item("RecalculateOnDamage").GetValue<bool>())
                    {
                        RecalculateFinalDamage();
                    }
                }
            }
        }

        private static void CleanDamageCache()
        {
            float curTime = Utils.TickCount;
            int timeFrame = 1000 * menu.Item("TimeFrame").GetValue<Slider>().Value;

            foreach (var damageInfo in dmgCache)
            {
                if (curTime - damageInfo.timestamp > timeFrame)
                {
                    Utility.DelayAction.Add(0, () => dmgCache.Remove(damageInfo));
                }
                else
                {
                    break;
                }
            }

            foreach (var damageInfo in myDmgCache)
            {
                if (curTime - damageInfo.timestamp > timeFrame)
                {
                    Utility.DelayAction.Add(0, () => myDmgCache.Remove(damageInfo));
                }
                else
                {
                    break;
                }
            }
        }

    }
}
