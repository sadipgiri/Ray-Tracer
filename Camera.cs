using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Camera
    {
        public Vector position, u, v, w, lookAt;
        public double fov;
        public int numberOfSamples;
        public int numberOfJittered;
        public int height = 700;
        public int width = 700;
        public double pixelSize = 1;

        public int rayDepth;

        private double apertureSize;
        private double focalLength;
        private bool DOFUsed;

        private bool IsReflective;



        public Camera(Vector position, Vector lookAt) // for the perspective camera
        {
            this.position = position; // this is the location of the camera
            this.lookAt = lookAt; // this is the looking point of the perspective camera
            this.fov = 60; // setting fov as 52 degree angle
            this.numberOfSamples = 1; // setting number of samples to be 1 as default
            this.numberOfJittered = 1; // setting number of jittered to be 1 as default
            //Calculate u, v, w from position and lookAt
            Vector cameraMoveVector = new Vector(0, 1, 0);

            //this.rayDepth = 5;

            //to make the camera to move 
            //checking whether the x and y coordinates of position of camera to be zero than giving it's camera move vector to be new vector that is (1,0,0)
            if (position.x == 0 && position.z == 0)
                cameraMoveVector = new Vector(1, 0, 0);

            w = (lookAt - position).Normalize(); // calculating w which is the unit position vector from position to look at point of the camera
            u = cameraMoveVector % w; // calculating u which is the cross product of y and w
            v = w % u; // calculating v which is the cross product of w and u

        }

        // constructor initializer list
        public Camera(Vector position, Vector lookAt, int numberOfSamples) : this(position, lookAt) {
            this.numberOfSamples = numberOfSamples;
        }

        //constructor initializer list
        public Camera(Vector position, Vector lookAt, double fov) : this(position, lookAt) {
            this.fov = fov;
        }

        // constructor initializer list
        public Camera(Vector position, Vector lookAt, double fov, int numberOfSamples) : this(position, lookAt)
        {
            this.fov = fov; // setting fov as optional parameter for defining the camera
            this.numberOfSamples = numberOfSamples; // setting numberOfSamples as optional parameter for defining the camera
        }

        // making a methos for checking whether depth of field is given for the camera or not
        // so that it will easier for to check it and then only continue calculating other things for blur image
        public void useDOF(double apertureSize, double focalLength)
        {
            this.apertureSize = apertureSize;
            this.focalLength = focalLength;
            DOFUsed = true;
        }

        // this helps to convert to world camera coordinates for perspective camera
        public Vector convertCameraToWorldCoordinates(Vector point)
        {
            return (u * point.x + v * point.y + w * point.z); // converting into world coordinates
        }


        public void Render(Scene scene, Bitmap bmp)
        {
            //creating a array for storing different types of combinations for jittered sampling
            double[] possibleCombinations = new double[numberOfJittered];

            //if jittered is given then only it will push variables to the array
            if (numberOfJittered > 1) {
                for (int l = 0; l < numberOfJittered; l++) {
                    possibleCombinations[l] = (double)(2 * l + 1) / (numberOfJittered * 2);
                }
            }

            // getting the color of the 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                   
                    // for getting random values from 0 to 1 for super sampling or random sampling
                    Random randomNumber = new Random();

                    // creating one empty color space for averaging the color for that pixel in multi sampling
                    SColor samplingColor = new SColor();

                    //giving background color as black at first
                    bmp.SetPixel(j, i, Color.FromArgb(255, 0, 0, 0));



                    //this is the loop for anti-aliasing(AA) that is number of samples
                    if (numberOfJittered > 1) numberOfSamples = (int)Math.Pow(numberOfJittered, 2);

                    double dz = (height / 2) / (Math.Tan(Algebra.convertToRad(fov * 0.5))); // calculating dz
                    if (DOFUsed)
                    {
                        pixelSize = focalLength / dz;
                    } else
                    {
                        pixelSize = 1 / dz;
                    }

                    Vector pointInThePixel = (new Vector((-width / 2), (height / 2), 0) + new Vector(0.5, -0.5, 0) + new Vector(j, -i, position.z)) * pixelSize; // changing the basis i.e. in terms of i and j of the image
                    Double wOffset = 1;
                    if (DOFUsed)
                    {
                        wOffset = focalLength;
                    }
                    for (int k = 0; k < numberOfSamples; k++)
                    {
                        Ray ray = new Ray(position, new Vector());
                        //checking number of jittered is given or not through it's default value 
                        //that is if number of jittered is not given than calculating as usual number of sampling way 
                        //otherwise that is if number of jittered is given than calculating number of jitterd out of it and checking through midpoints of those number of samples of each pixel

                        if (numberOfJittered == 1)
                        {
                            double m = randomNumber.NextDouble(); // gives random number between 0 and 1

                            ////
                            //Vector coordinate = new Vector((-width / 2), (height / 2), 0) + new Vector(0.5, -0.5, 0) + new Vector(j, -i, position.z); // changing the basis i.e. in terms of i and j of the image
                            ////

                            //if number of samplings is 1 then here the value of m will be 0.5 otherwise it will be m which is the random number from 0 to 1
                            double dx = (j - (width / 2) + (numberOfSamples == 1 ? 0.5 : m)) * pixelSize; // calculating dx
                            double dy = (((height / 2) - i) + (numberOfSamples == 1 ? 0.5 : m)) * pixelSize; // calculating dy
                            //double dz = 1;// (height / 2) / (Math.Tan(Algebra.convertToRad(fov * 0.5))); // calculating dz
                            pointInThePixel = new Vector(dx, dy, wOffset);
                            ray.direction = convertCameraToWorldCoordinates(pointInThePixel).Normalize(); // so vector (dx, dy, dz) will be the direction of the ray which is normalized

                        }
                        else
                        {
                            int kX = (k % numberOfJittered); // this is for finding the index of the array for giving possible value of mid points of jittered grid

                            int kY = (k / numberOfJittered); // this is for finding the next index of the array for giving possible value pf mid points of jittered grid

                            //Debug.Assert(kX == 0.25);

                            double dx = j - (width / 2) + (numberOfSamples == 1 ? 0.5 : possibleCombinations[kX]); // calculating dx where possible combination is given by indexing the array
                            double dy = ((height / 2) - i) + (numberOfSamples == 1 ? 0.5 : possibleCombinations[kY]); // calculating dy
                            //double dz = 1;// (height / 2) / (Math.Tan(Algebra.convertToRad(fov * 0.5))); // calculating dz
                            ray.direction = convertCameraToWorldCoordinates(new Vector(dx, dy, 1)).Normalize(); // so vector (dx, dy, dz) will be the direction of the ray which is normalized

                        }


                        if (DOFUsed)
                        {
                            ray = FindDOFRay(pointInThePixel);

                        }
                        
                          // adding all the color of the samples that is given for multi sampling

                    samplingColor += TraceRay(ray, scene, 0);
                }

                //taking the average of the color of those all samples
                SColor colorSampling = samplingColor / numberOfSamples;

                //now giving that average color of all those samples
                bmp.SetPixel(j, i, Color.FromArgb(colorSampling.GetAlphaColor(), colorSampling.GetRedColor(), colorSampling.GetGreenColor(), colorSampling.GetBlueColor()));
            }
        }

    }
    // method for finding the new ray origin and new ray direction according to the DOF
    Ray FindDOFRay(Vector pointInThePixel)
    {
        //getting random value for aperture size that is the radius if the circle which will be in blur
        double randomRadius = Algebra.getRandomNumber(0, apertureSize);

        //similarly getting the random angle of the circle for getting the random point in the aperture size or the circle in focus
        double randomAngle = Algebra.getRandomNumber(0, 2 * Math.PI);

        //new x and y positions of camera are determined by random radius and random angle of circle having radius equal to  aperture size 
        double xPositionOfCamera = randomRadius * Math.Cos(Algebra.convertToRad(randomAngle));
        double yPositionOfCamera = randomRadius * Math.Sin(Algebra.convertToRad(randomAngle));

        //here assigning new x and y values for camera 
        Vector rayOrigin = new Vector(xPositionOfCamera, yPositionOfCamera, 0);

        //similarly finding new ray direction and converting to world coordinates and getting it's unit vector 
        Vector direction = pointInThePixel - rayOrigin;
        Vector newRayDirection = convertCameraToWorldCoordinates(direction).Normalize();
        return new Ray(rayOrigin + position, newRayDirection);
    }
    //creating the method for multiplying matrix 4by4 and point of intersection
    Vector FindNewPoint(Matrix4By4 matrix, Vector point)
    {
        Vector vector1 = matrix * point;
        return (vector1 + matrix.v1);

    }

        SColor TraceRay(Ray ray, Scene scene, int rayDepth)
        {
            int maximumNumberOfReflections = 10;

            if (rayDepth >= maximumNumberOfReflections)
                return new SColor(0,0,0,0);

            SColor shapeColor = new SColor();

            SColor reflectionColor = new SColor();

            SColor averageReflectionColor = new SColor();

            Shape closestShape = scene.shapes[0]; // rendering multiple objects so searching for the closest shape to render

            double closestT = double.MaxValue; // shape which has closest T need to be rendered at first

           

            foreach (Shape shape in scene.shapes)
            {
                //transforming the ray origin along with the given tranformation matrices
                // origin is transformed like the point tranformation whereas ray direction is transformed like the vector transformation

                Vector newOrigin = FindNewPoint(shape.inverseTransformMatrix, ray.origin);
                Vector newDirection = (shape.inverseTransformMatrix * ray.direction).Normalize();

                double t = shape.DoesIntersect(newOrigin, newDirection); // passing transformed origin and transformed direction 
                if ((t < closestT) && (t >= 0.0001))
                {
                    closestT = t;
                    closestShape = shape;
                }
            }

            Vector closeOrigin = FindNewPoint(closestShape.inverseTransformMatrix, ray.origin);
            Vector closeDirection = (closestShape.inverseTransformMatrix * ray.direction).Normalize();


            if (closestShape.DoesIntersect(closeOrigin, closeDirection) >= 0) // checking whether the ray hits the sphere or not
            {
                Vector point = closeOrigin + (closeDirection * closestT); // point of intersection

                SColor color = closestShape.material.ColorAtPoint(point);
                double ambient = closestShape.material.ambient;
                double specularCoefficient = closestShape.material.specularCoefficient;

                SColor lightContribution = new SColor();


                //transforming the point of intersection according to the transformation matrices
                point = FindNewPoint(closestShape.transformMatrix, point);

                Vector normal = closestShape.NormalAtPoint(point);

                // also transforming the normal of the shapes according to the transformation matrix
                normal = Matrix4By4.Transpose(closestShape.inverseTransformMatrix) * normal;

                // for multiple lighting
                foreach (Light light in scene.lights)
                {
                    // for also checking shadows
                    Vector shadowRayDirection = (light.location - point).Normalize(); // calculating the direction of the ray of the shadow

                    // at first assigning inShadow variable boolean value false so that we can check whether any shape is in shadow or not
                    bool inShadow = false;

                    foreach (Shape shape in scene.shapes)
                    {
                        //checking whether each shape is in shadow or not
                        // also checking whether the distance from the point of intersection and the direction of the ray is greater than the ray intersection distance

                        //transforming the ray origin along with the given tranformation matrices
                        Vector transformedShadowRayDirection = (shape.inverseTransformMatrix * shadowRayDirection).Normalize();
                        if (shape.DoesIntersect(point + (normal * 0.5), transformedShadowRayDirection) >= 0 && Math.Abs((light.location - point).Magnitude()) > shape.DoesIntersect(position, closeDirection))
                        {
                            inShadow = true;
                            break; // giving break point if it's in in shadow
                        }
                    }
                    if (inShadow)
                    {
                        continue; // if it's in shadow than continuing that is casting a shadow
                    }

                    Vector lightToPoint = point - light.location; // position vector from light to point of intersection
                    Vector lightDriection = lightToPoint.Normalize(); // and then normalizing it to get the direction of that vector
                    double cosineAngle = -(lightDriection * normal); // finding the scalar value which gives the light intensity

                    
                    if (cosineAngle < 0)
                        cosineAngle = 0;

                    // for specular highlight
                    Vector cameraToIntersectionPoint = (position - point).Normalize();
                    Vector reflectedRayDirection = (closestShape.material.ReflectedRay(ray.origin, ray.direction, closestShape).direction).Normalize();

                    
                   
                    

                    //averageReflectionColor = reflectionColor * (1 / rayDepth);
                    //color = averageReflectionColor + color;


                    double specularFactor = Math.Pow((cameraToIntersectionPoint * reflectedRayDirection), closestShape.material.smoothness);
                    if (specularFactor < 0)
                        specularFactor = 0;
                 

                    if (cosineAngle >= 0 || specularFactor >= 0)
                    {
                        lightContribution = lightContribution + (light.lightColor * color * (cosineAngle*closestShape.material.diffuseCoefficient + specularFactor * closestShape.material.specularCoefficient) * light.Intensity);
                    }
                }
                /*
                if (rayDepth < maximumNumberOfReflections)
                {
                    Ray reflectedRay = closestShape.material.ReflectedRay(closeOrigin, closeDirection, closestShape);
                    reflectionColor = TraceRay(reflectedRay, scene, rayDepth + 1); // recursive method
                }
                */
                shapeColor = lightContribution + (color * ambient);
                
            }

           
            return (shapeColor + closestShape.material.reflectionCoefficient * reflectionColor);

        }
}
}



