using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace EyeTrackingAim1.Scripts.GazouNinnsiki
{
    public class PupilDiameter
    {

        public static double GetPupilDiameter(Mat rawmat)
        {
            Mat hanntennmat = new Mat();
            Cv2.BitwiseNot(rawmat, hanntennmat);
            Mat glaymat = new Mat();
            Cv2.CvtColor(hanntennmat, glaymat, ColorConversionCodes.BGR2GRAY);

            byte[,] kernel = new byte[2, 2];

            for (int i = 0; i < kernel.GetLongLength(0); i++)
            {
                for (int j = 0; j < kernel.GetLongLength(1); j++)
                {
                    kernel[i, j] = 1;
                }
            }

            Mat erosion = new Mat();
            Cv2.Erode(glaymat, erosion, InputArray.Create(kernel));
            Mat thresh1 = new Mat();
            Cv2.Threshold(erosion, thresh1,  220, 255, ThresholdTypes.Binary);

            //Point[][] points = n
            //Cv2.FindContours(thresh1, ,, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            return 0;
        }



    }
}
