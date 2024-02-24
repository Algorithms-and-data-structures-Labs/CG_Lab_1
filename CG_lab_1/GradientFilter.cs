using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class GradientFilter : Filters
    {
        private readonly int[,] dilationKernel = new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
        private readonly int[,] erosionKernel = new int[,] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int dilationResult = DilationOperation(sourceImage, x, y);
            int erosionResult = ErosionOperation(sourceImage, x, y);
            int gradValue = dilationResult - erosionResult;
            return Color.FromArgb(Clamp(gradValue, 0, 255), Clamp(gradValue, 0, 255), Clamp(gradValue, 0, 255));
        }
        private int DilationOperation(Bitmap sourceImage, int x, int y)
        {
            int result = 0;
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int kernelValue = dilationKernel[k + 1, l + 1];
                    result = Math.Max(result, neighborColor.R + kernelValue);
                }
            }
            return result;
        }
        private int ErosionOperation(Bitmap sourceImage, int x, int y)
        {
            int result = 255;
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int kernelValue = erosionKernel[k + 1, l + 1];
                    result = Math.Min(result, neighborColor.R - kernelValue);
                }
            }
            return result;
        }
    }
}
