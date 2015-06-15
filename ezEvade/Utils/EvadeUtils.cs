using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace ezEvade
{
    public static class EvadeUtils
    {
        private static Stopwatch stopWatch = Stopwatch.StartNew();

        static EvadeUtils()
        {

        }

        public static float TickCount
        {
            get
            {
                return stopWatch.ElapsedMilliseconds;
            }
        }
    }
}
