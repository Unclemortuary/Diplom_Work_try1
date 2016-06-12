using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Diplom_Work_try1
{
    public class Contour
    {
        private Dot[] dots;
        public static int Denom = 16;


        public Contour(List<PointF> points)
        {
            dots = new Dot[points.Count];
            var maxY = FindMaxY(points);
            dots = setCoords(points, maxY);
        }

        private Dot[] setCoords(List<PointF> p, int maxYindex)
        {
            var interval = p.Count / Denom;
            var Z_coord = Form.ProgrammEngine.Step * Form.ProgrammEngine.Counter;
            var coordinates = new List<Dot>();
            for (int i = 0, j = maxYindex; i < Denom; i ++, j = j + interval)
            {
                if (j >= p.Count)
                    j = j - p.Count;
                coordinates.Add(new Dot(p[j].X, p[j].Y, Z_coord));
            }
            return coordinates.ToArray();
        }

        public int GetCountOfDots()
        {
            return dots.Count();
        }

        public float GetCoordX(int a)
        {
            return dots[a].getX();
        }

        public float GetCoordY(int a)
        {
            return dots[a].getY();
        }

        public float GetCoordZ(int a)
        {
            return dots[a].getZ();
        }

        public int FindMaxY(List<PointF> list)
        {
            float value = list[0].Y;
            int index = 0;
            for(int i = 0; i < list.Count; i++)
            {
                var currY = list[i].Y;
                if (currY > value)
                {
                    value = currY;
                    index = i;
                }
            }
            return index;
        }
    }
}
