using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTrackingAim1.Scripts.EyeTrackingAim
{
    public class Window_Not_Move
    {

        public static double wnm_move_time = 0.5;
        public static double wnm_cool_time = -1;

        public static System.Windows.Vector buf_wnm_mouse;
        public static System.Windows.Vector wnm_mouse;
        public static System.Windows.Vector wnm_save_mouse;
        public static double wnm_waru = 0.0;
        public static int wnm_flag = 0;
        public static System.Diagnostics.Stopwatch sw_wnm_move = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch sw_wnm_cool = new System.Diagnostics.Stopwatch();

        public static void Init_Data()
        {
            buf_wnm_mouse = new System.Windows.Vector();
            wnm_mouse = new System.Windows.Vector();
            wnm_save_mouse = new System.Windows.Vector();
            wnm_flag = 0;

        }
        public static void Do_Window_Not_Move(System.Windows.Vector vector)
        {
            
            wnm_waru = wnm_move_time * DoEyeTrackingAim.timer_fps;

            /*if (wnm_flag == 0)
            {
                sw_wnm_move.Restart();
                wnm_flag = 1;

                buf_wnm_mouse = EyeTrackingAim_Setting.VectorToMouse(vector, new System.Windows.Vector(1920, 1080));
                wnm_save_mouse = buf_wnm_mouse / wnm_waru;

            }
            else if (wnm_flag == 1 && (sw_wnm_move.ElapsedMilliseconds * 0.001) >= wnm_move_time)
            {

                sw_wnm_move.Stop();
                sw_wnm_cool.Restart();
                wnm_flag = 2;
            }
            else if (wnm_flag == 2 && (sw_wnm_cool.ElapsedMilliseconds * 0.001) >= wnm_cool_time)
            {
                sw_wnm_cool.Stop();
                wnm_flag = 0;
            }*/

            buf_wnm_mouse = EyeTrackingAim_Setting.VectorToMouse(vector, new System.Windows.Vector(1920, 1080));
            wnm_save_mouse = buf_wnm_mouse / wnm_waru;

            //if (wnm_flag == 1)
            {
                
                wnm_mouse = wnm_save_mouse;
                buf_wnm_mouse = buf_wnm_mouse - wnm_mouse;

                if (Math.Sign(wnm_save_mouse.X) * buf_wnm_mouse.X < 0)
                {
                    buf_wnm_mouse.X = buf_wnm_mouse.X + wnm_mouse.X;
                    wnm_mouse.X = buf_wnm_mouse.X;
                    buf_wnm_mouse.X = buf_wnm_mouse.X - wnm_mouse.X;
                }

                if (Math.Sign(wnm_save_mouse.Y) * buf_wnm_mouse.Y < 0)
                {
                    buf_wnm_mouse.Y = buf_wnm_mouse.Y + wnm_mouse.Y;
                    wnm_mouse.Y = buf_wnm_mouse.Y;
                    buf_wnm_mouse.Y = buf_wnm_mouse.Y - wnm_mouse.Y;
                }
            }
            /*else if (wnm_flag == 2)
            {
                if (Math.Sign(wnm_save_mouse.X) * buf_wnm_mouse.X < 0)
                {
                    buf_wnm_mouse.X = buf_wnm_mouse.X + wnm_mouse.X;
                    wnm_mouse.X = buf_wnm_mouse.X;
                    buf_wnm_mouse.X = buf_wnm_mouse.X - wnm_mouse.X;
                }

                if (Math.Sign(wnm_save_mouse.Y) * buf_wnm_mouse.Y < 0)
                {
                    buf_wnm_mouse.Y = buf_wnm_mouse.Y + wnm_mouse.Y;
                    wnm_mouse.Y = buf_wnm_mouse.Y;
                    buf_wnm_mouse.Y = buf_wnm_mouse.Y - wnm_mouse.Y;
                }

                if (Math.Sign(wnm_save_mouse.X) * buf_wnm_mouse.X >= 0)
                {
                   
                    wnm_mouse.X = buf_wnm_mouse.X;
                    buf_wnm_mouse.X = 0.0;
                }

                if (Math.Sign(wnm_save_mouse.Y) * buf_wnm_mouse.Y >= 0)
                {
                   
                    wnm_mouse.Y = buf_wnm_mouse.Y;
                    buf_wnm_mouse.Y = 0.0;

                }
            }*/


        }
    }
}
