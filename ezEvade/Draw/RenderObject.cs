using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade.Draw
{
    abstract class RenderObject
    {
        public float endTime = 0;
        public float startTime = 0;

        abstract public void Draw();
    }

    class RenderObjects
    {
        private static List<RenderObject> objects = new List<RenderObject>();

        static RenderObjects()
        {
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            Render();
        }

        private static void Render()
        {
            foreach (RenderObject obj in objects)
            {                
                if (obj.endTime - EvadeUtils.TickCount > 0)
                {
                    obj.Draw(); //weird after draw
                }
                else
                {
                    DelayAction.Add(1, () => objects.Remove(obj));
                }
            }
        }

        public static void Add(RenderObject obj)
        {
            objects.Add(obj);
        }
    }
}
