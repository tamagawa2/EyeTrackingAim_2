using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MathNet.Numerics.LinearAlgebra.Double;
using EyeTrackingAim1.Scripts.RawInput;
using EyeTrackingAim1.Scripts.EyeData;
using EyeTrackingAim1.Scripts.UDP;
using System.Drawing;
using System.Numerics;

namespace EyeTrackingAim1.Scripts.Calibration
{
    public class DoCalibration
    {
        int CalibrationCount = 0;
        bool Cali_finish_flag = false;
        int[] CaliCo_flag = new int[1];
        int time_count = 0;
        public bool InTimerCalibration( 
            string name,
            CalibrationClass.CalibrationData[] calibrationData,
            System.Windows.Vector EyePoint, Vector3 HeadPose, Vector3 HeadRota, Vector3 eye_origin_left_point, Vector3 eye_origin_right_point, 
            System.Windows.Vector Mouse_move,
            UDPReceive.StrageEyeData strageEyeData,
            long timestumps, double dt,
            List<int> time_list,
            List<int> kirikae_list,
            int kirikae_count,
            Form1 form)
        {
            double we = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2.0;
            double he = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2.0;

            for (int i = 0; i < calibrationData.Length; i++)
            {
                calibrationData[i].nowuseflag = false;
            }


            

            for (int i = 0; i < calibrationData.Length; i++)
            {
                System.Windows.Vector vector = new System.Windows.Vector(EyePoint.X - calibrationData[i].CalibrationTarget.X, EyePoint.Y - calibrationData[i].CalibrationTarget.Y);

                //代入
                if(i == 0)
                {
                    if (calibrationData[i].CalibrationCompleate == false)
                    {
                        if (vector.Length < 30000 && RawInputKey.RawInputJudge((KeyData)((int)KeyData.N2 - (i % 2))))
                        {


                            if (calibrationData[i].CalibrationCount < calibrationData[i].CalibrationPointArray.Length)
                            {
                                if (CaliCo_flag[0] == 0)
                                {
                                    CaliCo_flag[0] = 1;

                                    time_count = 0;
                                }

                                time_count = time_count + 1;

                                calibrationData[i].nowuseflag = true;
                                calibrationData[i].CalibrationEyeArray[calibrationData[i].CalibrationCount].X = EyePoint.X;
                                calibrationData[i].CalibrationEyeArray[calibrationData[i].CalibrationCount].Y = EyePoint.Y;

                                calibrationData[i].CalibrationHeadPoseArray[calibrationData[i].CalibrationCount].X = HeadPose.X;
                                calibrationData[i].CalibrationHeadPoseArray[calibrationData[i].CalibrationCount].Y = HeadPose.Y;
                                calibrationData[i].CalibrationHeadPoseArray[calibrationData[i].CalibrationCount].Z = HeadPose.Z;
                                calibrationData[i].CalibrationHeadRotaArray[calibrationData[i].CalibrationCount].X = HeadRota.X;
                                calibrationData[i].CalibrationHeadRotaArray[calibrationData[i].CalibrationCount].Y = HeadRota.Y;
                                calibrationData[i].CalibrationHeadRotaArray[calibrationData[i].CalibrationCount].Z = HeadRota.Z;


                                calibrationData[i].Calibration_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].X = eye_origin_left_point.X;
                                calibrationData[i].Calibration_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Y = eye_origin_left_point.Y;
                                calibrationData[i].Calibration_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Z = eye_origin_left_point.Z;
                                calibrationData[i].Calibration_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].X = eye_origin_right_point.X;
                                calibrationData[i].Calibration_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Y = eye_origin_right_point.Y;
                                calibrationData[i].Calibration_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Z = eye_origin_right_point.Z;


                                calibrationData[i].Calibration_iPhone_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.L_Eye_Pos_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.L_Eye_Pos_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.L_Eye_Pos_Z;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.R_Eye_Pos_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.R_Eye_Pos_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.R_Eye_Pos_Z;

                                calibrationData[i].Calibration_iPhone_eye_origin_left_rota_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.L_Eye_Rota_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_rota_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.L_Eye_Rota_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_rota_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.L_Eye_Rota_Z;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_rota_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.R_Eye_Rota_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_rota_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.R_Eye_Rota_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_rota_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.R_Eye_Rota_Z;

                                calibrationData[i].Calibration_iPhone_acceleration_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.iPhone_acceleration_X;
                                calibrationData[i].Calibration_iPhone_acceleration_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.iPhone_acceleration_Y;
                                calibrationData[i].Calibration_iPhone_acceleration_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.iPhone_acceleration_Z;

                                calibrationData[i].Calibration_iPhone_rota_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.iPhone_rota_X;
                                calibrationData[i].Calibration_iPhone_rota_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.iPhone_rota_Y;
                                calibrationData[i].Calibration_iPhone_rota_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.iPhone_rota_Z;

                                calibrationData[i].Mouse_X[calibrationData[i].CalibrationCount] = Mouse_move.X;
                                calibrationData[i].Mouse_Y[calibrationData[i].CalibrationCount] = Mouse_move.Y;


                                calibrationData[i].CalibrationPointArray[calibrationData[i].CalibrationCount].X = EyePoint.X - calibrationData[i].CalibrationTarget.X;
                                calibrationData[i].CalibrationPointArray[calibrationData[i].CalibrationCount].Y = EyePoint.Y - calibrationData[i].CalibrationTarget.Y;

                                calibrationData[i].timestumps[calibrationData[i].CalibrationCount] = timestumps;
                                calibrationData[i].dt[calibrationData[i].CalibrationCount] = dt;

                                calibrationData[i].CalibrationCount = calibrationData[i].CalibrationCount + 1;


                            }
                            else
                            {
                                if (CaliCo_flag[0] == 1)
                                {
                                    kirikae_list.Add(kirikae_count);
                                    time_list.Add(time_count);
                                    CaliCo_flag[0] = 0;
                                }



                                calibrationData[i].CalibrationCompleate = true;
                                calibrationData[i].focus = false;
                                if (i < calibrationData.Length  - 1)
                                {
                                    calibrationData[i + 1].focus = true;
                                } 
                                
                                

                                CalibrationCount = CalibrationCount + 1;
                            }
                        }
                        else
                        {
                            if (CaliCo_flag[0] == 1)
                            {
                                kirikae_list.Add(kirikae_count);
                                time_list.Add(time_count);
                                CaliCo_flag[0] = 0;     
                            }


                        }
                    }
                } else
                {
                    
                    if (calibrationData[i].CalibrationCompleate == false && calibrationData[i - 1].CalibrationCompleate == true)
                    {
                        if (vector.Length < 30000 && RawInputKey.RawInputJudge((KeyData)((int)KeyData.N2 - (i % 2))) && RawInputKey.RawInputJudge((KeyData)((int)KeyData.N2 - ((i - 1) % 2))) == false)
                        {
                            

                           

                            if (calibrationData[i].CalibrationCount < calibrationData[i].CalibrationPointArray.Length)
                            {
                                if (CaliCo_flag[0] == 0)
                                {
                                    CaliCo_flag[0] = 1;

                                    time_count = 0;
                                }

                                time_count = time_count + 1;

                                calibrationData[i].nowuseflag = true;
                                calibrationData[i].CalibrationEyeArray[calibrationData[i].CalibrationCount].X = EyePoint.X;
                                calibrationData[i].CalibrationEyeArray[calibrationData[i].CalibrationCount].Y = EyePoint.Y;

                                calibrationData[i].CalibrationHeadPoseArray[calibrationData[i].CalibrationCount].X = HeadPose.X;
                                calibrationData[i].CalibrationHeadPoseArray[calibrationData[i].CalibrationCount].Y = HeadPose.Y;
                                calibrationData[i].CalibrationHeadPoseArray[calibrationData[i].CalibrationCount].Z = HeadPose.Z;
                                calibrationData[i].CalibrationHeadRotaArray[calibrationData[i].CalibrationCount].X = HeadRota.X;
                                calibrationData[i].CalibrationHeadRotaArray[calibrationData[i].CalibrationCount].Y = HeadRota.Y;
                                calibrationData[i].CalibrationHeadRotaArray[calibrationData[i].CalibrationCount].Z = HeadRota.Z;

                                
                                calibrationData[i].Calibration_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].X = eye_origin_left_point.X;
                                calibrationData[i].Calibration_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Y = eye_origin_left_point.Y;
                                calibrationData[i].Calibration_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Z = eye_origin_left_point.Z;     
                                calibrationData[i].Calibration_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].X = eye_origin_right_point.X;
                                calibrationData[i].Calibration_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Y = eye_origin_right_point.Y;
                                calibrationData[i].Calibration_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Z = eye_origin_right_point.Z;


                                calibrationData[i].Calibration_iPhone_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.L_Eye_Pos_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.L_Eye_Pos_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_point_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.L_Eye_Pos_Z;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.R_Eye_Pos_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.R_Eye_Pos_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_point_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.R_Eye_Pos_Z;

                                calibrationData[i].Calibration_iPhone_eye_origin_left_rota_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.L_Eye_Rota_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_rota_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.L_Eye_Rota_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_left_rota_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.L_Eye_Rota_Z;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_rota_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.R_Eye_Rota_X;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_rota_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.R_Eye_Rota_Y;
                                calibrationData[i].Calibration_iPhone_eye_origin_right_rota_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.R_Eye_Rota_Z;


                                calibrationData[i].Calibration_iPhone_acceleration_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.iPhone_acceleration_X;
                                calibrationData[i].Calibration_iPhone_acceleration_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.iPhone_acceleration_Y;
                                calibrationData[i].Calibration_iPhone_acceleration_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.iPhone_acceleration_Z;


                                calibrationData[i].Calibration_iPhone_rota_Array[calibrationData[i].CalibrationCount].X = (float)strageEyeData.iPhone_rota_X;
                                calibrationData[i].Calibration_iPhone_rota_Array[calibrationData[i].CalibrationCount].Y = (float)strageEyeData.iPhone_rota_Y;
                                calibrationData[i].Calibration_iPhone_rota_Array[calibrationData[i].CalibrationCount].Z = (float)strageEyeData.iPhone_rota_Z;

                                calibrationData[i].Mouse_X[calibrationData[i].CalibrationCount] = Mouse_move.X;
                                calibrationData[i].Mouse_Y[calibrationData[i].CalibrationCount] = Mouse_move.Y;


                                calibrationData[i].CalibrationPointArray[calibrationData[i].CalibrationCount].X = EyePoint.X - calibrationData[i].CalibrationTarget.X;
                                calibrationData[i].CalibrationPointArray[calibrationData[i].CalibrationCount].Y = EyePoint.Y - calibrationData[i].CalibrationTarget.Y;

                                calibrationData[i].timestumps[calibrationData[i].CalibrationCount] = timestumps;
                                calibrationData[i].dt[calibrationData[i].CalibrationCount] = dt;


                                calibrationData[i].CalibrationCount = calibrationData[i].CalibrationCount + 1;


                            }
                            else
                            {

                                if (CaliCo_flag[0] == 1)
                                {
                                    kirikae_list.Add(kirikae_count);
                                    time_list.Add(time_count);
                                    CaliCo_flag[0] = 0;
                                }

                                

                                calibrationData[i].CalibrationCompleate = true;


                                calibrationData[i].focus = false;
                                if (i < calibrationData.Length - 1)
                                {
                                    calibrationData[i + 1].focus = true;
                                }
                                CalibrationCount = CalibrationCount + 1;
                            }
                        }
                        else
                        {
                            if (CaliCo_flag[0] == 1)
                            {
                                kirikae_list.Add(kirikae_count);
                                time_list.Add(time_count);
                                CaliCo_flag[0] = 0;
                            }
                        }
                    }
                }

                    

            }



            if (CalibrationCount == calibrationData.Length)
            {
                
                CalibrationClass.CalibrationDataHozon cahozon = new CalibrationClass.CalibrationDataHozon(calibrationData[0].CalibrationEyeArray.Length * calibrationData.Length);

                int time_array_count = 0;
                for (int m = 0; m < time_list.Count; m++)
                {
                    time_array_count = time_array_count + time_list[m];
                }

                     
                cahozon.time_list = time_list.ToArray();
                cahozon.kirikae_list = kirikae_list.ToArray();
                Console.WriteLine(kirikae_list);
                int count = 0;
                for (int m = 0; m < calibrationData.Length; m++)
                {
                    for (int s = 0; s < calibrationData[0].CalibrationEyeArray.Length; s++)
                    {
                        cahozon.CalibrationTarget[count].X = calibrationData[m].CalibrationTarget.X;
                        cahozon.CalibrationTarget[count].Y = calibrationData[m].CalibrationTarget.Y;
                        cahozon.CalibrationPointArray[count].X = calibrationData[m].CalibrationPointArray[s].X;
                        cahozon.CalibrationPointArray[count].Y = calibrationData[m].CalibrationPointArray[s].Y;
                        cahozon.CalibrationEyeArray[count].X = calibrationData[m].CalibrationEyeArray[s].X;
                        cahozon.CalibrationEyeArray[count].Y = calibrationData[m].CalibrationEyeArray[s].Y;
                        cahozon.CalibrationHeadPoseArrayX[count] = calibrationData[m].CalibrationHeadPoseArray[s].X;
                        cahozon.CalibrationHeadPoseArrayY[count] = calibrationData[m].CalibrationHeadPoseArray[s].Y;
                        cahozon.CalibrationHeadPoseArrayZ[count] = calibrationData[m].CalibrationHeadPoseArray[s].Z;
                        cahozon.CalibrationHeadRotaArrayX[count] = calibrationData[m].CalibrationHeadRotaArray[s].X;
                        cahozon.CalibrationHeadRotaArrayY[count] = calibrationData[m].CalibrationHeadRotaArray[s].Y;
                        cahozon.CalibrationHeadRotaArrayZ[count] = calibrationData[m].CalibrationHeadRotaArray[s].Z;

                        cahozon.Calibration_eye_origin_left_point_X[count] = calibrationData[m].Calibration_eye_origin_left_point_Array[s].X;
                        cahozon.Calibration_eye_origin_left_point_Y[count] = calibrationData[m].Calibration_eye_origin_left_point_Array[s].Y;
                        cahozon.Calibration_eye_origin_left_point_Z[count] = calibrationData[m].Calibration_eye_origin_left_point_Array[s].Z;
                        cahozon.Calibration_eye_origin_right_point_X[count] = calibrationData[m].Calibration_eye_origin_right_point_Array[s].X;
                        cahozon.Calibration_eye_origin_right_point_Y[count] = calibrationData[m].Calibration_eye_origin_right_point_Array[s].Y;
                        cahozon.Calibration_eye_origin_right_point_Z[count] = calibrationData[m].Calibration_eye_origin_right_point_Array[s].Z;


                        cahozon.Calibration_iPhone_eye_origin_left_point_X[count] = calibrationData[m].Calibration_iPhone_eye_origin_left_point_Array[s].X;
                        cahozon.Calibration_iPhone_eye_origin_left_point_Y[count] = calibrationData[m].Calibration_iPhone_eye_origin_left_point_Array[s].Y;
                        cahozon.Calibration_iPhone_eye_origin_left_point_Z[count] = calibrationData[m].Calibration_iPhone_eye_origin_left_point_Array[s].Z;
                        cahozon.Calibration_iPhone_eye_origin_right_point_X[count] = calibrationData[m].Calibration_iPhone_eye_origin_right_point_Array[s].X;
                        cahozon.Calibration_iPhone_eye_origin_right_point_Y[count] = calibrationData[m].Calibration_iPhone_eye_origin_right_point_Array[s].Y;
                        cahozon.Calibration_iPhone_eye_origin_right_point_Z[count] = calibrationData[m].Calibration_iPhone_eye_origin_right_point_Array[s].Z;


                        cahozon.Calibration_iPhone_eye_origin_left_rota_X[count] = calibrationData[m].Calibration_iPhone_eye_origin_left_rota_Array[s].X;
                        cahozon.Calibration_iPhone_eye_origin_left_rota_Y[count] = calibrationData[m].Calibration_iPhone_eye_origin_left_rota_Array[s].Y;
                        cahozon.Calibration_iPhone_eye_origin_left_rota_Z[count] = calibrationData[m].Calibration_iPhone_eye_origin_left_rota_Array[s].Z;
                        cahozon.Calibration_iPhone_eye_origin_right_rota_X[count] = calibrationData[m].Calibration_iPhone_eye_origin_right_rota_Array[s].X;
                        cahozon.Calibration_iPhone_eye_origin_right_rota_Y[count] = calibrationData[m].Calibration_iPhone_eye_origin_right_rota_Array[s].Y;
                        cahozon.Calibration_iPhone_eye_origin_right_rota_Z[count] = calibrationData[m].Calibration_iPhone_eye_origin_right_rota_Array[s].Z;


                        cahozon.Calibration_iPhone_acc_X[count] = calibrationData[m].Calibration_iPhone_acceleration_Array[s].X;
                        cahozon.Calibration_iPhone_acc_Y[count] = calibrationData[m].Calibration_iPhone_acceleration_Array[s].Y;
                        cahozon.Calibration_iPhone_acc_Z[count] = calibrationData[m].Calibration_iPhone_acceleration_Array[s].Z;


                        cahozon.Calibration_iPhone_rota_X[count] = calibrationData[m].Calibration_iPhone_rota_Array[s].X;
                        cahozon.Calibration_iPhone_rota_Y[count] = calibrationData[m].Calibration_iPhone_rota_Array[s].Y;
                        cahozon.Calibration_iPhone_rota_Z[count] = calibrationData[m].Calibration_iPhone_rota_Array[s].Z;

                        cahozon.Mouse_X[count] = calibrationData[m].Mouse_X[s];
                        cahozon.Mouse_Y[count] = calibrationData[m].Mouse_Y[s];

                        cahozon.timestumps[count] = calibrationData[m].timestumps[s];
                        cahozon.dt[count] = calibrationData[m].dt[s];
                        

                        count = count + 1;
                    }
                }

                
                //保存データ
                name = name + ".bin";
                RecordCalibration.RecoldCaliData(cahozon, name);
                Cali_finish_flag = true;

            }

                

            

            return Cali_finish_flag;
        }
    }
}
