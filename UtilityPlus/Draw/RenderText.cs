using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.Draw
{
    class RenderText : RenderObject
    {
        public Vector2 renderPosition = new Vector2(0, 0);
        public string text = "";

        public Color color = Color.White;

        public RenderText(string text, Vector2 renderPosition, float renderTime)
        {
            this.startTime = HelperUtils.TickCount;
            this.endTime = this.startTime + renderTime;
            this.renderPosition = renderPosition;

            this.text = text;
        }

        public RenderText(string text, Vector2 renderPosition, float renderTime,
            Color color)
        {
            this.startTime = HelperUtils.TickCount;
            this.endTime = this.startTime + renderTime;
            this.renderPosition = renderPosition;

            this.color = color;

            this.text = text;
        }

        override public void Draw()
        {
            if (renderPosition.IsOnScreen())
            {
                var textDimension = TextUtils.GetTextExtent(text);
                var wardScreenPos = Drawing.WorldToScreen(renderPosition.To3D());

                TextUtils.DrawText(wardScreenPos.X - textDimension.Width / 2, wardScreenPos.Y, color, text);
            }
        }
    }
}
