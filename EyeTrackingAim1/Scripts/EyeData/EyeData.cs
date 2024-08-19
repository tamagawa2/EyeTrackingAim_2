using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;


namespace EyeTrackingAim1.Scripts.EyeData
{
    public class EyeData
    {
        // EyeTrackingAim
        [JsonPropertyName("Appname")] public string Appname { get; set; } = "GameName";
        [JsonPropertyName("FovW")] public double FovW { get; set; }
        [JsonPropertyName("Sensitivity")] public Vector Sensitivity { get; set; }
        [JsonPropertyName("Magnification")] public double Magnification { get; set; } = 0.99;
        [JsonPropertyName("UseRawEye")] public bool UseRawEye { get; set; } = false;
        [JsonPropertyName("CancelY")] public bool CancelY { get; set; } = false;
        [JsonPropertyName("Offset")] public Vector Offset { get; set; }
        [JsonPropertyName("FPS")] public int FPS { get; set; } = 100;
        [JsonPropertyName("InputKey")] public KeyData InputKey { get; set; } = KeyData.F3;
        [JsonPropertyName("ReleaseEyeTrackingKey")] public KeyData ReleaseEyeTrackingKey { get; set; } = KeyData.F12;
        [JsonPropertyName("ForcedreleaseEyeTrackingAim")] public KeyData ForcedreleaseEyeTrackingAimKey { get; set; } = KeyData.R;
        [JsonPropertyName("DelayTime")] public double DelayTime { get; set; }
        [JsonPropertyName("EyeCorectionRange")] public double EyeCorectionRange { get; set; } = 0.5;        
        [JsonPropertyName("UseMagWindow")] public bool UseMagWindow { get; set; } = false;
        


    }
}
