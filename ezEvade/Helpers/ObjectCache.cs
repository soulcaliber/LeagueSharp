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
    public class HeroInfo
    {
        public Obj_AI_Hero hero;
        public Vector2 serverPos2D;
        public float boundingRadius;
        public float moveSpeed;

        public HeroInfo(Obj_AI_Hero hero)
        {
            this.hero = hero;
            Game.OnUpdate += Game_OnGameUpdate;
        }

        private void Game_OnGameUpdate(EventArgs args)
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            serverPos2D = hero.ServerPosition.To2D();
            boundingRadius = hero.BoundingRadius;
            moveSpeed = hero.MoveSpeed;
        }
    }

    public class MenuCache
    {
        public Menu menu;
        public Dictionary<string, MenuItem> cache = new Dictionary<string, MenuItem>();

        public MenuCache(Menu menu)
        {
            this.menu = menu;

            foreach (var item in ReturnAllItems(menu))
            {
                cache.Add(item.Name, item);
            }
        }

        public static List<MenuItem> ReturnAllItems(Menu menu)
        {
            List<MenuItem> menuList = new List<MenuItem>();

            menuList.AddRange(menu.Items);

            foreach (var submenu in menu.Children)
            {
                menuList.AddRange(ReturnAllItems(submenu));
            }

            return menuList;
        }
    }

    public static class ObjectCache
    {
        public static Dictionary<int, Obj_AI_Turret> turrets = new Dictionary<int, Obj_AI_Turret>();

        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }

        public static HeroInfo myHeroCache = new HeroInfo(myHero);
        public static MenuCache menuCache = new MenuCache(Evade.menu);

        public static float gamePing = 0;

        static ObjectCache()
        {
            InitializeCache();
            Game.OnUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            gamePing = Game.Ping;
        }

        private static void InitializeCache()
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Turret>())
            {
                if (!turrets.ContainsKey(obj.NetworkId))
                {
                    turrets.Add(obj.NetworkId, obj);
                }
            }
        }
    }
}
