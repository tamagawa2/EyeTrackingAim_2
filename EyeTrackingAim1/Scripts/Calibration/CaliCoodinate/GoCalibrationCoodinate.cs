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
    public class GoCalibrationCoodinate
    {
        public Form1 form;
        float height = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        float width = (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

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



                    //cali_x_head
                    double[] py_cali_x_head = new double[1];
                    py_cali_x_head[0] = HeadRota.Z;
                    EstimateClass.py_cali_x_head = ActiveEyeKannsuu.mlp_cali_x_head(py_cali_x_head, EstimateClass.py_cali_x_head);

                    //x = x - EstimateClass.py_cali_x_head.reg[0][0];
                    //x = x - CalibrationClass.cali_Manual_offset.manual_offset.X;

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

                

                //Console.WriteLine("HeadRota_X {0} HeadRota_Y {1} HeadRota_Z {2}", HeadRota.X * (180 / Math.PI), HeadRota.Y * (180 / Math.PI), HeadRota.Z * (180 / Math.PI));
                //Console.WriteLine("HeadPose_X {0} HeadPose_Y {1} HeadPose_Z {2}", HeadPose.X, HeadPose.Y, HeadPose.Z);




                string dir = "EyeData/Calibration_X/Cali_Track";
                string file = "/Data";
                int file_n = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Length;
                string path = dir + file + file_n.ToString();

                cali_finish_flag = doCalibration.InTimerCalibration(

                    path,
                    CalibrationClass.calibrationCoodinateDatas,
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
                    RecordCalibration.Recold_cali_manual(CalibrationClass.cali_Manual_offset, "KeisuuData/cali_Manual_offset.bin");

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
                    Form1.GoCaribrationCoodinateFlag = false;
                    Cali_Co_thread.Abort();
                }
            };
        }

        public void StartCalibrationCoodinate()
        {
            kirikae_list = new List<int>();
            kirikae_count = 0;

            double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;
            double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;

            
            for (int i = 0; i < CalibrationClass.calibrationCoodinateDatas.Length; i++)
            {
                CalibrationClass.calibrationCoodinateDatas[i] = new CalibrationClass.CalibrationData(CalibrationClass.usenumberCoodinate);
            }
            CalibrationClass.calibrationCoodinateDatas[0].focus = true;
            int xbnnkatu = CalibrationClass.xbnnkatu;
            int ybnnkatu = CalibrationClass.ybnnkatu;

            double waruwe = CalibrationClass.yoko_l / (xbnnkatu + 1);
            double waruhe = CalibrationClass.tate_l / (ybnnkatu + 1);

            randomtime = 4.0;

            for (int i = 0; i < xbnnkatu; i++)
            {
                for (int j = 0; j < ybnnkatu; j++)
                {
                    CalibrationClass.calibrationCoodinateDatas[i * ybnnkatu + j].CalibrationTarget.X = waruwe * (i + 1) + ((width / 2.0) - (CalibrationClass.yoko_l / 2.0));
                    CalibrationClass.calibrationCoodinateDatas[i * ybnnkatu + j].CalibrationTarget.Y = waruhe * (j + 1) + ((height / 2.0) - (CalibrationClass.tate_l / 2.0));
                }
            }

            cali_co_0_hozon = CalibrationClass.calibrationCoodinateDatas[0].CalibrationTarget;

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
