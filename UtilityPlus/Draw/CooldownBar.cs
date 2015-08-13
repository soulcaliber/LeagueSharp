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
    class CooldownBar : RenderObject
    {
        public float duration = 0;
        public Obj_AI_Base obj;
        public Vector2 position = Vector2.Zero;
        public float extraHeight = 0;

        public CooldownBar(Vector2 position, float duration, float extraHeight = 0, float startTime = 0)
        {
            this.startTime = startTime == 0 ? HelperUtils.TickCount : startTime;
            this.endTime = this.startTime + duration;
            this.position = position;

            this.duration = duration;
            this.extraHeight = extraHeight;
        }

        public CooldownBar(Obj_AI_Base obj, float duration, float extraHeight = 0, float startTime = 0)
        {
            this.startTime = startTime == 0 ? HelperUtils.TickCount : startTime;
            this.endTime = this.startTime + duration;
            this.obj = obj;

            this.duration = duration;
            this.extraHeight = extraHeight;
        }

        override public void Draw()
        {
            var pos = position != Vector2.Zero ? position : obj.Position.To2D();

            if (pos.IsOnScreen())
            {                
                pos = Drawing.WorldToScreen(pos.To3D());
                pos.X -= 25;

                var percent = (endTime - HelperUtils.TickCount) / duration;

                if (percent > 0)
                {
                    Drawing.DrawLine(new Vector2(pos.X - 1, pos.Y - 1 + extraHeight),
                                 new Vector2(pos.X + 1 + 50 * percent, pos.Y - 1 + extraHeight),
                                 7, Color.Black);

                    Drawing.DrawLine(new Vector2(pos.X, pos.Y + extraHeight),
                                     new Vector2(pos.X + 50 * percent, pos.Y + extraHeight),
                                     5, Color.LightGray);
                }                
            }
        }
    }
        
}
