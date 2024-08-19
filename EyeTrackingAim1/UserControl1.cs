using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using EyeTrackingAim1.Scripts.EyeData;
using EyeTrackingAim1.Scripts.SendInput;
using EyeTrackingAim1.Scripts.Calibration;
using EyeTrackingAim1.Scripts.Prediction;
using EyeTrackingAim1.Scripts.DisPlay_Setting;
using EyeTrackingAim1.Scripts.EyeTrackingAim;
using EyeTrackingAim1.Scripts.KannsuuHozon;
using EyeTrackingAim1.Scripts.Ball_Move;
using EyeTrackingAim1.Scripts.Face_Byouga;
using EyeTrackingAim1.Scripts.RawInput;
using EyeTrackingAim1.Scripts.Fine_tunig;
using EyeTrackingAim1.Scripts.GazouNinnsiki;
using EyeTrackingAim1.Scripts.WindowColor;
using System.Numerics;
using Tobii.StreamEngine;
using System.Diagnostics;
using System.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using MathNet.Numerics.LinearAlgebra.Double;
using EyeTrackingAim1.Scripts.UDP;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace EyeTrackingAim1
{
    public partial class UserControl1 : UserControl
    {
        Form1 form;
        public static bool driver_ok = false;
        
        public UserControl1(Form1 form1)
        {
           InitializeComponent();

           form = form1;

           

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            
            //EyeData
            Form1.eyeDatas = Recold.ReciveEye();
            //manual
            CalibrationClass.cali_Manual_offset = RecordCalibration.Recive_cali_manual("PyData/cali_Manual_offset.bin");
            //mlp
            EstimateClass.py_cali_co = RecordPrediction.Recive_Py_mlp("PyData/py_cali_co_fine.bin");
            EstimateClass.py_cali_x_head = RecordPrediction.Recive_Py_mlp("PyData/py_cali_x_head.bin");
            EstimateClass.py_cali_x_pre_mlp = RecordPrediction.Recive_Py_mlp("PyData/py_cali_x_pre_mlp.bin");
            //lstm
            EstimateClass.py_lstm_cali_y = RecordPrediction.Receive_lstm_Keisuu("PyData/py_lstm_cali_y.bin");
            EstimateClass.py_lstm_cali_y_flick_stop = RecordPrediction.Receive_lstm_Keisuu("PyData/py_lstm_cali_y_flick_stop.bin");
            //Conv1d
            EstimateClass.py_Conv1d_cali_y = RecordPrediction.ReceiveConv1d_network("PyData/pytorch_cali_y.bin");
            EstimateClass.py_Conv1d_cali_y_v_reg = RecordPrediction.ReceiveConv1d_network("PyData/pytorch_cali_y_v_reg.bin");
            EstimateClass.py_Conv1d_cali_x_pre_label = RecordPrediction.ReceiveConv1d_network("PyData/pytorch_cali_x_pre_label.bin");
            EstimateClass.py_Conv1d_cali_x_pre = RecordPrediction.ReceiveConv1d_network("PyData/pytorch_cali_x_pre_All.bin");
            //filter
            DegitalFilter.filter_Keisuu_y0 = RecordPrediction.Receive_Filter_Keisuu("PyData/filter_Keisuu_y0.bin");
            DegitalFilter.filter_Keisuu_y0_offset = RecordPrediction.Receive_Filter_Keisuu("PyData/filter_Keisuu_y0.bin");
            DegitalFilter.filter_Keisuu_x = RecordPrediction.Receive_Filter_Keisuu("PyData/filter_Keisuu_x.bin");
            DegitalFilter.filter_Keisuu_pre_x = RecordPrediction.Receive_Filter_Keisuu("PyData/filter_Keisuu_pre_x.bin");

            DegitalFilter.filter_Keisuu_inverse_pre_x = RecordPrediction.Receive_Filter_inverse_Keisuu("PyData/filter_Keisuu_pre_x.bin");


            double[] test = new double[66];
            for (int i = 0; i < test.GetLength(0); i++)
            {
                test[i] = i;
            }

            EstimateClass.py_cali_x_pre_mlp = ActiveEyeKannsuu.mlp_cali_x_pre(test, EstimateClass.py_cali_x_pre_mlp);
            for (int i = 0; i < 3; i++)
            {
                //Console.WriteLine("py_cali_x_pre_mlp   " + EstimateClass.py_cali_x_pre_mlp.reg[0][i]);
            }

            int test_conv_size = 15;
            double[, ] test_conv = new double[1, test_conv_size];
            for (int i = 0; i < test_conv_size; i++)
            {
                test_conv[0, i] = i;
            }
            EstimateClass.py_Conv1d_cali_x_pre = ActiveEyeKannsuu.py_cali_x_pre(test_conv, EstimateClass.py_Conv1d_cali_x_pre);
             
             for (int i = 0; i < test_conv_size; i++)
             {
                 Console.WriteLine("py_Conv1d_cali_x_pre  " + EstimateClass.py_Conv1d_cali_x_pre.reg[i]);
             }


            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int i = 0; i < Form1.eyeDatas.Count; i++)
            {
                comboBox1.Items.Add(Form1.eyeDatas[i].Appname);
            }

            comboBox1.SelectedIndex = Form1.nowvalue;

        }

        private void buttonGoSetting_Click(object sender, EventArgs e)
        {
            Form1.nowvalue = comboBox1.SelectedIndex;
            Form1.userControl2 = new UserControl2(form);
            Form1.userControl2.Anchor = AnchorStyles.Top;
            form.Controls.Add(Form1.userControl2);
            this.Dispose();
        }

        private void buttonPruss_Click(object sender, EventArgs e)
        {
            Form1.nowvalue = comboBox1.SelectedIndex;
            
            Form1.eyeDatas.Add(new Scripts.EyeData.EyeData());

            comboBox1.Items.Clear();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int i = 0; i < Form1.eyeDatas.Count; i++)
            {
                comboBox1.Items.Add(Form1.eyeDatas[i].Appname);
            }

            comboBox1.SelectedIndex = Form1.nowvalue;
        }

        private void buttonGoEyeTracking_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GoEyetrackingAim();
            
           
        }

       
        public void GoEyetrackingAim()
        {
            if(Form1.Do_Game == false)
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
                form.BackColor = Color.White;
                form.TransparencyKey = form.BackColor;
                form.TopMost = true;
                ball.Init_ball(form);

                
            }


            float height = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            float width = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;


            //ウィンドウ
            Form1.rawEye = new RawEye(form);
            Form1.rawEye.FormBorderStyle = FormBorderStyle.None;
            Form1.rawEye.TransparencyKey = Form1.rawEye.BackColor;
            Form1.rawEye.Opacity = 0.5;
            Setwindow.TopMostNonActiveWindow(Form1.rawEye.Handle, 70, 70);
            Form1.rawEye.Size = new System.Drawing.Size(120, 120);
            

            Form1.modifyEye = new ModifyEye(form);
            Form1.modifyEye.FormBorderStyle = FormBorderStyle.None;
            Form1.modifyEye.TransparencyKey = Form1.modifyEye.BackColor;
            Form1.modifyEye.Opacity = 0.75;
            Setwindow.TopMostNonActiveWindow(Form1.modifyEye.Handle, 70, 70);
            Form1.modifyEye.Size = new System.Drawing.Size(80, 80);

            
            DoEyeTrackingAim.buruburuflag = 0;

            

            //Face_Byouga_Setting
            float b = (float)(10 * (62.23) * (9 / Math.Sqrt(Math.Pow(16, 2) + Math.Pow(9, 2))));
            b = b / 2.0f - 100;
            Byouga_Setting.Byouga_Init(new Vector3(0, b, -250), new Vector3(0, b, -15), 40);
            Byouga_Setting.enn_origin_0 = Byouga_Setting.ennTeigi(100, 100, 200, 0);
            Byouga_Setting.enn_origin_1 = Byouga_Setting.ennTeigi(100, 100, 200, 1);
            Byouga_Setting.enn_origin_2 = Byouga_Setting.ennTeigi(100, 100, 200, 2);
            Byouga_Setting.enn_0 = Byouga_Setting.ennTeigi(100, 100, 200, 0);
            Byouga_Setting.enn_1 = Byouga_Setting.ennTeigi(100, 100, 200, 1);
            Byouga_Setting.enn_2 = Byouga_Setting.ennTeigi(100, 100, 200, 2);
            /*Byouga_Setting.Face_Byouga_window = new Face_Byouga_Form();
            Byouga_Setting.Face_Byouga_window.Show();
            Byouga_Setting.Face_Byouga_window.FormBorderStyle = FormBorderStyle.None;
            Byouga_Setting.Face_Byouga_window.TransparencyKey = Byouga_Setting.Face_Byouga_window.BackColor;
            Byouga_Setting.Face_Byouga_window.TopMost = true;
            Byouga_Setting.Face_Byouga_window.Location = new System.Drawing.Point((int)(960.0 - Byouga_Setting.Face_Byouga_window.Width / 2.0), (int)(540.0 - Byouga_Setting.Face_Byouga_window.Height / 2.0));*/

            //UDP
            //UDPReceive.InitUDP();

            //Gui
            Form1.nowvalue = comboBox1.SelectedIndex;
            Form1.formsize = new System.Windows.Vector(form.Size.Width, form.Size.Height);
            DoEyeTrackingAim.WindowCoordinate = new System.Windows.Vector(width / 2.0, height / 2.0);
            EyeTrackingAim_Setting.Zscreen = (EyeTrackingAim_Setting.windowsize.X / 2.0) * (1 / Math.Tan(Form1.eyeDatas[Form1.nowvalue].FovW / (2.0) * (Math.PI / 180)));

            //ドライバー起動
            if (driver_ok)
            {
                DriverSendInput.CreateFileEyeTrackingAim();
                DriverSendInput.DriverSendInput_LoopSwitch();
            }
            
            //Akarusa
            //Akarusa.InitCascade(Akarusa.gakusyuufailepath);
            //sleep
            //Form1.timeBeginPeriod(1);

            //ゲームのウィンドウハンドル取得          
            EyeTrackingAim_Setting.windowptr = Win32api.FindWindow(null, Form1.eyeDatas[Form1.nowvalue].Appname);


            //GetColor
            //GetColor.Init_GetColor();
            //Display_color.test_Set_Display_color();
            

            //cali_tuning_setting
            CalibrationClass.cali_tuning = new CalibrationClass.CalibrationData(100);
            CalibrationClass.cali_tuning_n = 0;

            //StopWatchKidou
            DoEyeTrackingAim.sw_EyeTracking_fps.Restart();
            DoEyeTrackingAim.sw_yosoku_fps.Restart();
            DoEyeTrackingAim.sw_tobii_fps.Restart();
            
            TobiiSetting();
            Form1.GoEyeTrackingFlag = true;

            form.timerEye = new System.Windows.Forms.Timer()
            {
                Interval = 1,
                Enabled = true,
            };

            form.timerEye.Tick += (sender1, e1) =>
            {             
                Form1.modifyEye.Invalidate();
                form.Invalidate();
                Vector3 heikou = new Vector3((float)DoEyeTrackingAim.tobii_data[0, 2], (float)DoEyeTrackingAim.tobii_data[0, 3], (float)DoEyeTrackingAim.tobii_data[0, 4]);
                Vector3 eulerAngle = new Vector3((float)DoEyeTrackingAim.tobii_data[0, 5], (float)DoEyeTrackingAim.tobii_data[0, 6], (float)DoEyeTrackingAim.tobii_data[0, 7]);
                //Byouga_Setting.enn_Kaitenn_Heikouidou(heikou, eulerAngle);
                //Byouga_Setting.Face_Byouga_window.Invalidate();

                if (Form1.eyeDatas[Form1.nowvalue].CancelY == false)
                {
                    Form1.rawEye.Location = new System.Drawing.Point((int)(960 - Form1.rawEye.Size.Width / 2.0), (int)(DoEyeTrackingAim.RawEyePoint.Y - Form1.rawEye.Size.Height / 2.0));
                    Form1.modifyEye.Location = new System.Drawing.Point((int)(960 - Form1.modifyEye.Size.Width / 2.0), (int)(DoEyeTrackingAim.ModifyEyePoint.Y - Form1.modifyEye.Size.Height / 2.0)); 
                }
                else
                {
                    Form1.rawEye.Location = new System.Drawing.Point((int)(DoEyeTrackingAim.RawEyePoint.X - Form1.rawEye.Size.Width / 2.0), (int)((DoEyeTrackingAim.RawEyePoint.Y) - Form1.rawEye.Size.Height / 2.0));
                    Form1.modifyEye.Location = new System.Drawing.Point((int)(DoEyeTrackingAim.ModifyEyePoint.X - Form1.modifyEye.Size.Width / 2.0), (int)((DoEyeTrackingAim.RawEyePoint.Y) - Form1.modifyEye.Size.Height / 2.0));  
                }
            };

            Form1.AimThread = new Thread(new ThreadStart(() =>
            {              
                DoEyeTrackingAim.EyeTracking(form);
            }));

           Form1.Camera_Thread = new Thread(new ThreadStart(() =>
           {
               /*while (true)
               {
                   Akarusa.FaceNinnsiki();
                   form.BackgroundImage = BitmapConverter.ToBitmap(Akarusa.rawcamera);
                   Thread.Sleep(1);
               }*/
           }));


            Form1.Tobii_Thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {        
                    Form1.intlib.WaitAndUpdate();
                }
            }));

            Form1.AimThread.Start();
            Form1.Camera_Thread.Start();
            Form1.Tobii_Thread.Start();

        }

        
        public void TobiiSetting()
        {

            //FineTunig
            set_fine_tunig_data set_Fine_Tunig_Data = new set_fine_tunig_data();
            set_Fine_Tunig_Data.Init_set_fine_tunig_data();
            Form1.eyecenter = new Eyecenter[set_Fine_Tunig_Data.cali_co_vec.Length];

            for (int i = 0; i < Form1.eyecenter.Length; i++)
            {
                Form1.eyecenter[i] = new Eyecenter(form);
                Form1.eyecenter[i].FormBorderStyle = FormBorderStyle.None;
                Form1.eyecenter[i].TransparencyKey = Form1.eyecenter[i].BackColor;
                Setwindow.TopMostNonActiveWindow(Form1.eyecenter[i].Handle, 100, 100);
                Form1.eyecenter[i].Size = new System.Drawing.Size(4, 4);
                Form1.eyecenter[i].Location = new System.Drawing.Point((int)(set_Fine_Tunig_Data.cali_co_vec[i].X - Form1.eyecenter[i].Size.Width / 2.0), (int)(set_Fine_Tunig_Data.cali_co_vec[i].Y - Form1.eyecenter[i].Size.Height / 2.0));

            }


            DoEyeTrackingAim.Filter_hozon_Y[1] = 540;

            Form1.intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(
                    Tobii.InteractionLib.FieldOfUse.Interactive);
            Form1.intlib.CoordinateTransformAddOrUpdateDisplayArea(1920.0f, 1080.0f);
            Form1.intlib.CoordinateTransformSetOriginOffset(0, 0);

            Form1.intlib.GazeOriginDataEvent += evt0 =>
            {
            };

            Form1.intlib.HeadPoseDataEvent += evt1 =>
            {
                DoEyeTrackingAim.tobii_data[1, 2] = DoEyeTrackingAim.tobii_data[0, 2];
                DoEyeTrackingAim.tobii_data[1, 3] = DoEyeTrackingAim.tobii_data[0, 3];
                DoEyeTrackingAim.tobii_data[1, 4] = DoEyeTrackingAim.tobii_data[0, 4];
                DoEyeTrackingAim.tobii_data[1, 5] = DoEyeTrackingAim.tobii_data[0, 5];
                DoEyeTrackingAim.tobii_data[1, 6] = DoEyeTrackingAim.tobii_data[0, 6];
                DoEyeTrackingAim.tobii_data[1, 7] = DoEyeTrackingAim.tobii_data[0, 7];

                DoEyeTrackingAim.tobii_data[0, 2] = evt1.position_x;
                DoEyeTrackingAim.tobii_data[0, 3] = evt1.position_y;
                DoEyeTrackingAim.tobii_data[0, 4] = evt1.position_z;
                DoEyeTrackingAim.tobii_data[0, 5] = evt1.rotation_x;
                DoEyeTrackingAim.tobii_data[0, 6] = evt1.rotation_y;
                DoEyeTrackingAim.tobii_data[0, 7] = evt1.rotation_z;

            };


            Form1.intlib.GazePointDataEvent += evt =>
            {
                


                

                double dt = (evt.timestamp_us - DoEyeTrackingAim.time) / 1000000.0;
                
                DoEyeTrackingAim.timer_fps = 1.0 / (DoEyeTrackingAim.sw_tobii_fps.ElapsedMilliseconds * 0.001);
                //Console.WriteLine("Tobii_fps" + DoEyeTrackingAim.timer_fps);
                DoEyeTrackingAim.sw_tobii_fps.Restart();

               
                if (dt != 0)
                {
                    DoEyeTrackingAim.tobii_data[1, 0] = DoEyeTrackingAim.tobii_data[0, 0];
                    DoEyeTrackingAim.tobii_data[1, 1] = DoEyeTrackingAim.tobii_data[0, 1];

                    DoEyeTrackingAim.tobii_data[0, 0] = evt.x;
                    DoEyeTrackingAim.tobii_data[0, 1] = evt.y;

                    DoEyeTrackingAim.time = evt.timestamp_us;

                    //リングバッファ
                    DoEyeTrackingAim.I = (DoEyeTrackingAim.I + 1) % DoEyeTrackingAim.velo_hozon_length;

                    System.Windows.Vector offset = new System.Windows.Vector();

                    offset.X = 0 
                    + CalibrationClass.cali_Manual_offset.manual_offset.X;
                    ;

                    offset.Y = 0
                    + CalibrationClass.cali_Manual_offset.manual_offset.Y
                    ;

                    //座標
                    System.Windows.Vector X = new System.Windows.Vector(evt.x - offset.X, evt.y - offset.Y);
                    //速度
                    System.Windows.Vector V = (X - DoEyeTrackingAim.RawEyePoint) / dt;
                    //加速度
                    System.Windows.Vector A = (V - DoEyeTrackingAim.EyeVelosity) / dt;


                    DoEyeTrackingAim.RawEyePoint = X;
                    DoEyeTrackingAim.EyeVelosity = V;
                    DoEyeTrackingAim.EyeAccelation = A;

                    set_Fine_Tunig_Data.set_fine_tuning(RawInputKey.RawInputJudge(KeyData.F8), DoEyeTrackingAim.RawEyePoint);
                    yosoku_setting(dt);
                }

                
                
            };
        }

        void yosoku_setting(double dt)
        {


            //GetColor
            //GetColor.GetColor_function(new System.Windows.Vector(960, 540), 200, 200);
            //Set_Display_color
            //Display_color.Set_Display_color();


            //予測のための、リングバッファに速度保存(RawEye)
            for (int j = 0; j < DoEyeTrackingAim.Velo_Hozon.GetLength(1); j++)
            {
                DoEyeTrackingAim.Velo_Hozon[DoEyeTrackingAim.I, j] = (DoEyeTrackingAim.tobii_data[0, j] - DoEyeTrackingAim.tobii_data[1, j]) / dt;
            }
           
            //Cali_y_Hozon 代入
            for (int i = 0; i < DoEyeTrackingAim.Cali_y_Hozon.GetLength(0); i++)
            {
                for (int j = 0; j < DoEyeTrackingAim.Cali_y_Hozon.GetLength(1); j++)
                {
                    int i_ring = (DoEyeTrackingAim.I - j + DoEyeTrackingAim.velo_hozon_length) % DoEyeTrackingAim.velo_hozon_length;
                    DoEyeTrackingAim.Cali_y_Hozon[i, j] = DoEyeTrackingAim.Velo_Hozon[i_ring, i + 1];
                }

            }

            
            
            double x = 0;
            double y = 0;
            
            x = DoEyeTrackingAim.RawEyePoint.X;
            y = DoEyeTrackingAim.RawEyePoint.Y;


            //cali_x_head
            double[] py_cali_x_head = new double[1];
            py_cali_x_head[0] = DoEyeTrackingAim.tobii_data[0, 7];
            //EstimateClass.py_cali_x_head = ActiveEyeKannsuu.mlp_cali_x_head(py_cali_x_head, EstimateClass.py_cali_x_head);
            //x = x - EstimateClass.py_cali_x_head.reg[0][0];

            DoEyeTrackingAim.RawEyeHeadX_data[1, 0] = DoEyeTrackingAim.RawEyeHeadX_data[0, 0];
            DoEyeTrackingAim.RawEyeHeadX_data[0, 0] = x;

            //予測のための、リングバッファに速度保存(RawEyeHead)
            for (int j = 0; j < DoEyeTrackingAim.Velo_Head_Hozon.GetLength(1); j++)
            {
                DoEyeTrackingAim.Velo_Head_Hozon[DoEyeTrackingAim.I, j] = (DoEyeTrackingAim.RawEyeHeadX_data[0, j] - DoEyeTrackingAim.RawEyeHeadX_data[1, j]) / dt;
            }

            //Cali_x_pre_Hozon 代入//conv用
            for (int i = 0; i < DoEyeTrackingAim.Cali_x_pre_Hozon.GetLength(0); i++)
            {
                for (int j = 0; j < DoEyeTrackingAim.Cali_x_pre_Hozon.GetLength(1); j++)
                {
                    int i_ring = (DoEyeTrackingAim.I - j + DoEyeTrackingAim.velo_hozon_length) % DoEyeTrackingAim.velo_hozon_length;
                    DoEyeTrackingAim.Cali_x_pre_Hozon[i, j] = DoEyeTrackingAim.Velo_Head_Hozon[i_ring, i];
                }

            }

            EstimateClass.py_Conv1d_cali_x_pre = ActiveEyeKannsuu.py_cali_x_pre(DoEyeTrackingAim.Cali_x_pre_Hozon, EstimateClass.py_Conv1d_cali_x_pre);
            //EstimateClass.py_Conv1d_cali_x_pre_label = ActiveEyeKannsuu.py_cali_x_pre_label(DoEyeTrackingAim.Cali_x_pre_Hozon, EstimateClass.py_Conv1d_cali_x_pre_label);
            //EstimateClass.f_color = (DenseVector)EstimateClass.py_Conv1d_cali_x_pre_label.reg.Clone();
            EstimateClass.f_color = new DenseVector(new double[] { 0.0, 1.0, 0.0 });
            //mlp用
            double[] z = new double[DoEyeTrackingAim.Cali_x_pre_Hozon.GetLength(1)];
            for (int i = 0; i < z.Length; i++)
            {
                z[i] = DoEyeTrackingAim.Cali_x_pre_Hozon[0, i];
            }
            //EstimateClass.py_cali_x_pre_mlp = ActiveEyeKannsuu.mlp_cali_x_pre(z, EstimateClass.py_cali_x_pre_mlp);
            //x = x - EstimateClass.py_cali_x_pre_mlp.reg[0][0];


            x = x - EstimateClass.py_Conv1d_cali_x_pre.reg[0];

            y = DegitalFilter.filter_Keisuu_y0.H_Filter(y);

           /* x = x - CalibrationClass.cali_Manual_offset.manual_offset_modify.X;
            y = y - CalibrationClass.cali_Manual_offset.manual_offset_modify.Y;*/

            DoEyeTrackingAim.ModifyEyePoint = new System.Windows.Vector(x, y);


            Form1.set_Auto_Encoder_Data.set_data(RawInputKey.RawInputJudge(KeyData.F8), DoEyeTrackingAim.RawEyePoint, (long)DoEyeTrackingAim.time);
            if (DoEyeTrackingAim.calistate[0] == 0 && RawInputKey.RawInputJudge(KeyData.F10))
            {
                DoEyeTrackingAim.calistate[0] = 1;
            }
            else if (DoEyeTrackingAim.calistate[0] == 1 && RawInputKey.RawInputJudge(KeyData.F10) == true)
            {
                //
            }
            else if (DoEyeTrackingAim.calistate[0] == 1 && RawInputKey.RawInputJudge(KeyData.F10) == false)
            {
                //目の原点合わせ
                System.Windows.Vector vector = new System.Windows.Vector();
                vector.X = DoEyeTrackingAim.RawEyePoint.X + CalibrationClass.cali_Manual_offset.manual_offset.X - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0);
                vector.Y = DoEyeTrackingAim.RawEyePoint.Y + CalibrationClass.cali_Manual_offset.manual_offset.Y - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0);
                CalibrationClass.cali_Manual_offset.manual_offset = vector;

                vector.X = DoEyeTrackingAim.ModifyEyePoint.X + CalibrationClass.cali_Manual_offset.manual_offset_modify.X - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0);
                vector.Y = DoEyeTrackingAim.ModifyEyePoint.Y + CalibrationClass.cali_Manual_offset.manual_offset_modify.Y - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0);
                CalibrationClass.cali_Manual_offset.manual_offset_modify = vector;
                //cali_y
                //filter
                DegitalFilter.filter_Keisuu_y0.filter_reset(DoEyeTrackingAim.RawEyePoint.Y);
                DegitalFilter.filter_Keisuu_x.filter_reset(DoEyeTrackingAim.ModifyEyePoint.X);
                
                //lstm
                EstimateClass.py_lstm_cali_y.h = new DenseMatrix(EstimateClass.py_lstm_cali_y.lstm.w_ii.RowCount, 1);
                EstimateClass.py_lstm_cali_y.c = new DenseMatrix(EstimateClass.py_lstm_cali_y.lstm.w_ii.RowCount, 1);
                //hozon
                DoEyeTrackingAim.Filter_hozon_Y[0] = 540;
                DoEyeTrackingAim.calistate[0] = 0;
            }


            if (DoEyeTrackingAim.EyeState == 1)
            {

                //if (Math.Abs(DoEyeTrackingAim.Cali_x_pre_Hozon[0, 0]) < 1000)
                {
                    //DoEyeTrackingAim.bufMouse = DoEyeTrackingAim.bufMouse + EyeTrackingAim_Setting.VectorToMouse(DoEyeTrackingAim.ModifyEyePoint - DoEyeTrackingAim.WindowCoordinate, EyeTrackingAim_Setting.windowsize);
                    //DoEyeTrackingAim.bufWindow = DoEyeTrackingAim.bufWindow + (DoEyeTrackingAim.ModifyEyePoint - DoEyeTrackingAim.WindowCoordinate) * 1.0;


                    Window_Not_Move.Do_Window_Not_Move((DoEyeTrackingAim.ModifyEyePoint - new System.Windows.Vector(960.0, 540.0)));
                    DoEyeTrackingAim.bufMouse = DoEyeTrackingAim.bufMouse + Window_Not_Move.wnm_mouse;


                    //DoEyeTrackingAim.bufMouse = DoEyeTrackingAim.bufMouse + EyeTrackingAim_Setting.VectorToMouse((DoEyeTrackingAim.ModifyEyePoint - new System.Windows.Vector(960.0, 540.0)) * 1, new System.Windows.Vector(1920, 1080));
                    //DoEyeTrackingAim.bufWindow = new System.Windows.Vector();

                    DoEyeTrackingAim.save_bufMouse = new System.Windows.Vector(DoEyeTrackingAim.bufMouse.X, DoEyeTrackingAim.bufMouse.Y) / DoEyeTrackingAim.fpsSabunn;
                    DoEyeTrackingAim.save_bufWindow = new System.Windows.Vector(DoEyeTrackingAim.bufWindow.X, DoEyeTrackingAim.bufWindow.Y) / DoEyeTrackingAim.fpsSabunn;

                }


            }
            else
            {
                DoEyeTrackingAim.bufMouse = EyeTrackingAim_Setting.VectorToMouse(DoEyeTrackingAim.ModifyEyePoint - DoEyeTrackingAim.WindowCoordinate, EyeTrackingAim_Setting.windowsize);
                DoEyeTrackingAim.bufWindow = DoEyeTrackingAim.ModifyEyePoint - DoEyeTrackingAim.WindowCoordinate;
            }
        } 

        private void buttonCaliCoodinate_Click(object sender, EventArgs e)
        {
            this.Dispose();

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            var a = form.BackColor;
            form.BackColor = Color.White;
            //form.TransparencyKey = form.BackColor;
            //form.TopMost = true;

            Form inputPopup = new Form();
            inputPopup.Size = new System.Drawing.Size(600, 600);
            TextBox[] inputBox = new TextBox[4];
            for (int i = 0; i < inputBox.Length; i++)
            {
                inputBox[i] = new TextBox();
                inputBox[i].Size = new System.Drawing.Size(100, 40);
                inputBox[i].Location = new System.Drawing.Point(i * 100 + 100, 300);
                inputPopup.Controls.Add(inputBox[i]);
            }
            inputBox[0].Text = CalibrationClass.xbnnkatu.ToString();
            inputBox[1].Text = CalibrationClass.ybnnkatu.ToString();
            inputBox[2].Text = CalibrationClass.yoko_l.ToString();
            inputBox[3].Text = CalibrationClass.tate_l.ToString();

            Button submitButton = new Button();
            submitButton.Text = "次";
            submitButton.Size = new System.Drawing.Size(80, 30);
            submitButton.Location = new System.Drawing.Point(10, 40);
            submitButton.Click += (sender2, e2) => {
                
                CalibrationClass.xbnnkatu = int.Parse(inputBox[0].Text);
                CalibrationClass.ybnnkatu = int.Parse(inputBox[1].Text);
                CalibrationClass.yoko_l = double.Parse(inputBox[2].Text);
                CalibrationClass.tate_l = double.Parse(inputBox[3].Text);

                inputPopup.Close();
                
            };
            
            inputPopup.Controls.Add(submitButton);
            inputPopup.ShowDialog();

            DialogResult result = MessageBox.Show(
                "保存するフォルダを選んでください", "かくにん",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var dlg = new CommonOpenFileDialog())
                {
                    // フォルダ選択ダイアログ（falseにするとファイル選択ダイアログ）
                    dlg.IsFolderPicker = true;

                    if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        //開く以外を選択された場合はfalseを返す。
                        CalibrationClass.Cali_Coodinate_path = dlg.FileName;
                    }
                }

                Form1.GoCaribrationCoodinateFlag = true;
                form.goCalibrationCoodinate = new GoCalibrationCoodinate();
                form.goCalibrationCoodinate.InitializeCalibrationCoodinate(form);
                form.goCalibrationCoodinate.StartCalibrationCoodinate();


            }
            else if (result == DialogResult.No)
            {

                
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.WindowState = FormWindowState.Normal;
                form.TopMost = false;
                form.TransparencyKey = Color.Empty;
                form.BackColor = a;
                Form1.userControl1.Anchor = AnchorStyles.Top;
                Form1.userControl1 = new UserControl1(form);
                form.Controls.Add(Form1.userControl1);
            }
        }

        private void buttonCaliOffset_Click(object sender, EventArgs e)
        {
            this.Dispose();

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            var a = form.BackColor;
            form.BackColor = Color.White;

            Form multiButtonPopup = new Form();
            multiButtonPopup.Size = new System.Drawing.Size(200, 100);
            Button TrrackButton = new Button();
            TrrackButton.Text = "Track";
            TrrackButton.Location = new System.Drawing.Point(10, 10);
            Button FlickButton = new Button();
            FlickButton.Text = "Flick";
            FlickButton.Location = new System.Drawing.Point(110, 10);
            TrrackButton.Click += (sender2, e2) => {
                multiButtonPopup.Close();

                GoCalibrationOffset.Calioffsetflag = 1;
                Form inputPopup = new Form();
                inputPopup.Size = new System.Drawing.Size(600, 600);
                TextBox[] inputBox = new TextBox[1];
                for (int i = 0; i < inputBox.Length; i++)
                {
                    inputBox[i] = new TextBox();
                    inputBox[i].Size = new System.Drawing.Size(100, 40);
                    inputBox[i].Location = new System.Drawing.Point(i * 100 + 100, 300);
                    inputPopup.Controls.Add(inputBox[i]);
                }
                inputBox[0].Text = GoCalibrationOffset.TrackVelo.ToString();

                Button submitButton = new Button();
                submitButton.Text = "次";
                submitButton.Size = new System.Drawing.Size(80, 30);
                submitButton.Location = new System.Drawing.Point(10, 40);
                submitButton.Click += (sender3, e3) => {

                    GoCalibrationOffset.TrackVelo = double.Parse(inputBox[0].Text);
                    inputPopup.Close();
                };

                inputPopup.Controls.Add(submitButton);
                inputPopup.ShowDialog();

            };
            FlickButton.Click += (sender2, e2) => {
                multiButtonPopup.Close();

                GoCalibrationOffset.Calioffsetflag = 2;
                Form inputPopup = new Form();
                inputPopup.Size = new System.Drawing.Size(600, 600);
                TextBox[] inputBox = new TextBox[1];
                for (int i = 0; i < inputBox.Length; i++)
                {
                    inputBox[i] = new TextBox();
                    inputBox[i].Size = new System.Drawing.Size(100, 40);
                    inputBox[i].Location = new System.Drawing.Point(i * 100 + 100, 300);
                    inputPopup.Controls.Add(inputBox[i]);
                }
                inputBox[0].Text = GoCalibrationOffset.FlickOffset.ToString();

                Button submitButton = new Button();
                submitButton.Text = "送信";
                submitButton.Size = new System.Drawing.Size(80, 30);
                submitButton.Location = new System.Drawing.Point(10, 40);
                submitButton.Click += (sender3, e3) => {

                    GoCalibrationOffset.FlickOffset = double.Parse(inputBox[0].Text);
                    inputPopup.Close();
                };

                inputPopup.Controls.Add(submitButton);
                inputPopup.ShowDialog();
            };
            multiButtonPopup.Controls.Add(TrrackButton);
            multiButtonPopup.Controls.Add(FlickButton);
            multiButtonPopup.ShowDialog();



            DialogResult result = MessageBox.Show(
                "保存するフォルダを選んでください", "かくにん",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                using (var dlg = new CommonOpenFileDialog())
                {
                    // フォルダ選択ダイアログ（falseにするとファイル選択ダイアログ）
                    dlg.IsFolderPicker = true;

                    if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        //開く以外を選択された場合はfalseを返す。
                        CalibrationClass.Cali_offset_path = dlg.FileName;
                    }
                }

                Form1.GoCalibrationOffsetFlag = true;
                form.goCalibrationOffset = new GoCalibrationOffset();
                form.goCalibrationOffset.InitializeCalibrationCoodinate(form);
                form.goCalibrationOffset.StartCalibrationCoodinate();

            }
            else if (result == DialogResult.No)
            {
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.WindowState = FormWindowState.Normal;
                form.TopMost = false;
                //form.TransparencyKey = Color.Empty;
                form.BackColor = a;
                Form1.userControl1.Anchor = AnchorStyles.Top;
                Form1.userControl1 = new UserControl1(form);
                form.Controls.Add(Form1.userControl1);
            }

        }


    }
}
