using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTrackingAim1.Scripts.Calibration;
using System.IO;

namespace EyeTrackingAim1.Scripts.Fine_tunig
{
    public class set_Auto_encoder_data
    {
        int f8_state = 0;
        int count = 0;

        public CalibrationClass.CalibrationDataHozon cali_Auto_encoder = new CalibrationClass.CalibrationDataHozon(100000);
        string path;

        
        public void set_data(bool f8_flag, System.Windows.Vector eye_pos, long timestumps)
        {
            if (f8_state == 1)
            {
                cali_Auto_encoder.CalibrationEyeArray[count] = eye_pos;
                cali_Auto_encoder.timestumps[count] = timestumps;
                
                count += 1;
                Console.WriteLine("count" + count);
            }

            if (f8_state == 0 && f8_flag == true)
            {
                f8_state = 1;
            }
            else if (f8_state == 1 && count >= cali_Auto_encoder.CalibrationEyeArray.Length)
            {
                string dir = "EyeData/Calibration_X/Auto_Encoder";
                string file = "/Data";

                int file_n = Directory.GetFiles(dir, "*", SearchOption.AllDirectories).Length;
                path = dir + file + file_n.ToString() + ".bin";


                cali_Auto_encoder.time_list = new int[1];
                cali_Auto_encoder.time_list[0] = cali_Auto_encoder.CalibrationEyeArray.Length;
                cali_Auto_encoder.bunnkatu = 1;
                RecordCalibration.RecoldCaliData(cali_Auto_encoder, path);
                f8_state = 0;
                count = 0;
            }


        }

    }

}
