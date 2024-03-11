using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class IdealReflectionFilter : Filters
    {
        private int max_R = 0;
        private int max_G = 0;
        private int max_B = 0;

        private bool maxValuesFound = false;

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            if (!maxValuesFound)
            {
                FindMaxValues(sourceImage);
                maxValuesFound = true;
            }

            Color sourceColor = sourceImage.GetPixel(x, y);

            int newR = (int)(255 * sourceColor.R / (float)max_R);
            int newG = (int)(255 * sourceColor.G / (float)max_G);
            int newB = (int)(255 * sourceColor.B / (float)max_B);

            newR = Clamp(newR, 0, 255);
            newG = Clamp(newG, 0, 255);
            newB = Clamp(newB, 0, 255);

            return Color.FromArgb(newR, newG, newB);
        }
        private void FindMaxValues(Bitmap sourceImage)
        {
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pixelColor = sourceImage.GetPixel(i, j);
                    max_R = Math.Max(max_R, pixelColor.R);
                    max_G = Math.Max(max_G, pixelColor.G);
                    max_B = Math.Max(max_B, pixelColor.B);
                }
            }
        }
    }
}
