using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;



using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;

namespace Diplom_Work_try1
{
    class Surface
    {
        private Contour[] cuts;
        private int count_points = Contour.Denom + 1;
        private float[,,] ctrlpoints = new float[Contour.Denom + 1, Form.ProgrammEngine.CountShots, 3];
        private float[,,] Vectors = new float[Contour.Denom + 1, Form.ProgrammEngine.CountShots, 3];
        private int count_img = Form.ProgrammEngine.CountShots;

        public delegate void Inval();

        public Surface(List<Contour> contours)
        {
            cuts = contours.ToArray();
            //Initializing();
        }

        internal void Initializing()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB | Glut.GLUT_DEPTH);


            Gl.glViewport(0, 0, Form2.GLWindow.Width, Form2.GLWindow.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-10.0, 15.0, 17.0, -8.0, -25.0, 25.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glShadeModel(Gl.GL_FLAT);

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            init_surface();
            Display(0.0F, 0.0F);
        }

        private void init_surface()
        {
            for(int i = 0; i < Contour.Denom; i++)
                for(int j = 0; j < Form.ProgrammEngine.CountShots; j++)
                {
                    ctrlpoints[i, j, 0] = cuts[j].GetCoordX(i) / 10.0F;
                    ctrlpoints[i, j, 1] = cuts[j].GetCoordY(i) / 10.0F;
                    ctrlpoints[i, j, 2] = cuts[j].GetCoordZ(i) / 10.0F;
                }
            for(int j = 0; j < Form.ProgrammEngine.CountShots; j++)
            {
                ctrlpoints[Contour.Denom, j, 0] = ctrlpoints[0, j, 0];
                ctrlpoints[Contour.Denom, j, 1] = ctrlpoints[0, j, 1];
                ctrlpoints[Contour.Denom, j, 2] = ctrlpoints[0, j, 2];
            }
        }

        public void Display(float angleX, float angleY)
        {
            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(0.0F, 0.0F, 0.0F, 0.0F);

            Gl.glLoadIdentity();

            Gl.glPushMatrix();

            Gl.glRotatef(angleX, 0.5F, 0.0F, 0.0F);
            Gl.glRotatef(angleY, 0.0F, 0.5F, 0.0F);

            Gl.glScaled(2.0, 2.0, 2.0);

            Gl.glTranslated(-15.0, -8.0, 0.0);

            Gl.glColor3f(0.0F, 150.0F, 55.0F);

            Gl.glBegin(Gl.GL_LINE_STRIP);
            BuildSplain(ctrlpoints, 10);
            Gl.glEnd();
            Gl.glPopMatrix();
            // завершаем рисование 
            Gl.glFlush();
            Form2.GLWindow.Invalidate();
        }

