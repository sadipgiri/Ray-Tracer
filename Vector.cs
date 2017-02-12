using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Vector
    {
        public double x, y, z;
        //private bool is4D;
         
        // setting a default vector
        public Vector()
        {
            x = 0;
            y = 0;
            z = 0;
            //is4D = false;
        }

        // constructing a vector
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
           // is4D = false;
        }

       /* public Vector(double x, double y, double z, double w) : this(x, y, z)
        {
            this.w = w;
           // is4D = true;
        }
        */

        // operation overloading for addtion of any two vector using + sign
        public static Vector operator +(Vector a, Vector b)
        {
           // if (!a.is4D && !b.is4D)
                return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
           // return new Vector(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        // operation overloading for addition of vector and any scalar using + sign  
        public static Vector operator +(Vector a, double b)
        {
            return new Vector(a.x + b, a.y + b, a.z + b);
        }

        // operation overloading for subtraction of any two vector using - sign
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        // operation overloading for dot product of any two vectors using * sign
        public static double operator *(Vector a, Vector b)
        {
            return (double)(a.x * b.x + a.y * b.y + a.z * b.z);
        }

        // operation overloading for product of any scalar and any vector
        public static Vector operator *(Vector a, double b) {
            return new Vector(a.x * b, a.y * b, a.z * b);
        }

        public static Vector operator *(double a, Vector b)
        {
            return b * a;
        }

        // operation overloading for cross product of any two vectors
        public static Vector operator %(Vector a, Vector b)
        {
            return new Vector((a.y * b.z - a.z * b.y), (a.z * b.x - a.x * b.z), (a.x * b.y - a.y * b.x));
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        // defining function to find magnitude of any given vector
        public double Magnitude() {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        // defining function to normalize the any given vector
        public Vector Normalize()
        {
            double d = Magnitude();
            double newX =  x / d;
            double newY = y / d;
            double newZ = z / d;
            return new Vector(newX, newY, newZ);
        }

        
    }
}
