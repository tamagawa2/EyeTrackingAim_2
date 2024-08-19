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
using EyeTrackingAim1.Scripts.python;
using EyeTrackingAim1.Scripts.Prediction;
using EyeTrackingAim1.Scripts.DisPlay_Setting;
using EyeTrackingAim1.Scripts.KannsuuHozon;
using EyeTrackingAim1.Scripts.Face_Byouga;
using EyeTrackingAim1.Scripts.UDP;
using EyeTrackingAim1.Scripts;
using EyeTrackingAim1.Scripts.WindowColor;
using System.Windows;
using System.Runtime.InteropServices;
using System.Management;

namespace EyeTrackingAim1.Scripts.EyeTrackingAim
{
    public class DoEyeTrackingAim
    {
        static double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;
        static double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;

        

        //EyeMove
        public static double time;
        public static Vector RawEyePoint = new Vector();
        public static Vector ModifyEyePoint = new Vector();
        public static Vector EyeVelosity = new Vector();
        public static Vector EyeAccelation = new Vector();
        public static double[] Filter_hozon_Y = new double[10];
        public static double offset_y = 0;

        public static double[,] tobii_data = new double[2,8];
        public static double[,] RawEyeHeadX_data = new double[2, 1];

        public static int I = 0;
        public static int velo_hozon_length = 300;
        public static double[,] Velo_Hozon = new double[velo_hozon_length, 8];
        public static double[,] Velo_Head_Hozon = new double[velo_hozon_length, 1];
        public static double[,] Cali_y_Hozon = new double[1, 15];
        public static double[,] Cali_x_pre_Hozon = new double[1, 15];
        

        //EyeTrackingAim
        public static Vector bufMouse = new Vector();
        public static Vector bufWindow = new Vector();
        public static Vector save_bufMouse = new Vector();
        public static Vector save_bufWindow = new Vector();
        public static Vector WindowCoordinate = new Vector();
        public static double fpsSabunn = 0.0;

        //Flag
        public static bool F3Downflag = false;
        public static bool F4Downflag = false;
        public static int EyeTrackingAimState = 0;
        public static int EyeState = 0;
        public static int F3state = 0;
        public static int F4state = 0;
        public static bool MouseLeftflag = false;
        public static bool MouseRightflag = false;
        public static bool MouseMiddleflag = false;
        public static int[] calistate = new int[3];
        public static int fpsSetteiflag = 0;
        public static int offsetflag = 0;
        public static int[] cali_y_flag = new int[2];

        //StopWatch
        public static System.Diagnostics.Stopwatch swUp = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch swrelease = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch swfpsoffset = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch sw_EyeTracking_fps = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch sw_yosoku_fps = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch sw_tobii_fps = new System.Diagnostics.Stopwatch();
        public static System.Diagnostics.Stopwatch sw_cali_y = new System.Diagnostics.Stopwatch();

        //EyeTrackingAim_fps
        public static double aimfpsSetting = 200;
        public static double yosokufpsSetting = 100;

        public static double timer_fps = 0;

        public static int buruburuflag = 0;
        


