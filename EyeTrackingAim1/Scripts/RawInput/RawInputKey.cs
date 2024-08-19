using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTrackingAim1.Scripts.EyeData;
using System.Windows.Forms;
namespace EyeTrackingAim1.Scripts.RawInput
{
   public class RawInputKey
    {
        public static bool RawInputJudge(KeyData key)
        {

            if (Win32apiRawInput.GetAsyncKeyState((int)key) <  0)
            {
                return true;
            } else
            {
                return false;
            }
            
        }

        public static bool[] pastkeystate = new bool[Enum.GetNames(typeof(KeyData)).Length];
        public static bool[] KeyStateKioku()
        {
            bool[] keystates = new bool[Enum.GetNames(typeof(KeyData)).Length];
            
            for (int i = 1; i <= Enum.GetNames(typeof(KeyData)).Length; i++)
            {
                
                keystates[i - 1] = RawInputJudge((KeyData)i);
            }

            return keystates;
        }

        public static void KeyStateSaisei()
        {
            
            for (int i = 1; i <= Enum.GetNames(typeof(KeyData)).Length; i++)
            {
               if(pastkeystate[i - 1] != RawInputJudge((KeyData)i))
                {
                    if (pastkeystate[i])
                    {
                        SendInput.SendInputMethod.KeyDown((KeyData)i);
                    } else
                    {
                        SendInput.SendInputMethod.KeyUp((KeyData)i);
                    }
                }
            }
        }

        




    }
}
