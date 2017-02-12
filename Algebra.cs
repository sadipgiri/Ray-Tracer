using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randomImage
{
    class Algebra // this is general algebra class for computing certain mathematical operations
    {
        // method to convert angle in degree into radian angle
        public static double convertToRad(double angleInDegrees)
        {
            return (Math.PI / 180) * angleInDegrees;
        }

        // making a swap method for interchanging the two variables values
        public static void swap(double a, double b)
        {
            double t = a;
            a = b;
            b = t;
        }

        // to get the random number
        public static double getRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}
