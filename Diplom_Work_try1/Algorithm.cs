using System.Drawing;

namespace Diplom_Work_try1
{
    class Algorithm
    {
        public static Point GetCounterDot(Bitmap img, int location_x, int location_y, Point prev)
        {
            int plusX = 1, plusY = 1;
            Point counterDot = new Point();
            bool dotFounded = false;
            while(!dotFounded)
            {
                var color1 = img.GetPixel(location_x + plusX, location_y);
                var color2 = img.GetPixel(location_x - plusX, location_y);
                var color3 = img.GetPixel(location_x, location_y + plusY);
                var color4 = img.GetPixel(location_x, location_y - plusY);

                if(color1.R > 50 && color2.R > 50)
                {
                    dotFounded = true;
                    if(prev.X < location_x)
                    {
                        counterDot.X = location_x - plusX;
                        counterDot.Y = location_y;
                    }
                    else
                    {
                        counterDot.X = location_x + plusX;
                        counterDot.Y = location_y;
                    }
                }
                if (color3.R > 50 && color4.R > 50)
                {
                    dotFounded = true;
                    if (prev.Y < location_y)
                    {
                        counterDot.X = location_x;
                        counterDot.Y = location_y + plusY;
                    }
                    else
                    {
                        counterDot.X = location_x;
                        counterDot.Y = location_y - plusY;
                    }
                }
                else
                {
                    if (color1.R > 50)
                    {
                        dotFounded = true;
                        counterDot.X = location_x + plusX;
                        counterDot.Y = location_y;
                    }
                    else
                    {
                        if (color2.R > 50)
                        {
                            dotFounded = true;
                            counterDot.X = location_x - plusX;
                            counterDot.Y = location_y;
                        }
                        else
                        {
                            if (color3.R > 50)
                            {
                                dotFounded = true;
                                counterDot.X = location_x;
                                counterDot.Y = location_y + plusY;
                            }
                            else
                            {
                                if (color4.R > 50)
                                {
                                    dotFounded = true;
                                    counterDot.X = location_x;
                                    counterDot.Y = location_y - plusY;
                                }
                            }
                        }
                    }
                }
                plusX++;
                plusY++;
            }
            return counterDot;
        }

        public static Point GetCounterDot(Bitmap img, int location_x, int location_y)
        {
            int plusX = 1, plusY = 1;
            Point counterDot = new Point();
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
                    counterDot.X = location_x + plusX;
                    counterDot.Y = location_y;
                }
                else
                {
                    if (color2.R > 50)
                    {
                        dotFounded = true;
                        counterDot.X = location_x - plusX;
                        counterDot.Y = location_y;
                    }
                    else
                    {
                        if (color3.R > 50)
                        {
                            dotFounded = true;
                            counterDot.X = location_x;
                            counterDot.Y = location_y + plusY;
                        }
                        else
                        {
                            if (color4.R > 50)
                            {
                                dotFounded = true;
                                counterDot.X = location_x;
                                counterDot.Y = location_y - plusY;
                            }
                        }
                    }
                }
                plusX++;
                plusY++;
            }
            return counterDot;
        }
    }
}
