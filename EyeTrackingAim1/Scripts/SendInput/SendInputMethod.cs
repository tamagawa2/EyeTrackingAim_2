using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EyeTrackingAim1.Scripts.EyeData;

namespace EyeTrackingAim1.Scripts.SendInput
{
    public enum SendInputMouseFlag
    {
        MOUSEEVENTF_MOVE = 0x0001,
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        MOUSEEVENTF_LEFTUP = 0x0004,
        MOUSEEVENTF_VIRTUALDESK = 0x4000,
        MOUSEEVENTF_ABSOLUTE = 0x8000,
        MOUSEEVENTF_RIGHTDOWN = 0x0008,
        MOUSEEVENTF_RIGHTUP = 0x0010,

    }

    public enum SendInputKeyBoardFlag
    {
        KEYEVENTF_EXTENDEDKEY = 0x0001,
        KEYEVENTF_KEYUP = 0x0002,
        KEYEVENTF_SCANCODE = 0x0008,
        KEYEVENTF_UNICODE = 0x0004

    }
    [StructLayout(LayoutKind.Sequential)]

    public struct MOUSEINPUT
    {

        public int dx;

        public int dy;

        public int mouseData;

        public int dwFlags;

        public int time;

        public IntPtr dwExtraInfo;

    };



    [StructLayout(LayoutKind.Sequential)]

    public struct KEYBDINPUT
    {

        public short wVk;

        public short wScan;

        public int dwFlags;

        public int time;

        public IntPtr dwExtraInfo;

    };



    [StructLayout(LayoutKind.Sequential)]

    public struct HARDWAREINPUT
    {

        public int uMsg;

        public short wParamL;

        public short wParamH;

    };



    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {

        [FieldOffset(0)]

        public int type;

        [FieldOffset(8)]

        public MOUSEINPUT mi;

        [FieldOffset(8)]

        public KEYBDINPUT ki;

        [FieldOffset(8)]

        public HARDWAREINPUT hi;

    };
    public class SendInputMethod
    {
       // public static INPUT iNPUT = new INPUT();
        public static void MouseMove(int mousex,int mousey)
        {
            INPUT iNPUT = new INPUT();
            iNPUT.type = 0;
            iNPUT.mi.dx = mousex;
            iNPUT.mi.dy = mousey;
            iNPUT.mi.mouseData = 0;
            iNPUT.mi.dwFlags = (int)SendInputMouseFlag.MOUSEEVENTF_MOVE;
            iNPUT.mi.time = 0;
            iNPUT.mi.dwExtraInfo = new IntPtr(10);
            int a = Win32api.SendInput(1, ref iNPUT,Marshal.SizeOf(iNPUT));
           
        }

        public static void MouseUpDown(SendInputMouseFlag sendInputMouseFlag)
        {
            INPUT iNPUT = new INPUT();
            iNPUT.type = 0;
            iNPUT.mi.dx = 0;
            iNPUT.mi.dy = 0;
            iNPUT.mi.mouseData = 0;
            iNPUT.mi.dwFlags = (int)sendInputMouseFlag;
            iNPUT.mi.time = 0;
            iNPUT.mi.dwExtraInfo = new IntPtr(10); 
            Win32api.SendInput(1, ref iNPUT, Marshal.SizeOf(iNPUT));
        }

        public static void KeyDown(KeyData keyData)
        {
            INPUT iNPUT = new INPUT();
            iNPUT.type = 1;
            iNPUT.ki.wVk = 0;
            iNPUT.ki.wScan = (short)Win32api.MapVirtualKey((short)keyData, 0);
            iNPUT.ki.dwFlags = (int)SendInputKeyBoardFlag.KEYEVENTF_SCANCODE | (int)SendInputKeyBoardFlag.KEYEVENTF_UNICODE;
            iNPUT.ki.dwExtraInfo = new IntPtr(10);
            Win32api.SendInput(1, ref iNPUT, Marshal.SizeOf(iNPUT));
        }

        public static void KeyUp(KeyData keyData)
        {
            INPUT iNPUT = new INPUT();
            iNPUT.type = 1;
            iNPUT.ki.wVk = 0;
            iNPUT.ki.wScan = (short)Win32api.MapVirtualKey((short)keyData, 0);
            iNPUT.ki.dwFlags = (int)SendInputKeyBoardFlag.KEYEVENTF_KEYUP | (int)SendInputKeyBoardFlag.KEYEVENTF_SCANCODE | (int)SendInputKeyBoardFlag.KEYEVENTF_UNICODE;
            iNPUT.ki.dwExtraInfo = new IntPtr(10);
            Win32api.SendInput(1, ref iNPUT, Marshal.SizeOf(iNPUT));
        }
    }
}
