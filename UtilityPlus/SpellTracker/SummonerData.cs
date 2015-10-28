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
    class SummonerData
    {
        public static Dictionary<int, Color> heroS1Color = new Dictionary<int, Color>();
        public static Dictionary<int, Color> heroS2Color = new Dictionary<int, Color>();

        public static Dictionary<int, SummonerSpellTracker> heroS1Tracker = new Dictionary<int, SummonerSpellTracker>();
        public static Dictionary<int, SummonerSpellTracker> heroS2Tracker = new Dictionary<int, SummonerSpellTracker>();

        static SummonerData()
        {
            //LoadSummonerSpell();
        }

        public static void LoadSummonerSpell()
        {
            foreach (var hero in HeroManager.AllHeroes)
            {
                var spell1 = hero.Spellbook.GetSpell(SpellSlot.Summoner1);
                var spell2 = hero.Spellbook.GetSpell(SpellSlot.Summoner2);

                heroS1Color.Add(hero.NetworkId, GetSummonerColor(spell1.Name));
                heroS2Color.Add(hero.NetworkId, GetSummonerColor(spell2.Name));

                if (Tracker.useSummonerIcons && !hero.IsMe)
                {
                    heroS1Tracker.Add(hero.NetworkId, new SummonerSpellTracker(hero, spell1.Name));
                    heroS2Tracker.Add(hero.NetworkId, new SummonerSpellTracker(hero, spell2.Name, false));
                }                
            }
        }

        public static Color GetSummonerColor(string name)
        {
            Color color;
                        
            //Game.PrintChat(name);

            switch (name.ToLower())
            {
                case "summonerbarrier":
                    color = Color.SandyBrown;
                    break;
                case "summonersnowball":
                    color = Color.White;
                    break;
                case "summonerodingarrison":
                    color = Color.Green;
                    break;
                case "summonerclairvoyance":
                    color = Color.Blue;
                    break;
                case "summonerboost": //cleanse
                    color = Color.LightBlue;
                    break;
                case "summonermana":
                    color = Color.Blue;
                    break;
                case "summonerteleport":
                    color = Color.Purple;
                    break;
                case "summonerheal":
                    color = Color.GreenYellow;
                    break;
                case "summonerexhaust":
                    color = Color.Brown;
                    break;
                case "summonersmite":
                    color = Color.Orange;
                    break;
                case "summonerdot":
                    color = Color.Red;
                    break;
                case "summonerhaste":
                    color = Color.SkyBlue;
                    break;
                case "summonerflash":
                    color = Color.Yellow;
                    break;
                case "s5_summonersmiteduel":
                    color = Color.Orange;
                    break;
                case "s5_summonersmiteplayerganker":
                    color = Color.Orange;
                    break;
                case "s5_summonersmitequick":
                    color = Color.Orange;
                    break;
                case "itemsmiteaoe":
                    color = Color.Orange;
                    break;
                default:
                    color = Color.White;
                    break;
            }

            return color;
        }
    }
}
