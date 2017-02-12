using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    public class SColor
    {
        public double r, g, b, a;

        //making default color as black color
        public SColor()
        {
            a = 1;
            r = 0;
            g = 0;
            b = 0;
        }

        public SColor(double r, double g, double b, double a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        // operator overloading * sign for color and scalar number
        public static SColor operator *(SColor color, double s) {
            return new SColor(color.r * s, color.g * s, color.b * s, 1); // double not multiplying the alpha of a color
        }

        //operator overloading * sign for color and scalar nuber 
        public static SColor operator *(double s, SColor color) {
            return color * s;
        }

        //operator overloading / sign for color and scalar number
        public static SColor operator /(SColor color, double s) {
            return new SColor(color.r / s, color.g / s, color.b / s, 1);
        }

        //operator overloading / sign for color and scalar number
        public static SColor operator /(double s, SColor color)
        {
            return color / s;
        }

        // operator overloading + sign for addition of two color
        public static SColor operator +(SColor x, SColor y) {
            return new SColor(x.r + y.r, x.g + y.g, x.b + y.b, 1); // for addition of two colors only alpha of the first color is returned rather than additing those two
        }

        // operator overloading * sign for dot product of two color
        public static SColor operator *(SColor x, SColor y) {
            return new SColor(x.r * y.r, x.g * y.g, x.b * y.b, 1);
        }
        

        // giving each r, g, b and a gamma values 
        public int GetRedColor()
        {
            // checking whether the value of r goes beyond the max 255 or not
            if (r <= 0)
            {
                return 0;
            } else if (r >= 1)
            {
                return 255;
            }
            return (int)(Math.Pow(r, (1 / 2.2)) * 255);
        }

        public int GetGreenColor() {
            // checking whether the value of g goes beyond the max 255 or not
            if (g <= 0)
            {
                return 0;
            }
            else if (g >= 1)
            {
                return 255;
            }
            return (int)(Math.Pow(g, (1 / 2.2)) * 255);
        }

        public int GetBlueColor() {
            // checking whether the value of b goes beyond the max 255 or not
            if (b <= 0)
            {
                return 0;
            }
            else if (b >= 1)
            {
                return 255;
            }
            return (int)(Math.Pow(b, (1 / 2.2)) * 255);
        }

        public int GetAlphaColor() {
            // checking whether the value of a goes beyond the max 255 or not
            if (a < 0)
            {
                return 0;
            }
            else if (a > 1)
            {
                return 255;
            }
            return (int)(Math.Pow(a, (1 / 2.2)) *255);
        }

       
    }
}
