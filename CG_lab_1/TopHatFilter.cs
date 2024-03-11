using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class TopHatFilter : Filters
    {
        private readonly double[,] kernel;

        public TopHatFilter(double[,] selectedKernel)
        {
            this.kernel = selectedKernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int openingResult = OpeningOperation(sourceImage, x, y);
            int topHatValue = sourceImage.GetPixel(x, y).R - openingResult;
            return Color.FromArgb(Clamp(topHatValue, 0, 255), Clamp(topHatValue, 0, 255), Clamp(topHatValue, 0, 255));
        }
        private int OpeningOperation(Bitmap sourceImage, int x, int y)
        {
            int result = 255; // Инициализируем максимальным значением, чтобы был гарантировано нахождение минимума
            int kernelSize = kernel.GetLength(0);

            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    double kernelValue = kernel[l + 1, k + 1]; // Получаем значение ядра для текущего соседа
                    result = (int)Math.Min(result, neighborColor.R * kernelValue); // Учитываем вес соседа согласно ядру
                }
            }

            return result;
        }


    }
}
