using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace xAwareness
{
    class TextUtils
    {
        static TextUtils()
        {

        }

        public static void DrawText(float x, float y, Color c, string text)
        {
            if (text != null)
            {
                Drawing.DrawText(x, y, c, text);
            }
        }

        public static System.Drawing.Size GetTextExtent(string text)
        {
            if (text != null)
            {
                return Drawing.GetTextExtent(text);
            }
            else
            {
                return Drawing.GetTextExtent("A");
            }
        }

        public static string FormatTime(double time)
        {
            if (time > 0)
            {
                return Utils.FormatTime(time);
            }
            else
            {
                return "00:00";
            }
        }
    }
}
