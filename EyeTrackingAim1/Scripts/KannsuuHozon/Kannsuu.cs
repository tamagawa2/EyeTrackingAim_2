using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTrackingAim1.Scripts.Prediction;

namespace EyeTrackingAim1.Scripts.KannsuuHozon
{
    public class Kannsuu
    {

        static Random random = new Random();
        public static double RandamCreate(double min, double max)
        {

            double x = random.NextDouble();
            double y = 0;

            y = (max - min) * x + min;

            return y;

        }

        public static int RandamCreateMainuto1To1()
        {

            double x = random.NextDouble();
            int y = 0;

            if (x >= 0 && x < 0.5)
            {
                y = -1;
            }
            else
            {
                y = 1;
            }


            return y;

        }


        public static int RandamCreate_int(int a)
        {
            double x = random.NextDouble();
            double f = 1.0 / (double)a;

            for (int i = 0; i < a; i++)
            {
                if (x >= f * i && x < f * (i + 1))
                {
                    return i;
                }
            }

            return 0;
            
        }




    }
}
