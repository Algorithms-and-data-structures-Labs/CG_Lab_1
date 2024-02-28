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
        private int[,] sobelX;
        private int[,] sobelY;
        private int medianSize;

        public EmbossFilter()
        {
            this.sobelX = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            this.sobelY = new int[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            this.medianSize = 7;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Median filter
            List<int> redValues = new List<int>();
            List<int> greenValues = new List<int>();
            List<int> blueValues = new List<int>();

            int radiusX = medianSize / 2;
            int radiusY = medianSize / 2;

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

            int medianRed = redValues[redValues.Count / 2];
            int medianGreen = greenValues[greenValues.Count / 2];
            int medianBlue = blueValues[blueValues.Count / 2];

            // Sobel filter
            float resultRX = 0, resultGX = 0, resultBX = 0;
            float resultRY = 0, resultGY = 0, resultBY = 0;

            radiusX = 1;
            radiusY = 1;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultRX += neighborColor.R * sobelX[k + radiusX, l + radiusY];
                    resultGX += neighborColor.G * sobelX[k + radiusX, l + radiusY];
                    resultBX += neighborColor.B * sobelX[k + radiusX, l + radiusY];
                    resultRY += neighborColor.R * sobelY[k + radiusX, l + radiusY];
                    resultGY += neighborColor.G * sobelY[k + radiusX, l + radiusY];
                    resultBY += neighborColor.B * sobelY[k + radiusX, l + radiusY];
                }
            }

            int resultR = Clamp((int)Math.Sqrt(Math.Pow(resultRX, 2.0) + Math.Pow(resultRY, 2.0)), 0, 255);
            int resultG = Clamp((int)Math.Sqrt(Math.Pow(resultGX, 2.0) + Math.Pow(resultGY, 2.0)), 0, 255);
            int resultB = Clamp((int)Math.Sqrt(Math.Pow(resultBX, 2.0) + Math.Pow(resultBY, 2.0)), 0, 255);

            // If the pixel is not an edge, set it to black
            if (resultR < 128 && resultG < 128 && resultB < 128)
            {
                resultR = 0;
                resultG = 0;
                resultB = 0;
            }

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
}
