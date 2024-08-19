using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EyeTrackingAim1.Scripts.Prediction;
using EyeTrackingAim1.Scripts.EyeTrackingAim;
using EyeTrackingAim1.Scripts.RawInput;
using EyeTrackingAim1.Scripts.EyeData;
using EyeTrackingAim1.Scripts.UDP;
using Tobii.StreamEngine;
using System.Numerics;
using System.Threading;
using System.IO;
using System.Media;


namespace EyeTrackingAim1.Scripts.Calibration
{
    public class GoCalibrationOffset
    {
        public Form1 form;
        public static int Calioffsetflag = 0;
        float height = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        float width = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

        string dir;
        string file;
        int file_n;
        string path;


        Tobii.InteractionLib.IInteractionLib intlib;
        public System.Windows.Vector Eyepoint = new System.Windows.Vector();
        public System.Windows.Vector Raw_Eyepoint = new System.Windows.Vector();
        
        public Vector3 HeadPose = new Vector3();
        public Vector3 HeadRota = new Vector3();   
        public Vector3 eye_origin_left_point = new Vector3();
        public Vector3 eye_origin_right_point = new Vector3();
        public long timestumps = new long();
        public double dt = new long();
        public List<int> time_list;
        public static bool cali_finish_flag = false;
        static System.Windows.Vector cali_co_0_hozon = new System.Windows.Vector();

        public static double TrackVelo = 0.0;
        public static double FlickOffset = 0.0;

        DoCalibration doCalibration = new DoCalibration();
        public Thread Cali_Co_thread;

        public static int[] calistate = new int[4];
        public static System.Diagnostics.Stopwatch sw_move = new System.Diagnostics.Stopwatch();
        static List<int> kirikae_list;
        int kirikae_count = 0;
        
        double randomtime = 0.0;


        public void InitializeCalibrationCoodinate(Form1 form1)
        {
            form = form1;

            intlib = Tobii.InteractionLib.InteractionLibFactory.CreateInteractionLib(Tobii.InteractionLib.FieldOfUse.Interactive);
            intlib.CoordinateTransformAddOrUpdateDisplayArea(width, height);
            intlib.CoordinateTransformSetOriginOffset(0, 0);

            time_list = new List<int>();

            intlib.GazeOriginDataEvent += evt0 =>
            {
                eye_origin_left_point.X = evt0.left_x;
                eye_origin_left_point.Y = evt0.left_y;
                eye_origin_left_point.Z = evt0.left_z;

                eye_origin_right_point.X = evt0.right_x;
                eye_origin_right_point.Y = evt0.right_y;
                eye_origin_right_point.Z = evt0.right_z;
          
            };

            intlib.GazePointDataEvent += evt =>
            {
                if (evt.validity == Tobii.InteractionLib.Validity.Valid)
                {
                    if (calistate[3] == 0 && RawInputKey.RawInputJudge(KeyData.F5))
                    {
                        calistate[3] = 1;
                    }      
                    else if (calistate[3] == 1 && RawInputKey.RawInputJudge(KeyData.F5) == false)
                    {
                        //目の原点合わせ
                        System.Windows.Vector vector = new System.Windows.Vector();
                        vector.X = Raw_Eyepoint.X + CalibrationClass.cali_Manual_offset.manual_offset.X - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0);
                        vector.Y = Raw_Eyepoint.Y + CalibrationClass.cali_Manual_offset.manual_offset.Y - (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0);
                        CalibrationClass.cali_Manual_offset.manual_offset = vector;

                        calistate[3] = 0;
                    }

                    double x = 0.0;
                    double y = 0.0;

                    //Ready
                    System.Windows.Vector offset = new System.Windows.Vector();

                    offset.X = 0
                    + CalibrationClass.cali_Manual_offset.manual_offset.X;
                    ;
                    offset.Y = 0
                    + CalibrationClass.cali_Manual_offset.manual_offset.Y
                    ;
                    Raw_Eyepoint = new System.Windows.Vector(evt.x - offset.X, evt.y - offset.Y);

                    x = Raw_Eyepoint.X;
                    y = Raw_Eyepoint.Y;

                    Eyepoint = new System.Windows.Vector(x, y);
                }

            };

