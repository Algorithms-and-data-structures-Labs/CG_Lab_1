using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class GlowEdgeFilter : MatrixFilter
    {
        public GlowEdgeFilter() : base(CreateKernel())
        {
        }

        private static float[,] CreateKernel()
        {
            return new float[,]
            {
            { -1, -1, -1 },
            { -1,  8, -1 },
            { -1, -1, -1 }
            };
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            }
            resultR = Clamp((int)(resultR + 128), 0, 255);
            resultG = Clamp((int)(resultG + 128), 0, 255);
            resultB = Clamp((int)(resultB + 128), 0, 255);

            return Color.FromArgb((int)resultR, (int)resultG, (int)resultB);
        }
    }
}
