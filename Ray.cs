using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class Ray
    {
        public Vector origin;
        public Vector direction;

        public Ray(Vector o, Vector d)
        {
            this.origin = o;
            this.direction = d;
        }
    }
}
