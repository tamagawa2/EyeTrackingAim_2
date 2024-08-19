using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;



namespace EyeTrackingAim1.Scripts.RawInput
{
   public class Win32apiRawInput
    {
        [DllImport("user32.dll", SetLastError = true)]
        public extern static short GetAsyncKeyState(int vKey);
    }
}
