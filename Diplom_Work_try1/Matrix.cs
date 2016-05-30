using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom_Work_try1
{
    class Matrix
    {
        private float[,] result;

        public Matrix(float[,] splains, float[,] dots)
        {
            var inverse = Matrice_Inverse(splains);
            result = MatriceMultiply(inverse, dots);
        }

        private float[,] Matrice_Inverse(float[,] matrice)
        {
            var count = matrice.GetLength(0);
            int r;
            float scale;
            float[,] source = (float[,])matrice.Clone();
            float[,] inverse = new float[count, count];
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                        inverse[i, j] = 1.0F;
                    else
                        inverse[i, j] = 0.0F;
                }
            for (int c = 0; c < count; ++c)
            {
                //Scale the current row to start with 1

                //Swap rows if the current value is too close to 0.0

                if (Math.Abs(source[c, c]) <= 2.0F * float.Epsilon)
                {
                    for (r = c + 1; r < count; ++r)
                        if (Math.Abs(source[r, c]) <= 2.0F * float.Epsilon)
                        {
                            RowSwap(source, count, c, r);
                            RowSwap(inverse, count, c, r);
                            break;
                        }
                }
                scale = 1.0F / source[c, c];
                RowScale(source, count, scale, c);
                RowScale(inverse, count, scale, c);



                //Zero out the rest of the column

                for (r = 0; r < count; ++r)
                {
                    if (r != c)
                    {
                        scale = -source[r, c];
                        RowScaleAdd(source, count, scale, c, r);
                        RowScaleAdd(inverse, count, scale, c, r);
                    }
                }
            }
            return inverse;
        }

        private static void RowSwap(float[,] data, int cols, int r0, int r1)
        {
            float tmp;
            for (int i = 0; i < cols; i++)
            {
                tmp = data[r0, i];
                data[r0, i] = data[r1, i];
                data[r1, i] = tmp;
            }
        }

        private static void RowScale(float[,] data, int cols, float a, int r)
        {
            for (int i = 0; i < cols; ++i)
                data[r, i] *= a;
        }

        private static void RowScaleAdd(float[,] data, int cols,
                                float a, int r0, int r1)
        {
            for (int i = 0; i < cols; i++)
                data[r1, i] += a * data[r0, i];
        }

        private float[,] MatriceMultiply(float[,] matrice_a, float[,] matrice_b)
        {
            var rows = matrice_a.GetLength(0);
            var cols = matrice_b.GetLength(1);
            var n = matrice_a.GetLength(1);
            float mult = 0.0F;
            float[,] solution = new float[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    for (int r = 0; r < n; r++)
                    {
                        mult += matrice_a[i, r] * matrice_b[r, j];
                    }
                    solution[i, j] = mult;
                    mult = 0.0F;
                }
            return solution;
        }

        public float[,] GetResultMatrix()
        {
            return result;
        }

    }//end of Matrix class
}
