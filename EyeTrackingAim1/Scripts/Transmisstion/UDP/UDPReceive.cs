using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using EyeTrackingAim1.Scripts.Calibration;
using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.UDP
{
    public class UDPReceive
    {

        //[System.Serializable]
        public class StrageEyeData
        {
            [JsonPropertyName("L_Eye_Pos_X")] public double L_Eye_Pos_X { get; set; } = 0.0;
            [JsonPropertyName("L_Eye_Pos_Y")] public double L_Eye_Pos_Y { get; set; } = 0.0;
            [JsonPropertyName("L_Eye_Pos_Z")] public double L_Eye_Pos_Z { get; set; } = 0.0;
            [JsonPropertyName("R_Eye_Pos_X")] public double R_Eye_Pos_X { get; set; } = 0.0;
            [JsonPropertyName("R_Eye_Pos_Y")] public double R_Eye_Pos_Y { get; set; } = 0.0;
            [JsonPropertyName("R_Eye_Pos_Z")] public double R_Eye_Pos_Z { get; set; } = 0.0;

            [JsonPropertyName("L_Eye_Rota_X")] public double L_Eye_Rota_X { get; set; } = 0.0;
            [JsonPropertyName("L_Eye_Rota_Y")] public double L_Eye_Rota_Y { get; set; } = 0.0;
            [JsonPropertyName("L_Eye_Rota_Z")] public double L_Eye_Rota_Z { get; set; } = 0.0;
            [JsonPropertyName("R_Eye_Rota_X")] public double R_Eye_Rota_X { get; set; } = 0.0;
            [JsonPropertyName("R_Eye_Rota_Y")] public double R_Eye_Rota_Y { get; set; } = 0.0;
            [JsonPropertyName("R_Eye_Rota_Z")] public double R_Eye_Rota_Z { get; set; } = 0.0;

            [JsonPropertyName("Face_Pos_X")] public double Face_Pos_X { get; set; } = 0.0;
            [JsonPropertyName("Face_Pos_Y")] public double Face_Pos_Y { get; set; } = 0.0;
            [JsonPropertyName("Face_Pos_Z")] public double Face_Pos_Z { get; set; } = 0.0;

            [JsonPropertyName("Face_Rota_X")] public double Face_Rota_X { get; set; } = 0.0;
            [JsonPropertyName("Face_Rota_Y")] public double Face_Rota_Y { get; set; } = 0.0;
            [JsonPropertyName("Face_Rota_Z")] public double Face_Rota_Z { get; set; } = 0.0;

            [JsonPropertyName("Face_data")] public double[] Face_data { get; set; }
            [JsonPropertyName("Face_data_string")] public string[] Face_data_string { get; set; }


            [JsonPropertyName("iPhone_acceleration_X")] public double iPhone_acceleration_X { get; set; } = 0.0;
            [JsonPropertyName("iPhone_acceleration_Y")] public double iPhone_acceleration_Y { get; set; } = 0.0;
            [JsonPropertyName("iPhone_acceleration_Z")] public double iPhone_acceleration_Z { get; set; } = 0.0;

            [JsonPropertyName("iPhone_rota_X")] public double iPhone_rota_X { get; set; } = 0.0;
            [JsonPropertyName("iPhone_rota_Y")] public double iPhone_rota_Y { get; set; } = 0.0;
            [JsonPropertyName("iPhone_rota_Z")] public double iPhone_rota_Z { get; set; } = 0.0;
        }


        static int port = 6666;
        public static UdpClient udp;
        public static Thread thread;
        public static StrageEyeData strageEyeData = new StrageEyeData();

        public static void InitUDP()
        {
            udp = new UdpClient(port);
       
            thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    GetUDPData();

                }

            }));

            thread.Start();
        }

        public static double EyeSquintRight;


        public static void GetUDPData()
        {
            IPEndPoint remoteEP = null;
            byte[] data = udp.Receive(ref remoteEP);
            string eyedata = Encoding.ASCII.GetString(data);

            StrageEyeData eyestragedata = JsonSerializer.Deserialize<StrageEyeData>(eyedata);
            strageEyeData = eyestragedata;

            for (int i = 0; i < strageEyeData.Face_data_string.Length; i++)
            {
                if (strageEyeData.Face_data_string[i] == "EyeSquintRight")
                {
                    Console.WriteLine("Face_data_string" + strageEyeData.Face_data[i]);
                    EyeSquintRight = strageEyeData.Face_data[i];
                }
                
            }
            
            
            


        }









    }
}
