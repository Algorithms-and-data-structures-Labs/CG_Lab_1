using System;
using System.Drawing;

namespace CG_lab_1
{
    internal class GradientFilter : Filters
    {
        private readonly double[,] kernel;

        public GradientFilter(double[,] selectedKernel)
        {
            this.kernel = selectedKernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int gradientValue = CalculateGradient(sourceImage, x, y);
            return Color.FromArgb(Clamp(gradientValue, 0, 255), Clamp(gradientValue, 0, 255), Clamp(gradientValue, 0, 255));
        }

        private int CalculateGradient(Bitmap sourceImage, int x, int y)
        {
            int maxGradient = 0;
            int minGradient = 255;
            int kernelSize = kernel.GetLength(0);

            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    double kernelValue = kernel[l + 1, k + 1];
                    int gradient = (int)(neighborColor.R * kernelValue);
                    maxGradient = Math.Max(maxGradient, gradient);
                    minGradient = Math.Min(minGradient, gradient);
                }
            }

            return maxGradient - minGradient;
        }
    }
}
