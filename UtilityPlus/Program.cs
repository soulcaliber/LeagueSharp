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
            menu.AddToMainMenu();

            spellTracker = new SpellTracker.Tracker(menu);
            wardTracker = new WardTracker.Tracker(menu);
        }
    }
}
