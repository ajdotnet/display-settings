// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using System.Diagnostics;

namespace DisplaySettings.Core
{
    [DebuggerDisplay("{Width} x {Height}, {ColorDepth} bpp, {ScreenRefreshRate} Hz, {DeviceName}")]
    public class DisplayData
    {
        NativeMethods.DEVMODE _Data;

        internal NativeMethods.DEVMODE Data { get { return _Data; } }
        
        public int Width { get { return _Data.dmPelsWidth; } set { _Data.dmPelsWidth = value; } }
        public int Height { get { return _Data.dmPelsHeight; } set { _Data.dmPelsHeight = value; } }
        public int ColorDepth { get { return _Data.dmBitsPerPel; } set { _Data.dmBitsPerPel = (short)value; } }
        public int ScreenRefreshRate { get { return _Data.dmDisplayFrequency; } set { _Data.dmDisplayFrequency = value; } }
        public string DeviceName { get { return _Data.dmDeviceName; } }

        public bool IsCurrent { get; private set; }

        public string DisplayText
        {
            get { return this.Width + " x " + this.Height + ", " + this.ColorDepth + " bpp, " + this.ScreenRefreshRate + " Hertz"; }
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
