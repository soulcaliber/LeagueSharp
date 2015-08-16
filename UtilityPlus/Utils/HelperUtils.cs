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
    }
}
