using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class EmbossFilter : MatrixFilter
    {
        public EmbossFilter()
        {
            kernel = new float[,]
            {
            { 0, 1, 0 },
            { 1, 0, -1 },
            { 0, -1, 0 }
            };
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color resultColor = base.calculateNewPixelColor(sourceImage, x, y);
            int newR = 255 - resultColor.R;
            int newG = 255 - resultColor.G;
            int newB = 255 - resultColor.B;

            return Color.FromArgb(newR, newG, newB);
        }
    }
}
