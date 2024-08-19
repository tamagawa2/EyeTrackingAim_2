using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using EyeTrackingAim1.Scripts.EyeTrackingAim;

namespace EyeTrackingAim1.Scripts.SendInput
{
    
    public class Win32api
    {

        [DllImport("user32.dll")]
        public extern static int SendInput(int nInputs, ref INPUT pInputs, int cbsize);

        [DllImport("user32.dll", SetLastError = true)]
        public extern static IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA", SetLastError = true)]
        public extern static int MapVirtualKey(int wCode, int wMapType);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc,IntPtr lparam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr handle, EnumWindowsDelegate enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd,StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd,StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, long dwNewLong);
        [DllImport("user32.dll")]
        public static extern int CallWindowProc(int lpPrevWndFunc, IntPtr hwnd, int msg, int wParam, int lParam);

        //Delegate
        public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);
        public static List<IntPtr> intPtrs = new List<IntPtr>();

        public static bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {

            intPtrs.Add(hWnd);

            int textLen = GetWindowTextLength(hWnd);
            if (0 < textLen)
            {
                //ウィンドウのタイトルを取得する
                StringBuilder tsb = new StringBuilder(textLen + 1);
                GetWindowText(hWnd, tsb, tsb.Capacity);
                
                if (Form1.eyeDatas[Form1.nowvalue].Appname == tsb.ToString())
                {
                    EyeTrackingAim_Setting.windowptr = hWnd;
                    
                }
                
            }


            //すべてのウィンドウを列挙する
            return true;
        }

        public static bool EnumChildWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {
            //ウィンドウのタイトルの長さを取得する
            int textLen = GetWindowTextLength(hWnd);
            
            if (0 < textLen)
            {
                //ウィンドウのタイトルを取得する
                StringBuilder tsb = new StringBuilder(textLen + 1);
                GetWindowText(hWnd, tsb, tsb.Capacity);
                


                if (Form1.eyeDatas[Form1.nowvalue].Appname == tsb.ToString())
                {
                    EyeTrackingAim_Setting.windowptr = hWnd;
                    
                }
                

            }

            //すべてのウィンドウを列挙する
            return true;
        }


        //GetPixel
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        [DllImport("gdi32.dll")]
        public static extern uint SetPixel(IntPtr hdc, int X, int Y, uint crColor);
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hObject);

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int hookType, HookHandler hookDelegate,IntPtr module, uint threadId);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public System.IntPtr dwExtraInfo;
        }


        private delegate IntPtr HookHandler(int ncode, IntPtr wParam, IntPtr lParam);
        static HookHandler mousefookhandler;
        static IntPtr hookid;
        static IntPtr MouseHookHandler(int ncode, IntPtr wParam, IntPtr lParam)
        {
            MSLLHOOKSTRUCT ms = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

            Console.WriteLine(ms.flags);

            if(ms.flags > 0)
            {
                ms.flags = 0;
            }

            int size = Marshal.SizeOf(ms);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(ms, ptr, false);
            return CallNextHookEx(hookid, ncode, wParam, ptr);
            //return new IntPtr(1);
        }

        public static void StartHook()
        {
            if(hookid == IntPtr.Zero)
            {
                mousefookhandler = MouseHookHandler;
                using (var curProcess = Process.GetCurrentProcess())
                {
                    using (ProcessModule curModule = curProcess.MainModule)
                    {
                        hookid = SetWindowsHookEx(14, mousefookhandler, GetModuleHandle(curModule.ModuleName), 0);
                    }
                }
            }
            
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hookid);
            hookid = IntPtr.Zero;
        }


        [DllImport("user32.dll")]
        public static extern void BlockInput(bool fBlockit);


    }
}