            intlib.HeadPoseDataEvent += evt1 =>
            {

                HeadPose.X = evt1.position_x;
                HeadPose.Y = evt1.position_y;
                HeadPose.Z = evt1.position_z;
                HeadRota.X = evt1.rotation_x;
                HeadRota.Y = evt1.rotation_y;
                HeadRota.Z = evt1.rotation_z;
                dt = (evt1.timestamp_us - timestumps) / 1000000.0;
                timestumps = evt1.timestamp_us;

                if (calistate[1] == 0 && RawInputKey.RawInputJudge(KeyData.N2))
                {
                    sw_move = new System.Diagnostics.Stopwatch();
                    sw_move.Start();
                    calistate[1] = 1;
                    kirikae_count = 0;

                }
                else if (calistate[1] == 1 && RawInputKey.RawInputJudge(KeyData.N2) == false)
                {
                    calistate[1] = 0;
                    calistate[2] = 0;
                    sw_move.Reset();

                }


                if (calistate[2] == 0 && (sw_move.ElapsedMilliseconds / 1000.0) >= 3.0)
                {
                    calistate[2] = 1;
                }
                

                if (Calioffsetflag == 1)
                {
                    
                    if (calistate[2] == 0)
                    {
                        cali_co_0_hozon.X = 960.0;
                        kirikae_count += 1;
                    }
                    else if (calistate[2] == 1)
                    {
                        cali_co_0_hozon.X = cali_co_0_hozon.X + 10;
                        kirikae_count += 1;
                    }
                }
                else if (Calioffsetflag == 2)
                {
                    if (calistate[2] == 0)
                    {
                        cali_co_0_hozon.X = 960.0;
                        kirikae_count += 1;
                    }
                    else if (calistate[2] == 1)
                    {
                        cali_co_0_hozon.X = 960.0 + FlickOffset;
                        kirikae_count += 1;
                    }
                }

                CalibrationClass.calibrationOffsetDatas[0].CalibrationTarget.X = cali_co_0_hozon.X;

                cali_finish_flag = doCalibration.InTimerCalibration(

                    path,
                    CalibrationClass.calibrationOffsetDatas,
                    Eyepoint, HeadPose, HeadRota, eye_origin_left_point, eye_origin_right_point, 
                    Form1.Mouse_move,
                    UDPReceive.strageEyeData,
                    timestumps, dt,
                    time_list,
                    kirikae_list,
                    kirikae_count,
                    form);


                form.Invoke((MethodInvoker)delegate
                {
                    form.Invalidate();
                }
                );

                if (cali_finish_flag == true)
                {
                    intlib.Dispose();
                    //save
                    RecordCalibration.Recold_cali_manual(CalibrationClass.cali_Manual_offset, "CalibrationData/cali_Manual_offset.bin");

                    form.Invoke((MethodInvoker)delegate
                    {
                        form.FormBorderStyle = FormBorderStyle.Sizable;
                        form.WindowState = FormWindowState.Normal;
                        form.TopMost = false;
                        //form.TransparencyKey = Color.Empty;
                        Form1.userControl1.Anchor = AnchorStyles.Top;
                        Form1.userControl1 = new UserControl1(form);
                        form.Controls.Add(Form1.userControl1);
                    }
                    );
                    Form1.GoCalibrationOffsetFlag = false;
                    Cali_Co_thread.Abort();
                }
            };
        }

        public void StartCalibrationCoodinate()
        {
            dir = CalibrationClass.Cali_Head_path;
            file = "/Data";
            file_n = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Length;
            path = dir + file + file_n.ToString();

            kirikae_list = new List<int>();
            kirikae_count = 0;

            double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;
            double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;

            CalibrationClass.calibrationOffsetDatas[0] = new CalibrationClass.CalibrationData(CalibrationClass.usenumberOffset);


            for (int i = 0; i < CalibrationClass.calibrationOffsetDatas.Length; i++)
            {
                CalibrationClass.calibrationOffsetDatas[i] = new CalibrationClass.CalibrationData(CalibrationClass.usenumberOffset);
            }

            CalibrationClass.calibrationOffsetDatas[0].CalibrationTarget = new System.Windows.Vector(960.0, 540.0);
            cali_co_0_hozon = CalibrationClass.calibrationOffsetDatas[0].CalibrationTarget;

            Cali_Co_thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    intlib.WaitAndUpdate();       
                }

            }));

            Cali_Co_thread.Start();
        }

    }
}
