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
            while (count < 10)
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


        public static List<Point> Beatle(Bitmap img, int startCoord_x, int startCoord_y, Point end, Point blackside)
        {
            Side direction;
            if(blackside.X == startCoord_x)
            {
                if (blackside.Y < startCoord_y)
                    direction = Side.South;
                else
                    direction = Side.North;
            }
            if (blackside.X < startCoord_x)
                direction = Side.West;
            else
                direction = Side.East;
            int[] sh = new int[1];
            int cont = 0;
            int currcoordX = startCoord_x;
            int currcoordY = startCoord_y;
            bool prevBlack = false;
            var TransleteCoords = new List<Point>();
            var beatle = img.GetPixel(startCoord_x, startCoord_y);
            if(beatle.R != 200)
            {
                sh = FindWhite(img, startCoord_x, startCoord_y);
                currcoordX = currcoordX + sh[0];
                currcoordY = currcoordY + sh[1];
            }

            while (cont < 100)
            {
                beatle = img.GetPixel(currcoordX, currcoordY);
                if(beatle.R != 200)
                {
                    if (!prevBlack)
                        TransleteCoords.Add(new Point(currcoordX, currcoordY));
                    prevBlack = true;
                    switch(direction)
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
                }
                else
                {
                    if(prevBlack)
                        TransleteCoords.Add(new Point(currcoordX, currcoordY));
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
                }
                cont++;
            }
            return TransleteCoords;
        }

        private static int[] FindWhite(Bitmap img, int startx, int starty)
        {
            int x = startx;
            int y = starty;
            int[] shift = { 0, 0 };
            var color = img.GetPixel(startx, starty).R;
            var countr = 0;
            while (countr < 20)
            {
                var plusx = 1;
                var plusy = 1;
                var down = img.GetPixel(x, y + plusy);
                var up = img.GetPixel(x, y - plusy);
                var left = img.GetPixel(x + plusx, y);
                var right = img.GetPixel(x - plusx, y);

                if (up.R == 200)
                {
                    shift.SetValue(-plusy, 1);
                    return shift;
                }
                if (down.R == 200)
                {
                    shift.SetValue(plusy, 1);
                    return shift;
                }

                if (left.R == 200)
                {
                    shift.SetValue(plusx, 0);
                    return shift;
                }
                if (right.R == 200)
                {
                    shift.SetValue(-plusx, 0);
                    return shift;
                }
                plusx++;
                plusy++;
                countr++;
            }
            return shift;
        }
        //end of Algorithm class
    }
}
