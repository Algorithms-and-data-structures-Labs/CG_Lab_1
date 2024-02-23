using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class WavesFilter:Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newX = Clamp((int)(x + 20 * Math.Sin(2 * Math.PI * y / 30)), 0, sourceImage.Width - 1);
            int newY = y;
            return sourceImage.GetPixel(newX, newY); ;
        }
    }
}
