// Source: https://github.com/ajdotnet/display-settings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace DisplaySettings.Core
{
    static partial class NativeMethods
    {
        public enum ChangeSettingsReturnValues : long
        {
            DISP_CHANGE_SUCCESSFUL = 0,
            DISP_CHANGE_RESTART = 1,
            DISP_CHANGE_FAILED = -1,
            DISP_CHANGE_BADMODE = -2,
            DISP_CHANGE_NOTUPDATED = -3,
            DISP_CHANGE_BADFLAGS = -4,
            DISP_CHANGE_BADPARAM = -5,
            DISP_CHANGE_BADDUALVIEW = -6
        }

        [Flags]
        public enum ChangeDisplayFlags : uint
        {
            CDS_DYNAMICALLY = 0,
            CDS_UPDATEREGISTRY = 0x00000001,
            CDS_TEST = 0x00000002,
            CDS_FULLSCREEN = 0x00000004,
            CDS_GLOBAL = 0x00000008,
            CDS_SET_PRIMARY = 0x00000010,
            CDS_VIDEOPARAMETERS = 0x00000020,
            CDS_RESET = 0x40000000,
            CDS_NORESET = 0x10000000
        }

        public const int ENUM_CURRENT_SETTINGS = -1;

        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0")]
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        //[DllImport("user32.dll")]
        //public static extern int ChangeDisplaySettings(ref DEVMODE devMode, uint flags);

        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0")]
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        static extern int ChangeDisplaySettingsEx(string deviceName, ref DEVMODE devMode, IntPtr hwnd, uint flags, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0")]
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumDisplayDevices(string device, uint devNum, ref DISPLAY_DEVICE lpDisplayDevice, uint flags);

    }
}
