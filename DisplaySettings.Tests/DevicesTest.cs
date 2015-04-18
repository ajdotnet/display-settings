// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace DisplaySettings.Tests
{
    [TestClass]
    public class DevicesTest
    {
        [TestMethod]
        public void Test_QueryAllDevices()
        {
            var devices = DisplayLogic.QueryDevices().ToArray();
            Assert.IsTrue(devices.Length > 0);
        }

        [TestMethod]
        public void Test_QueryDeviceDisplaySettings()
        {
            var devices = DisplayLogic.QueryDevices().ToArray();
            foreach (var device in devices)
            {
                var modes = DisplayLogic.QueryAllDisplaySettings(device.DeviceName).ToArray();
                Assert.IsTrue(modes.Length > 0, device.DeviceName);
            }
        }
    }
}