        public static void EyeTracking(Form1 form1)
        {
            while (true)
            {
                

                F3Downflag = RawInputKey.RawInputJudge(KeyData.Z);
                F4Downflag = RawInputKey.RawInputJudge(KeyData.P);

                if (EyeTrackingAimState == 0 && RawInputKey.RawInputJudge(KeyData.X) == true)
                {

                    EyeTrackingAimState = 1;
                    Display_color.SetGamma(1.0, 0.0, 1.0);
                    Window_Not_Move.Init_Data();

                    form1.Invoke((MethodInvoker)delegate
                    {
                        Form1.modifyEye.Invalidate();
                    }
                    );
                }
                else if (EyeTrackingAimState == 1 && RawInputKey.RawInputJudge(KeyData.X) == false)
                {
                    EyeTrackingAimState = 2;
                }
                else if (EyeTrackingAimState == 2 && RawInputKey.RawInputJudge(KeyData.X) == true)
                {
                    EyeTrackingAimState = 3;
                }
                else if (EyeTrackingAimState == 3 && RawInputKey.RawInputJudge(KeyData.X) == false)
                {
                    //reset
                    EyeTrackingAimState = 0;
                    swUp = new System.Diagnostics.Stopwatch();
                    EyeState = 0;
                    WindowCoordinate = new Vector(we, he);
                    EyeTrackingAim_Setting.SetWindow();
                    Display_color.SetGamma(1.0, 0.0, 1.0);

                    bufMouse = new Vector();
                    bufWindow = new Vector();
                    save_bufMouse = new Vector();
                    save_bufWindow = new Vector();

                    form1.Invoke((MethodInvoker)delegate
                    {
                        Form1.modifyEye.Invalidate();
                    }
                    );

                }

                /*if (EyeState == 0 && ((F3Downflag) || (F4Downflag)) && (EyeTrackingAimState == 1 || EyeTrackingAimState == 2))
                {
                    EyeState = 1;


                    *//*Vector sendM = EyeTrackingAim_Setting.vec_int(bufMouse);
                    Vector sendW = EyeTrackingAim_Setting.vec_int(bufWindow);
                    WindowCoordinate = WindowCoordinate + sendW;
                    bufMouse = bufMouse - sendM;
                    bufWindow = bufWindow - sendW;
                    EyeTrackingAim_Setting.EyeTrackingAim(sendM, WindowCoordinate);*//*
                    Window_Not_Move.Init_Data();

                    swUp.Restart();

                }
                else if (EyeState == 1)
                {

                    if ((MouseLeftflag == false && F3Downflag) || (MouseRightflag == false && F4Downflag))
                    {
                        swUp.Restart();
                        swrelease = new System.Diagnostics.Stopwatch();
                    }
                    else if ((MouseLeftflag || MouseRightflag) && (F3Downflag == false && F4Downflag == false))
                    {
                        swrelease.Restart();
                    }
                    else if ((swUp.ElapsedMilliseconds / 1000.0) > 0.2)
                    {
                        swUp = new System.Diagnostics.Stopwatch();
                        EyeState = 2;
                        bufMouse = new Vector();
                        bufWindow = new Vector();

                    }
                    else if ((swrelease.ElapsedMilliseconds / 1000.0) > 0.3)
                    {
                        swUp = new System.Diagnostics.Stopwatch();
                        swrelease = new System.Diagnostics.Stopwatch();
                        EyeState = 0;
                        WindowCoordinate = new Vector(we, he);
                        EyeTrackingAim_Setting.SetWindow();
                        bufMouse = new Vector();
                        bufWindow = new Vector();
                    }
                }
                else if (EyeState == 2)
                {
                    if ((MouseLeftflag || MouseRightflag) && (F3Downflag == false && F4Downflag == false))
                    {
                        swrelease.Restart();
                    }
                    else if (((MouseLeftflag == false && F3Downflag) || (MouseRightflag == false && F4Downflag)))
                    {
                        EyeState = 1;


                        *//*Vector sendM = EyeTrackingAim_Setting.vec_int(bufMouse);
                        Vector sendW = EyeTrackingAim_Setting.vec_int(bufWindow);
                        WindowCoordinate = WindowCoordinate + sendW;
                        bufMouse = bufMouse - sendM;
                        bufWindow = bufWindow - sendW;
                        EyeTrackingAim_Setting.EyeTrackingAim(sendM, WindowCoordinate);*//*
                        Window_Not_Move.Init_Data();

                        swUp.Restart();
                        swrelease = new System.Diagnostics.Stopwatch();

                    }
                    else if ((swrelease.ElapsedMilliseconds / 1000.0) > 0.3)
                    {
                        swUp = new System.Diagnostics.Stopwatch();
                        swrelease = new System.Diagnostics.Stopwatch();
                        EyeState = 0;
                        WindowCoordinate = new Vector(we, he);
                        EyeTrackingAim_Setting.SetWindow();
                        bufMouse = new Vector();
                        bufWindow = new Vector();

                    }
                }*/



                /*if (EyeState == 0 && (F3Downflag || F4Downflag) && (EyeTrackingAimState == 1 || EyeTrackingAimState == 2))
                {
                    EyeState = 1;
                    Window_Not_Move.Init_Data();

                }
                else if (EyeState == 1 && F3Downflag == false && F4Downflag == false)
                {
                    swUp.Start();
                    EyeState = 2;
                }
                else if (EyeState == 2 && (F3Downflag || F4Downflag))
                {
                    swUp = new System.Diagnostics.Stopwatch();
                    EyeState = 1;
                }
                else if (EyeState == 2 && swUp.ElapsedMilliseconds >= Form1.eyeDatas[Form1.nowvalue].DelayTime)
                {
                    swUp = new System.Diagnostics.Stopwatch();
                    EyeState = 0;
                    WindowCoordinate = new Vector(we, he);
                    EyeTrackingAim_Setting.SetWindow();
                    bufMouse = new Vector();
                    bufWindow = new Vector();

                   
                }*/



                if (EyeState == 0 && F3Downflag && (EyeTrackingAimState == 1 || EyeTrackingAimState == 2))
                {
                    EyeState = 1;
                    Window_Not_Move.Init_Data();

                }
                else if (EyeState == 1 && F3Downflag == false)
                {
                    EyeState = 2;
                }
                else if (EyeState == 2 && F3Downflag)
                {
                    EyeState = 3;
                }
                else if (EyeState == 3 && F3Downflag == false)
                {
                    
                    EyeState = 0;
                    WindowCoordinate = new Vector(we, he);
                    EyeTrackingAim_Setting.SetWindow();
                    bufMouse = new Vector();
                    bufWindow = new Vector();


                }


                if (sw_EyeTracking_fps.ElapsedMilliseconds > (1000 * (1.0 / aimfpsSetting)))
                {


                    double EyeTrack_fps = 1.0 / (sw_EyeTracking_fps.ElapsedMilliseconds * 0.001);
                    fpsSabunn = (EyeTrack_fps / timer_fps);

                    //Console.WriteLine("fpsSabunn" + fpsSabunn);
                    //Console.WriteLine("EyeTrack_fps" + EyeTrack_fps);

                    if (EyeState == 1 || EyeState == 2)         
                    {

                        Vector sendM = EyeTrackingAim_Setting.vec_int(save_bufMouse);
                        Vector sendW = EyeTrackingAim_Setting.vec_int(save_bufWindow);


                        bufMouse = bufMouse - sendM;
                        bufWindow = bufWindow - sendW;


                        if (Math.Sign(save_bufMouse.X) * bufMouse.X < 0)
                        {
                            bufMouse.X = bufMouse.X + sendM.X;
                            sendM.X = bufMouse.X - bufMouse.X % 1.0;
                            bufMouse.X = bufMouse.X - sendM.X;
                        }
                        if (Math.Sign(save_bufMouse.Y) * bufMouse.Y < 0)
                        {
                            bufMouse.Y = bufMouse.Y + sendM.Y;
                            sendM.Y = bufMouse.Y - bufMouse.Y % 1.0;
                            bufMouse.Y = bufMouse.Y - sendM.Y;
                        }
                        if (Math.Sign(save_bufWindow.X) * bufWindow.X < 0)
                        {
                            bufWindow.X = bufWindow.X + sendW.X;
                            sendW.X = bufWindow.X - bufWindow.X % 1.0;
                            bufWindow.X = bufWindow.X - sendW.X;
                        }
                        if (Math.Sign(save_bufWindow.Y) * bufWindow.Y < 0)
                        {
                            bufWindow.Y = bufWindow.Y + sendW.Y;
                            sendW.Y = bufWindow.Y - bufWindow.Y % 1.0;
                            bufWindow.Y = bufWindow.Y - sendW.Y;
                        }

                        Console.WriteLine(sendM);
                        WindowCoordinate = WindowCoordinate + sendW;
                        //EyeTrackingAim_Setting.EyeTrackingAim(sendM, WindowCoordinate);

                        EyeTrackingAim_Setting.EyeTrackingAim((ModifyEyePoint - new Vector(960.0, 540.0)) * 0.03, WindowCoordinate);
                    }
                    
                    sw_EyeTracking_fps.Restart();

                }




                if (MouseLeftflag == false && F3Downflag)
                {
                    
                    //MouseLeftflag = true;
                    //DriverSendInput.SendMouseMove(DriverSendInput.butoonConvert(MouseLeftflag, MouseRightflag, MouseMiddleflag), 0, 0);

                }
                else if (MouseLeftflag && F3Downflag == false)
                {
                    //MouseLeftflag = false;

                    //DriverSendInput.SendMouseMove(DriverSendInput.butoonConvert(MouseLeftflag, MouseRightflag, MouseMiddleflag), 0, 0);
                }

                if (MouseRightflag == false && F4Downflag)
                {

                   
                    //MouseRightflag = true;
                    //DriverSendInput.SendMouseMove(DriverSendInput.butoonConvert(MouseLeftflag, MouseRightflag, MouseMiddleflag), 0, 0);

                }
                else if (MouseRightflag && F4Downflag == false)
                {
                    //MouseRightflag = false;
                    //DriverSendInput.SendMouseMove(DriverSendInput.butoonConvert(MouseLeftflag, MouseRightflag, MouseMiddleflag), 0, 0);
                }




                //finetuning



                if (RawInputKey.RawInputJudge(Form1.eyeDatas[Form1.nowvalue].ReleaseEyeTrackingKey))
                {
                    form1.Invoke((MethodInvoker)delegate
                    {
                        Modoru(form1);
                    }
                    );

                }

                Thread.Sleep(1);

            }

        }



