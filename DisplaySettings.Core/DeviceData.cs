// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DisplaySettings.Core
{
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    public class DeviceData
    {
        bool _isAdapter;
        NativeMethods.DISPLAY_DEVICE _device;

        internal NativeMethods.DISPLAY_DEVICE Device { get { return _device; } }

        public string DeviceID { get { return _device.DeviceID; } }
        public string DeviceKey { get { return _device.DeviceKey; } }
        public string DeviceName { get { return _device.DeviceName; } }
        public string DeviceString { get { return _device.DeviceString; } }

        public bool IsAdapter { get { return _isAdapter; } }
        public bool IsMonitor { get { return !_isAdapter; } }
        public bool IsPrimary { get { return _isAdapter ? (_device.StateFlags & NativeMethods.DisplayDeviceStateFlags.PrimaryDevice) != 0 : false; } }

        internal DeviceData(NativeMethods.DISPLAY_DEVICE device, bool isAdapter)
        {
            _isAdapter = isAdapter;
            _device = device;
        }

        public static bool AreEqual(DeviceData deviceData1, DeviceData deviceData2)
        {
            Guard.AssertNotNull(deviceData1, "deviceData1");
            Guard.AssertNotNull(deviceData2, "deviceData2");

            if (deviceData1.IsAdapter != deviceData2.IsAdapter)
                return false;
            return NativeMethods.AreEqual(deviceData1.Device, deviceData2.Device);
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private string GetDebuggerDisplay()
        {
            if (IsAdapter)
                return "Adapter \"" + DeviceName + "\", \"" + DeviceString + "\", " + (IsPrimary ? " (primary)" : "");
            return "Monitor \"" + DeviceName + "\", \"" + DeviceString + "\"";
        }
    }
}
