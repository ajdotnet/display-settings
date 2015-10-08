// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DisplaySettings.Core
{
    static partial class NativeMethods
    {
        public static DEVMODE QueryCurrentDisplaySettings(string deviceName)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(dm);
            // Note: starting with windows 10, this call returns the device name in uppercase (while enumerating all settings still returns lowercase!)
            EnumDisplaySettings(deviceName, ENUM_CURRENT_SETTINGS, ref dm);
            return dm;
        }

        public static IEnumerable<DEVMODE> QueryAllDisplaySettings(string deviceName)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (ushort)Marshal.SizeOf(dm);
            for (int i = 0; EnumDisplaySettings(deviceName, i, ref dm) != 0; ++i)
            {
                // Note: with windows 10, this call returns the device names in lowercae as before (while getting the current settings returns it in uppercase!)
                yield return dm;
            }
        }

        public static int ChangeSettings(DEVMODE devmode, out string errorMessage, string deviceName)
        {
            errorMessage = "";

            devmode.dmFields = DmFields.DM_PELSHEIGHT | DmFields.DM_PELSWIDTH | DmFields.DM_BITSPERPEL | DmFields.DM_DISPLAYFREQUENCY;
            ChangeDisplayFlags flags = ChangeDisplayFlags.CDS_UPDATEREGISTRY;
            ChangeSettingsReturnValues iRet = (ChangeSettingsReturnValues)ChangeDisplaySettingsEx(deviceName, ref devmode, IntPtr.Zero, (uint)flags, IntPtr.Zero);

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
                    && (string.Compare(x.dmDeviceName, y.dmDeviceName, StringComparison.OrdinalIgnoreCase) == 0))  // see windows 10 notes above
                return true;
            return false;
        }

        public static bool Contains(this DEVMODE x, RECT rect)
        {
            RECT screenBounds = new RECT
            {
                Left = x.UnionA.dmPositionX,
                Top = x.UnionA.dmPositionY,
                Right = x.UnionA.dmPositionX + (int)x.dmPelsWidth,
                Bottom = x.UnionA.dmPositionY + (int)x.dmPelsHeight
            };
            return screenBounds.ContainsTopLeftCorner(rect);
        }

        public static bool ContainsTopLeftCorner(this RECT rect, RECT test)
        {
            return
                (rect.Left <= test.Left) && (test.Left <= rect.Right) &&
                (rect.Top <= test.Top) && (test.Top <= rect.Bottom);
        }
    }
}
