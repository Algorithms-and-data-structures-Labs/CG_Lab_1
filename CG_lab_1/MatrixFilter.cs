using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class MatrixFilter:Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }

        public MatrixFilter(float[,] kernel)
        { 
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourcelmage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourcelmage.Width - 1);
                    int idY = Clamp(y + 1, 0, sourcelmage.Height - 1);
                    Color neighborColor = sourcelmage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, 1 + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, 1 + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, 1 + radiusY];
                }
            return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
        }


    }
}
