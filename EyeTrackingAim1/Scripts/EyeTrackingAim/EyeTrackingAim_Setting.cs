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
using EyeTrackingAim1.Scripts.DisPlay_Setting;
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

namespace EyeTrackingAim1.Scripts.EyeTrackingAim
{
    public class EyeTrackingAim_Setting
    {
        public static double Zscreen;
        public static System.Windows.Vector windowsize = new System.Windows.Vector(1920, 1080);
        //pointer
        public static IntPtr windowptr = IntPtr.Zero;

        public static System.Windows.Vector VectorToMouse(System.Windows.Vector vector, System.Windows.Vector windowsize)
        {
            Zscreen = ((double)windowsize.X / 2.0) * (1 / Math.Tan(Form1.eyeDatas[Form1.nowvalue].FovW / (2.0) * (Math.PI / 180)));
            Vector3 Z = new Vector3(0, 0, (float)Zscreen);

            System.Windows.Vector returnvector = new System.Windows.Vector();
            if (vector.Length != 0)
            {
                Vector3 P = new Vector3((float)(vector.X), (float)(vector.Y), (float)Zscreen);
                Vector3 PN = Vector3.Normalize(P);
                Vector3 ZN = Vector3.Normalize(Z);
                double Angle = Math.Acos(Vector3.Dot(PN, ZN)) * (180.0 / Math.PI);

                double r = vector.Length;
                double x = Angle * (vector.X / r) * Form1.eyeDatas[Form1.nowvalue].Sensitivity.X;
                double y = Angle * (vector.Y / r) * Form1.eyeDatas[Form1.nowvalue].Sensitivity.Y;

                returnvector.X = x;
                returnvector.Y = y;

            }

            return returnvector;

        }


        public static System.Windows.Vector MouseToVector(System.Windows.Vector mousevector)
        {
            System.Windows.Vector windowvector = new System.Windows.Vector();

            double AngleX = mousevector.X / Form1.eyeDatas[Form1.nowvalue].Sensitivity.X;
            double AngleY = mousevector.Y / Form1.eyeDatas[Form1.nowvalue].Sensitivity.Y;

            double Angle = new System.Windows.Vector(AngleX, AngleY).Length;

            if (Angle != 0)
            {
                windowvector.X = Math.Tan(Angle * (Math.PI / 180.0)) * Zscreen * (AngleX / Angle);
                windowvector.Y = Math.Tan(Angle * (Math.PI / 180.0)) * Zscreen * (AngleY / Angle);
            }

            return windowvector;
        }

        public static System.Windows.Vector vec_int(System.Windows.Vector vector)
        {
            System.Windows.Vector re = new System.Windows.Vector();
            re.X = vector.X - (vector.X % 1.0);
            re.Y = vector.Y - (vector.Y % 1.0);

            return re;
        }

        

        

        public static void SetWindow()
        {
            double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            const uint SWP_NOZORDER = 0x0004;
            const uint SWP_NOSIZE = 0x0001;


            Win32api.SetWindowPos(windowptr, IntPtr.Zero, (int)(we / 2.0 - we / 2.0), (int)(he / 2.0 - he / 2.0), (int)we, (int)he, SWP_NOZORDER);
        }

        public static void UpdateWindow(System.Windows.Vector WindowCoo, System.Windows.Size size)
        {
            double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;


            double offsetx = (int)(Form1.eyeDatas[Form1.nowvalue].Offset.X * (we / 2));
            double offsety = (int)(Form1.eyeDatas[Form1.nowvalue].Offset.Y * (he / 2));

            uint noz = (uint)Setwindow.SWP.NOZORDER;
            uint noredraw = (uint)Setwindow.SWP.NOREDRAW;

            if (Form1.eyeDatas[Form1.nowvalue].CancelY == false)
            {
                Win32api.SetWindowPos(windowptr, IntPtr.Zero, (int)(WindowCoo.X - (size.Width / 2.0 + offsetx)), (int)(WindowCoo.Y - (size.Height / 2.0 + offsety)), (int)size.Width, (int)size.Height, (noz));

            }
            else
            {
                Win32api.SetWindowPos(windowptr, IntPtr.Zero, (int)(WindowCoo.X - (size.Width / 2.0)), (int)(size.Height / 2.0 - (size.Height / 2.0)), (int)size.Width, (int)size.Height, (noz));

            }

        }

        public static void EyeTrackingAim(System.Windows.Vector MouseVector, System.Windows.Vector WindowVector)
        {
            DriverSendInput.SendMouseMove(DriverSendInput.butoonConvert(DoEyeTrackingAim.MouseLeftflag, DoEyeTrackingAim.MouseRightflag, DoEyeTrackingAim.MouseMiddleflag), (Int16)MouseVector.X, (Int16)(0.0));
            UpdateWindow(WindowVector, new System.Windows.Size(1920, 1080));

        }

       
    }
}
