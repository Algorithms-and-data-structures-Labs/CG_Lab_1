using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab_1
{
    internal class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter()
        {
            kernel = GenerateMotionBlurKernel();
        }

        private float[,] GenerateMotionBlurKernel()
        {
            int size = 9;
            float[,] kernel = new float[size, size];
            float weight = 1.0f / size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                        kernel[i, j] = weight;
                    else
                        kernel[i, j] = 0;
                }
            }

            return kernel;
        }
    }
}
