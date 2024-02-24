using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class MedianFilter : MatrixFilter
    {
        public MedianFilter(int size) : base(CreateKernel(size))
        {
        }

        private static float[,] CreateKernel(int size)
        {
            return new float[size, size];
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            List<int> redValues = new List<int>();
            List<int> greenValues = new List<int>();
            List<int> blueValues = new List<int>();

            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    redValues.Add(neighborColor.R);
                    greenValues.Add(neighborColor.G);
                    blueValues.Add(neighborColor.B);
                }
            }

            redValues.Sort();
            greenValues.Sort();
            blueValues.Sort();

            int medianIndex = redValues.Count / 2;
            int medianRed = redValues[medianIndex];
            int medianGreen = greenValues[medianIndex];
            int medianBlue = blueValues[medianIndex];

            return Color.FromArgb(medianRed, medianGreen, medianBlue);
        }
    }
}
