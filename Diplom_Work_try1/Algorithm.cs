using System.Collections.Generic;
using System.Drawing;

namespace Diplom_Work_try1
{
    class Algorithm
    {
        public static Point GetCountourDot(Bitmap img, int location_x, int location_y) // Метод, который ищет "цветную" точку в окресности переданной точки. На вход получает img - отфильтрованный снимок, location_x, location_y - координаты точки, Prev - предыдущая нарисованная точка
        {
            int plusX = 0, plusY = 0; // Счетчики по x и y
            Point contourDot = new Point(location_x, location_y); // Возвращаемая точка
            var count = 0;
            while (count < 6)
            {
                var color1 = img.GetPixel(location_x + plusX, location_y);
                var color2 = img.GetPixel(location_x - plusX, location_y);
                var color3 = img.GetPixel(location_x, location_y + plusY);
                var color4 = img.GetPixel(location_x, location_y - plusY);

                if (color1.R == 255)
                {
                    contourDot.X = location_x + plusX;
                    contourDot.Y = location_y;
                    return contourDot;
                }
                if (color2.R == 255)
                {
                    contourDot.X = location_x - plusX;
                    contourDot.Y = location_y;
                    return contourDot;
                }
                if (color3.R == 255)
                {
                    contourDot.X = location_x;
                    contourDot.Y = location_y + plusY;
                    return contourDot;
                }
                if (color4.R == 255)
                {
                    contourDot.X = location_x;
                    contourDot.Y = location_y - plusY;
                    return contourDot;
                }
                plusX++;
                plusY++;
                count++;
            }
            return contourDot;
        }

        enum Side { North, East, South, West, };


        public static List<PointF> Beatle(Bitmap img, Point blackside)
        {
            Side direction;
            var shift = FindWhite(img, blackside);
            if (shift < 0)
                direction = Side.East;
            else
                direction = Side.West;
            int currcoordX = blackside.X + shift;
            int currcoordY = blackside.Y;
            bool prevBlack = false;
            var TransleteCoords = new List<PointF>();
            PointF start_point = new PointF();
            PointF end_point = new PointF();
            do
            {
                var beatle = img.GetPixel(currcoordX, currcoordY);
                if (beatle.R != 200)
                {
                    if (!prevBlack)
                    {
                        TransleteCoords.Add(new PointF(currcoordX, currcoordY));
                        if (TransleteCoords.Count == 1)
                            start_point = TransleteCoords[0];
                    }
                    prevBlack = true;
                    switch (direction)
                    {
                        case Side.East:
                            currcoordY--;
                            direction = Side.North;
                            break;
                        case Side.North:
                            currcoordX--;
                            direction = Side.West;
                            break;
                        case Side.South:
                            currcoordX++;
                            direction = Side.East;
                            break;
                        case Side.West:
                            currcoordY++;
                            direction = Side.South;
                            break;
                    }
                    end_point = new PointF(currcoordX, currcoordY);
                }
                else
                {
                    if (prevBlack)
                        TransleteCoords.Add(new PointF(currcoordX, currcoordY));
                    prevBlack = false;
                    switch (direction)
                    {
                        case Side.East:
                            currcoordY++;
                            direction = Side.South;
                            break;
                        case Side.North:
                            currcoordX++;
                            direction = Side.East;
                            break;
                        case Side.South:
                            currcoordX--;
                            direction = Side.West;
                            break;
                        case Side.West:
                            currcoordY--;
                            direction = Side.North;
                            break;
                    }
                    end_point = new PointF(currcoordX, currcoordY);
                }
            } while (end_point != start_point);
            return TransleteCoords;
        }

        private static int FindWhite(Bitmap img, Point centr)
        {
            int x = centr.X;
            int y = centr.Y;
            int shift = 0;
            var color = img.GetPixel(x, y).R;
            var finded = false;
            while (!finded)
            {
                var left = img.GetPixel(x + shift, y);
                var right = img.GetPixel(x - shift, y);
                if (left.R == 200)
                    finded = true;
                if (right.R == 200)
                {
                    shift = -shift;
                    finded = true;
                }
                shift++;
            }
            return shift;
        }
        //end of Algorithm class
    }
}
