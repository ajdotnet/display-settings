// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Collections.Generic;
using System.Linq;

namespace DisplaySettings.Core
{
    public static class Display
    {
        public static DisplayData QueryCurrentDisplaySettings()
        {
            var dmCurrent = NativeMethods.QueryCurrentDisplaySettings();
            return new DisplayData(dmCurrent, true);
        }

        public static IEnumerable<DisplayData> QueryAllDisplaySettings()
        {
            var dmCurrent = NativeMethods.QueryCurrentDisplaySettings();
            var result = NativeMethods.QueryAllDisplaySettings().Select(dm => new DisplayData(dm, NativeMethods.AreEqual(dm, dmCurrent)));
            return result;
        }

        public static int ChangeSettings(DisplayData data, out string err)
        {
            Guard.AssertNotNull(data, "data");

            return NativeMethods.ChangeSettings(data.Data, out err);
        }
    }
}
