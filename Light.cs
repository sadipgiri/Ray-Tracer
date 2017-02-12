using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Light
    {
        public Vector location;
        public SColor lightColor;
        public double Intensity;
       

        public Light(Vector location, double Intensity, SColor lightColor) {
            this.location = location;
            this.Intensity = Intensity;
            this.lightColor = lightColor;
        }

      

    }
}
