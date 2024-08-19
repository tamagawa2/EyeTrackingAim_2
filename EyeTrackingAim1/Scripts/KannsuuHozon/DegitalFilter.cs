using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.KannsuuHozon
{
    public class DegitalFilter
    {
        public class Filter_Keisuu
        {
            public DenseVector a = new DenseVector(0);
            public DenseVector b = new DenseVector(0);
            public DenseVector x_hozon = new DenseVector(0);
            public DenseVector y_hozon = new DenseVector(0);


            public Filter_Keisuu(double[] a_, double[] b_)
            {
                a = new DenseVector(a_);
                b = new DenseVector(b_);
                x_hozon = new DenseVector(new double[a.Count]);
                y_hozon = new DenseVector(new double[b.Count]);
                
            }

            public double H_Filter(double x)
            {
                DenseVector kari_x = (DenseVector)x_hozon.Clone();
                DenseVector kari_y = (DenseVector)y_hozon.Clone();

                for (int i = 1; i < x_hozon.Count; i++)
                {
                    x_hozon[i] = kari_x[i - 1];
                }
                x_hozon[0] = x;

                double y = 0;
                y = x_hozon * a - y_hozon * b;

                for (int i = 1; i < y_hozon.Count; i++)
                {
                    y_hozon[i] = kari_y[i - 1];
                }

                y_hozon[0] = y;


                return y;

            }

            public void filter_reset(double x)
            {
                x_hozon = new DenseVector(a.Count);
                y_hozon = new DenseVector(b.Count);

                for (int i = 0; i < x_hozon.Count; i++)
                {
                    x_hozon[i] = x;
                }

                for (int i = 0; i < y_hozon.Count; i++)
                {
                    y_hozon[i] = x;
                }

            }

        }


        public class Filter_Keisuu_invvrese
        {
            public DenseVector a = new DenseVector(0);
            public double a0 = 0.0;
            public DenseVector b = new DenseVector(0);
            public DenseVector x_hozon = new DenseVector(0);
            public DenseVector y_hozon = new DenseVector(0);
            public double y0 = 0.0;

            

            public Filter_Keisuu_invvrese(double[] a_, double[] b_)
            {
                a = new DenseVector(a_.Length - 1);
                b = new DenseVector(b_);

                for (int i = 0; i < a.Count; i++)
                {
                    a[i] = a_[i + 1];
                }
                a0 = a_[0];


                x_hozon = new DenseVector(new double[a_.Length - 1]);
                y_hozon = new DenseVector(new double[b_.Length]);

            }

            public double H_Filter(double y)
            {
                DenseVector kari_x = (DenseVector)x_hozon.Clone();
                DenseVector kari_y = (DenseVector)y_hozon.Clone();

                for (int i = 1; i < y_hozon.Count; i++)
                {
                    y_hozon[i] = kari_y[i - 1];
                }
                y_hozon[0] = y0;
                y0 = y;

                double x = (1.0 / a0) * (y0 + b * y_hozon - a * x_hozon);

                for (int i = 1; i < x_hozon.Count; i++)
                {
                    x_hozon[i] = kari_x[i - 1];
                }

                if (x_hozon.Count != 0)
                {
                    x_hozon[0] = x;
                }
                


                return x;

            }

            public void filter_reset(double x)
            {
                for (int i = 0; i < x_hozon.Count; i++)
                {
                    x_hozon[i] = x;
                }

                for (int i = 0; i < y_hozon.Count; i++)
                {
                    y_hozon[i] = x;
                }
                y0 = x;

            }

        }



        public class Degital_Filter_Keisuu_json
        {
            [JsonPropertyName("a")] public double[] a { get; set; } = new double[0];
            [JsonPropertyName("b")] public double[] b { get; set; } = new double[0];

        }
        
       
        public static Filter_Keisuu filter_Keisuu_y0;
        public static Filter_Keisuu filter_Keisuu_y0_offset;
        public static Filter_Keisuu filter_Keisuu_x;
        public static Filter_Keisuu filter_Keisuu_pre_x;

        public static Filter_Keisuu_invvrese filter_Keisuu_inverse_pre_x;
    }
}
