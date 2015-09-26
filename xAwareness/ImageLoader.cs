using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using LeagueSharp;
using xAwareness.Properties;

using LeagueSharp.Common;

namespace xAwareness
{
    public class ImageLoader
    {
        public static float iconOpacity = ExtendedAwareness.menu.Item("IconOpacity").GetValue<Slider>().Value / 100.0f;
        public static int arrowOpacity = (int)Math.Min(iconOpacity * 255, 150);

        public static Bitmap arrowEnemyImg = ImageLoader.CreateArrow(120, Color.FromArgb(arrowOpacity, 255, 0, 0));
        public static Bitmap arrowAllyImg = ImageLoader.CreateArrow(120, Color.FromArgb(arrowOpacity, 0, 255, 0));

        public static Bitmap circleEnemyImg = ImageLoader.CreateCircle(120, Color.FromArgb(arrowOpacity, 255, 0, 0), 10);
        public static Bitmap circleAllyImg = ImageLoader.CreateCircle(120, Color.FromArgb(arrowOpacity, 0, 255, 0), 10);

        public static Bitmap Load(string championName)
        {
            string cachedPath = GetCachedPath(championName);
            if (File.Exists(cachedPath))
            {
                return ChangeOpacity(new Bitmap(cachedPath));
            }
            var bitmap = Resources.ResourceManager.GetObject(championName + "_Square_0") as Bitmap;
            if (bitmap == null)
            {
                Console.WriteLine(championName + "_Square_0.png not found.");
                return ChangeOpacity(CreateFinalImage(Resources.Default));
            }
            Bitmap finalBitmap = CreateFinalImage(bitmap);
            finalBitmap.Save(cachedPath);
            return ChangeOpacity(finalBitmap);
        }

        private static string GetCachedPath(string championName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xAwarenessCache");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, Game.Version);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return Path.Combine(path, championName + ".png");
        }

        public static Bitmap CreateCircle(int width, Color color, int padding)
        {
            var img = new Bitmap(width, width);

            SolidBrush opaqueBrush = new SolidBrush(Color.FromArgb(0, 255, 255, 255));
            SolidBrush redBrush = new SolidBrush(color);

            using (Graphics g = Graphics.FromImage(img))
            {
                var p = new Pen(color, padding) { Alignment = PenAlignment.Inset };
                g.DrawEllipse(p, 0, 0, width, width);
            }

            return img;
        }

        public static Bitmap CreateArrow(int width, Color color)
        {
            var img = new Bitmap(width / 2, width / 2);

            SolidBrush opaqueBrush = new SolidBrush(Color.FromArgb(0, 255, 255, 255));

            SolidBrush redBrush = new SolidBrush(color);
            
            using (Graphics g = Graphics.FromImage(img))
            {
                g.CompositingMode = CompositingMode.SourceCopy;

                g.FillRectangle(redBrush, 0, 0, width / 2, width / 2);
                g.FillPie(opaqueBrush, -width / 2, -width / 2, width, width, 0, 90);
            }

            return img;
        }

        private static Bitmap CreateFinalImage(Bitmap srcBitmap)
        {
            var img = new Bitmap(srcBitmap.Width, srcBitmap.Width);
            var cropRect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Width);

            using (Bitmap sourceImage = srcBitmap)
            {
                using (Bitmap croppedImage = sourceImage.Clone(cropRect, sourceImage.PixelFormat))
                {
                    using (var tb = new TextureBrush(croppedImage))
                    {
                        using (Graphics g = Graphics.FromImage(img))
                        {
                            g.FillEllipse(tb, 0, 0, srcBitmap.Width, srcBitmap.Width);
                        }
                    }
                }
            }
            srcBitmap.Dispose();
            return img;
        }

        private static Bitmap ChangeOpacity(Bitmap img)
        {            
            var bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            var colormatrix = new ColorMatrix { Matrix33 = iconOpacity };
            var imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(
                img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel,
                imgAttribute);
            graphics.Dispose(); // Releasing all resource used by graphics
            img.Dispose();
            return bmp;
        }
    }
}