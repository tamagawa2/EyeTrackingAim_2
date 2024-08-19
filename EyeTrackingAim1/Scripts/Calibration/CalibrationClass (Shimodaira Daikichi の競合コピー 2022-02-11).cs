using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;

using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.Calibration
{
    public class CalibrationClass
    {
        public static int countnunber = 3000;
        public static int usenumber = 3000;
        public class CalibrationData
        {
            public System.Windows.Vector CalibrationTarget;
            public System.Windows.Vector[] CalibrationPointArray = new System.Windows.Vector[countnunber];
            public System.Windows.Vector[] CalibrationEyeArray = new System.Windows.Vector[countnunber];
            public Vector3[] CalibrationHeadPoseArray = new Vector3[countnunber];
            public Vector3[] CalibrationHeadRotaArray = new Vector3[countnunber];
            public bool CalibrationCompleate = false;
            public int CalibrationCount = 0;
            public double[] avgR = new double[countnunber];
            public double[] avgG = new double[countnunber];
            public double[] avgB = new double[countnunber];
        }

        public static int Upnumber = 300;
        public class CalibrationData1
        {
            
            public System.Windows.Vector[] CalibrationPointArray = new System.Windows.Vector[Upnumber];
            public double[] avgR = new double[Upnumber];
            public double[] avgG = new double[Upnumber];
            public double[] avgB = new double[Upnumber];
            public bool CalibrationCompleate = false;
            public int CalibrationCount = 0;
        }

        public static CalibrationData[] calibrationDatas = new CalibrationData[5];
        public static CalibrationData1 calibrationDatas1 = new CalibrationData1();

        public static CalibrationData[] calibrationDatas2 = new CalibrationData[7];

        public static int pupilnumber = 1000;
        public class CalibrationPupill
        {
            public System.Windows.Vector[] CalibrationTarget = new System.Windows.Vector[pupilnumber];
            public System.Windows.Vector[] CalibrationPointArray = new System.Windows.Vector[pupilnumber];
            public System.Windows.Vector[] CalibrationEyeArray = new System.Windows.Vector[pupilnumber];      
            public double[] avgR = new double[pupilnumber];
            public double[] avgG = new double[pupilnumber];
            public double[] avgB = new double[pupilnumber];
            public int CalibrationCount = 0;
        }
        
        public static CalibrationPupill calibrationPupill = new CalibrationPupill();
        static int pupilkeisuukazu = 16;
        public class CaribrationResult
        {
            [JsonPropertyName("KeisuuX")] public double[] KeisuuX { get; set; } = new double[7 + pupilkeisuukazu];            
            [JsonPropertyName("offsethabitx")] public double offsethabitx { get; set; }
            [JsonPropertyName("offsethabitmousex")] public double offsethabitmousex { get; set; } = 1.0;
            [JsonPropertyName("KeisuuY")] public double[] KeisuuY { get; set; } = new double[7 + pupilkeisuukazu];            
            [JsonPropertyName("offsethabity")] public double offsethabity { get; set; }
            [JsonPropertyName("offsethabitmousey")] public double offsethabitmousey { get; set; } = 1.0;

            [JsonPropertyName("Heikinn")] public double[] Heikinn { get; set; } = new double[7 + pupilkeisuukazu];
            [JsonPropertyName("Hensa")] public double[] Hensa { get; set; } = new double[7 + pupilkeisuukazu];
            
        }

        public static CaribrationResult caribrationResult = new CaribrationResult();

        public class CalibrationDataHozon
        {
            [JsonPropertyName("CalibrationTarget")] public System.Windows.Vector[] CalibrationTarget { get; set; } = new System.Windows.Vector[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationPointArray")] public System.Windows.Vector[] CalibrationPointArray { get; set; } = new System.Windows.Vector[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationEyeArray")] public System.Windows.Vector[] CalibrationEyeArray { get; set; } = new System.Windows.Vector[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationHeadPoseArrayX")] public double[] CalibrationHeadPoseArrayX { get; set; } = new double[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationHeadPoseArrayY")] public double[] CalibrationHeadPoseArrayY { get; set; } = new double[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationHeadPoseArrayZ")] public double[] CalibrationHeadPoseArrayZ { get; set; } = new double[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationHeadRotaArrayX")] public double[] CalibrationHeadRotaArrayX { get; set; } = new double[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationHeadRotaArrayY")] public double[] CalibrationHeadRotaArrayY { get; set; } = new double[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationHeadRotaArrayZ")] public double[] CalibrationHeadRotaArrayZ { get; set; } = new double[countnunber * calibrationDatas.Length];
            [JsonPropertyName("CalibrationPupilTarget")] public System.Windows.Vector[] CalibrationPupilTarget { get; set; } = new System.Windows.Vector[countnunber * calibrationDatas2.Length];
            [JsonPropertyName("CalibrationPupilPointArray")] public System.Windows.Vector[] CalibrationPupilPointArray { get; set; } = new System.Windows.Vector[countnunber * calibrationDatas2.Length];
            [JsonPropertyName("CalibrationPupilEyeArray")] public System.Windows.Vector[] CalibrationPupilEyeArray { get; set; } = new System.Windows.Vector[countnunber * calibrationDatas2.Length];
            [JsonPropertyName("avgR")] public double[] avgR { get; set; } = new double[pupilnumber];
            [JsonPropertyName("avgG")] public double[] avgG { get; set; } = new double[pupilnumber];
            [JsonPropertyName("avgB")] public double[] avgB { get; set; } = new double[pupilnumber];

        }

        public static CalibrationDataHozon calibrationDataHozon = new CalibrationDataHozon();
        

        public static void EstimateKeisuu(System.Windows.Vector[] TargetData, System.Windows.Vector[] EyePoint,
            double[] HPX, double[] HPY, double[] HPZ,
            double[] HRX, double[] HRY, double[] HRZ,
           int number)
        {
            Vector3[] HeadPose = new Vector3[number];
            Vector3[] HeadRota = new Vector3[number];
            for (int i = 0; i < number; i++)
            {
                HeadPose[i].X = (float)HPX[i];
                HeadPose[i].Y = (float)HPY[i];
                HeadPose[i].Z = (float)HPZ[i];

                HeadRota[i].X = (float)HRX[i];
                HeadRota[i].Y = (float)HRY[i];
                HeadRota[i].Z = (float)HRZ[i];

            }
            
            double[] goukei = new double[7];

            for (int i = 0; i < number; i++)
            {
                goukei[0] = goukei[0] + 1;
                goukei[1] = goukei[1] + HeadPose[i].X;
                goukei[2] = goukei[2] + HeadPose[i].Y;
                goukei[3] = goukei[3] + HeadPose[i].Z;
                goukei[4] = goukei[4] + HeadRota[i].X;
                goukei[5] = goukei[5] + HeadRota[i].Y;
                goukei[6] = goukei[6] + HeadRota[i].Z;
                
            }

            for (int i = 0; i < CalibrationClass.caribrationResult.KeisuuX.Length - pupilkeisuukazu; i++)
            {
                caribrationResult.Heikinn[i] = goukei[i] / (number);
            }
            

            double[] hennsa = new double[7];

            for (int i = 0; i < number; i++)
            {
                hennsa[0] = hennsa[0] + (1 - caribrationResult.Heikinn[0]) * (1 - caribrationResult.Heikinn[0]);
                hennsa[1] = hennsa[1] + (HeadPose[i].X - caribrationResult.Heikinn[1]) * (HeadPose[i].X - caribrationResult.Heikinn[1]);
                hennsa[2] = hennsa[2] + (HeadPose[i].Y - caribrationResult.Heikinn[2]) * (HeadPose[i].Y - caribrationResult.Heikinn[2]);
                hennsa[3] = hennsa[3] + (HeadPose[i].Z - caribrationResult.Heikinn[3]) * (HeadPose[i].Z - caribrationResult.Heikinn[3]);
                hennsa[4] = hennsa[4] + (HeadRota[i].X - caribrationResult.Heikinn[4]) * (HeadRota[i].X - caribrationResult.Heikinn[4]);
                hennsa[5] = hennsa[5] + (HeadRota[i].Y - caribrationResult.Heikinn[5]) * (HeadRota[i].Y - caribrationResult.Heikinn[5]);
                hennsa[6] = hennsa[6] + (HeadRota[i].Z - caribrationResult.Heikinn[6]) * (HeadRota[i].Z - caribrationResult.Heikinn[6]);
               

            }
            for (int i = 0; i < CalibrationClass.caribrationResult.KeisuuX.Length - pupilkeisuukazu; i++)
            {
                caribrationResult.Hensa[i] = Math.Sqrt(hennsa[i] / (number));
            }

            

            DenseMatrix OX = new DenseMatrix(number, 1);
            DenseMatrix X = new DenseMatrix(number, 7);

            DenseMatrix OY = new DenseMatrix(number, 1);
            DenseMatrix Y = new DenseMatrix(number, 7);

            int count = 0;


            for (int i = 0; i < number; i++)
            {
                OX[count, 0] = TargetData[i].X;

                OY[count, 0] = TargetData[i].Y;


                X[count, 0] = Y[count, 0] = 1.0;
                X[count, 1] = Y[count, 1] = (1.0 / caribrationResult.Hensa[1]) * (HeadPose[i].X - caribrationResult.Heikinn[1]);
                X[count, 2] = Y[count, 2] = (0.00001 / caribrationResult.Hensa[2]) * (HeadPose[i].Y - caribrationResult.Heikinn[2]);
                X[count, 3] = Y[count, 3] = (0.00001 / caribrationResult.Hensa[3]) * (HeadPose[i].Z - caribrationResult.Heikinn[3]);
                X[count, 4] = Y[count, 4] = (0.00001 / caribrationResult.Hensa[4]) * (HeadRota[i].X - caribrationResult.Heikinn[4]);
                X[count, 5] = Y[count, 5] = (1.0 / caribrationResult.Hensa[5]) * (HeadRota[i].Y - caribrationResult.Heikinn[5]);
                X[count, 6] = Y[count, 6] = (1.0 / caribrationResult.Hensa[6]) * (HeadRota[i].Z - caribrationResult.Heikinn[6]);
                


                count = count + 1;

            }

            DiagonalMatrix ad = new DiagonalMatrix(7, 7, new double[] { 1, 1, 1, 1, 1, 1, 1});
            var AX = X.TransposeThisAndMultiply(X) + ad;
           // Console.WriteLine(X.TransposeThisAndMultiply(X));
            var BX = X.TransposeThisAndMultiply(OX);

            var InverseX = AX.Inverse();

            //var betaX = AX.Solve(BX);
            var betaX = InverseX.Multiply(BX);
            //Console.WriteLine(CalibrationClass.caribrationResult.KeisuuX.Length);
            for (int i = 0; i < CalibrationClass.caribrationResult.KeisuuX.Length - pupilkeisuukazu; i++)
            {

                CalibrationClass.caribrationResult.KeisuuX[i] = betaX[i, 0];
            }

           
            var AY = Y.TransposeThisAndMultiply(Y) + ad;
            var BY = Y.TransposeThisAndMultiply(OY);

            var InverseY = AY.Inverse();

            //var betaY = AY.Solve(BY);
            var betaY = InverseY.Multiply(BY);

            for (int i = 0; i < CalibrationClass.caribrationResult.KeisuuY.Length - pupilkeisuukazu; i++)
            {
                CalibrationClass.caribrationResult.KeisuuY[i] = betaY[i, 0];
            }
            
        }



        
        public static void EstimatePupilKeisuu(System.Windows.Vector[] TargetData, double[] avgR, double[] avgG, double[] avgB, System.Windows.Vector[] EyePoint, int number)
        {
            double[] goukei = new double[4];
            double[] hennsa = new double[4];

            for (int i = 0; i < number; i++)
            {
                goukei[0] = goukei[0] + (avgR[i]);
                goukei[1] = goukei[1] + (avgG[i]);
                goukei[2] = goukei[2] + (avgB[i]);
                goukei[3] = goukei[3] + (avgR[i] + avgG[i] + avgB[i]);
            }

            caribrationResult.Heikinn[8] = goukei[0] / number;
            caribrationResult.Heikinn[9] = goukei[1] / number;
            caribrationResult.Heikinn[10] = goukei[2] / number;
            caribrationResult.Heikinn[11] = goukei[3] / number;

            for (int i = 0; i < number; i++)
            {
                hennsa[0] = hennsa[0] + ((avgR[i]) - caribrationResult.Heikinn[8])
                                      * ((avgR[i]) - caribrationResult.Heikinn[8]);

                hennsa[1] = hennsa[1] + ((avgG[i]) - caribrationResult.Heikinn[9])
                                      * ((avgG[i]) - caribrationResult.Heikinn[9]);

                hennsa[2] = hennsa[2] + ((avgB[i]) - caribrationResult.Heikinn[10])
                                      * ((avgB[i]) - caribrationResult.Heikinn[10]);

                hennsa[3] = hennsa[3] + ((avgR[i] + avgG[i] + avgB[i]) - caribrationResult.Heikinn[11])
                                      * ((avgR[i] + avgG[i] + avgB[i]) - caribrationResult.Heikinn[11]);
            }

            caribrationResult.Hensa[8] = Math.Sqrt(hennsa[0] / (number));
            caribrationResult.Hensa[9] = Math.Sqrt(hennsa[1] / (number));
            caribrationResult.Hensa[10] = Math.Sqrt(hennsa[2] / (number));
            caribrationResult.Hensa[11] = Math.Sqrt(hennsa[3] / (number));


            DenseMatrix OX = new DenseMatrix(number, 1);
            DenseMatrix X = new DenseMatrix(number, pupilkeisuukazu);

            DenseMatrix OY = new DenseMatrix(number, 1);
            DenseMatrix Y = new DenseMatrix(number, pupilkeisuukazu);

            int count = 0;


            for (int i = 0; i < number; i++)
            {
                OX[count, 0] = TargetData[i].X;

                OY[count, 0] = TargetData[i].Y;


                X[count, 0] = Y[count, 0] = 1.0;
                
                double xr = (1.0 / caribrationResult.Hensa[8]) * ((avgR[i]) - caribrationResult.Heikinn[8]);
                double xg = (1.0 / caribrationResult.Hensa[9]) * ((avgG[i]) - caribrationResult.Heikinn[9]);
                double xb = (1.0 / caribrationResult.Hensa[10]) * ((avgB[i]) - caribrationResult.Heikinn[10]);
                double xrgb = (1.0 / caribrationResult.Hensa[11]) * ((avgR[i] + avgG[i] + avgB[i]) - caribrationResult.Heikinn[11]);
                X[count, 1] = Y[count, 1] = xrgb;
                X[count, 2] = Y[count, 2] = xrgb * xrgb;
                X[count, 3] = Y[count, 3] = xrgb * xrgb * xrgb;



                count = count + 1;
            }

            double[] omominn = new double[pupilkeisuukazu];

            for (int i = 0; i < pupilkeisuukazu; i++)
            {
                omominn[i] = 1;
            }
            DiagonalMatrix ad = new DiagonalMatrix(pupilkeisuukazu, pupilkeisuukazu, omominn);
            var AX = X.TransposeThisAndMultiply(X) + ad;

            var BX = X.TransposeThisAndMultiply(OX);
            Console.WriteLine(BX);
            var InverseX = AX.Inverse();

            //var betaX = AX.Solve(BX);
            var betaX = InverseX.Multiply(BX);
            //Console.WriteLine(CalibrationClass.caribrationResult.KeisuuX.Length);

            for (int i = 7; i < CalibrationClass.caribrationResult.KeisuuX.Length; i++)
            {
                CalibrationClass.caribrationResult.KeisuuX[i] = betaX[i - 7, 0];
            }


            var AY = Y.TransposeThisAndMultiply(Y) + ad;
            var BY = Y.TransposeThisAndMultiply(OY);

            var InverseY = AY.Inverse();

            //var betaY = AY.Solve(BY);
            var betaY = InverseY.Multiply(BY);

            for (int i = 7; i < CalibrationClass.caribrationResult.KeisuuY.Length; i++)
            {
                CalibrationClass.caribrationResult.KeisuuY[i] = betaY[i - 7, 0];
            }


        }

        
        
































        //        if ((RawEyePoint - new System.Windows.Vector(we, he)).Length<Form1.eyeDatas[Form1.nowvalue].EyeCorectionRange && EyeVelosity.Length< 0)
        //            {

        //                if(LScount == CalibrationClass.Upnumber - 1)
        //                {
        //                    LScount = 0;
        //                    Console.WriteLine("tinntinn");
        //                }

        //    CalibrationClass.calibrationDatas1.CalibrationEyeArray[LScount] = EyePointLS;
        //                CalibrationClass.calibrationDatas1.CalibrationHeadPoseArray[LScount] = HP;
        //                CalibrationClass.calibrationDatas1.CalibrationHeadRotaArray[LScount] = HR;
        //                CalibrationClass.calibrationDatas1.CalibrationPointArray[LScount] = EyePointLS - new System.Windows.Vector(we, he);

        //                LScount = LScount + 1;





        //                System.Windows.Vector[] CPA = new System.Windows.Vector[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    System.Windows.Vector[] CEA = new System.Windows.Vector[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    double[] CHPX = new double[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    double[] CHPY = new double[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    double[] CHPZ = new double[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    double[] CHRX = new double[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    double[] CHRY = new double[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];
        //    double[] CHRZ = new double[CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount];


        //    int countinstant = 0;
        //                for (int i = 0; i<CalibrationClass.calibrationDatas.Length* CalibrationClass.usenumber + LScount; i++)
        //                {
        //                    if (countinstant<LScount)
        //                    {
        //                        CPA[i] = CalibrationClass.calibrationDatas1.CalibrationPointArray[countinstant];
        //                        CEA[i] = CalibrationClass.calibrationDatas1.CalibrationEyeArray[countinstant];
        //                        CHPX[i] = CalibrationClass.calibrationDatas1.CalibrationHeadPoseArray[countinstant].X;
        //                        CHPY[i] = CalibrationClass.calibrationDatas1.CalibrationHeadPoseArray[countinstant].Y;
        //                        CHPZ[i] = CalibrationClass.calibrationDatas1.CalibrationHeadPoseArray[countinstant].Z;
        //                        CHPX[i] = CalibrationClass.calibrationDatas1.CalibrationHeadRotaArray[countinstant].X;
        //                        CHPY[i] = CalibrationClass.calibrationDatas1.CalibrationHeadRotaArray[countinstant].Y;
        //                        CHPZ[i] = CalibrationClass.calibrationDatas1.CalibrationHeadRotaArray[countinstant].Z;
        //                        countinstant = countinstant + 1;
        //                    } else
        //{
        //    CPA[i] = CalibrationClass.calibrationDataHozon.CalibrationPointArray[countinstant - LScount];
        //    CEA[i] = CalibrationClass.calibrationDataHozon.CalibrationEyeArray[countinstant - LScount];
        //    CHPX[i] = CalibrationClass.calibrationDataHozon.CalibrationHeadPoseArrayX[countinstant - LScount];
        //    CHPY[i] = CalibrationClass.calibrationDataHozon.CalibrationHeadPoseArrayY[countinstant - LScount];
        //    CHPZ[i] = CalibrationClass.calibrationDataHozon.CalibrationHeadPoseArrayZ[countinstant - LScount];
        //    CHRX[i] = CalibrationClass.calibrationDataHozon.CalibrationHeadRotaArrayX[countinstant - LScount];
        //    CHRY[i] = CalibrationClass.calibrationDataHozon.CalibrationHeadRotaArrayY[countinstant - LScount];
        //    CHRZ[i] = CalibrationClass.calibrationDataHozon.CalibrationHeadRotaArrayZ[countinstant - LScount];
        //    countinstant = countinstant + 1;
        //}


        //                }

        //                CalibrationClass.EstimateKeisuuUpdate(
        //                       CPA,
        //                       CEA,
        //                       CHPX,
        //                       CHPY,
        //                       CHPZ,
        //                       CHRX,
        //                       CHRY,
        //                       CHRZ,
        //                       CalibrationClass.calibrationDatas.Length * CalibrationClass.usenumber + LScount,
        //                       LScount,
        //                       10000.0);


        //            }



    }
}
