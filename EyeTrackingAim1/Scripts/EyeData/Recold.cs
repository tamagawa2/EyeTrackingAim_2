using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.EyeData
{
   public class Recold
    {
        public static void RecoldEyeData(List<EyeData> eye)
        {
            string jsonstr = JsonSerializer.Serialize(eye);


            FileStream fileStream = new FileStream("saveeye.bin", FileMode.Create, FileAccess.Write);
            byte[] write = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(jsonstr);
            fileStream.Write(write, 0, write.Length);
            fileStream.Close();
        }


        public static List<EyeData> ReciveEye()
        {

            FileStream filestream = new FileStream("saveeye.bin", FileMode.Open);
            byte[] read = new byte[filestream.Length];
            filestream.Read(read, 0, read.Length);
            string jsonstr = System.Text.Encoding.GetEncoding("Shift_JIS").GetString(read);
            filestream.Close();

            return JsonSerializer.Deserialize<List<EyeData>>(jsonstr);
        }
    }
}
