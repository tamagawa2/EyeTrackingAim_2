using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTrackingAim1.Scripts.SendInput
{
   public class Setwindow

    {

		public static IntPtr HWND_BOTTOM = new IntPtr(1);
		public static IntPtr HWND_NOTOPMOST = new IntPtr(-2);
		public static IntPtr HWND_TOP = IntPtr.Zero;
		public static IntPtr HWND_TOPMOST = new IntPtr(-1);

		
		public enum SWP : uint
		{
			NOSIZE = 0x0001,
			NOMOVE = 0x0002,
			NOZORDER = 0x0004,
			NOREDRAW = 0x0008,
			NOACTIVATE = 0x0010,
			FRAMECHANGED = 0x0020,
			SHOWWINDOW = 0x0040,
			HIDEWINDOW = 0x0080,
			NOCOPYBITS = 0x0100,
			NOOWNERZORDER = 0x0200,
			NOSENDCHANGING = 0x400
		}
		public enum GWL : int
		{
			WINDPROC = -4,
			HINSTANCE = -6,
			HWNDPARENT = -8,
			ID = -12,
			STYLE = -16,
			EXSTYLE = -20,
			USERDATA = -21,
		}

		public enum Style : long
        {
			WS_POPUP = 0x80000000L,
		}

		public enum ExStyle : long
        {
			WS_EX_LAYERED = 0x00080000,
			WS_EX_TRANSPARENT = 0x00000020L
		}

		public static void TopMostNonActiveWindow(IntPtr windowhandle , int cx , int cy)
        {
			Win32api.SetWindowLongPtr(windowhandle, (int)GWL.STYLE, (long)Style.WS_POPUP);
			Win32api.SetWindowLongPtr(windowhandle, (int)GWL.EXSTYLE, (long)(ExStyle.WS_EX_LAYERED | ExStyle.WS_EX_TRANSPARENT));
			Win32api.SetWindowPos(windowhandle, HWND_TOPMOST, 0, 0, cx, cy, (uint)(SWP.NOSIZE | SWP.NOACTIVATE | SWP.SHOWWINDOW));
        }

		
    }
}
