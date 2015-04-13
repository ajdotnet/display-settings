// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Collections.Generic;

namespace DisplaySettings.Core
{
    public static class Devices
    {
        public static IEnumerable<DeviceData> QueryDevices()
        {
            foreach (var adapter in NativeMethods.QueryAllDisplayAdapters())
            {
                yield return new DeviceData(adapter, true);
                foreach (var monitor in NativeMethods.QueryAllDisplayMonitors(adapter.DeviceName))
                {
                    yield return new DeviceData(monitor, false);
                }
            }
        }
    }
}
