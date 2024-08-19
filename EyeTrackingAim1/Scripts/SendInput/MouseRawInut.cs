using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace EyeTrackingAim1.Scripts.SendInput
{
    public class MouseRawInut
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;

            [FieldOffset(16 + 8)]
            public RAWMOUSE mouse;

            [FieldOffset(16 + 8)]
            public RAWKEYBOARD keyboard;

            [FieldOffset(16 + 8)]
            public RAWHID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
            [MarshalAs(UnmanagedType.U4)]
            public int dwSize;
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int wParam;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RAWMOUSE
        {
            /// <summary>
            /// The mouse state.
            /// </summary>
            [FieldOffset(0)]
            public ushort Flags;
            /// <summary>
            /// Flags for the event.
            /// </summary>
            [FieldOffset(4)]
            public ushort ButtonFlags;
            /// <summary>
            /// If the mouse wheel is moved, this will contain the delta amount.
            /// </summary>
            [FieldOffset(6)]
            public ushort ButtonData;
            /// <summary>
            /// Raw button data.
            /// </summary>
            [FieldOffset(8)]
            public uint RawButtons;
            /// <summary>
            /// The motion in the X direction. This is signed relative motion or
            /// absolute motion, depending on the value of usFlags.
            /// </summary>
            [FieldOffset(12)]
            public int LastX;
            /// <summary>
            /// The motion in the Y direction. This is signed relative motion or absolute motion,
            /// depending on the value of usFlags.
            /// </summary>
            [FieldOffset(16)]
            public int LastY;
            /// <summary>
            /// The device-specific additional information for the event.
            /// </summary>
            [FieldOffset(20)]
            public uint ExtraInformation;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWKEYBOARD
        {
            public ushort MakeCode;
            public ushort Flags;
            public ushort Reserved;
            public ushort VKey;
            public uint Message;
            public ulong ExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWHID
        {
            int dwSizeHid;
            int dwCount;
            byte bRawData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public ushort dwFlags;
            public IntPtr hwndTarget;
        }

        public const int WM_INPUT = 0x00FF;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetRawInputData(IntPtr hRawInput, int uiCommand, out RAWINPUT pData, ref uint pcbSize, int cbSizeHeader);

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE pRawInputDevices, uint uiNumDevices, int cbSize);

        public static void RegisterWindow(IntPtr handle)
        {
            RAWINPUTDEVICE rAWINPUTDEVICE = new RAWINPUTDEVICE();
            rAWINPUTDEVICE.usUsagePage = 0x01; //HID_USAGE_PAGE_GENERIC
            rAWINPUTDEVICE.usUsage = 0x02; //HID_USAGE_GENERIC_MOUSE
            rAWINPUTDEVICE.dwFlags = 0x00000100; //RIDEV_INPUTSINK
            rAWINPUTDEVICE.hwndTarget = handle;
            RegisterRawInputDevices(rAWINPUTDEVICE, 1, Marshal.SizeOf(rAWINPUTDEVICE));

        }
    }
}
