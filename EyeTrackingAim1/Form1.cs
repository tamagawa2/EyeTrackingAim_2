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
using EyeTrackingAim1.Scripts.Ball_Move;
using EyeTrackingAim1.Scripts.python;
using EyeTrackingAim1.Scripts.Transmisstion.IPC;
using EyeTrackingAim1.Scripts.DisPlay_Setting;
using EyeTrackingAim1.Scripts.KannsuuHozon;
using EyeTrackingAim1.Scripts.UDP;
using EyeTrackingAim1.Scripts.WindowColor;
using EyeTrackingAim1.Scripts.GazouNinnsiki;
using EyeTrackingAim1.Scripts.Fine_tunig;
using EyeTrackingAim1.Scripts;
using System.Windows;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Management;
using Tobii.StreamEngine;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace EyeTrackingAim1
{
    public partial class Form1 : Form
    {

        //EyeTracking
        public static RawEye rawEye;
        public static ModifyEye modifyEye;
        public static Eyecenter[] eyecenter;
        public static EstimateGrapth estimateGrapth;
        public static System.Windows.Vector formsize = new System.Windows.Vector();
        public static bool GoEyeTrackingFlag = false;
        public static UserControl1 userControl1;
        public static UserControl2 userControl2;
        public static List<EyeData> eyeDatas = new List<EyeData>();
        public static set_Auto_encoder_data set_Auto_Encoder_Data = new set_Auto_encoder_data();
        public static int nowvalue;
        public static bool ShowBallFlag = true;
        public static bool Do_Game = true;
        public static System.Windows.Vector Mouse_move = new System.Windows.Vector();
        //Timer
        public System.Windows.Forms.Timer timerEye = new System.Windows.Forms.Timer();
        //Thread
        public static Thread AimThread;
        public static Thread Camera_Thread;
        public static Thread Tobii_Thread;

        //Tobii
        public static Tobii.InteractionLib.IInteractionLib intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(Tobii.InteractionLib.FieldOfUse.Interactive);

        //CaribrationCoodinate
        public static bool GoCaribrationCoodinateFlag = false;
        public GoCalibrationCoodinate goCalibrationCoodinate;
        
        //Prediction
        public static bool GoCalibrationOffsetFlag = false;
        public GoCalibrationOffset goCalibrationOffset;

        [DllImport("Winmm.dll")]
        public static extern uint timeBeginPeriod(uint uuPeriod);

        [DllImport("Winmm.dll")]
        public static extern uint timeEndPeriod(uint uuPeriod);

        public Form1()
        {  
            InitializeComponent();
            timeBeginPeriod(1);


            Display_color.SetGamma(1.0, 0.0, 1.0);
            //Akarusa.camera_open();
            this.BackColor = Color.White;
            try
            {
                //ドライバーの状態をファイルから取得
                RecordDriverSendinput.Driver_SendInput_State = RecordDriverSendinput.ReciveDriverSendInputSatte(
                "DriverSendInputState/DriverSendInputState.bin");
            }
            catch { };

            set_Auto_Encoder_Data = new set_Auto_encoder_data();

            this.Text = "EyeTrackingAim";
            this.DoubleBuffered = true;
            MouseRawInut.RegisterWindow(this.Handle);
            eyeDatas.Add(new EyeData());
            userControl1 = new UserControl1(this);
            userControl2 = new UserControl2(this);
            userControl1.Anchor = AnchorStyles.Top;
            this.Controls.Add(userControl1);       
        }

        //マウス入力受け取り
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MouseRawInut.WM_INPUT)
            {
                MouseRawInut.RAWINPUT rAWINPUT = new MouseRawInut.RAWINPUT();

                uint size = (uint)Marshal.SizeOf(rAWINPUT);

                MouseRawInut.GetRawInputData(m.LParam, 0x10000003, out rAWINPUT, ref size, Marshal.SizeOf(typeof(MouseRawInut.RAWINPUTHEADER)));

                Mouse_move.X = rAWINPUT.mouse.LastX;
                Mouse_move.Y = rAWINPUT.mouse.LastY;
                

                if (rAWINPUT.mouse.ButtonFlags == 0x0400)
                {
                    

                    double delta = (short)rAWINPUT.mouse.ButtonData / 120.0;
                    
                    if (Do_Game == false)
                    {
                        if (ball.v + delta >= 1)
                        {
                            ball.v = ball.v + delta;
                        }
                    }
                    

                }
            }

            base.WndProc(ref m);
        }

        //画面に色々描画
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            
            //EyeTrackingAim
            if (GoEyeTrackingFlag)
            {
                if (Do_Game == false)
                {
                    int rr = 4;
                    Brush TargetBrush = new SolidBrush(Color.Blue);
                    Rectangle TargetRect = new Rectangle((int)(ball.x - (rr / 2.0)), (int)(540 - (rr / 2.0)), rr, rr);
                    e.Graphics.FillRectangle(TargetBrush, TargetRect);
                    TargetBrush.Dispose();

                    Font font = new Font("Times New Roman", 10, FontStyle.Regular);
                    Brush fontbrush = new SolidBrush(Color.Blue);

                   

                    SizeF v_size = e.Graphics.MeasureString(ball.v.ToString(), font);
                    SizeF offset_from_modi_size = e.Graphics.MeasureString(ball.offset_modi_ave.ToString(), font);


                    e.Graphics.DrawString(ball.v.ToString(), font, fontbrush, (int)(960 - v_size.Width), (int)(540 + v_size.Height));
                    e.Graphics.DrawString(ball.offset_modi_ave.ToString(), font, fontbrush, (int)(960 + offset_from_modi_size.Width), (int)(540 + offset_from_modi_size.Height));

                    font.Dispose();
                }
                

            }


            //GoCalibrationOffset
            if (GoCalibrationOffsetFlag)
            {

                double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;
                double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;

                //目ようのペン
                int huchi = 6;
                int hannkei = 50;
                Pen pen = new Pen(Color.FromArgb(255, 50, 255, 50), huchi);
                Rectangle rectangle = new Rectangle((int)(goCalibrationOffset.Eyepoint.X - (hannkei + huchi)), (int)(540.0 - (hannkei + huchi)), 2 * (hannkei + huchi), 2 * (hannkei + huchi));
                e.Graphics.DrawEllipse(pen, rectangle);
                pen.Dispose();
                Rectangle TargetRect = new Rectangle((int)CalibrationClass.calibrationOffsetDatas[0].CalibrationTarget.X - 15, (int)CalibrationClass.calibrationOffsetDatas[0].CalibrationTarget.Y - 15, 30, 30);
                Brush TargetBrush = new SolidBrush(Color.Red);
                e.Graphics.FillEllipse(TargetBrush, TargetRect);
                TargetBrush.Dispose();
 
            }

            //CalibrationCoodinate
            if (GoCaribrationCoodinateFlag)
            {
                
                double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;
                double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;

                //目ようのペン
                int huchi = 6;
                int hannkei = 50;
                Pen pen = new Pen(Color.FromArgb(255, 50, 255, 50), huchi);
                Rectangle rectangle = new Rectangle((int)(goCalibrationCoodinate.Eyepoint.X - (hannkei + huchi)), (int)(540.0 - (hannkei + huchi)), 2 * (hannkei + huchi), 2 * (hannkei + huchi));
                e.Graphics.DrawEllipse(pen, rectangle);
                pen.Dispose();

                //ターゲットようのペンとボール


               

                for (int i = 0; i < CalibrationClass.calibrationCoodinateDatas.Length; i++)
                {
                    int index = i;

                    

                    if (CalibrationClass.calibrationCoodinateDatas[index].CalibrationCompleate == false)
                    {
                        int rr = 16;

                        Brush TargetBrush = new SolidBrush(Color.Red);


                        if (CalibrationClass.calibrationCoodinateDatas[index].nowuseflag)
                        {
                            TargetBrush = new SolidBrush(Color.Red);

                        } else
                        {
                            TargetBrush = new SolidBrush(Color.LightBlue);
                            if (CalibrationClass.calibrationCoodinateDatas[index].focus)
                            {
                                TargetBrush = new SolidBrush(Color.LightGreen);
                            }

                        }

                        Rectangle TargetRect = new Rectangle((int)(CalibrationClass.calibrationCoodinateDatas[index].CalibrationTarget.X - (rr / 2)), (int)(CalibrationClass.calibrationCoodinateDatas[index].CalibrationTarget.Y - (rr / 2)), rr, rr);
                        e.Graphics.FillEllipse(TargetBrush, TargetRect);
                        TargetBrush.Dispose();                
                    }
                    else
                    {
                        int rr = 30;
                        Brush TargetBrush = new SolidBrush(Color.LightBlue);
                        Rectangle TargetRect = new Rectangle((int)(CalibrationClass.calibrationCoodinateDatas[index].CalibrationTarget.X - (rr / 2)), (int)(CalibrationClass.calibrationCoodinateDatas[index].CalibrationTarget.Y - (rr / 2)), rr, rr);

                        e.Graphics.FillEllipse(TargetBrush, TargetRect);
                        TargetBrush.Dispose();
                    }     
                }
            }

        }

    }

}

