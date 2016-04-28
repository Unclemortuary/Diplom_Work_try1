using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Diplom_Work_try1
{
    public partial class Form1 : Form
    {
        public int countShots = 1; // Количество загружаемых снимков
        public int counter; // Счетчик для снимков
        private List<Image> MRTShots;
        private Graphics g;
        private bool ispaint = false; // Факт рисования
        private Point currentPoint;
        private Point prevPoint;
        private Pen p;
        private Color currentColor = Color.Red;
        private Engine ProgrammEngine;
        private ArrayList DotsForCounter;
        Form f;
        PictureBox pb;

        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            DotsForCounter = new ArrayList();
            pb = new PictureBox();
        }

        private void Form1_Load(object sender, EventArgs e) // Загрузчик формы
        {
            ProgrammEngine = new Engine();
            p = new Pen(currentColor);
        }

        //Методы

        private void button1_Click(object sender, EventArgs e)
        {
            MRTShots = Engine.LoadImages(countShots);
            g.DrawImage(MRTShots[0], 1, 1);
            ProgrammEngine.currentFiltredImage = Engine.GetFiltredShot(new Bitmap(MRTShots[0]));
            f = new Form();
            f.Size = new Size(this.panel1.Width + 10, this.panel1.Height + 10);
            f.Controls.Add(pb);
            pb.Location = new Point(1, 1);
            pb.Size = this.panel1.Size;
            pb.Image = ProgrammEngine.currentFiltredImage;
            f.Show();
        }

        private void CountOfShots_ValueChanged(object sender, EventArgs e)
        {
            countShots = (int)CountOfShots.Value;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ispaint)
            {
                ispaint = true;
                currentPoint = e.Location;
            }
            else
                ispaint = false;
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(ispaint)
            {
                prevPoint = currentPoint;
                currentPoint = e.Location;
                Painting();
            }
        }

        public void Painting()
        {
            var firstPoint = ProgrammEngine.DotForPainting(prevPoint.X, prevPoint.Y);
            var secondPoint = ProgrammEngine.DotForPainting(currentPoint.X, currentPoint.Y, firstPoint);
            g.DrawLine(p, firstPoint, secondPoint);
            DotsForCounter.Add(firstPoint);
            DotsForCounter.Add(secondPoint);
            counter++;
        }

        ~Form1()
        {
            g.Dispose();
            g = null;
            f.Dispose();
            f = null;
        }
    }
}
