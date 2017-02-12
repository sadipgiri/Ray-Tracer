using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Stripes:Material // making stripes class a sub-class of material class
    {
        public SColor color2; // assigning different colors for fantastic stripes

        public Stripes(SColor color1, SColor color2) : base(color1) {
            this.color2 = color2;
        }

        // over-riding the method called ColorAtPoint of the material class 
        // sothat we'd assign stripes in place of color of the material class
        public override SColor ColorAtPoint(Vector point) {
            // inorder to assign different color patterns to stripes 
            // giving it different conditions
            if ((Math.Sin(point.z) > 0.3 && Math.Sin(point.x) > 0.3 ) || (Math.Sin(point.z) < -0.3 && Math.Sin(point.x) < -0.3)) 
            {
                return color2;
            }
            else if ((Math.Sin(point.z) > 0 && Math.Sin(point.x) > 0) || (Math.Sin(point.z) < 0 && Math.Sin(point.x) < 0))
            {
                return new SColor(1,0,0,1);
            }
            else
            {
                return color;
            }
        }
    }
}
