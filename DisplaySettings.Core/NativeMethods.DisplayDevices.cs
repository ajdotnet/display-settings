// Source: https://github.com/ajdotnet/display-settings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace DisplaySettings.Core
{
    static partial class NativeMethods
    {

        public static IEnumerable<DISPLAY_DEVICE> QueryAllDisplayAdapters()
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162609(v=vs.85).aspx
            DISPLAY_DEVICE dd = new DISPLAY_DEVICE();
            dd.cb = (uint)Marshal.SizeOf(dd);

            // display adapter
            for (uint iAdapter = 0; EnumDisplayDevices(null, iAdapter, ref dd, 0); ++iAdapter)
            {
                // "unsichtbare" devices
                if ((dd.StateFlags & DisplayDeviceStateFlags.AttachedToDesktop) == 0)
                    continue;

                yield return dd;
            }
        }

        public static IEnumerable<DISPLAY_DEVICE> QueryAllDisplayMonitors(string adapterName)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd162609(v=vs.85).aspx
            DISPLAY_DEVICE dd = new DISPLAY_DEVICE();
            dd.cb = (uint)Marshal.SizeOf(dd);

            for (uint iMonitor = 0; EnumDisplayDevices(adapterName, iMonitor, ref dd, 0); ++iMonitor)
            {
                // "unsichtbare" devices
                if ((dd.StateFlags & DisplayDeviceStateFlags.AttachedToDesktop) == 0)
                    continue;

                yield return dd;
            }
        }

        public static bool AreEqual(DISPLAY_DEVICE x, DISPLAY_DEVICE y)
        {
            if (x.DeviceID == y.DeviceID
                    && x.DeviceKey == y.DeviceKey
                    && x.DeviceName == y.DeviceName
                    && x.DeviceString == y.DeviceString
                    && x.StateFlags == y.StateFlags)
                return true;
            return false;
        }
    }
}
