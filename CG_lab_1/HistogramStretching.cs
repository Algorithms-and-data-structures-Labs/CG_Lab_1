using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class HistogramStretching: Filters
    {
        private int minR = 255, maxR = 0;
        private int minG = 255, maxG = 0;
        private int minB = 255, maxB = 0;

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Рассчитываем значение пикселя после линейного растяжения гистограммы
            Color pixel = sourceImage.GetPixel(x, y);

            int newR = (int)(255 * ((pixel.R - minR) / (maxR - minR)));
            int newG = (int)(255 * ((pixel.G - minG) / (maxG - minG)));
            int newB = (int)(255 * ((pixel.B - minB) / (maxB - minB)));

            newR = Clamp(newR, 0, 255);
            newG = Clamp(newG, 0, 255);
            newB = Clamp(newB, 0, 255);

            return Color.FromArgb(newR, newG, newB);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // Находим минимальное и максимальное значение для каждого канала цвета
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);
                    minR = Math.Min(minR, pixel.R);
                    maxR = Math.Max(maxR, pixel.R);
                    minG = Math.Min(minG, pixel.G);
                    maxG = Math.Max(maxG, pixel.G);
                    minB = Math.Min(minB, pixel.B);
                    maxB = Math.Max(maxB, pixel.B);
                }
            }

            // Проходим по всем пикселям и применяем линейное растяжение гистограммы
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / sourceImage.Width * 100));

                if (worker.CancellationPending)
                    return null;

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);

                    int newR = (int)(255 * ((pixel.R - minR) / (maxR - minR)));
                    int newG = (int)(255 * ((pixel.G - minG) / (maxG - minG)));
                    int newB = (int)(255 * ((pixel.B - minB) / (maxB - minB)));

                    newR = Clamp(newR, 0, 255);
                    newG = Clamp(newG, 0, 255);
                    newB = Clamp(newB, 0, 255);

                    resultImage.SetPixel(i, j, Color.FromArgb(newR, newG, newB));
                }
            }

            return resultImage;
        }

    }
}
