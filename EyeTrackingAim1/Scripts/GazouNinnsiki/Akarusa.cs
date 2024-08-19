using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;



namespace EyeTrackingAim1.Scripts.GazouNinnsiki
{
    public class Akarusa
    {
        //WebCamera        
        public static string gakusyuufailepath = "haarcascades/haarcascade_frontalface_default.xml";

        public static VideoCapture camera;
        public static Mat rawcamera;
        
        public static CascadeClassifier Cascade;
        static bool first_flag = false;

        public static void camera_open()
        {
            if (first_flag == false)
            {
                camera = new VideoCapture();
                camera.Open(0);
                Console.WriteLine("VideoCaptureProperties.AutoExposure");
                Console.WriteLine(camera.Set(VideoCaptureProperties.Exposure, -3));
                first_flag = true;
            }
            
        }
        public static void InitCascade(string cascadepath)
        {
            
            rawcamera = new Mat();
            Cascade = new CascadeClassifier(cascadepath);
        }

        public static double[] FaceNinnsiki()
        {

            Akarusa.camera.Read(Akarusa.rawcamera);
            
            //グレー画像
            Mat matglay = new Mat();
            //結果画像
            //Mat matRetImage = matSrcImage.Clone();

            //グレースケール
            Cv2.CvtColor(
                Akarusa.rawcamera,
                matglay,
                ColorConversionCodes.BGR2GRAY);


            //顔認識
            Rect[] faces = Cascade.DetectMultiScale(
                matglay,
                1.1,
                3,
                HaarDetectionTypes.ScaleImage,
                new Size(100, 100));

            double[] re_color = new double[3];
            if (faces.Length > 0)
            {
                Mat roiImage = rawcamera[faces[0]];
                Scalar mean = Cv2.Mean(roiImage);
                re_color[0] = mean.Val2;
                re_color[1] = mean.Val1;
                re_color[2] = mean.Val0;

            }

            foreach (var face in faces)
            {
                Cv2.Rectangle(
                    img: Akarusa.rawcamera,
                    rect: new Rect(face.X, face.Y, face.Width, face.Height),
                    color: new Scalar(0, 255, 255),
                    thickness: 2);
            }


            return re_color;
        }

        

    }
}
