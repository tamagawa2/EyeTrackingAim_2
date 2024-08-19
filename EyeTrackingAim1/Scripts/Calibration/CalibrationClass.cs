using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;

using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.Calibration
{
    public class CalibrationClass
    {
        //変数一時格納

        
        public class CalibrationData
        {
            
            
            public System.Windows.Vector CalibrationTarget;
            public System.Windows.Vector[] CalibrationPointArray = new System.Windows.Vector[1];
            public System.Windows.Vector[] CalibrationEyeArray = new System.Windows.Vector[1];
            public Vector3[] CalibrationHeadPoseArray = new Vector3[1];
            public Vector3[] CalibrationHeadRotaArray = new Vector3[1];
            public Vector3[] CalibrationHeadPoseVeloArray = new Vector3[1];
            public Vector3[] CalibrationHeadRotaVeloArray = new Vector3[1];
            public Vector3[] Calibration_eye_origin_left_point_Array = new Vector3[1];
            public Vector3[] Calibration_eye_origin_right_point_Array = new Vector3[1];
            public Vector3[] Calibration_iPhone_eye_origin_left_point_Array = new Vector3[1];
            public Vector3[] Calibration_iPhone_eye_origin_right_point_Array = new Vector3[1];
            public Vector3[] Calibration_iPhone_eye_origin_left_rota_Array = new Vector3[1];
            public Vector3[] Calibration_iPhone_eye_origin_right_rota_Array = new Vector3[1];
            public Vector3[] Calibration_iPhone_acceleration_Array = new Vector3[1];
            public Vector3[] Calibration_iPhone_rota_Array = new Vector3[1];
            public long[] timestumps = new long[1];
            public double[] dt = new double[1];
            public double[] Mouse_X = new double[1];
            public double[] Mouse_Y = new double[1];
            public CalibrationData(int number)
            {
                CalibrationPointArray = new System.Windows.Vector[number];
                CalibrationEyeArray = new System.Windows.Vector[number];
                CalibrationHeadPoseArray = new Vector3[number];
                CalibrationHeadRotaArray = new Vector3[number];  
                Calibration_eye_origin_left_point_Array = new Vector3[number];
                Calibration_eye_origin_right_point_Array = new Vector3[number];
                Calibration_iPhone_eye_origin_left_point_Array = new Vector3[number];
                Calibration_iPhone_eye_origin_right_point_Array = new Vector3[number];
                Calibration_iPhone_eye_origin_left_rota_Array = new Vector3[number];
                Calibration_iPhone_eye_origin_right_rota_Array = new Vector3[number];
                Calibration_iPhone_acceleration_Array = new Vector3[number];
                Calibration_iPhone_rota_Array = new Vector3[number];
                timestumps = new long[number];
                dt = new double[number];
                Mouse_X = new double[number];
                Mouse_Y = new double[number];
            }
            
            public bool CalibrationCompleate = false;
            public int CalibrationCount = 0;
            public bool nowuseflag = false;
            public bool focus = false;
        }

        //頭
        public static int usenumberHead = 3000;
        public static CalibrationData[] calibrationHeadDatas = new CalibrationData[1];

        //座標
        public static int usenumberCoodinate = 3;
        
        //CoodindateKazu
        public static int xbnnkatu = 1;
        public static int ybnnkatu = 1;
        public static CalibrationData[] calibrationCoodinateDatas = new CalibrationData[xbnnkatu * ybnnkatu];
        public static double yoko_l = 1920; 
        public static double tate_l = 1080; 


        //色
        public static int usenumberColor = 255;
        public class CalibrationColorData
        {

            public System.Windows.Vector CalibrationTarget;
            public System.Windows.Vector[] CalibrationPointArray = new System.Windows.Vector[usenumberColor];
            public System.Windows.Vector[] CalibrationEyeArray = new System.Windows.Vector[usenumberColor];
            public double[] avgR = new double[usenumberColor];
            public double[] avgG = new double[usenumberColor];
            public double[] avgB = new double[usenumberColor];
            public double[] camera_avgR = new double[usenumberColor];
            public double[] camera_avgG = new double[usenumberColor];
            public double[] camera_avgB = new double[usenumberColor];

            public bool CalibrationCompleate = false;
            public int CalibrationCount = 0;
            public bool nowuseflag = false;
        }

        public static CalibrationColorData calibrationColorData = new CalibrationColorData();

        //offset
        public static CalibrationData[] calibrationOffsetDatas = new CalibrationData[1];
        public static int usenumberOffset = 1000;

        //保存するデータ
        //頭と座標
        public class CalibrationDataHozon
        {
            [JsonPropertyName("CalibrationTarget")] public System.Windows.Vector[] CalibrationTarget { get; set; } = new System.Windows.Vector[1];
            [JsonPropertyName("CalibrationPointArray")] public System.Windows.Vector[] CalibrationPointArray { get; set; } = new System.Windows.Vector[1];
            [JsonPropertyName("CalibrationEyeArray")] public System.Windows.Vector[] CalibrationEyeArray { get; set; } = new System.Windows.Vector[1];
            [JsonPropertyName("CalibrationHeadPoseArrayX")] public double[] CalibrationHeadPoseArrayX { get; set; } = new double[1];
            [JsonPropertyName("CalibrationHeadPoseArrayY")] public double[] CalibrationHeadPoseArrayY { get; set; } = new double[1];
            [JsonPropertyName("CalibrationHeadPoseArrayZ")] public double[] CalibrationHeadPoseArrayZ { get; set; } = new double[1];
            [JsonPropertyName("CalibrationHeadRotaArrayX")] public double[] CalibrationHeadRotaArrayX { get; set; } = new double[1];
            [JsonPropertyName("CalibrationHeadRotaArrayY")] public double[] CalibrationHeadRotaArrayY { get; set; } = new double[1];
            [JsonPropertyName("CalibrationHeadRotaArrayZ")] public double[] CalibrationHeadRotaArrayZ { get; set; } = new double[1];
            [JsonPropertyName("Calibration_eye_origin_left_point_X")] public double[] Calibration_eye_origin_left_point_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_eye_origin_left_point_Y")] public double[] Calibration_eye_origin_left_point_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_eye_origin_left_point_Z")] public double[] Calibration_eye_origin_left_point_Z { get; set; } = new double[1];
            [JsonPropertyName("Calibration_eye_origin_right_point_X")] public double[] Calibration_eye_origin_right_point_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_eye_origin_right_point_Y")] public double[] Calibration_eye_origin_right_point_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_eye_origin_right_point_Z")] public double[] Calibration_eye_origin_right_point_Z { get; set; } = new double[1];
            [JsonPropertyName("bunnkatu")] public int bunnkatu { get; set; } = 0;
            [JsonPropertyName("timestumps")] public long[] timestumps { get; set; } = new long[1];
            [JsonPropertyName("time_list")] public int[] time_list { get; set; } = new int[1];
            [JsonPropertyName("dt")] public double[] dt { get; set; } = new double[1];
            [JsonPropertyName("Mouse_X")] public double[] Mouse_X { get; set; } = new double[1];
            [JsonPropertyName("Mouse_Y")] public double[] Mouse_Y { get; set; } = new double[1];

            [JsonPropertyName("kirikae_list")] public int[] kirikae_list { get; set; } = new int[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_left_point_X")] public double[] Calibration_iPhone_eye_origin_left_point_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_left_point_Y")] public double[] Calibration_iPhone_eye_origin_left_point_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_left_point_Z")] public double[] Calibration_iPhone_eye_origin_left_point_Z { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_right_point_X")] public double[] Calibration_iPhone_eye_origin_right_point_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_right_point_Y")] public double[] Calibration_iPhone_eye_origin_right_point_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_right_point_Z")] public double[] Calibration_iPhone_eye_origin_right_point_Z { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_left_rota_X")] public double[] Calibration_iPhone_eye_origin_left_rota_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_left_rota_Y")] public double[] Calibration_iPhone_eye_origin_left_rota_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_left_rota_Z")] public double[] Calibration_iPhone_eye_origin_left_rota_Z { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_right_rota_X")] public double[] Calibration_iPhone_eye_origin_right_rota_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_right_rota_Y")] public double[] Calibration_iPhone_eye_origin_right_rota_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_eye_origin_right_rota_Z")] public double[] Calibration_iPhone_eye_origin_right_rota_Z { get; set; } = new double[1];

            [JsonPropertyName("Calibration_iPhone_acc_X")] public double[] Calibration_iPhone_acc_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_acc_Y")] public double[] Calibration_iPhone_acc_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_acc_Z")] public double[] Calibration_iPhone_acc_Z { get; set; } = new double[1];

            [JsonPropertyName("Calibration_iPhone_rota_X")] public double[] Calibration_iPhone_rota_X { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_rota_Y")] public double[] Calibration_iPhone_rota_Y { get; set; } = new double[1];
            [JsonPropertyName("Calibration_iPhone_rota_Z")] public double[] Calibration_iPhone_rota_Z { get; set; } = new double[1];

            public CalibrationDataHozon(int number)
            {
                CalibrationTarget = new System.Windows.Vector[number];
                CalibrationPointArray = new System.Windows.Vector[number];
                CalibrationEyeArray = new System.Windows.Vector[number];
                CalibrationHeadPoseArrayX = new double[number];
                CalibrationHeadPoseArrayY = new double[number];
                CalibrationHeadPoseArrayZ = new double[number];
                CalibrationHeadRotaArrayX = new double[number];
                CalibrationHeadRotaArrayY = new double[number];
                CalibrationHeadRotaArrayZ = new double[number];  
                Calibration_eye_origin_left_point_X = new double[number];
                Calibration_eye_origin_left_point_Y = new double[number];
                Calibration_eye_origin_left_point_Z = new double[number];
                Calibration_eye_origin_right_point_X = new double[number];
                Calibration_eye_origin_right_point_Y = new double[number];
                Calibration_eye_origin_right_point_Z = new double[number];
                timestumps = new long[number];
                dt = new double[number];
                Mouse_X = new double[number];
                Mouse_Y = new double[number];
                bunnkatu = usenumberCoodinate;
                Calibration_iPhone_eye_origin_left_point_X = new double[number];
                Calibration_iPhone_eye_origin_left_point_Y = new double[number];
                Calibration_iPhone_eye_origin_left_point_Z = new double[number];
                Calibration_iPhone_eye_origin_right_point_X = new double[number];
                Calibration_iPhone_eye_origin_right_point_Y = new double[number];
                Calibration_iPhone_eye_origin_right_point_Z = new double[number];
                Calibration_iPhone_eye_origin_left_rota_X = new double[number];
                Calibration_iPhone_eye_origin_left_rota_Y = new double[number];
                Calibration_iPhone_eye_origin_left_rota_Z = new double[number];
                Calibration_iPhone_eye_origin_right_rota_X = new double[number];
                Calibration_iPhone_eye_origin_right_rota_Y = new double[number];
                Calibration_iPhone_eye_origin_right_rota_Z = new double[number];
                Calibration_iPhone_acc_X = new double[number];
                Calibration_iPhone_acc_Y = new double[number];
                Calibration_iPhone_acc_Z = new double[number];
                Calibration_iPhone_rota_X = new double[number];
                Calibration_iPhone_rota_Y = new double[number];
                Calibration_iPhone_rota_Z = new double[number];
            }

        }

        public class Calibration_Data_json
        {
            [JsonPropertyName("ave_head_pos")] public double[] ave_head_pos { get; set; } = new double[0];
        }

        public class Calibration_Data
        {
            public Vector3 ave_head_pos;
            public float length_from_ave;
        }

        public static Calibration_Data calibration_Data = new Calibration_Data();
        //色

        public class CalibrationColorDataHozon
        {
            [JsonPropertyName("CalibrationTarget")] public System.Windows.Vector[] CalibrationTarget { get; set; } = new System.Windows.Vector[usenumberColor];
            [JsonPropertyName("CalibrationPointArray")] public System.Windows.Vector[] CalibrationPointArray { get; set; } = new System.Windows.Vector[usenumberColor];
            [JsonPropertyName("CalibrationEyeArray")] public System.Windows.Vector[] CalibrationEyeArray { get; set; } = new System.Windows.Vector[usenumberColor];          
            [JsonPropertyName("avgR")] public double[] avgR { get; set; } = new double[usenumberColor];
            [JsonPropertyName("avgG")] public double[] avgG { get; set; } = new double[usenumberColor];
            [JsonPropertyName("avgB")] public double[] avgB { get; set; } = new double[usenumberColor];
            [JsonPropertyName("camera_avgR")] public double[] camera_avgR { get; set; } = new double[usenumberColor];
            [JsonPropertyName("camera_avgG")] public double[] camera_avgG { get; set; } = new double[usenumberColor];
            [JsonPropertyName("camera_avgB")] public double[] camera_avgB { get; set; } = new double[usenumberColor];

        }

        public static CalibrationColorDataHozon calibrationColorDataHozon = new CalibrationColorDataHozon();


        public class Cali_manual_offset
        {
            [JsonPropertyName("manual_offset")] public System.Windows.Vector manual_offset { get; set; } = new System.Windows.Vector();

            [JsonPropertyName("manual_offset_modify")] public System.Windows.Vector manual_offset_modify { get; set; } = new System.Windows.Vector();

        }

        public static Cali_manual_offset cali_Manual_offset = new Cali_manual_offset();
        public static CalibrationData cali_tuning = new CalibrationData(100);
        public static int cali_tuning_n = 0;
        public static string Cali_Head_path = "";
        public static string Cali_Coodinate_path = "";
        public static string Cali_Color_path = "";
        public static string Cali_offset_path = "";
    }
}