        private void BuildSplain(float[,,] points, int N)
        {
            int i = 0, j = 0, q = 0, g = 0;
            float X = 0.0F, Y = 0.0F, Z = 0.0F;
            float xA = 0.0F, xB = 0.0F, yA = 0.0F, yB = 0.0F, zA = 0.0F, zB = 0.0F, uA = 0.0F, uB = 0.0F, vA = 0.0F, vB = 0.0F, wA = 0.0F, wB = 0.0F, t = 0.0F;
            float a0 = 0.0F, a1 = 0.0F, a2 = 0.0F, a3 = 0.0F, b0 = 0.0F, b1 = 0.0F, b2 = 0.0F, b3 = 0.0F, c0 = 0.0F, c1 = 0.0F, c2 = 0.0F, c3 = 0.0F;
            float[,,] AllPoints = new float[N * (count_points - 1), count_img, 3];

            //запись начальных и конечных векторов
            for (g = 0; g < count_img; g++)
            {
                //векторы 1-го сплайна
                //первая точка
                Vectors[0, g, 0] = (points[1, g, 0] - points[count_points - 1, g, 0]) / 2;
                Vectors[0, g, 1] = (points[1, g, 1] - points[count_points - 1, g, 1]) / 2;
                Vectors[0, g, 2] = (points[1, g, 2] - points[count_points - 1, g, 2]) / 2;
                //конечная точка
                Vectors[count_points - 1, g, 0] = (points[1, g, 0] - points[count_points - 1, g, 0]) / 2;
                Vectors[count_points - 1, g, 1] = (points[1, g, 1] - points[count_points - 1, g, 1]) / 2;
                Vectors[count_points - 1, g, 2] = (points[1, g, 2] - points[count_points - 1, g, 2]) / 2;
            }


            for (g = 0; g < count_img; g++)
            {
                for (q = 1; q < count_points - 1; q++)
                {
                    Vectors[q, g, 0] = (points[q + 1, g, 0] - points[q - 1, g, 0]) / 2;
                    Vectors[q, g, 1] = (points[q + 1, g, 1] - points[q - 1, g, 1]) / 2;
                    Vectors[q, g, 2] = (points[q + 1, g, 2] - points[q - 1, g, 2]) / 2;
                }
            }

            for (g = 0; g < count_img; g++)
            {
                for (i = 1; i < count_points; i++)
                {
                    //точки опорные
                    xA = (float)points[i - 1, g, 0];
                    xB = (float)points[i, g, 0];
                    yA = (float)points[i - 1, g, 1];
                    yB = (float)points[i, g, 1];
                    zA = (float)points[i - 1, g, 2];
                    zB = (float)points[i, g, 2];
                    //вектора
                    uA = (float)Vectors[i - 1, g, 0];
                    uB = (float)Vectors[i, g, 0];
                    vA = (float)Vectors[i - 1, g, 1];
                    vB = (float)Vectors[i, g, 1];
                    wA = (float)Vectors[i - 1, g, 2];
                    wB = (float)Vectors[i, g, 2];

                    //коэффициенты параметрического уравнения
                    a3 = (2 * xA - 2 * xB + uA + uB);
                    a2 = (-3 * xA + 3 * xB - 2 * uA - uB);
                    a1 = uA;
                    a0 = xA;

                    b3 = (2 * yA - 2 * yB + vA + vB);
                    b2 = (-3 * yA + 3 * yB - 2 * vA - vB);
                    b1 = vA;
                    b0 = yA;

                    c3 = (2 * zA - 2 * zB + wA + wB);
                    c2 = (-3 * zA + 3 * zB - 2 * wA - wB);
                    c1 = wA;
                    c0 = zA;

                    // отрисовка сегментов 

                    for (j = 0; j < N; j++)
                    {
                        if (i == count_points - 1)
                        {
                            for (j = 0; j <= N; j++)
                            {
                                if (j != N)
                                {
                                    // параметр t на отрезке от 0 до 1 
                                    t = (float)j / (float)N;
                                    // генерация координат 
                                    X = (((a3 * t + a2) * t + a1) * t + a0);
                                    Y = (((b3 * t + b2) * t + b1) * t + b0);
                                    Z = (((c3 * t + c2) * t + c1) * t + c0);
                                    //сохранение координат
                                    AllPoints[N * (i - 1) + j, g, 0] = X;
                                    AllPoints[N * (i - 1) + j, g, 1] = Y;
                                    AllPoints[N * (i - 1) + j, g, 2] = Z;

                                    // и установка вершин 
                                    Gl.glVertex3f(X, Y, Z);
                                }
                                else
                                {
                                    // параметр t на отрезке от 0 до 1 
                                    t = (float)j / (float)N;
                                    // генерация координат 
                                    X = (((a3 * t + a2) * t + a1) * t + a0);
                                    Y = (((b3 * t + b2) * t + b1) * t + b0);
                                    Z = (((c3 * t + c2) * t + c1) * t + c0);

                                    // и установка вершин 
                                    Gl.glVertex3f(X, Y, Z);
                                }
                            }
                        }
                        else
                        {
                            // параметр t на отрезке от 0 до 1 
                            t = (float)j / (float)N;

                            // генерация координат 
                            X = (((a3 * t + a2) * t + a1) * t + a0);
                            Y = (((b3 * t + b2) * t + b1) * t + b0);
                            Z = (((c3 * t + c2) * t + c1) * t + c0);
                            //сохранение координат
                            AllPoints[N * (i - 1) + j, g, 0] = X;
                            AllPoints[N * (i - 1) + j, g, 1] = Y;
                            AllPoints[N * (i - 1) + j, g, 2] = Z;

                            // и установка вершин 
                            Gl.glVertex3f(X, Y, Z);
                        }
                    }

                }

            }

            //завершили строить сплайны
            //строим поверхность поперечными сплайнами    
            for (i = 0; i < ((N * (count_points - 1)) / 2); i++)
                    {
                        for (g = 0; g < 2 * count_img; g++)
                        {
                            if (g != 0)
                            {
                                if (g != count_img - 2)
                                {
                                    if (g != count_img - 1)
                                    {
                                        if (g != count_img)
                                        {
                                            if (g != 2 * count_img - 2)
                                            {
                                                if (g != 2 * count_img - 1)
                                                {
                                                    if (g > count_img)
                                                    {
                                                        //точки опорные
                                                        xA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 2 * count_img - g - 1, 0];
                                                        xB = (float)AllPoints[i + (N * (count_points - 1)) / 2, 2 * count_img - g - 2, 0];
                                                        yA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 2 * count_img - g - 1, 1];
                                                        yB = (float)AllPoints[i + (N * (count_points - 1)) / 2, 2 * count_img - g - 2, 1];
                                                        zA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 2 * count_img - g - 1, 2];
                                                        zB = (float)AllPoints[i + (N * (count_points - 1)) / 2, 2 * count_img - g - 2, 2];
                                                        //вектора
                                                        uA = ((float)AllPoints[i, 2 * count_img - g - 2, 0] - (float)AllPoints[i, 2 * count_img - g, 0]) / 2;
                                                        uB = ((float)AllPoints[i, 2 * count_img - g - 3, 0] - (float)AllPoints[i, 2 * count_img - g - 1, 0]) / 2;
                                                        vA = ((float)AllPoints[i, 2 * count_img - g - 2, 1] - (float)AllPoints[i, 2 * count_img - g, 1]) / 2;
                                                        vB = ((float)AllPoints[i, 2 * count_img - g - 3, 1] - (float)AllPoints[i, 2 * count_img - g - 1, 1]) / 2;
                                                        wA = ((float)AllPoints[i, 2 * count_img - g - 2, 2] - (float)AllPoints[i, 2 * count_img - g, 2]) / 2;
                                                        wB = ((float)AllPoints[i, 2 * count_img - g - 3, 2] - (float)AllPoints[i, 2 * count_img - g - 1, 2]) / 2;
                                                    }
                                                    else
                                                    {
                                                        //точки опорные
                                                        xA = (float)AllPoints[i, g, 0];
                                                        xB = (float)AllPoints[i, g + 1, 0];
                                                        yA = (float)AllPoints[i, g, 1];
                                                        yB = (float)AllPoints[i, g + 1, 1];
                                                        zA = (float)AllPoints[i, g, 2];
                                                        zB = (float)AllPoints[i, g + 1, 2];
                                                        //вектора
                                                        uA = ((float)AllPoints[i, g + 1, 0] - (float)AllPoints[i, g - 1, 0]) / 2;
                                                        uB = ((float)AllPoints[i, g + 2, 0] - (float)AllPoints[i, g, 0]) / 2;
                                                        vA = ((float)AllPoints[i, g + 1, 1] - (float)AllPoints[i, g - 1, 1]) / 2;
                                                        vB = ((float)AllPoints[i, g + 2, 1] - (float)AllPoints[i, g, 1]) / 2;
                                                        wA = ((float)AllPoints[i, g + 1, 2] - (float)AllPoints[i, g - 1, 2]) / 2;
                                                        wB = ((float)AllPoints[i, g + 2, 2] - (float)AllPoints[i, g, 2]) / 2;
                                                    }
                                                }
                                                else
                                                {
                                                    //точки опорные
                                                    xA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 0];
                                                    xB = (float)AllPoints[i, 0, 0];
                                                    yA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 1];
                                                    yB = (float)AllPoints[i, 0, 1];
                                                    zA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 2];
                                                    zB = (float)AllPoints[i, 0, 2];
                                                    //вектора
                                                    uA = ((float)AllPoints[i, 0, 0] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 0]) / 2;
                                                    uB = ((float)AllPoints[i, 1, 0] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 0]) / 2;
                                                    vA = ((float)AllPoints[i, 0, 1] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 1]) / 2;
                                                    vB = ((float)AllPoints[i, 1, 1] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 1]) / 2;
                                                    wA = ((float)AllPoints[i, 0, 2] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 2]) / 2;
                                                    wB = ((float)AllPoints[i, 1, 2] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 2]) / 2;
                                                }
                                            }
                                            else
                                            {
                                                //точки опорные
                                                xA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 0];
                                                xB = (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 0];
                                                yA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 1];
                                                yB = (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 1];
                                                zA = (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 2];
                                                zB = (float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 2];
                                                //вектора
                                                uA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 0] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 2, 0]) / 2;
                                                uB = ((float)AllPoints[i, 0, 0] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 0]) / 2;
                                                vA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 1] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 2, 1]) / 2;
                                                vB = ((float)AllPoints[i, 0, 1] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 1]) / 2;
                                                wA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, 0, 2] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 2, 2]) / 2;
                                                wB = ((float)AllPoints[i, 0, 2] - (float)AllPoints[i + (N * (count_points - 1)) / 2, 1, 2]) / 2;
                                            }
                                        }
                                        else
                                        {
                                            //точки опорные
                                            xA = (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 0];
                                            xB = (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 2, 0];
                                            yA = (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 1];
                                            yB = (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 2, 1];
                                            zA = (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 2];
                                            zB = (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 2, 2];
                                            //вектора
                                            uA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 2, 0] - (float)AllPoints[i, g - 1, 0]) / 2;
                                            uB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 3, 0] - (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 0]) / 2;
                                            vA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 2, 1] - (float)AllPoints[i, g - 1, 1]) / 2;
                                            vB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 1] - (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 1]) / 2;
                                            wA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 2, 2] - (float)AllPoints[i, g - 1, 2]) / 2;
                                            wB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 2] - (float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 2]) / 2;
                                        }
                                    }
                                    else
                                    {
                                        //точки опорные
                                        xA = (float)AllPoints[i, g, 0];
                                        xB = (float)AllPoints[i + (N * (count_points - 1)) / 2, g, 0];
                                        yA = (float)AllPoints[i, g, 1];
                                        yB = (float)AllPoints[i + (N * (count_points - 1)) / 2, g, 1];
                                        zA = (float)AllPoints[i, g, 2];
                                        zB = (float)AllPoints[i + (N * (count_points - 1)) / 2, g, 2];
                                        //вектора
                                        uA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g, 0] - (float)AllPoints[i, g - 1, 0]) / 2;
                                        uB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 0] - (float)AllPoints[i, g, 0]) / 2;
                                        vA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g, 1] - (float)AllPoints[i, g - 1, 1]) / 2;
                                        vB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 1] - (float)AllPoints[i, g, 1]) / 2;
                                        wA = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g, 2] - (float)AllPoints[i, g - 1, 2]) / 2;
                                        wB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g - 1, 2] - (float)AllPoints[i, g, 2]) / 2;
                                    }
                                }
                                else
                                {
                                    //точки опорные
                                    xA = (float)AllPoints[i, g, 0];
                                    xB = (float)AllPoints[i, g + 1, 0];
                                    yA = (float)AllPoints[i, g, 1];
                                    yB = (float)AllPoints[i, g + 1, 1];
                                    zA = (float)AllPoints[i, g, 2];
                                    zB = (float)AllPoints[i, g + 1, 2];
                                    //вектора
                                    uA = ((float)AllPoints[i, g + 1, 0] - (float)AllPoints[i, g - 1, 0]) / 2;
                                    uB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g + 1, 0] - (float)AllPoints[i, g, 0]) / 2;
                                    vA = ((float)AllPoints[i, g + 1, 1] - (float)AllPoints[i, g - 1, 1]) / 2;
                                    vB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g + 1, 1] - (float)AllPoints[i, g, 1]) / 2;
                                    wA = ((float)AllPoints[i, g + 1, 2] - (float)AllPoints[i, g - 1, 2]) / 2;
                                    wB = ((float)AllPoints[i + (N * (count_points - 1)) / 2, g + 1, 2] - (float)AllPoints[i, g, 2]) / 2;
                                }
                            }
                            else
                            {
                                //точки опорные
                                xA = (float)AllPoints[i, g, 0];
                                xB = (float)AllPoints[i, g + 1, 0];
                                yA = (float)AllPoints[i, g, 1];
                                yB = (float)AllPoints[i, g + 1, 1];
                                zA = (float)AllPoints[i, g, 2];
                                zB = (float)AllPoints[i, g + 1, 2];
                                //вектора
                                uA = ((float)AllPoints[i, g + 1, 0] - (float)AllPoints[i + (N * (count_points - 1)) / 2, g, 0]) / 2;
                                uB = ((float)AllPoints[i, g + 2, 0] - (float)AllPoints[i, g, 0]) / 2;
                                vA = ((float)AllPoints[i, g + 1, 1] - (float)AllPoints[i + (N * (count_points - 1)) / 2, g, 1]) / 2;
                                vB = ((float)AllPoints[i, g + 2, 1] - (float)AllPoints[i, g, 1]) / 2;
                                wA = ((float)AllPoints[i, g + 1, 2] - (float)AllPoints[i + (N * (count_points - 1)) / 2, g, 2]) / 2;
                                wB = ((float)AllPoints[i, g + 2, 2] - (float)AllPoints[i, g, 2]) / 2;
                            }
                            //коэффициенты параметрического уравнения
                            a3 = (2 * xA - 2 * xB + uA + uB);
                            a2 = (-3 * xA + 3 * xB - 2 * uA - uB);
                            a1 = uA;
                            a0 = xA;

                            b3 = (2 * yA - 2 * yB + vA + vB);
                            b2 = (-3 * yA + 3 * yB - 2 * vA - vB);
                            b1 = vA;
                            b0 = yA;

                            c3 = (2 * zA - 2 * zB + wA + wB);
                            c2 = (-3 * zA + 3 * zB - 2 * wA - wB);
                            c1 = wA;
                            c0 = zA;

                            // отрисовка сегментов 

                            for (j = 0; j <= N; j++)
                            {
                                // параметр t на отрезке от 0 до 1 
                                t = (float)j / (float)N;

                                // генерация координат 
                                X = (((a3 * t + a2) * t + a1) * t + a0);
                                Y = (((b3 * t + b2) * t + b1) * t + b0);
                                Z = (((c3 * t + c2) * t + c1) * t + c0);

                                // и установка вершин 
                                Gl.glVertex3f(X, Y, Z);
                            }

                        }
                    }
        
        
        }
    }
    //end of Surface class
}
