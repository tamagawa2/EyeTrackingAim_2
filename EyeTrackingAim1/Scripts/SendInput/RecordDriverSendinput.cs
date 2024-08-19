using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.SendInput
{
    public class RecordDriverSendinput
    {

        public class DriverSendInputState
        {
            [JsonPropertyName("driverSendInputState")] public int driverSendInputState { get; set; } = 1;

        }
        public static DriverSendInputState Driver_SendInput_State = new DriverSendInputState();

        public static void RecoldDriverSendInputSatte(DriverSendInputState record, string filename)
        {
            string jsonstr = JsonSerializer.Serialize(record);


            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            byte[] write = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(jsonstr);
            fileStream.Write(write, 0, write.Length);
            fileStream.Close();
        }


        public static DriverSendInputState ReciveDriverSendInputSatte(string filename)
        {

            FileStream filestream = new FileStream(filename, FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();


            return JsonSerializer.Deserialize<DriverSendInputState>(jsonstr);
        }
    }
}
