using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Box : Shape // inheriting shape class where minPosition of the box is related to position of the shape class
    {
        public Vector maxPosition; // this is the another position of the box

      

        // box needs two Vector position i.e. one minimum vector position and another maximum Vector position
        public Box(Vector position, Vector maxPosition, Material material) : base(position, material){
            this.maxPosition = maxPosition;
        }

        

        // making does intersect method which returns t values
        public override double DoesIntersect(Vector origin, Vector direction) {

            double txmin = (position.x - origin.x) / direction.x; // this is minimum x value of t
            double txmax = (maxPosition.x - origin.x) / direction.x; // this is maximum x value of t

            // if txmin is greater than txmax than interchanging the txmin and txmax
            if (txmin > txmax)
                Algebra.swap(txmin, txmax);  
            

            // also doing the same process for y values of minimum and maximum positions of a box
            double tymin = (position.y - origin.y) / direction.y;
            double tymax = (maxPosition.y - origin.y) / direction.y;

            // if tymin is greater than tymax than interchanging both values
            if (tymin > tymax) 
                Algebra.swap(tymin, tymax);
            

            // ray doesnot intersect if either txmin is greater than tymax or tymin is greater than txmax
            if ((txmin > tymax) || (tymin > txmax)) 
                return -1;
           

            // whereas txmax is greater than tymax than txmax will be tymax
            if (tymax < txmax) 
                txmax = tymax;
            

            // similarly doing same process for z of position vectors of box i.e. minimum and maximum vectors
            double tzmin = (position.z - origin.z) /direction.z;
            double tzmax = (maxPosition.z - origin.z) / direction.z;

            // similarly swaping tzmin and tzmax if tzmin is greater than tzmax
            if (tzmin > tzmax) 
                Algebra.swap(tzmin, tzmax);
            

            // ray doesnot intersect either txmin is greater than tzmax or tzmin is greater than txmax
            if ((txmin > tzmax) || (tzmin > txmax)) 
                return -1;
            

            // if tzmin is greater than txmin than assigning the value of txmin as tzmin
            if (tzmin > txmin) 
                txmin = tzmin;
            

            // if tzmax is greater than txmax than assigning the value of tzmax as txmax
            if (tzmax < txmax) 
                txmax = tzmax;
           

            // so returning tmin at last
            return txmin > txmax ? txmax : txmin;
        }

        // getting the normal of a box
        public override Vector NormalAtPoint(Vector point)
        {
            const double EPSILON = 0.000001; // for precise floating point
            // checking the condition and returning the normals of the box from different positions
            if (Math.Abs(position.x - point.x) < EPSILON)
                return new Vector(-1, 0, 0);
            if (Math.Abs(position.y - point.y) < EPSILON)
                return new Vector(0, -1, 0);
            if (Math.Abs(position.z - point.z) < EPSILON)
                return new Vector(0, 0, -1);
            if (Math.Abs(maxPosition.x - point.x) < EPSILON)
                return new Vector(1, 0, 0);
            if (Math.Abs(maxPosition.y - point.y) < EPSILON)
                return new Vector(0, 1, 0);
            if (Math.Abs(maxPosition.z - point.z) < EPSILON)
                return new Vector(0, 0, 1);

            return new Vector();
        }

    }
}
