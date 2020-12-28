using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncingGame.Helpers
{
    public class MathHelperExtension
    {
        public static float Map(float value, float minSource, float maxSource, float minDestination, float maxDestination)
        {
            return minDestination + (maxDestination - minDestination) * ((maxSource - value) / (maxSource - minSource));
        }

        public static double Map(double value, double minSource, double maxSource, double minDestination, double maxDestination)
        {
            return minDestination + (maxDestination - minDestination) * ((maxSource - value) / (maxSource - minSource));
        }
    }
}
