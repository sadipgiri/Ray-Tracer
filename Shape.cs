using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Shape
    {
        public Vector position;
        public Material material;
        public Matrix4By4 transformMatrix = new Matrix4By4(new Matrix(), new Vector());
        public Matrix4By4 inverseTransformMatrix = new Matrix4By4(new Matrix(), new Vector());

        public Shape(Vector position, Material material)
        {
            this.position = position;
            this.material = material;
 
        }


        public virtual double DoesIntersect(Vector origin, Vector direction)
        {
            return -1;
        }

        public virtual Vector NormalAtPoint(Vector point)
        {
            return new Vector(0, 0, 0);
        }

        /*
        // to find the reflected ray after reflection
        public virtual Vector ReflectedRay(Vector direction, Vector normal) {
            Vector reflectionRay = direction - 2 * (normal * direction) * normal;
            return reflectionRay;
        }*/

        public void AddTranslationToTranformationMatrix(Vector translate) {
             transformMatrix = new Matrix4By4(new Matrix(), translate) * transformMatrix;
            inverseTransformMatrix *= new Matrix4By4(new Matrix(), (-1) * translate); 
         }

        public void AddScaleToTransformationMatrix(Vector scale) {
            transformMatrix = new Matrix4By4(new Matrix(new Vector(scale.x,0,0), new Vector(0,scale.y,0), new Vector(0,0,scale.z)), new Vector());
            inverseTransformMatrix *= new Matrix4By4(new Matrix(new Vector((1/scale.x),0,0), new Vector(0,(1/scale.y),0), new Vector(0,0,(1/scale.z))), new Vector());
        }

        public void AddRotationThroughXAxisToTransformationMatrix(double rotationAngleThroughXAxis) {
            transformMatrix = Matrix4By4.XRotation(rotationAngleThroughXAxis) * transformMatrix;
            inverseTransformMatrix *= Matrix4By4.XRotation(-rotationAngleThroughXAxis);
        }

        public void AddRotationThroughYAxisToTransformationMatrix(double rotationAngleThroughYAxis) {
            transformMatrix = Matrix4By4.XRotation(rotationAngleThroughYAxis) * transformMatrix;
            inverseTransformMatrix *= Matrix4By4.YRotation(-rotationAngleThroughYAxis);
        }

        public void AddRotationThroughZAxisToTransformationMatrix(double rotationAngleThroughZAxis) {
            transformMatrix = Matrix4By4.ZRotation(rotationAngleThroughZAxis) * transformMatrix;
            inverseTransformMatrix *= Matrix4By4.ZRotation(-rotationAngleThroughZAxis);
        }

        public void AddRotationThroughAllAxisToTransformationMatrix(double rotationAngleThroughXAxis, double rotationAngleThroughYAxis, double rotaionAngleThroughZAxis) {
            transformMatrix = Matrix4By4.ZRotation(rotaionAngleThroughZAxis) * Matrix4By4.YRotation(rotationAngleThroughYAxis) * Matrix4By4.XRotation(rotationAngleThroughXAxis) * transformMatrix;
            inverseTransformMatrix *= Matrix4By4.XRotation(-rotationAngleThroughXAxis) * Matrix4By4.YRotation(-rotationAngleThroughYAxis) * Matrix4By4.ZRotation(-rotaionAngleThroughZAxis);
        }



       
    }
}
