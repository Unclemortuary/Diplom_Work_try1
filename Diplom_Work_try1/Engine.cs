using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom_Work_try1
{
    class Engine
    {
        public Bitmap currentFiltredImage;
        private int currentShot;
        private Algorithm AlgorithmOfCounterSegmentation;

        public Engine()
        {
            AlgorithmOfCounterSegmentation = new Algorithm();
        }

        public static List<Image> LoadImages(int count)
        {
            var images = new List<Image>(count);
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG files(*.jpg)|*jpg|PNG files(*.png)|*png|All files (*.*)|*.*";
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.FileNames.Count() == count)
                    {
                        foreach (string name in dlg.FileNames)
                            images.Add(Image.FromFile(name));
                    }
                    else
                        MessageBox.Show("Выберите указанное количество изображений");
                }
                return images;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при попытке открыть изображение :" + ex);
            }
            return null;
        }

        public static Bitmap GetFiltredShot(Bitmap original)
        {
            Bitmap b = original;
            Bitmap bb = original;
            //_invert(b);
            int width = b.Width;
            int height = b.Height;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            int limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = b.GetPixel(i, j).R;
                    allPixG[i, j] = b.GetPixel(i, j).G;
                    allPixB[i, j] = b.GetPixel(i, j).B;
                }
            }

            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {

                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;
                    rc = 0;
                    gc = 0;
                    bc = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
                        bb.SetPixel(i, j, Color.White);
                    else
                        bb.SetPixel(i, j, Color.Black);
                }
            }
            return bb;
        }

        private static Bitmap _invert(Image img)
        {
            var image1 = new Bitmap(img);
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);

                    var colomNumber = pixelColor.R;

                    if (pixelColor.R > 50 && pixelColor.R < 100)
                    {
                        colomNumber = 200;
                    }
                    else
                    {
                        colomNumber = 0;
                    }

                    Color newColor = Color.FromArgb(colomNumber, colomNumber, colomNumber);
                    image1.SetPixel(x, y, newColor);
                }
            }
            return image1;
        }

        public Point DotForPainting(int current_x, int current_y, Point pred)
        {
            Point paintingDot = Algorithm.GetCounterDot(currentFiltredImage, current_x, current_y, pred);
            return paintingDot;
        }

        public Point DotForPainting(int current_x, int current_y)
        {
            Point paintingDot = Algorithm.GetCounterDot(currentFiltredImage, current_x, current_y);
            return paintingDot;
        }
    }
}
