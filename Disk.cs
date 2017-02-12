using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Disk : Shape // inheritating shape class where the center of the disk is related to the position of the shape class
    {
        public Vector normal; // this is the normal of the plane containing the disk
        public double radius; // radius of the disk

        public Disk(Vector normal, Vector position, double radius, Material material) : base(position, material)
        {
            this.normal = normal;
            this.radius = radius;
        }

        // creating a function to check whether the ray intersect the plane or not containing disk on it
        double DoesIntersectDiskPlane(Vector origin, Vector direction)
        {
            Plane diskPlane = new Plane(normal, position, material); // checking whether the ray intersect the plane containing the disk
            return diskPlane.DoesIntersect(origin, direction);
        }

        // knowing whether the ray intersect the disk or not 
        public override double DoesIntersect(Vector origin, Vector direction)
        {
            double t = DoesIntersectDiskPlane(origin, direction);
            if (t > 0) // at first let's find whether the ray intersect the plane or not 
            {                                           // if so then 
                Vector p = origin + direction * t;      // point of intersection in the plane
                Vector q = p - position;                // position vector from point to center of the disk
                double d = q.Magnitude();         // magnitude of the vector which gives the distance inside the disk
                if (d > radius)
                {
                    t = -1;
                }
            }

            return t;
        }

        // getting normal of a disk
        public override Vector NormalAtPoint(Vector point)
        {
            return this.normal;
        }
    }
}
