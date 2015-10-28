using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using LeagueSharp;
using UtilityPlus.Properties;

using System.Collections;
using System.Globalization;
using System.Resources;

using LeagueSharp.Common;

namespace UtilityPlus.SpellTracker
{
    public class ImageLoader
    {
        private static float iconOpacity = 1f;
        public static int imageHeight = 16;
        public static int imageWidth = 16;

        static ImageLoader()
        {
            Resources.ResourceManager.IgnoreCase = true;

            ResourceSet resourceSet = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key as string;
                object resource = entry.Value;

                if (resource.GetType() == typeof(Bitmap)
                    && resourceKey != null)
                {
                    Load(resourceKey);
                }
            }

        }

        public static Bitmap Load(string spellName)
        {
            string cachedPath = GetCachedPath(spellName);
            if (File.Exists(cachedPath))
            {
                return new Bitmap(cachedPath);
            }

            var bitmap = Resources.ResourceManager.GetObject(spellName) as Bitmap;
            if (bitmap == null)
            {
                Console.WriteLine(spellName + ".png not found.");
                return CreateFinalImage(Resources.Default);
            }
            Bitmap finalBitmap = CreateFinalImage(bitmap);
            //finalBitmap = ChangeOpacity(finalBitmap);
            finalBitmap.Save(cachedPath);

            return finalBitmap;
        }

        private static string GetCachedPath(string championName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UtilityPlusCache");
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

        private static Bitmap CreateFinalImage(Bitmap srcBitmap)
        {
            float scale = 0.25f;
            int scaledSize = (int)(scale * srcBitmap.Width);

            Bitmap resized = new Bitmap(srcBitmap, new Size(imageWidth, imageHeight));

            srcBitmap.Dispose();
            return resized;
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