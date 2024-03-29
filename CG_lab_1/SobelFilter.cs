﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    class SobelFilter : MatrixFilter // Выделение границ (оператор Собеля)
    {
        protected int[,] X = null;
        protected int[,] Y = null;
        public SobelFilter()
        {
            X = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            Y = new int[3, 3] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        }
        public SobelFilter(int[,] _X, int[,] _Y)
        {
            this.X = _X; this.Y = _Y;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = 1;
            int radiusY = 1;
            float resultRX = 0; float resultGX = 0; float resultBX = 0;
            float resultRY = 0; float resultGY = 0; float resultBY = 0;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color NeighbourColor = sourceImage.GetPixel(idX, idY);
                    resultRX += NeighbourColor.R * X[k + radiusX, l + radiusY];
                    resultGX += NeighbourColor.G * X[k + radiusX, l + radiusY];
                    resultBX += NeighbourColor.B * X[k + radiusX, l + radiusY];
                    resultRY += NeighbourColor.R * Y[k + radiusX, l + radiusY];
                    resultGY += NeighbourColor.G * Y[k + radiusX, l + radiusY];
                    resultBY += NeighbourColor.B * Y[k + radiusX, l + radiusY];
                }
            int resultR = Clamp((int)Math.Sqrt(Math.Pow(resultRX, 2.0) + Math.Pow(resultRY, 2.0)), 0, 255);
            int resultG = Clamp((int)Math.Sqrt(Math.Pow(resultGX, 2.0) + Math.Pow(resultGY, 2.0)), 0, 255);
            int resultB = Clamp((int)Math.Sqrt(Math.Pow(resultBX, 2.0) + Math.Pow(resultBY, 2.0)), 0, 255);
            return Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
        }
    }

    class ScharrFilter : SobelFilter // Выделение границ (оператор Щарра)
    {
        public ScharrFilter()
        {
            X = new int[3, 3] { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };
            Y = new int[3, 3] { { 3, 10, 3 }, { 0, 0, 0 }, { -3, -10, -3 } };
        }
    }

    class PrewitteFilter : SobelFilter // Выделение границ (оператор Прюитта)
    {
        public PrewitteFilter()
        {
            X = new int[3, 3] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            Y = new int[3, 3] { { -1, -1, -1 }, { 0, 0, 0 }, { 1, 1, 1 } };
        }
    }
}
