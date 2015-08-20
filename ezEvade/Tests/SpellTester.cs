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
    class SpellTester
    {
        public static Menu menu;
        public static Menu selectSpellMenu;

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        private static Dictionary<string, Dictionary<string, SpellData>> spellCache
                 = new Dictionary<string, Dictionary<string, SpellData>>();

        public static Vector3 spellStartPosition = myHero.ServerPosition;
        public static Vector3 spellEndPostion = myHero.ServerPosition
                              + (myHero.Direction.To2D().Perpendicular() * 500).To3D();

        public static float lastSpellFireTime = 0;

        public SpellTester()
        {
            menu = new Menu("Spell Tester", "DummySpellTester", true);

            selectSpellMenu = new Menu("Select Spell", "SelectSpellMenu");
            menu.AddSubMenu(selectSpellMenu);

            Menu setSpellPositionMenu = new Menu("Set Spell Position", "SetPositionMenu");
            setSpellPositionMenu.AddItem(new MenuItem("SetDummySpellStartPosition", "Set Start Position").SetValue(false));
            setSpellPositionMenu.AddItem(new MenuItem("SetDummySpellEndPosition", "Set End Position").SetValue(false));
            setSpellPositionMenu.Item("SetDummySpellStartPosition").ValueChanged += OnSpellStartChange;
            setSpellPositionMenu.Item("SetDummySpellEndPosition").ValueChanged += OnSpellEndChange;

            menu.AddSubMenu(setSpellPositionMenu);

            Menu fireDummySpellMenu = new Menu("Fire Dummy Spell", "FireDummySpellMenu");
            fireDummySpellMenu.AddItem(new MenuItem("FireDummySpell", "Fire Dummy Spell Key").SetValue(new KeyBind('O', KeyBindType.Press)));

            fireDummySpellMenu.AddItem(new MenuItem("SpellInterval", "Spell Interval").SetValue(new Slider(2500, 0, 5000)));

            menu.AddSubMenu(fireDummySpellMenu);

            menu.AddToMainMenu();

            LoadSpellDictionary();

            Game.OnUpdate += Game_OnGameUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            foreach (var spell in SpellDetector.drawSpells.Values)
            {
                Vector2 spellPos = spell.currentSpellPosition;

                if (spell.heroID == myHero.NetworkId)
                {
                    if (spell.spellType == SpellType.Line)
                    {
                        if (spellPos.Distance(myHero) <= myHero.BoundingRadius + spell.radius
                            && EvadeUtils.TickCount - spell.startTime > spell.info.spellDelay
                            && spell.startPos.Distance(myHero) < spell.info.range)
                        {
                            Draw.RenderObjects.Add(new Draw.RenderCircle(spellPos, 1000, Color.Red,
                                (int)spell.radius, 10));
                            DelayAction.Add(1, () => SpellDetector.DeleteSpell(spell.spellID));
                        }
                        else
                        {
                            Render.Circle.DrawCircle(new Vector3(spellPos.X, spellPos.Y, myHero.Position.Z), (int)spell.radius, Color.White, 5);
                        }
                    }
                    else if (spell.spellType == SpellType.Circular)
                    {
                        if (myHero.ServerPosition.To2D().InSkillShot(spell, myHero.BoundingRadius))
                        {

                        }
                    }
                }
            }
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            if (menu.Item("FireDummySpell").GetValue<KeyBind>().Active == true)
            {
                float interval = menu.Item("SpellInterval").GetValue<Slider>().Value;

                if (EvadeUtils.TickCount - lastSpellFireTime > interval)
                {
                    var charName = selectSpellMenu.Item("DummySpellHero").GetValue<StringList>().SelectedValue;
                    var spellName = selectSpellMenu.Item("DummySpellList").GetValue<StringList>().SelectedValue;
                    var spellData = spellCache[charName][spellName];

                    if (!ObjectCache.menuCache.cache.ContainsKey(spellName + "DodgeSpell"))
                    {
                        SpellDetector.LoadDummySpell(spellData);
                    }

                    SpellDetector.CreateSpellData(myHero, spellStartPosition, spellEndPostion, spellData);
                    lastSpellFireTime = EvadeUtils.TickCount;
                }
            }
        }

        private void OnSpellEndChange(object sender, OnValueChangeEventArgs e)
        {
            e.Process = false;

            spellEndPostion = myHero.ServerPosition;
            Draw.RenderObjects.Add(new Draw.RenderCircle(spellEndPostion.To2D(), 1000, Color.Red, 100, 20));
        }

        private void OnSpellStartChange(object sender, OnValueChangeEventArgs e)
        {
            e.Process = false;

            spellStartPosition = myHero.ServerPosition;
            Draw.RenderObjects.Add(new Draw.RenderCircle(spellStartPosition.To2D(), 1000, Color.Red, 100, 20));
        }

        private void LoadSpellDictionary()
        {
            foreach (var spell in SpellDatabase.Spells)
            {
                if (spellCache.ContainsKey(spell.charName))
                {
                    var spellList = spellCache[spell.charName];
                    if (spellList != null && !spellList.ContainsKey(spell.spellName))
                    {
                        spellList.Add(spell.spellName, spell);
                    }
                }
                else
                {
                    spellCache.Add(spell.charName, new Dictionary<string, SpellData>());
                    var spellList = spellCache[spell.charName];
                    if (spellList != null && !spellList.ContainsKey(spell.spellName))
                    {
                        spellList.Add(spell.spellName, spell);
                    }
                }
            }

            selectSpellMenu.AddItem(new MenuItem("DummySpellDescription", "    -- Select A Dummy Spell To Fire --    "));

            var heroList = spellCache.Keys.ToArray();
            selectSpellMenu.AddItem(new MenuItem("DummySpellHero", "Hero")
               .SetValue(new StringList(heroList, 0)));

            var selectedHeroStr = selectSpellMenu.Item("DummySpellHero").GetValue<StringList>().SelectedValue;
            var selectedHero = spellCache[selectedHeroStr];
            var selectedHeroList = selectedHero.Keys.ToArray();

            selectSpellMenu.AddItem(new MenuItem("DummySpellList", "Spell")
               .SetValue(new StringList(selectedHeroList, 0)));

            selectSpellMenu.Item("DummySpellHero").ValueChanged += OnSpellHeroChange;
        }

        private void OnSpellHeroChange(object sender, OnValueChangeEventArgs e)
        {
            //var previousHeroStr = e.GetOldValue<StringList>().SelectedValue;
            var selectedHeroStr = e.GetNewValue<StringList>().SelectedValue;
            var selectedHero = spellCache[selectedHeroStr];
            var selectedHeroList = selectedHero.Keys.ToArray();

            selectSpellMenu.Item("DummySpellList").SetValue(new StringList(selectedHeroList, 0));
        }
    }
}
