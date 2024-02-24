using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class TranslationFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newX = x + 50;
            int newY = y;
            if (newX >= 0 && newX < sourceImage.Width && newY >= 0 && newY < sourceImage.Height)
            {
                return sourceImage.GetPixel(newX, newY);
            }
            else
            {
                return Color.Black;
            }
        }
    }
}
