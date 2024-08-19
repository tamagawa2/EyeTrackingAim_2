using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.EyeData;
using EyeTrackingAim1.Scripts.RawInput;
using EyeTrackingAim1.Scripts.SendInput;
using EyeTrackingAim1.Scripts.Calibration;
using EyeTrackingAim1.Scripts.Prediction;
using EyeTrackingAim1.Scripts.KannsuuHozon;
using EyeTrackingAim1.Scripts;
using System.Windows;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Management;
using Tobii.StreamEngine;
using TPLinkSmartDevices.Devices;
using OpenCvSharp;
using MathNet.Numerics.LinearAlgebra.Double;

namespace EyeTrackingAim1.Scripts.WindowColor
{
    public class GetColor
    {
        //画面のデバイスコンテキスト
        static IntPtr hdc = IntPtr.Zero;
        public static Bitmap bm;
        public static double[] rgb;
        public static int[] r_array = new int[256];
        public static int[] g_array = new int[256];
        public static int[] b_array = new int[256];

        static int gamma_number = 100;
        public static double[] hozon_gamma = new double[gamma_number];
        public static double[] hozon_beta = new double[gamma_number];

        public static int[,] hozon_gamma_r_array = new int[gamma_number, 256];
        public static int[,] hozon_gamma_g_array = new int[gamma_number, 256];
        public static int[,] hozon_gamma_b_array = new int[gamma_number, 256];
        public static int index_gamma = 0;

        public static double mokuteki_color = (225.0 / 255.0) * 65535.0;


        static int return_gamma(int j, double gamma, double beta)
        {
            int x = (int)(((1 - beta) * Math.Pow((double)j / 255.0, gamma) + beta) * 65535.0);
            if (x > 65535) x = 65535;
            if (x < 0) x = 0;

            return x;
        }

        public static void Init_GetColor()
        {
            hdc = Win32api.GetDC(IntPtr.Zero);
            rgb = new double[3];


            double gamma_min = 0.5;
            double gamma_max = 1.0;
            double gamma = gamma_max;

            Console.WriteLine("gamma");
            for (int i = 0; i < hozon_gamma.Length; i++)
            {
                Console.WriteLine(gamma);
                hozon_gamma[i] = gamma;
                gamma = gamma - (gamma_max - gamma_min) / (double)(gamma_number);
                
            }
            
            

            Console.WriteLine("beta");
            double beta_min = 0.0;
            double beta_max = 0.3;
            double beta = beta_min;
            for (int i = 0; i < hozon_beta.Length; i++)
            {
                //Console.WriteLine(beta);
                hozon_beta[i] = beta;
                beta = beta + (beta_max - beta_min) / (double)(gamma_number);
                
            }
            
            

            for (int i = 0; i < hozon_gamma_r_array.GetLength(0); i++)
            {
                for (int j = 0; j < hozon_gamma_r_array.GetLength(1); j++)
                {    
                    hozon_gamma_r_array[i, j] = hozon_gamma_g_array[i, j] = hozon_gamma_b_array[i, j] = return_gamma(j, hozon_gamma[i], hozon_beta[i]);
                    
                }
                
            }

        }

        public static void Exit_GetColor()
        {
            bm.Dispose();
            Win32api.DeleteDC(hdc);
        }
        public static void GetColor_function(System.Windows.Vector vector, int cx, int cy)
        {

            bm = new Bitmap(cx, cy);
            IntPtr bufdc = Win32api.CreateCompatibleDC(hdc);
            IntPtr hbm = bm.GetHbitmap();
            //SelectObject(変えるべきでーた、変える属性) -> 変えるべきデータの元
            IntPtr holdBmp = Win32api.SelectObject(bufdc, hbm);
            bool bit_e = Win32api.BitBlt(bufdc, 0, 0, cx, cy, hdc, (int)vector.X - (cx / 2), (int)vector.Y - (cy / 2), Win32api.TernaryRasterOperations.SRCCOPY);

            bm = (Bitmap)Image.FromHbitmap(hbm).Clone();

            //rgbの平均値を計算
            BitmapData bitmapData = bm.LockBits(
            new Rectangle(0, 0, bm.Width, bm.Height),
            ImageLockMode.ReadWrite,
            bm.PixelFormat);

            // ピクセルデータへのポインタを取得する
            IntPtr scan0 = bitmapData.Scan0;

            int width = bm.Width;
            int height = bm.Height;
            int stride = bitmapData.Stride;
            int bytesPerPixel = 4; // 1ピクセルあたりのバイト数（32bppの場合は4バイト）

            //ヒストグラム
            r_array = new int[256];
            g_array = new int[256];
            b_array = new int[256];

            // 平均値を計算するための変数を初期化する
            int totalRed = 0, totalGreen = 0, totalBlue = 0;
            unsafe
            {
                byte* p = (byte*)(void*)scan0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // RGB値を取得する
                        int blue = p[x * bytesPerPixel];
                        int green = p[x * bytesPerPixel + 1];
                        int red = p[x * bytesPerPixel + 2];

                        r_array[red] = r_array[red] + 1;
                        g_array[green] = g_array[green] + 1;
                        b_array[blue] = b_array[blue] + 1;


                        // RGB値を合計する
                        totalRed += red;
                        totalGreen += green;
                        totalBlue += blue;
                    }

                    // 次の行に進む
                    p += stride;
                }
            }

            // 画像のピクセル数を計算する
            int pixelCount = width * height;

            // RGBの平均値を計算する
            int averageRed = totalRed / pixelCount;
            int averageGreen = totalGreen / pixelCount;
            int averageBlue = totalBlue / pixelCount;

            /*Console.WriteLine("heikinn");
            Console.WriteLine(averageRed * 0.3 + averageGreen * 0.6 + averageBlue * 0.1);*/

            //なんか計算
            double[] hikari = new double[hozon_gamma_r_array.GetLength(0)];

            //実験
            /*r_array = new int[256];
            g_array = new int[256];
            b_array = new int[256];
            r_array[255] = pixelCount;
            g_array[255] = pixelCount;
            b_array[255] = pixelCount;*/

            for (int i = 0; i < hozon_gamma_r_array.GetLength(0); i++)
            {
                double naiseki_r = 0.0;
                double naiseki_g = 0.0;
                double naiseki_b = 0.0;

                for (int j = 0; j < hozon_gamma_r_array.GetLength(1); j++)
                {
                    naiseki_r = naiseki_r + ((double)r_array[j] * hozon_gamma_r_array[i, j]);
                    naiseki_g = naiseki_g + ((double)g_array[j] * hozon_gamma_g_array[i, j]);
                    naiseki_b = naiseki_b + ((double)b_array[j] * hozon_gamma_b_array[i, j]);
                }

                hikari[i] = (naiseki_r * 0.3 + naiseki_g * 0.6 + naiseki_b * 0.1) / (double)pixelCount;
                //Console.WriteLine((hikari[i] / 65535.0) * 255.0);
                
            }
            

            int index;
            for (index = 0; index < gamma_number; index++)
            {
                if (mokuteki_color <= hikari[index]) {
                    break;
                } 
            }

            //Console.WriteLine("index");
            //Console.WriteLine(index);

            if (index >= gamma_number)
            {
                index = gamma_number - 1;
            }

            index_gamma = index;
            


            bm.UnlockBits(bitmapData);

            //リソース解放
            Win32api.DeleteObject(hbm);
            Win32api.DeleteDC(bufdc);


        }
    }
}
