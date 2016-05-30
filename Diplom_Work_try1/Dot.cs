using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Diplom_Work_try1
{
    class Dot
    {
        private float X;
        private float Y;
        private float Z;


        public Dot(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float getX()
        {
            return X;
        }

        public float getY()
        {
            return Y;
        }

        public float getZ()
        {
            return Z;
        }
    }
}
