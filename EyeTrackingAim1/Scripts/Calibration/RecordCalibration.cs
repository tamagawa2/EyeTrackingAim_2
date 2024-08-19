using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;


namespace EyeTrackingAim1.Scripts.Calibration
{
    public class RecordCalibration
    {
        public static void RecoldCaliData(CalibrationClass.CalibrationDataHozon record, string name)
        {
            string jsonstr = JsonSerializer.Serialize(record);


            FileStream fileStream = new FileStream(name, FileMode.Create, FileAccess.Write);
            byte[] write = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(jsonstr);
            fileStream.Write(write, 0, write.Length);
            fileStream.Close();
        }

        public static CalibrationClass.CalibrationDataHozon ReciveCaliData(string name)
        {

            FileStream filestream = new FileStream(name, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            return JsonSerializer.Deserialize<CalibrationClass.CalibrationDataHozon>(jsonstr);
        }

        public static CalibrationClass.Calibration_Data Recive_Calibration_Data(string name)
        {

            FileStream filestream = new FileStream(name, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            CalibrationClass.Calibration_Data_json jsondata = JsonSerializer.Deserialize<CalibrationClass.Calibration_Data_json>(jsonstr);

            CalibrationClass.Calibration_Data re = new CalibrationClass.Calibration_Data();

            re.ave_head_pos.X = (float)jsondata.ave_head_pos[0];
            re.ave_head_pos.Y = (float)jsondata.ave_head_pos[1];
            re.ave_head_pos.Z = (float)jsondata.ave_head_pos[2];

            return re;
        }


        public static void RecoldCaliColorData(CalibrationClass.CalibrationColorDataHozon record, string name)
        {
            string jsonstr = JsonSerializer.Serialize(record);


            FileStream fileStream = new FileStream(name, FileMode.Create, FileAccess.Write);
            byte[] write = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(jsonstr);
            fileStream.Write(write, 0, write.Length);
            fileStream.Close();
        }

        public static CalibrationClass.CalibrationColorDataHozon ReciveCaliColorData(string name)
        {

            FileStream filestream = new FileStream(name, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            return JsonSerializer.Deserialize<CalibrationClass.CalibrationColorDataHozon>(jsonstr);
        }



        public static void Recold_cali_manual(CalibrationClass.Cali_manual_offset record, string name)
        {
            string jsonstr = JsonSerializer.Serialize(record);


            FileStream fileStream = new FileStream(name, FileMode.Create, FileAccess.Write);
            byte[] write = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(jsonstr);
            fileStream.Write(write, 0, write.Length);
            fileStream.Close();
        }

        public static CalibrationClass.Cali_manual_offset Recive_cali_manual(string name)
        {

            FileStream filestream = new FileStream(name, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            return JsonSerializer.Deserialize<CalibrationClass.Cali_manual_offset>(jsonstr);
        }




    }
}
