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
    public static class HelperUtils
    {
        public static Random random = new Random(DateTime.Now.Millisecond);
        private static DateTime assemblyLoadTime = DateTime.Now;

        static HelperUtils()
        {

        }

        public static float TickCount
        {
            get
            {
                return (int)DateTime.Now.Subtract(assemblyLoadTime).TotalMilliseconds;
            }
        }

        public static string FormatSpellTime(float time)
        {
            int seconds = (int)Math.Ceiling(time / 1000);

            if (seconds > 60)
            {
                int minutes = (int)Math.Ceiling(seconds / 60.0f);

                if (minutes > 60)
                {
                    return "N/A";
                }
                else
                {
                    return minutes + "m";
                }
            }else{
                return seconds.ToString();
            }
        }
    }
}
