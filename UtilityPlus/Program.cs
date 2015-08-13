using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus
{
    class Program
    {
        private static SpellTracker.Tracker spellTracker;
        private static WardTracker.Tracker wardTracker;
        private static TowerRange.RangeIndicator towerRange;

        private static Menu menu;

        static void Main(string[] args)
        {
            LoadAssembly();
        }

        private static void LoadAssembly()
        {
            DelayAction.Add(0, () =>
            {
                if (LeagueSharp.Game.Mode == GameMode.Running)
                {
                    Game_OnGameLoad(new EventArgs());
                }
                else
                {
                    Game.OnStart += Game_OnGameLoad;
                }
            });
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            menu = new Menu("Utility+", "UtilityPlus", true);

            Menu pluginMenu = new Menu("PluginList", "PluginList");            
            pluginMenu.AddItem(new MenuItem("LoadSpellTracker", "Load SpellTracker Plugin").SetValue(true));
            pluginMenu.AddItem(new MenuItem("LoadWardTracker", "Load WardTracker Plugin").SetValue(true));
            pluginMenu.AddItem(new MenuItem("LoadTowerRange", "Load TowerRange Plugin").SetValue(true));
            pluginMenu.AddItem(new MenuItem("LoadPluginDescription", "    -- Reload to take effect --"));

            /*pluginMenu.Item("LoadSpellTracker").ValueChanged += OnLoadSpellTrackerChange;
            pluginMenu.Item("LoadWardTracker").ValueChanged += OnLoadWardTrackerChange;
            pluginMenu.Item("LoadTowerRange").ValueChanged += OnLoadTowerRangeChange;*/

            menu.AddSubMenu(pluginMenu);
            menu.AddToMainMenu();

            if (menu.Item("LoadSpellTracker").GetValue<bool>())
            {
                spellTracker = new SpellTracker.Tracker(menu);
            }

            if (menu.Item("LoadWardTracker").GetValue<bool>())
            {
                wardTracker = new WardTracker.Tracker(menu);
            }

            if (menu.Item("LoadTowerRange").GetValue<bool>())
            {
                towerRange = new TowerRange.RangeIndicator(menu);
            }
        }
    }
}
