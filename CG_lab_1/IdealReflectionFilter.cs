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
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int centerX = sourceImage.Width / 2;
            int centerY = sourceImage.Height / 2;
            int reflectedX = centerX - (x - centerX);
            int reflectedY = centerY - (y - centerY);
            reflectedX = Clamp(reflectedX, 0, sourceImage.Width - 1);
            reflectedY = Clamp(reflectedY, 0, sourceImage.Height - 1);

            return sourceImage.GetPixel(reflectedX, reflectedY);
        }
    }
}
