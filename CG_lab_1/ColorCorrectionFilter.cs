using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class ColorCorrectionFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color referenceColor=Color.Red;
            //Color referenceColor=Color.Green;
            //Color referenceColor=Color.Blue;
            int deltaR = sourceColor.R - referenceColor.R;
            int deltaG = sourceColor.G - referenceColor.G;
            int deltaB = sourceColor.B - referenceColor.B;
            int newR = Clamp(sourceColor.R + deltaR, 0, 255);
            int newG = Clamp(sourceColor.G + deltaG, 0, 255);
            int newB = Clamp(sourceColor.B + deltaB, 0, 255);

            return Color.FromArgb(newR, newG, newB);
        }
    }
}
