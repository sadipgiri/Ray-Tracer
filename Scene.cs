using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Scene
    {

        public Shape[] shapes;
        public Light[] lights;


        public Scene(Shape[] shapes, Light[] lights)
        {
            this.shapes = shapes;
            this.lights = lights;
        }



    }
}
