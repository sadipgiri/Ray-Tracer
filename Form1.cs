using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randomImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                       

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // making a sphere by assigning it it's parameters
            Vector sphereCenter = new Vector(0,0,0);
            Material material1 = new Material(new SColor(1,0,0,1));
            Sphere sphere1 = new Sphere(30, material1, sphereCenter);
            sphere1.AddScaleToTransformationMatrix(new Vector(1,4,1));
            //sphere1.AddTranslationToTranformationMatrix(new Vector(-10,0,0));
            //sphere1.AddRotationThroughAllAxisToTransformationMatrix(0,0,0);
            //sphere1.AddRotationThroughXAxisToTransformationMatrix(90);
            sphere1.AddRotationThroughZAxisToTransformationMatrix(145);
          

            // making a sphere by assigning it it's parameters
            Vector sphereCenter1 = new Vector(100, -100, 0);
            Sphere sphere2 = new Sphere(30, new Material(new SColor(1,1,0,1)), sphereCenter1);
            //sphere2.material.smoothness = 100;

            //making a sphere by assigning it it's parameters
            Sphere sphere3 = new Sphere(60, new Stripes(new SColor(0,0,1,1), new SColor(1,0,1,1)), new Vector(-100,100,0));

            // making a plane by assigning it it's parameters
            Vector planeNormal = new Vector(0,1,0); // this will give a plane which direction is towards the direction of camera.
            Vector planePoint = new Vector(0,-10,0);
            Plane  plane1 = new Plane(planeNormal, planePoint, new Stripes(new SColor(1,1,1,1), new SColor(1,1,0,1)));

            // making a disk by assigning it it's parameters
            Vector diskNormal = new Vector(0,1,0); // plane will be vertical  
            Vector diskCenter = new Vector(0,10,0);
            double diskRadius = 20;
            Disk disk1 = new Disk(diskNormal, diskCenter, diskRadius, new Material(new SColor(0,1,1,1), 0.29));

            //making a triangle
            Triangle triangle = new Triangle(new Vector(-10,0,0), new Vector(10,0,0), new Vector(0,10,0), new Material(new SColor(1,0,0,1), 0.25));
            //triangle.AddScaleToTransformationMatrix(new Vector(2,2,2));
            //triangle.AddRotationThroughYAxisToTransformationMatrix(180);
            // making a light i.e. a point light
            Light light1 = new Light(new Vector(-80,300,-150) , 1, new SColor(1,1,1,1));

            // making another light 
            Light light2 = new Light(new Vector(80, 300, -140), 1, new SColor(1, 1, 1, 1));

            // making a box by assigning it it's parameters
            Box box1 = new Box(new Vector(5,5,5), new Vector(15,15,15), new Material(new SColor(1,1,0,1)));

            // making a cylinder 
            Cylinder cylinder = new Cylinder(new Vector(5,0,0), new Vector(5,10,0), 2, new Material(new SColor(1,1,0,1)));
            //cylinder.AddTranslationToTranformationMatrix(new Vector(0,100,0));
            //cylinder.AddScaleToTransformationMatrix(new Vector(1,2,1));

            // defining a torus
            Torus torus = new Torus(new Vector(1,1,1), new Vector(1,2,1), 3,1,new Material(new SColor(1,1,0,1)));

            // setting our camera position and direction of the camera
            Camera camera = new Camera(new Vector(100,0,-500), new Vector(0,0,0));
            //camera.fov = 36;
            camera.numberOfSamples = 4;
            //camera.numberOfJittered = 4;
            //camera.useDOF(20, 10);
     
            // array for multiple shapes
            Shape[] shapes = {plane1, sphere3};

            // array for multiple lights
            Light[] lights = {light1,light2};

            
            Scene scene = new Scene(shapes, lights);


            Bitmap bmp = new Bitmap(camera.width, camera.height);
            camera.Render(scene, bmp);
            pictureBox1.Image = bmp;
            bmp.Save("image.jpeg");
        }

        
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
