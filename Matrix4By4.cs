using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Matrix4By4
    {
        public Matrix m; // this is 3 by 3 matrix
        public Vector v1; // this is another 3 by 3 4th vector 

        // making default 4 by 4 matrix as identity matrix
        public Matrix4By4()
        {
            Matrix m = new Matrix();
            Vector v1 = new Vector(); 
        }

        // 
        public Matrix4By4(Matrix m, Vector v1) {
            this.m = m;
            this.v1 = v1;
        }

        public static Matrix4By4 XRotation(double rotationAngleThroughXAxis)
        {
            return new Matrix4By4(new Matrix(new Vector(1, 0, 0), 
                new Vector(0, Math.Cos(Algebra.convertToRad(rotationAngleThroughXAxis)),
                Math.Sin(Algebra.convertToRad(rotationAngleThroughXAxis))),
                new Vector(0, (-1) * Math.Sin(Algebra.convertToRad(rotationAngleThroughXAxis)),
                Math.Cos(Algebra.convertToRad(rotationAngleThroughXAxis)))),
                new Vector());
        }


        public static Matrix4By4 YRotation(double rotationAngleThroughYAxis) {
            Matrix4By4 rm =  new Matrix4By4(new Matrix(new Vector(Math.Cos(Algebra.convertToRad(rotationAngleThroughYAxis)),
                                                            0, 
                                                            (-1) * Math.Sin(Algebra.convertToRad(rotationAngleThroughYAxis))), 
                                                            new Vector(0, 1, 0), 
                                                            new Vector(Math.Sin(Algebra.convertToRad(rotationAngleThroughYAxis)),
                                                            0,
                                                            Math.Cos(Algebra.convertToRad(rotationAngleThroughYAxis)))),
                                                            new Vector());
            return rm;
        }


        public static Matrix4By4 ZRotation(double rotationAngleThroughZAxis) {
            Matrix4By4 rm = 
                new Matrix4By4(new Matrix(
                    new Vector(Math.Cos(Algebra.convertToRad(rotationAngleThroughZAxis)), Math.Sin(Algebra.convertToRad(rotationAngleThroughZAxis)),0), 
                    new Vector((-1)*Math.Sin(Algebra.convertToRad(rotationAngleThroughZAxis)),Math.Cos(Algebra.convertToRad(rotationAngleThroughZAxis)),0), 
                    new Vector(0,0,1)), 
                new Vector());
            return rm;
        }


        public static Matrix4By4 operator +(Matrix4By4 firstMatrix, Matrix4By4 secondMatrix) {
            return new Matrix4By4(firstMatrix.m + secondMatrix.m, firstMatrix.v1 + secondMatrix.v1);
        }

       public static Matrix4By4 operator *(Matrix4By4 firstMatrix, Matrix4By4 secondMatrix) {
            return new Matrix4By4(firstMatrix.m * secondMatrix.m, firstMatrix.m*secondMatrix.v1 + firstMatrix.v1);
        }

        public static Vector operator *(Matrix4By4 firstMatrix, Vector vectorV) {
            Vector r1 = new Vector(firstMatrix.m.v1.x, firstMatrix.m.v2.x, firstMatrix.m.v3.x);
            Vector r2 = new Vector(firstMatrix.m.v1.y, firstMatrix.m.v2.y, firstMatrix.m.v3.y);
            Vector r3 = new Vector(firstMatrix.m.v1.z, firstMatrix.m.v2.z, firstMatrix.m.v3.z);
            return new Vector(r1*vectorV, r2*vectorV, r3*vectorV);
        }

        public static Matrix4By4 Transpose(Matrix4By4 matrix) {
            Vector firstRow = new Vector(matrix.m.v1.x, matrix.m.v2.x, matrix.m.v3.x);
            Vector secondRow = new Vector(matrix.m.v1.y, matrix.m.v2.y,matrix.m.v3.y);
            Vector thirdRow = new Vector(matrix.m.v1.z, matrix.m.v2.z, matrix.m.v3.z);
            return new Matrix4By4(new Matrix(firstRow,secondRow,thirdRow),new Vector(0,0,0));
        }

    }
}
