using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Sphere : Shape // inheritating shape class where center of the sphere is related to position of shape
    {
        public double radius;

        // defining a sphere with it's parameters
        public Sphere(double radius, Material material, Vector position) : base(position, material){
            this.radius = radius;
        }

        public override Vector NormalAtPoint(Vector point) {
            Vector positionToPoint = point - position;
            // normalizing position to point vector
            return positionToPoint.Normalize();

        }
        // knowing whether the ray intersect the sphere or not
        // strictly using geometric approach for efficient ray tracing 
        public override double DoesIntersect(Vector origin, Vector direction) {
            Vector p = position - origin; // position vector from center of the sphere to origin of the ray
            double d = direction * p; // projection onto the ray

            if (d < 0)
            {
                return -1;
            }

            double q = (p * p) - (d * d); // distance from the center to the ray hitting the sphere
            // finding 'x' distance inside the sphere

            double x = (radius * radius) - q; // here q is a squared term itself
            double t1 = 0;
            double t2 = 0;

            if (x < 0) {
                return x;
            } else if (x > 0) {
                t1 = d - Math.Sqrt(x); // x is a squared term so need to find its square root
                t2 = d + Math.Sqrt(x); 
            }

            if ((t1 < 0) && (t2 < 0))
            {
                return -1;
            }

            if (t1 < 0)
            {
                t1 = t2;
            }
            return t1;
        }
    }
}
