// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DisplaySettings.Core
{
    [DebuggerDisplay("{Width} x {Height}, {ColorDepth} bpp, {ScreenRefreshRate} Hz")]
    public class DisplayData
    {
        NativeMethods.DEVMODE _Data;

        internal NativeMethods.DEVMODE Data { get { return _Data; } }

        public int Width { get { return (int)_Data.dmPelsWidth; } set { _Data.dmPelsWidth = (uint)value; } }
        public int Height { get { return (int)_Data.dmPelsHeight; } set { _Data.dmPelsHeight = (uint)value; } }
        public int ColorDepth { get { return (int)_Data.dmBitsPerPel; } set { _Data.dmBitsPerPel = (uint)value; } }
        public int ScreenRefreshRate { get { return (int)_Data.dmDisplayFrequency; } set { _Data.dmDisplayFrequency = (uint)value; } }
        public bool IsCurrent { get; private set; }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "don't want this to show up in powershell!")]
        public string GetDisplayText()
        {
            return this.Width + " x " + this.Height + ", " + this.ColorDepth + " bpp, " + this.ScreenRefreshRate + " Hertz";
        }

        internal DisplayData(NativeMethods.DEVMODE data, bool isCurrent)
        {
            _Data = data;
            this.IsCurrent = isCurrent;
        }

        public static bool AreEqual(DisplayData displayData1, DisplayData displayData2)
        {
            Guard.AssertNotNull(displayData1, "displayData1");
            Guard.AssertNotNull(displayData2, "displayData2");

            return NativeMethods.AreEqual(displayData1.Data, displayData2.Data);
        }
    }
}
