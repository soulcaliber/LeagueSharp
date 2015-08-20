using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade.Draw
{
    class RenderLine : RenderObject
    {
        public Vector2 start = new Vector2(0, 0);
        public Vector2 end = new Vector2(0, 0);

        public int width = 3;
        public Color color = Color.White;

        public RenderLine(Vector2 start, Vector2 end, float renderTime,
            int radius = 65, int width = 3)
        {
            this.startTime = EvadeUtils.TickCount;
            this.endTime = this.startTime + renderTime;
            this.start = start;
            this.end = end;

            this.width = width;
        }

        public RenderLine(Vector2 start, Vector2 end, float renderTime,
            Color color, int radius = 65, int width = 3)
        {
            this.startTime = EvadeUtils.TickCount;
            this.endTime = this.startTime + renderTime;
            this.start = start;
            this.end = end;

            this.color = color;

            this.width = width;
        }

        override public void Draw()
        {
            if (start.IsOnScreen() || end.IsOnScreen())
            {
                var realStart = Drawing.WorldToScreen(start.To3D());
                var realEnd = Drawing.WorldToScreen(end.To3D());

                Drawing.DrawLine(realStart, realEnd, width, color);
            }
        }
    }
}
