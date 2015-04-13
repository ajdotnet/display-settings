// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace DisplaySettings.Tests
{
    [TestClass]
    public class DisplayTest
    {
        [TestMethod]
        public void Test_QueryCurrent()
        {
            var current = Display.QueryCurrentDisplaySettings();
            Assert.IsNotNull(current);
            Assert.IsTrue(current.Height > 0);
            Assert.IsTrue(current.Width > 0);
            Assert.IsTrue(current.ColorDepth > 0);
        }

        [TestMethod]
        public void Test_QueryAll()
        {
            var all = Display.QueryAllDisplaySettings();
            Assert.IsNotNull(all);
            var allArray = all.ToArray();
            Assert.IsTrue(allArray.Length > 0);
        }

        [TestMethod]
        public void Test_CurrentIsInAll()
        {
            var current = Display.QueryCurrentDisplaySettings();
            var all = Display.QueryAllDisplaySettings();
            var found = all.Where(ds => DisplayData.AreEqual(ds, current)).ToArray();
            Assert.AreEqual(1, found.Length);
        }

        [TestMethod]
        public void Test_Set()
        {
            var originalDS = Display.QueryCurrentDisplaySettings();
            var all = Display.QueryAllDisplaySettings().ToArray();
            if (all.Length == 1)
                Assert.Inconclusive("Only one possible resolution, cannot change to another...");
            var testDS = all.Reverse().Where(ds => !DisplayData.AreEqual(ds, originalDS)).FirstOrDefault();

            string errorMessage;
            try
            {
                var err = Display.ChangeSettings(testDS, out errorMessage);
                Thread.Sleep(5000);
                Assert.AreEqual(0, err);
                Assert.IsTrue(string.IsNullOrEmpty(errorMessage));


                var newDS = Display.QueryCurrentDisplaySettings();
                Assert.IsTrue(DisplayData.AreEqual(newDS, testDS));
                Assert.IsFalse(DisplayData.AreEqual(newDS, originalDS));
            }
            finally
            {
                var err = Display.ChangeSettings(originalDS, out errorMessage);
                Assert.AreEqual(0, err);
            }
        }
    }
}