        public static void Modoru(Form1 form1)
        {
            Form1.Tobii_Thread.Abort();
            Form1.Camera_Thread.Abort();
            //save

            RecordCalibration.Recold_cali_manual(CalibrationClass.cali_Manual_offset, "PyData/cali_Manual_offset.bin");

            if (Form1.Do_Game == false)
            {

            }

            form1.FormBorderStyle = FormBorderStyle.Sizable;
            form1.WindowState = FormWindowState.Normal;
            form1.TopMost = false;
            form1.BackgroundImage = null;
            form1.TransparencyKey = Color.Empty;
            form1.BackColor = Color.White;
            form1.Activate();

            Form1.rawEye.Close();
            Form1.modifyEye.Close();
            for (int i = 0; i < Form1.eyecenter.Length; i++)
            {
                Form1.eyecenter[i].Close();
            }
            //Byouga_Setting.Face_Byouga_window.Close();


            //sleep
            //Form1.timeEndPeriod(1);

            //UDP
            //UDPReceive.udp.Close();
            //UDPReceive.thread.Abort();

            //ドライバー
            if (UserControl1.driver_ok)
            {
                DriverSendInput.DriverSendInput_LoopSwitch();
                RecordDriverSendinput.RecoldDriverSendInputSatte(RecordDriverSendinput.Driver_SendInput_State,
                    "DriverSendInputState/DriverSendInputState.bin");

            }


            //Getcolor
            //GetColor.Exit_GetColor();
            //デスクトップ明るさ戻し
            Display_color.SetGamma(1.0, 0.0, 1.0);


            Form1.userControl1.Anchor = AnchorStyles.Top;
            Form1.userControl1 = new UserControl1(form1);
            form1.Controls.Add(Form1.userControl1);
            ;

            Filter_hozon_Y = new double[Filter_hozon_Y.Length];
            offset_y = 0;
            //YosokuHozon
            Velo_Hozon = new double[velo_hozon_length, 8];
            

            I = 0;

            //flag
            F3state = 0;
            F4state = 0;
            EyeState = 0;
            EyeTrackingAimState = 0;
            calistate = new int[calistate.Length];
            offsetflag = 0;
            Form1.GoEyeTrackingFlag = false;
            MouseLeftflag = false;
            MouseRightflag = false;
            MouseMiddleflag = false;
            F3Downflag = false;
            F4Downflag = false;
            cali_y_flag = new int[cali_y_flag.Length];

            //StopWatch
            swUp = new System.Diagnostics.Stopwatch();
            swrelease = new System.Diagnostics.Stopwatch();
            swfpsoffset = new System.Diagnostics.Stopwatch();
            sw_EyeTracking_fps = new System.Diagnostics.Stopwatch();
            sw_tobii_fps = new System.Diagnostics.Stopwatch();
            sw_yosoku_fps = new System.Diagnostics.Stopwatch();

            
            Form1.AimThread.Abort();
            
            

        }





    }
}
