using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EyeTrackingAim1.Scripts.WindowColor;



namespace EyeTrackingAim1.Scripts.DisPlay_Setting
{
    public class Display_color
    {

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("gdi32.dll")]
        static extern bool SetDeviceGammaRamp(IntPtr hdc, ref RAMP lplamp);
        [DllImport("gdi32.dll")]
        static extern bool GetDeviceGammaRamp(IntPtr hdc, IntPtr lplamp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct RAMP
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Red;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Green;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public UInt16[] Blue;
        }
        public static void test_Set_Display_color()
        {
            RAMP ramp = new RAMP();
            ramp.Red = new ushort[256];
            ramp.Green = new ushort[256];
            ramp.Blue = new ushort[256];

            for (int j = 0; j < GetColor.hozon_gamma_r_array.GetLength(0); j++)
            {
                for (int i = 0; i < 256; i++)
                {

                    ramp.Red[i] = (ushort)GetColor.hozon_gamma_r_array[j, i];
                    ramp.Green[i] = (ushort)GetColor.hozon_gamma_g_array[j, i];
                    ramp.Blue[i] = (ushort)GetColor.hozon_gamma_b_array[j, i];

                }

                bool un = SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
                Console.WriteLine("Setgamma");
                Console.WriteLine(un);
            }
            
        }

        public static void Set_Display_color()
        {
            RAMP ramp = new RAMP();
            ramp.Red = new ushort[256];
            ramp.Green = new ushort[256];
            ramp.Blue = new ushort[256];

            for (int i = 0; i < 256; i++)
            {

                ramp.Red[i] = (ushort)GetColor.hozon_gamma_r_array[GetColor.index_gamma, i];
                ramp.Green[i] = (ushort)GetColor.hozon_gamma_g_array[GetColor.index_gamma, i];
                ramp.Blue[i] = (ushort)GetColor.hozon_gamma_b_array[GetColor.index_gamma, i];

            }

            bool un = SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
            //Console.WriteLine("Setgamma");
            //Console.WriteLine(un);

        }
        public static void SetGamma(double a, double b, double c)
        {
            RAMP ramp = new RAMP();
            ramp.Red = new ushort[256];
            ramp.Green = new ushort[256];
            ramp.Blue = new ushort[256];
            for (int i = 1; i < 256; i++)
            {

                double da = 65535 * (a * Math.Pow((double)i / 255.0, 1.0 / c) + b);
                int iArrayValue = (int)(da);
                
                if (iArrayValue > 65535)
                {
                    iArrayValue = 65535;
                }

                ramp.Red[i] = ramp.Blue[i] = ramp.Green[i] = (ushort)iArrayValue;
            }


            bool un = SetDeviceGammaRamp(GetDC(IntPtr.Zero), ref ramp);
            Console.WriteLine("Setgamma");
            Console.WriteLine(un);
        }




    }
}
