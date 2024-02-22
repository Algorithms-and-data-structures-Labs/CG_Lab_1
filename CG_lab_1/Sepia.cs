using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class Sepia:Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intencity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B;
            int k = 35;
            Color resultColor = Color.FromArgb(Clamp((int)Intencity + k*2, 0, 255), Clamp((int)Intencity + (int)k*(1/2),0 , 255), Clamp((int)Intencity - 1*k, 0, 255));
            return resultColor;
        }
    }
}
