using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Triangle : Shape
    {
        public Vector secondPoint, thirdPoint;
        //public double alpha, beta, gamma;

        public Triangle(Vector position, Vector secondPoint, Vector thirdPoint, Material material) : base(position, material){
            this.secondPoint = secondPoint;
            this.thirdPoint = thirdPoint;
        }

        public override double DoesIntersect(Vector origin, Vector direction)
        {
            Matrix A = new Matrix(new Vector(position.x - secondPoint.x, position.y - secondPoint.y, position.z - secondPoint.z), new Vector(position.x - thirdPoint.x, position.y - thirdPoint.y, position.z - thirdPoint.z), new Vector(direction.x, direction.y, direction.z));

            Matrix A0 = new Matrix(new Vector(position.x - origin.x, position.y - origin.y, position.z - origin.z), new Vector(position.x - thirdPoint.x, position.y - thirdPoint.y, position.z - thirdPoint.z), new Vector(direction.x, direction.y, direction.z));

            Matrix A1 = new Matrix(new Vector(position.x - secondPoint.x, position.y - secondPoint.y, position.z - secondPoint.z), new Vector(position.x - origin.x, position.y - origin.y, position.z - origin.z), new Vector(direction.x, direction.y, direction.z));


            double beta = (A0.Determinant()) / (A.Determinant());

            double gamma = (A1.Determinant()) / (A.Determinant());

            if ((beta + gamma) > 1)
                return -1;

            

            double alpha = 1 - (beta + gamma);

            if ((0 < alpha && alpha < 1) && (0 < beta && beta < 1) && (0 < gamma && gamma < 1) && (beta + gamma) < 1 && (alpha + beta) < 1 && (alpha + gamma) < 1)
            {
                Matrix A2 = new Matrix(new Vector(position.x - secondPoint.x, position.y - secondPoint.y, position.z - secondPoint.z), new Vector(position.x - thirdPoint.x, position.y - thirdPoint.y, position.z - thirdPoint.z), new Vector(position.x - origin.x, position.y - origin.y, position.z - origin.z));

                double t = (A2.Determinant()) / (A.Determinant());
                return t;
            }
            return -1;
        }

        // to find the normal of a triangle
        public override Vector NormalAtPoint(Vector point)
        {
            // finding out the first position vector
            Vector onePointToAnother = (secondPoint - position);

            // another position vector of the triangle
            Vector anotherPointToAnother = (thirdPoint - position);

            // and then finding out the normal by cross product of those two position vectors
            Vector normal = (onePointToAnother % anotherPointToAnother).Normalize(); // and so normalizing the normal
            return normal;
        }



    }
}
