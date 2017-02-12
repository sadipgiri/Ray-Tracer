using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Cylinder:Shape
    {
        // there is some problem with the height of the cylinder that is the torso


        public Vector secondCenter;
        public double radius;

        public Cylinder(Vector position, Vector secondCenter, double radius, Material material) : base(position, material) {
            this.secondCenter = secondCenter;
            this.radius = radius;
        }

        // followed explicitly geometric approach and took help from below link
        // http://mrl.nyu.edu/~dzorin/rend05/lecture2.pdf

        public override double DoesIntersect(Vector origin, Vector direction)
        {
            //


            // this is the direction of vertical axis of the cylinder
            Vector verticalAxis = (position - secondCenter).Normalize();

            // first check the intersection with the upper plane 
            double dotProductWithUpperPlane = verticalAxis * direction;

            if (dotProductWithUpperPlane < 0) {
                double dotProductWithFirstCenter = -(verticalAxis * position);
                double firstT = -(dotProductWithFirstCenter + verticalAxis * origin) / dotProductWithUpperPlane;
                Vector point1 = origin + direction * firstT;

                if (Math.Pow((point1 - position).Magnitude(), 2) <= Math.Pow(radius,2)) {
                    return firstT;
                }
            }

            // check the intersection with the lower plane
            double dotProductWithLowerPlane = ((-1) * verticalAxis) * direction;
            if (dotProductWithLowerPlane < 0) {
                double dotProductWithSecondCenter = -(((-1)*verticalAxis) * secondCenter);
                double secondT = -(dotProductWithSecondCenter + ((-1)*verticalAxis) * origin) / dotProductWithLowerPlane;
                Vector point2 = origin + direction * secondT;

                if (Math.Pow((point2 - secondCenter).Magnitude(), 2) <= Math.Pow(radius, 2))
                {
                    return secondT;
                }
            }

            // check the intersection with the main part that is torso of the cylinder
            Vector positionVectorBetweenTwoCenters = position - secondCenter;
            Vector positionVectorBetweenOriginAndFirstCenter = origin - position;
            Vector crossProductWithPositionVectorBetweenRayOriginAndFirstCenter = positionVectorBetweenOriginAndFirstCenter % positionVectorBetweenTwoCenters;
            Vector crossProductWithRayDirection = direction % positionVectorBetweenTwoCenters;

            double ab2 = positionVectorBetweenTwoCenters * positionVectorBetweenTwoCenters;
            double A = crossProductWithRayDirection * crossProductWithRayDirection;
            double B = 2 * (crossProductWithRayDirection * crossProductWithPositionVectorBetweenRayOriginAndFirstCenter);
            double C = (crossProductWithPositionVectorBetweenRayOriginAndFirstCenter * crossProductWithPositionVectorBetweenRayOriginAndFirstCenter) - (Math.Pow(radius,2) * ab2);

            double discriminant = Math.Pow(B, 2) - 4 * A * C;
            if (discriminant < 0)
                return -1;

            double t1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
            double t2 = (-B - Math.Sqrt(discriminant))/(2 * A);

            double t = (t1 < t2) ? t1 : t2;

            if (t < 0)
                t = (t1 > t2) ? t1 : t2;

            if (t > 0)
            {
                Vector point = origin + direction * t;
                double dotFirstCenter = (point - position) * direction;
                double dotSecondCenter = (point - position) * ((-1) * direction);

                if (dotFirstCenter > 0 || dotSecondCenter > 0)
                    t = -1;
            }

            return t;
        }

        // for finding the normal of the cylinder
        public override Vector NormalAtPoint(Vector point)
        {
            // following Justin trick 
            // which I found really cool and interesting too
            /*
            // here making the y-component zero and normalizing it 
            // that is fixing the normal but we have to transfer the or scale to make the cylinder that we want to implement or use
            point.y = 0;
            return point.Normalize();
            */

            // but still above idea didn't work so trying to figure out the different approach
            
            //setting the epsilon
            double epsilon = Math.Pow(10,-6);

            // at first finding out the vertical axis of the given cylinder
            Vector verticalAxis = (position - secondCenter).Normalize();

            // check if the point is on the upper plane
            double d1 = -(verticalAxis * position);
            if (Math.Abs(verticalAxis * point + d1) <= epsilon)
                return verticalAxis;

            // check if the point is on the lower plane
            double d2 = -((-1)*verticalAxis * secondCenter);

            if (Math.Abs((-1) * verticalAxis * point + d2) <= epsilon)
                return (-1)*(verticalAxis);

            // finding the normal vector to the plane formed by two centers and the intersection point
            Vector perpendicularVector = (point - secondCenter) % verticalAxis;

            // the direction of the axis of the cylinder cross the above perpendicular vector will give me the normal
            return (verticalAxis % perpendicularVector).Normalize();
           
            
        }
    }
    }

