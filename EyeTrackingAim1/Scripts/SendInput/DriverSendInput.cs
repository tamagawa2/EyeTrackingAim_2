using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EyeTrackingAim1.Scripts.SendInput
{
    public class DriverSendInput
    {
        [DllImport("Project2.dll")]
        public static extern void CreateFileEyeTrackingAim();
        [DllImport("Project2.dll")]
        public static extern void LoopKaijyo();
        [DllImport("Project2.dll")]
        public static extern void LoopKidou();
        [DllImport("Project2.dll")]
        public static extern bool SendMouseMove(byte button, Int16 x, Int16 y);

        [DllImport("Project2.dll")]
        public static extern bool SendKeyMove(byte shift, byte[] KeyCode);
        [DllImport("Project2.dll")]
        public static extern void SendInputdll(int x, int y);
        [DllImport("Project2.dll")]
        public static extern bool WriteOutputData();
        [DllImport("Project2.dll")]
        public static extern bool ReadInputData();


        public static void DriverSendInput_LoopSwitch()
        {
            //state == 0 loopkidou
            //state == 1 loopkaijyo
            if(RecordDriverSendinput.Driver_SendInput_State.driverSendInputState == 0)
            {
                LoopKaijyo();
                RecordDriverSendinput.Driver_SendInput_State.driverSendInputState = 1;

            } else if (RecordDriverSendinput.Driver_SendInput_State.driverSendInputState == 1)
            {
                LoopKidou();
                RecordDriverSendinput.Driver_SendInput_State.driverSendInputState = 0;
            }
        }



        

        public static byte butoonConvert(bool left, bool right, bool middle)
        {
            byte button = 0;

            if (left == false && right == false && middle == false)
            {
                button = 0;
            } 
            else if (left == true && right == false && middle == false)
            {
                button = 1;
            }
            else if (left == true && right == true && middle == false)
            {
                button = 3;
            }
            else if (left == true && right == true && middle == true)
            {
                button = 7;
            }
            else if (left == false && right == true && middle == false)
            {
                button = 2;
            }
            else if (left == false && right == true && middle == true)
            {
                button = 6;
            }
            else if (left == false && right == false && middle == true)
            {
                button = 4;
            }
            else if (left == true && right == false && middle == true)
            {
                button = 5;
            }

            return button;
        }
    }
}
