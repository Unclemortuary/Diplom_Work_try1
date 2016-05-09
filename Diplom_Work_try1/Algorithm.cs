using System.Drawing;

namespace Diplom_Work_try1
{
    class Algorithm
    {
        public static Point GetCountourDot(Bitmap img, int location_x, int location_y, Point prev) // Метод, который ищет "цветную" точку в окресности переданной точки. На вход получает img - отфильтрованный снимок, location_x, location_y - координаты точки, Prev - предыдущая нарисованная точка
        {
            int plusX = 1, plusY = 1; // Счетчики по x и y
            Point countourDot = new Point(); // Возвращаемая точка
            bool dotFounded = false; // Точка найдена
            while(!dotFounded)
            {
                var color1 = img.GetPixel(location_x + plusX, location_y); //Вправо по x
                var color2 = img.GetPixel(location_x - plusX, location_y); // Влево по x
                var color3 = img.GetPixel(location_x, location_y + plusY); // Вверх по y
                var color4 = img.GetPixel(location_x, location_y - plusY); // Вниз по y

                if(color1.R > 50 && color2.R > 50)
                {
                    dotFounded = true;
                    if(prev.X < location_x)
                    {
                        countourDot.X = location_x - plusX;
                        countourDot.Y = location_y;
                    }
                    else
                    {
                        countourDot.X = location_x + plusX;
                        countourDot.Y = location_y;
                    }
                }
                if (color3.R > 50 && color4.R > 50)
                {
                    dotFounded = true;
                    if (prev.Y < location_y)
                    {
                        countourDot.X = location_x;
                        countourDot.Y = location_y + plusY;
                    }
                    else
                    {
                        countourDot.X = location_x;
                        countourDot.Y = location_y - plusY;
                    }
                }
                else
                {
                    if (color1.R > 50)
                    {
                        dotFounded = true;
                        countourDot.X = location_x + plusX;
                        countourDot.Y = location_y;
                    }
                    else
                    {
                        if (color2.R > 50)
                        {
                            dotFounded = true;
                            countourDot.X = location_x - plusX;
                            countourDot.Y = location_y;
                        }
                        else
                        {
                            if (color3.R > 50)
                            {
                                dotFounded = true;
                                countourDot.X = location_x;
                                countourDot.Y = location_y + plusY;
                            }
                            else
                            {
                                if (color4.R > 50)
                                {
                                    dotFounded = true;
                                    countourDot.X = location_x;
                                    countourDot.Y = location_y - plusY;
                                }
                            }
                        }
                    }
                }
                plusX++;
                plusY++;
            }
            return countourDot;
        }

        public static Point GetCountourDot(Bitmap img, int location_x, int location_y) // Перегрузка
        {
            int plusX = 1, plusY = 1;
            Point countourDot = new Point();
            bool dotFounded = false;
            while (!dotFounded)
            {
                var color1 = img.GetPixel(location_x + plusX, location_y);
                var color2 = img.GetPixel(location_x - plusX, location_y);
                var color3 = img.GetPixel(location_x, location_y + plusY);
                var color4 = img.GetPixel(location_x, location_y - plusY);

                if (color1.R > 50)
                {
                    dotFounded = true;
                    countourDot.X = location_x + plusX;
                    countourDot.Y = location_y;
                }
                else
                {
                    if (color2.R > 50)
                    {
                        dotFounded = true;
                        countourDot.X = location_x - plusX;
                        countourDot.Y = location_y;
                    }
                    else
                    {
                        if (color3.R > 50)
                        {
                            dotFounded = true;
                            countourDot.X = location_x;
                            countourDot.Y = location_y + plusY;
                        }
                        else
                        {
                            if (color4.R > 50)
                            {
                                dotFounded = true;
                                countourDot.X = location_x;
                                countourDot.Y = location_y - plusY;
                            }
                        }
                    }
                }
                plusX++;
                plusY++;
            }
            return countourDot;
        }

        public static Point[] Beatl(int startCoord_x, int startCoord_y, Bitmap img, Point end)
        {
            int currcoordX = startCoord_x;
            int currcoordY = startCoord_y;
            int prevcoordX;
            int prevcoordY;
            bool prevWhite = true;
            var TransleteCoords = new System.Collections.Generic.List<Point>();
            var beatl = img.GetPixel(startCoord_x, startCoord_y);
            while(currcoordX != end.X && currcoordY != end.Y)
            {
                prevcoordX = currcoordX;
                prevcoordY = currcoordY;
                var diffX = currcoordX - prevcoordX;
                var diffY = currcoordY - prevcoordY;
                switch(diffX)
                {
                    case 0:
                        if(beatl.R > 50)
                        {
                            if(diffY == -1)
                            {
                                currcoordX--;
                            }
                            else
                            {
                                currcoordX++;
                            }
                            if (!prevWhite)
                                TransleteCoords.Add(new Point(currcoordX, currcoordY));
                            prevWhite = true;
                            break;
                        }
                        else
                        {
                            if (diffY == -1)
                            {
                                currcoordX--;
                            }
                            else
                            {
                                currcoordY++;
                            }
                            if (prevWhite)
                                TransleteCoords.Add(new Point(currcoordX, currcoordY));
                            prevWhite = false;
                            break;
                        }
                    case -1:
                        if (beatl.R > 50)
                        {
                            currcoordY++;
                            if (!prevWhite)
                                TransleteCoords.Add(new Point(currcoordX, currcoordY));
                            prevWhite = true;
                            break;
                        }
                        else
                        {
                            currcoordY--;
                            if (prevWhite)
                                TransleteCoords.Add(new Point(currcoordX, currcoordY));
                            prevWhite = false;
                            break;
                        }
                    case 1:
                        if (beatl.R > 50)
                        {
                            currcoordY--;
                            if (!prevWhite)
                                TransleteCoords.Add(new Point(currcoordX, currcoordY));
                            prevWhite = true;
                            break;
                        }
                        else
                        {
                            currcoordY++;
                            if (prevWhite)
                                TransleteCoords.Add(new Point(currcoordX, currcoordY));
                            prevWhite = false;
                            break;
                        }
                }
                beatl = img.GetPixel(currcoordX, currcoordY);
            }
            return TransleteCoords.ToArray();
        }
        //end of Algorithm class
    }
}
