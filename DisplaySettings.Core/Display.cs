// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Collections.Generic;

namespace DisplaySettings.Core
{
    public static class Display
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
    }
}
