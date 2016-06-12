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

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Diplom_Work_try1
{
    public partial class Form : System.Windows.Forms.Form
    {
        public static Engine ProgrammEngine;
        private List<Contour> ct;

        private Graphics g; // Экземпляр класса графики(с помощью него и происходит рисование)
        private bool end = false;
        private bool notingCentr = false;
        private bool ispaint = false; // Факт рисования
        private Point currentPoint; // Текущая точка
        private Point prevPoint; // Предыдущая точка
        private Pen p; // Экземпляр класса кисточек
        private Color currentColor = Color.Red;
        private List<PointF> DotsForContour;

        public Form()
        {
            InitializeComponent();
            g = panel1.CreateGraphics(); // Указываешь, что графика создается на элементе panel1
            g.SmoothingMode = SmoothingMode.AntiAlias; // Сглаживание линий (оно всё-таки есть)
            DotsForContour = new List<PointF>();
            ct = new List<Contour>();
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
            ProgrammEngine.LoadImages();
            panel1.BackgroundImage = ProgrammEngine.GetCurrentShot();
            ProgrammEngine.GetFiltredShot();
            textBox2.Text = ProgrammEngine.CountShots.ToString();
            textBox1.Text = "0";
        }

        private void CountOfShots_ValueChanged(object sender, EventArgs e) //Обработчик изменения значения поля для количества снимков
        {
            var count = (int)CountOfShots.Value;
            ProgrammEngine.CountShots = count;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) // Обработчик щелчка мыши на panel1
        {
            if (notecentbutton.Enabled)
            {
                if (notingCentr)
                {
                    ProgrammEngine.Setcentr(e.Location);
                    notecentbutton.Enabled = false;
                    notingCentr = false;
                    return;
                }
                else
                {
                    MessageBox.Show("Отметьте центр глаза");
                    return;
                }
            }
            if (!ispaint)
            {
                ispaint = true;
                currentPoint = e.Location;
            }
            else
            {
                ispaint = false;
                if (!end)
                {
                    DotsForContour = ProgrammEngine.FinishCountour(DotsForContour);
                    RedrawCountour(DotsForContour);
                }
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
            p.Color = Color.Red;
            var last = DotsForContour.Count - 1;
            var firstPoint = ProgrammEngine.DotForPainting(prevPoint.X, prevPoint.Y);
            var secondPoint = ProgrammEngine.DotForPainting(currentPoint.X, currentPoint.Y); // Для второй точки вызывается метод с атрибутом Pred
            g.DrawLine(p, firstPoint, secondPoint);
            DotsForContour.Add(secondPoint);
            if(DotsForContour.Count > 2)
            {
                if (secondPoint != DotsForContour[DotsForContour.Count - 1])
                    DotsForContour.Add(secondPoint);
                end = Engine.CheckEnd(DotsForContour);
                if (end)
                {
                    ispaint = false;
                    RedrawCountour(DotsForContour);
                }
            }
        }

        private void RedrawCountour(List<PointF> dots)
        {
            panel1.Refresh();
            var count = dots.Count;
            p.Color = Color.Green;
            for (int i = 0; i < (count - 1); i++)
            {
                var point1 = dots[i];
                var point2 = dots[i + 1];
                g.DrawLine(p, point1, point2);
            }
        }

        private void RedrawCountour(PointF[] dots)
        {
            panel1.Refresh();
            var count = dots.Count();
            p.Color = Color.Chartreuse;
            for (int i = 0; i < (count - 1); i++)
            {
                var point1 = dots[i];
                var point2 = dots[i + 1];
                g.DrawLine(p, point1, point2);
            }
        }

        private void button2_Click(object sender, EventArgs e) //Обработчик кнопки "Очистить"
        {
            panel1.Refresh();
            DotsForContour.Clear();
            notecentbutton.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e) //Обработчик кнопки "Отметить центр"
        {
            notingCentr = true;
        }

        private void save_button_Click(object sender, EventArgs e) //Обработчик кнопки "Сохранить"
        {
            var dist = DotsForContour.Distinct();
            ct.Add(new Contour(dist.ToList()));
            textBox1.Text = (ProgrammEngine.Counter + 1).ToString();
            if (ProgrammEngine.CountShots > (ProgrammEngine.Counter + 1))
            {
                ProgrammEngine.MaintainCounter();
                panel1.BackgroundImage = ProgrammEngine.GetCurrentShot();
                ProgrammEngine.GetFiltredShot();
                DotsForContour.Clear();
            }
            else
            {
                Form2 f = new Form2(ct);
                f.Size = new Size(700, 510);
                f.Visible = true;
                //f.ShowDialog();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var value = (float)numericUpDown1.Value;
            ProgrammEngine.Step = value;
        }
    }
    //end of Form class
}
