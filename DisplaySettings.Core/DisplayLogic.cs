// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

namespace DisplaySettings.Core
{
    public static class DisplayLogic
    {
        public static DisplayData QueryCurrentDisplaySettings(string deviceName = null)
        {
            var dmCurrent = NativeMethods.QueryCurrentDisplaySettings(deviceName);
            return new DisplayData(dmCurrent, true);
        }

        public static IEnumerable<DisplayData> QueryAllDisplaySettings(string deviceName = null)
        {
            var dmCurrent = NativeMethods.QueryCurrentDisplaySettings(deviceName);
            foreach (var dm in NativeMethods.QueryAllDisplaySettings(deviceName))
                yield return new DisplayData(dm, NativeMethods.AreEqual(dm, dmCurrent));
        }

        public static int ChangeSettings(DisplayData data, out string errorMessage, string deviceName = null)
        {
            Guard.AssertNotNull(data, "data");

            return NativeMethods.ChangeSettings(data.Data, out errorMessage, deviceName);
        }

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

        public static IEnumerable<DeviceScreen> QueryDeviceScreens()
        {
            var devices = QueryDevices();
            DeviceData adapter = null;
            int number = 1;
            foreach (var data in devices)
            {
                if (data.IsAdapter)
                {
                    adapter = data;
                }
                else
                {
                    yield return new DeviceScreen(adapter, data) { Number = number };
                    ++number;
                }
            }
        }

        public static DeviceScreen GetDeviceScreen(int number)
        {
            Guard.AssertCondition(number > 0, "number", number);
            return QueryDeviceScreens().Skip(number - 1).FirstOrDefault();
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static int GetCurrentDeviceNumber()
        {
            // get console window rect
            var hwnd = NativeMethods.GetConsoleWindow();
            // GetWindowRect fails on minimized windows with -32000/-32000
            // http://blogs.msdn.com/b/oldnewthing/archive/2004/10/28/249044.aspx
            var wp= new NativeMethods.WINDOWPLACEMENT();
            wp.Length= Marshal.SizeOf(wp);
            NativeMethods.GetWindowPlacement(hwnd, ref wp);
            var rect = wp.NormalPosition;

            // go through all adapters...
            var adapters = NativeMethods.QueryAllDisplayAdapters();
            int number = 1;
            foreach (var adapter in adapters)
            {
                var devmode = NativeMethods.QueryCurrentDisplaySettings(adapter.DeviceName);
                if (devmode.Contains(rect))
                {
                    return number;
                }
                ++number;
            }

            return 0;
        }
    }
}
