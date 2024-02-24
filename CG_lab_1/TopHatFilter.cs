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
        private readonly int[,] kernel = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int openingResult = OpeningOperation(sourceImage, x, y);
            int topHatValue = sourceImage.GetPixel(x, y).R - openingResult;
            return Color.FromArgb(Clamp(topHatValue, 0, 255), Clamp(topHatValue, 0, 255), Clamp(topHatValue, 0, 255));
        }
        private int OpeningOperation(Bitmap sourceImage, int x, int y)
        {
            int result = sourceImage.GetPixel(x, y).R;
            for (int l = -1; l <= 1; l++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    result = Math.Min(result, neighborColor.R);
                }
            }
            return result;
        }
    }
}
