// Source: https://github.com/ajdotnet/display-settings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using DisplaySettings.Core.Properties;

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

        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "0")]
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        [DllImport("user32.dll")]
        public static extern int ChangeDisplaySettings(ref DEVMODE devMode, uint flags);

        public const int ENUM_CURRENT_SETTINGS = -1;

        public static DEVMODE QueryCurrentDisplaySettings(string deviceName = null)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(dm);
            EnumDisplaySettings(deviceName, ENUM_CURRENT_SETTINGS, ref dm);
            return dm;
        }

        public static IEnumerable<DEVMODE> QueryAllDisplaySettings(string deviceName = null)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(dm);
            for (int i = 0; EnumDisplaySettings(deviceName, i, ref dm) != 0; ++i)
            {
                yield return dm;
            }
        }

        public static int ChangeSettings(DEVMODE devmode, out string errorMessage, string deviceName = null)
        {
            errorMessage = "";

            devmode.dmDeviceName = deviceName;
            devmode.dmFields &= ~(DmFields.DM_DISPLAYFIXEDOUTPUT);
            ChangeDisplayFlags flags = ChangeDisplayFlags.CDS_UPDATEREGISTRY;
            ChangeSettingsReturnValues iRet = (ChangeSettingsReturnValues)ChangeDisplaySettings(ref devmode, (uint)flags);

            switch (iRet)
            {
                case ChangeSettingsReturnValues.DISP_CHANGE_SUCCESSFUL: errorMessage = Resources.DISP_CHANGE_SUCCESSFUL; break;
                case ChangeSettingsReturnValues.DISP_CHANGE_RESTART: errorMessage = Resources.DISP_CHANGE_RESTART; break;
                case ChangeSettingsReturnValues.DISP_CHANGE_FAILED: errorMessage = Resources.DISP_CHANGE_FAILED; break;
                case ChangeSettingsReturnValues.DISP_CHANGE_BADDUALVIEW: errorMessage = Resources.DISP_CHANGE_BADDUALVIEW; break;
                case ChangeSettingsReturnValues.DISP_CHANGE_BADFLAGS: errorMessage = Resources.DISP_CHANGE_BADFLAGS; break;
                case ChangeSettingsReturnValues.DISP_CHANGE_BADPARAM: errorMessage = Resources.DISP_CHANGE_BADPARAM; break;
                case ChangeSettingsReturnValues.DISP_CHANGE_NOTUPDATED: errorMessage = Resources.DISP_CHANGE_NOTUPDATED; break;
                default: errorMessage = Resources.DISP_CHANGE_OTHER; break;
            }
            return (int)iRet;
        }

        public static bool AreEqual(DEVMODE x, DEVMODE y)
        {
            if (x.dmPelsHeight == y.dmPelsHeight
                    && x.dmPelsWidth == y.dmPelsWidth
                    && x.dmBitsPerPel == y.dmBitsPerPel
                    && x.dmDisplayFrequency == y.dmDisplayFrequency
                    && x.dmDeviceName == y.dmDeviceName)
                return true;
            return false;
        }
    }
}
