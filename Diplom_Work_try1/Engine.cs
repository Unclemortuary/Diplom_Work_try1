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
    public class Engine
    {
        private float step = 3.0F; //Шаг
        public float Step
        {
            get
            {
                return step;
            }
            set
            {
                step = value;
            }
        }
        private static int countShots; //Число снимков
        public int CountShots
        {
            get
            {
                return countShots;
            }
            set
            {
                countShots = value;
            }
        }
        private static int counter = 0; //Внутренний счетчик для снимков
        public int Counter
        {
            get
            {
                return counter;
            }
        }
        private List<Image> MRTShots; // Список загруженных снимков
        private Point centr;
        public static Bitmap currentFiltredImage;
        public static Bitmap currentSegregation;


        public void LoadImages() // Загружает изображения
        {
            var images = new List<Image>(CountShots);
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG files(*.jpg)|*jpg|PNG files(*.png)|*png|All files (*.*)|*.*";
                dlg.Multiselect = true;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.FileNames.Count() == CountShots)
                    {
                        foreach (string name in dlg.FileNames)
                            images.Add(Image.FromFile(name));
                    }
                    else
                        MessageBox.Show("Выберите указанное количество изображений");
                }
                MRTShots = images;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при попытке открыть изображение :" + ex);
            }
        }

        public void GetFiltredShot() // Возвращает отфильтрованный снимок
        {
            var original = (Bitmap)MRTShots[Counter];
            Canny cannyData = new Canny(original);
            currentFiltredImage = cannyData.DisplayImage(cannyData.EdgeMap);
            currentSegregation = _invert(original);
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

                    if (pixelColor.R > 10 && pixelColor.R < 150)
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

                    //Методы для доступа к полям класса

        public void MaintainCounter()
        {
            counter++;
        }

        public void Setcentr(Point a)
        {
            centr = a;
        }

        public Image GetCurrentShot()
        {
            return MRTShots[Counter];
        }


        public Point DotForPainting(int current_x, int current_y) // Метод, который вызывает алгоритм для переданной в качестве атрибута точки
        {
            Point paintingDot = Algorithm.GetCountourDot(currentFiltredImage, current_x, current_y);
            return paintingDot; // Возвращает полученную через алгоритм точку
        }

        public List<PointF> FinishCountour(List<PointF> dots)
        {
            return Algorithm.Beatle(currentSegregation, centr);
        }

        public static bool CheckEnd(List<PointF> a)
        {
            var last = a.Count - 1;
            if (a[0].Equals(a[last]) || (a[last].X == a[0].X + 1.0F && a[last].Y == a[0].Y && a.Count > 10) || (a[last].X == a[0].X - 1.0F && a[last].Y == a[0].Y && a.Count > 10))
                return true;
            else
                return false;
        }
    }
    //end of Engine class
}
