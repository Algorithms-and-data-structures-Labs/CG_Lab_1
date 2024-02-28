using System;
using System.Collections.Generic;
using System.Drawing;

namespace CG_lab_1
{
    internal class BlackHatFilter : Filters
    {
        private readonly double[,] kernel;

        public BlackHatFilter(double[,] selectedKernel)
        {
            this.kernel = selectedKernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int closingResult = ClosingOperation(sourceImage, x, y);
            int blackHatValue = closingResult - sourceImage.GetPixel(x, y).R;
            return Color.FromArgb(Clamp(blackHatValue, 0, 255), Clamp(blackHatValue, 0, 255), Clamp(blackHatValue, 0, 255));
        }

        private int ClosingOperation(Bitmap sourceImage, int x, int y)
        {
            int result = 0;
            int kernelSize = kernel.GetLength(0);

            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    double kernelValue = kernel[l + 1, k + 1];
                    result += (int)(neighborColor.R * kernelValue);
                }
            }

            return Clamp(result, 0, 255);
        }
    }
}
