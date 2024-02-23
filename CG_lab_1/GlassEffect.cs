using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class GlassEffect:Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Random Rand = new Random();
            int newX = Clamp((int)(x + (Rand.NextDouble() - 0.5) * 10.0), 0, sourceImage.Width - 1);
            int newY = Clamp((int)(y + (Rand.NextDouble() - 0.5) * 10.0), 0, sourceImage.Height - 1);
            return sourceImage.GetPixel(newX, newY); ;
        }
    }
}
