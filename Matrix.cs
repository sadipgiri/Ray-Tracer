using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Matrix
    {
        public Vector v1, v2, v3; //it is 3 by 3 matrix where it takes three column vectors 

        // making default matrix as the identity matrix
        public Matrix() {
            v1 = new Vector(1,0,0);
            v2 = new Vector(0,1,0);
            v3 = new Vector(0,0,1);
        }

        public Matrix(Vector v1, Vector v2, Vector v3) {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }


        // operation overloading for addition of any two matrices using + sign
        public static Matrix operator +(Matrix a, Matrix b)
        {
            return new Matrix(a.v1 + b.v1, a.v2 + b.v2, a.v3 + b.v3);
        }

        public static Matrix operator -(Matrix a, Matrix b) {
            return new Matrix(a.v1 - b.v1, a.v2 - b.v2, a.v3 - b.v3);
        }

        // explicitly used in the Triangle class
        public double Determinant()
        {
            return ((v1.x * v2.y * v3.z) + (v2.x * v3.y *v1.z) + (v3.x * v1.y * v2.z) - (v3.x * v2.y * v1.z) - (v2.x * v1.y * v3.z) - (v1.x * v3.y * v2.z));
        }

        
        public static Matrix operator *(Matrix a, Matrix b) {
            Vector firstRowVectorOfFirstMatrix = new Vector(a.v1.x, a.v2.x, a.v3.x);
            Vector secondRowVectorOfFirstMatrix = new Vector(a.v1.y, a.v2.y, a.v3.y);
            Vector thirdRowVectorOfFirstMatrix = new Vector(a.v1.z, a.v2.z, a.v3.z);

            Vector firstColumnVector = new Vector(firstRowVectorOfFirstMatrix * b.v1, secondRowVectorOfFirstMatrix * b.v1, thirdRowVectorOfFirstMatrix * b.v1);
            Vector secondColumnVector = new Vector(firstRowVectorOfFirstMatrix * b.v2, secondRowVectorOfFirstMatrix * b.v2, thirdRowVectorOfFirstMatrix * b.v2);
            Vector thirdColumnVector = new Vector(firstRowVectorOfFirstMatrix * b.v3, secondRowVectorOfFirstMatrix * b.v3, thirdRowVectorOfFirstMatrix * b.v3);

            return new Matrix(firstColumnVector, secondColumnVector, thirdColumnVector);
        }

        //operator overloading for matrix times vector
        public static Vector operator *(Matrix a, Vector b) {
            Vector firstRowVector = new Vector(a.v1.x, a.v2.x, a.v3.x);
            Vector secondRowVector = new Vector(a.v1.y, a.v2.y, a.v3.y);
            Vector thirdRowVector = new Vector(a.v1.z, a.v2.z, a.v3.z);

            return new Vector(firstRowVector * b, secondRowVector * b, thirdRowVector * b);
        }

    }
}
