using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Torus:Shape
    {
        // I don't know why my torus is not working 
        // Actually, I want to make a torus where
        // users need to give two radii (minor and major radius) along with it's axis
    
        public Vector axis;
        public double majorRadius;
        public double minorRadius;

        public Torus(Vector position, Vector axis, double majorRadius, double minorRadius, Material material) :base(position, material){
            this.position = position;
            this.axis = axis.Normalize(); // normalizing the axis of the given torus
            this.majorRadius = majorRadius;
            this.minorRadius = minorRadius;
            this.material = material;
        }

        // using the efficient ray torus intersection 
        // taking help from the following link
        //http://users.wowway.com/~phkahler/torus.pdf

        public override double DoesIntersect(Vector origin, Vector direction) {

            axis = axis.Normalize();

            // creating an array for calculating the four t values 
            // that is four roots of polynomial equation 
            double[] tValues = new double[4];

            // finding the position vector from center to the ray origin
            Vector centerToRayOrigin = origin - position; //Q
            double AxisDotCenterToRayOrigin = axis * centerToRayOrigin; //u
            double AxisDotRayDirection = axis * direction; //v

            // for solving quadratic equation
            double a = direction*direction - Math.Pow(AxisDotRayDirection,2);
            double b = 2 * (centerToRayOrigin * direction - AxisDotCenterToRayOrigin * AxisDotRayDirection);
            double c = centerToRayOrigin * centerToRayOrigin - Math.Pow(AxisDotCenterToRayOrigin,2);

            // before the calculation of t value 
            // checking whether this quadratic equation is solvable or not inorder to return -1 before the calculation of huge t values
            /*if (Math.Pow(b,2) < (4*a*c))
                return -1;
*/
            // since there will be four t values 
            // that means we need to calculate 4 order polynomial equation 
            // i.e At^4 + B^t^3 + Ct^2 + Dt + E = 0.

            // before finding the corresponding components of t i.e. A,B,C,D,E
            // let us create another variable so that it will be easier to compute and we'd also understand the equation properly
            double d = (centerToRayOrigin*centerToRayOrigin + Math.Pow(majorRadius,2) - Math.Pow(minorRadius,2)); // creating a variable

            // creating corresponding components of t
            double A = 1; // (direction * direction)^2 since direcion of ray is an unit vector so it is equal to be 1
            double B = 4 * centerToRayOrigin * direction;
            double C = 2 * d + (1 / 4) * Math.Pow(B, 2) - 4 * Math.Pow(majorRadius, 2) * a;
            double D = B * d - 4 * Math.Pow(majorRadius, 2) * b;
            double E = Math.Pow(d, 2) - 4 * Math.Pow(majorRadius, 2) * c;

            // now finding out the t values
            // at first creating other formulas so that it will be helpful to solve it explicitly
            double p1 = 2 * Math.Pow(C, 3) - 9 * B * C * D + 27 * A *Math.Pow(D, 2) + 27 * Math.Pow(B, 2) * E - 72 * A * C * E;
            double p2 = p1 + Math.Sqrt((-4) * Math.Pow(Math.Pow(C, 2) - 3 * B * D + 12 * A * E, 3) + Math.Pow(p1, 2));
            double p3 = (C * C - 3 * B * D + 12 * A * E) / (3 * A * Math.Pow((p2 / 2), (1 / 3))) + (Math.Pow(p2 / 2, (1 / 3))) / (3 * A);
            double p4 = Math.Sqrt((Math.Pow(B,2))/(4*Math.Pow(A,2)) - (2*C)/(3*A) + p3);
            double p5 = Math.Pow(B, 2) / (2 * Math.Pow(A, 2)) - (4 * C) / (3 * A) - p3;
            double p6 = ((-1) * Math.Pow(B, 3) / Math.Pow(A, 3) + (4 * B * C) / Math.Pow(A, 2) - (8 * D) / A) / (4 * p4);

            // before calculating 4 roots i.e. 4 t values 
            // let's check some of the conditions
            /*
            if ((p5 - p6) < 0d)
                return -1;
            if ((p5 + p6) < 0d)
                return -1;
*/

            //since p4 value is returning NAN always so providing some condition so that it'd be fixed

            /*if (Math.Pow(p4, 2) < 0)
                p4 = 0;*/

            
            double epsilon = 0.0000001;

            // now calculating 4 t values
            // want to check in every positive t values and adding it to container
            double t1 = (-B) / (4 * A) - p4 / 2 - Math.Sqrt(p5 - p6) / 2;
            if(t1 > epsilon)
                tValues[0] = t1;
            double t2 = (-B) / (4 * A) - p4 / 2 + Math.Sqrt(p5 - p6) / 2;
            if(t2 > epsilon)
                tValues[1] = t2;
            double t3 = (-B) / (4 * A) + p4 / 2 - Math.Sqrt(p5 + p6) / 2;
            if(t3 > epsilon)
                tValues[2] = t3;
            double t4 = (-B) / (4 * A) + p4 / 2 + Math.Sqrt(p5 + p6) / 2;
            if(t4 > epsilon)
                tValues[3] = t4;

            double t = tValues.Min(); // at last finding put the minimum t value from all the positive t values

            return t;
        }

        // finding out the normal of the torus 
        public override Vector NormalAtPoint(Vector point)
        {
            // dot product of given axis of the torus and posiition vector from position of the torus to the given point of intersection
            double dotAxisWithPositionVector = (point - position) * axis;
            Vector difference = (point - position) - (dotAxisWithPositionVector * axis);
            Vector differenceNormalization = difference.Normalize(); // normalizing because we need direction (that is direction)
            Vector n = point - position - (differenceNormalization * majorRadius); // finding out the normal
            Vector normal = n.Normalize(); // normalizing the normal
            return normal;
        }
    }
}
