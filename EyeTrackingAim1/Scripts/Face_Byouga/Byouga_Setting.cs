using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using EyeTrackingAim1.Scripts.KannsuuHozon;

namespace EyeTrackingAim1.Scripts.Face_Byouga
{
    public class Byouga_Setting
    {
        static Vector3 camera_point = new Vector3();
        static Vector3 screen_point = new Vector3();
        static Vector3 n = new Vector3();

        static double fov = 0;

        public static void Byouga_Init(Vector3 camera_point_, Vector3 screen_point_, double fov_)
        {
            camera_point = camera_point_;
            screen_point = screen_point_;
            fov = fov_;

            n = camera_point - screen_point;

        }

        public static Vector2 GetScreenPoint(Vector3 p, double screen_width, double screen_height)
        {
            Vector3 beta = p - camera_point;
            Vector3 pa = p - screen_point;
            double t = (-Vector3.Dot(n, pa)) / Vector3.Dot(n, beta);
            Vector3 pdash = p + Vector3.Multiply((float)t, beta);

            double a = pdash.X - screen_point.X;
            double b = pdash.Y - screen_point.Y;

            double f = n.Length();
            double width = f * Math.Tan((fov / 2.0) * (Math.PI / 180.0));
            double height = width * (screen_height / screen_width);

            width = width * 2.0;
            height = height * 2.0;

            a = a * (screen_width / width) + (screen_width / 2.0);
            b = -b * (screen_height / height) + (screen_height / 2.0);

            return new Vector2((float)a, (float)b);
        }

        public static Vector3[] enn_0;
        public static Vector3[] enn_1;
        public static Vector3[] enn_2;

        public static Vector3[] enn_origin_0;
        public static Vector3[] enn_origin_1;
        public static Vector3[] enn_origin_2;

        public static Vector3[] ennTeigi(double a, double b, int kazu, int flag)
        {
            Vector3[] re = new Vector3[kazu];

            for (int i = 0; i < re.Length; i++)
            {
                float x = (float)(a * Math.Cos(i * ((2.0 * Math.PI) / re.Length)));
                float y = (float)(b * Math.Sin(i * ((2.0 * Math.PI) / re.Length)));
                if (flag == 0)
                {
                    re[i] = new Vector3(x, y, 0);
                }
                else if (flag == 1)
                {
                    re[i] = new Vector3(x, 0, y);
                }
                else if (flag == 2)
                {
                    re[i] = new Vector3(0, x, y);
                }

            }

            return re;
        }

        public static void enn_Kaitenn_Heikouidou(Vector3 Heikou, Vector3 eulerAngle)
        {
            
            for (int i = 0; i < enn_0.Length; i++)
            {
                enn_0[i] = KaitennGyouretu.Kaitenn(enn_origin_0[i], eulerAngle) + Heikou;
                enn_1[i] = KaitennGyouretu.Kaitenn(enn_origin_1[i], eulerAngle) + Heikou;
                enn_2[i] = KaitennGyouretu.Kaitenn(enn_origin_2[i], eulerAngle) + Heikou;
                
            }

        }


        public static Face_Byouga_Form Face_Byouga_window;



    }
}
