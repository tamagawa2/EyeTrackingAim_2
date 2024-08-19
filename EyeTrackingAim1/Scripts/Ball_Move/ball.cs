using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.EyeTrackingAim;
using EyeTrackingAim1.Scripts.RawInput;
using EyeTrackingAim1.Scripts.EyeData;

namespace EyeTrackingAim1.Scripts.Ball_Move
{
    public class ball
    {
        static System.Diagnostics.Stopwatch sw;
        static Timer timer;
        static double fps = 0;
        public static double x = 960.0;
        public static double v = 10.0;
        static double now_v = 10.0;
        static double offset = 150;


        public static double[] offset_modi = new double[100];
        static int I = 0;
        public static double offset_modi_ave = 0;


        public static void Init_ball(Form1 form)
        {
            fps = 0;
            x = 960.0;
            v = 10.0;
            now_v = 10.0;
            offset = 150;

            timer = new Timer
            {
                Interval = 1,
                Enabled = true,
            };

            sw = new System.Diagnostics.Stopwatch();

            timer.Tick += (sender, e) =>
            {

                fps = sw.ElapsedMilliseconds * 0.001;



                now_v = Math.Sign(now_v) * v;
                double karix = x + now_v * fps;

                if (karix > 960 + offset || karix < 960 - offset)
                {
                    now_v = -now_v;
                }

                x = x + now_v * fps;

                

                if (RawInputKey.RawInputJudge(KeyData.F10))
                {
                    I = (I + 1) % offset_modi.Length;
                    offset_modi[I] = Math.Abs(x - DoEyeTrackingAim.ModifyEyePoint.X);

                }
                else
                {
                    for (int i = 0; i < offset_modi.Length; i++)
                    {
                        offset_modi_ave = offset_modi_ave + offset_modi[i];
                    }
                    offset_modi_ave = offset_modi_ave / offset_modi.Length;
                }

                sw.Restart();
                form.Invalidate();
            };
        }

        public static void end_ball()
        {
            timer.Dispose();
            
        }



    }
}
