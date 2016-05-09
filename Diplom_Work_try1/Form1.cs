using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Diplom_Work_try1
{
    public partial class Form1 : Form
    {
        public int countShots = 1; // Количество загружаемых снимков
        public int counter; // Счетчик для снимков (пока не используется)
        private List<Image> MRTShots; // Список загруженных снимков
        private Graphics g; // Экземпляр класса графики(с помощью него и происходит рисование)
        private bool ispaint = false; // Факт рисования
        private Point currentPoint; // Текущая точка
        private Point prevPoint; // Предыдущая точка
        private Pen p; // Экземпляр класса кисточек
        private Color currentColor = Color.Red;
        private Engine ProgrammEngine; //Экземпляр класса Движок, через него вызываются почти все методы
        private ArrayList DotsForContour; // Вроде как(не проверял) динамический массив для сохранения точек
        private Point[] Countour;

        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics(); // Указываешь, что графика создается на элементе panel1
            //g2 = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Сглаживание линий (оно всё-таки есть)
            DotsForContour = new ArrayList(); // Инициализация массива
        }

        private void Form1_Load(object sender, EventArgs e) // Загрузчик формы
        {
            ProgrammEngine = new Engine(); // Инициализация нового экземпляра класса Движок
            p = new Pen(currentColor);
            p.Width = 3;
        }

        //Методы

        private void button1_Click(object sender, EventArgs e) //Обработчик кнопки "Загрузить"
        {
            MRTShots = Engine.LoadImages(countShots); //Вызываем метод из движка для загрузки снимков и результат записываем в MRTShots
            panel1.BackgroundImage = MRTShots[0];
            ProgrammEngine.currentFiltredImage = Engine.GetFiltredShot(new Bitmap(MRTShots[0])); // Вызываем метод, который возвращает отфильтрофанный(пока что только первый) снимок и приравниваем к текущему отфильтрованному снимку(определен в движке)
            pictureBox1.Image = ProgrammEngine.currentFiltredImage; // Выводит отфильтрованный снимок на picturebox1
        }

        private void CountOfShots_ValueChanged(object sender, EventArgs e) //Обработчик изменения значения поля для количества снимков
        {
            countShots = (int)CountOfShots.Value;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) // Обработчик щелчка мыши на panel1
        {
            if (!ispaint)
            {
                ispaint = true;
                currentPoint = e.Location;
            }
            else
            {
                //List<Point> partOfCountour1 = DotsForContour;
                ispaint = false;
                var partOfCountour2 = ProgrammEngine.FinishCountour(DotsForContour, e.X, e.Y);
                //Countour = partOfCountour1.Concat(partOfCountour2);
                
            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e) // -/- движения мыши
        {
            if(ispaint)
            {
                prevPoint = currentPoint;
                currentPoint = e.Location;
                Painting();
            }
        }

        public void Painting() // Метод, который рисует
        {
            var firstPoint = ProgrammEngine.DotForPainting(prevPoint.X, prevPoint.Y); // Для первой точки вызывается второй из перегрузки метод
            var secondPoint = ProgrammEngine.DotForPainting(currentPoint.X, currentPoint.Y, firstPoint); // Для второй точки вызывается метод с атрибутом Pred
            g.DrawLine(p, firstPoint, secondPoint);
            DotsForContour.Add(secondPoint);
            var last = DotsForContour.Count - 1;
            if(DotsForContour.Count > 2)
            {
                if (DotsForContour[0].Equals(DotsForContour[last]))
                {
                    ispaint = false;
                    panel1.Refresh();
                    RedrawCountour(Countour);
                }
            }
            //counter++;
        }

        private void RedrawCountour(Point[] dots)
        {
            //Point[] coords = (Point[]) dots.ToArray(typeof(Point));
            var count = dots.Count();
            p.Color = Color.Green;
            for (int i = 0; i < (count - 1); i++)
                g.DrawLine(p, dots[i], dots[i + 1]);
        }

        ~Form1() // необязательно
        {
            g.Dispose();
            g = null;
        }
    }
}
