using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Color = System.Drawing.Color;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace UtilityPlus.Tests
{
    class RenderPosition
    {
        public float renderEndTime = 0;
        public Vector2 renderPosition = new Vector2(0, 0);

        public int radius = 65;
        public int width = 5;
        public Color color = Color.White;

        public RenderPosition(Vector2 renderPosition, float renderTime,
            int radius = 65, int width = 5)
        {
            this.renderEndTime = HelperUtils.TickCount + renderTime;
            this.renderPosition = renderPosition;

            this.radius = radius;
            this.width = width;
        }

        public RenderPosition(Vector2 renderPosition, float renderTime,
            Color color, int radius = 65, int width = 5)
        {
            this.renderEndTime = HelperUtils.TickCount + renderTime;
            this.renderPosition = renderPosition;

            this.color = color;

            this.radius = radius;
            this.width = width;
        }

        public void Draw()
        {
            if (renderPosition.IsOnScreen())
            {
                Render.Circle.DrawCircle(renderPosition.To3D(), radius, color, width);
            }
        }
    }

    class RenderText
    {
        public float renderEndTime = 0;
        public Vector2 renderPosition = new Vector2(0, 0);
        public string text = "";

        public Color color = Color.White;

        public RenderText(string text, Vector2 renderPosition, float renderTime)
        {
            this.renderEndTime = HelperUtils.TickCount + renderTime;
            this.renderPosition = renderPosition;

            this.text = text;
        }

        public RenderText(string text, Vector2 renderPosition, float renderTime,
            Color color)
        {
            this.renderEndTime = HelperUtils.TickCount + renderTime;
            this.renderPosition = renderPosition;

            this.color = color;

            this.text = text;
        }

        public void Draw()
        {
            if (renderPosition.IsOnScreen())
            {
                var textDimension = Drawing.GetTextExtent(text);
                var wardScreenPos = Drawing.WorldToScreen(renderPosition.To3D());

                Drawing.DrawText(wardScreenPos.X - textDimension.Width / 2, wardScreenPos.Y, color, text);
            }
        }
    }

    class TestDrawer
    {
        private static List<RenderPosition> renderPositions = new List<RenderPosition>();
        private static List<RenderText> renderTexts = new List<RenderText>();

        static TestDrawer()
        {
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            RenderTestCircles();
            RenderTestTexts();
        }

        private static void RenderTestTexts()
        {
            foreach (RenderText rendPos in renderTexts)
            {
                if (rendPos.renderEndTime - HelperUtils.TickCount > 0)
                {
                    rendPos.Draw();
                }
                else
                {
                    DelayAction.Add(1, () => renderTexts.Remove(rendPos));
                }
            }
        }

        private static void RenderTestCircles()
        {
            foreach (RenderPosition rendPos in renderPositions)
            {
                if (rendPos.renderEndTime - HelperUtils.TickCount > 0)
                {
                    rendPos.Draw();
                }
                else
                {
                    DelayAction.Add(1, () => renderPositions.Remove(rendPos));
                }
            }
        }

        public static void AddTestText(string text, Vector3 position, float renderTime)
        {
            renderTexts.Add(new RenderText(text, position.To2D(), renderTime));
        }

        public static void AddTestText(string text, Vector3 position, float renderTime,
            Color color, int radius = 65, int width = 5)
        {
            renderTexts.Add(new RenderText(text, position.To2D(), renderTime, color));
        }

        public static void AddTestText(string text, Vector2 position, float renderTime)
        {
            renderTexts.Add(new RenderText(text, position, renderTime));
        }

        public static void AddTestText(string text, Vector2 position, float renderTime,
            Color color, int radius = 65, int width = 5)
        {
            renderTexts.Add(new RenderText(text, position, renderTime, color));
        }

        public static void AddTestCircle(Vector3 position, float renderTime,
            int radius = 65, int width = 5)
        {
            renderPositions.Add(new RenderPosition(position.To2D(), renderTime,
            radius, width));
        }

        public static void AddTestCircle(Vector3 position, float renderTime,
            Color color, int radius = 65, int width = 5)
        {
            renderPositions.Add(new RenderPosition(position.To2D(), renderTime,
            color, radius, width));
        }

        public static void AddTestCircle(Vector2 position, float renderTime,
            int radius = 65, int width = 5)
        {
            renderPositions.Add(new RenderPosition(position, renderTime,
            radius, width));
        }

        public static void AddTestCircle(Vector2 position, float renderTime,
            Color color, int radius = 65, int width = 5)
        {
            renderPositions.Add(new RenderPosition(position, renderTime,
            color, radius, width));
        }
    }
}
