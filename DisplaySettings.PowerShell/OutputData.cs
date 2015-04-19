// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using System.Collections.Generic;
using System.Linq;

namespace DisplaySettings.PowerShell
{
    static class Mapper
    {
        public static CurrentDisplayOutput ToOutput(this DisplayData data, DeviceScreen screen)
        {
            return new CurrentDisplayOutput(data, screen);
        }
        public static IEnumerable<AllDisplayOutput> ToOutput(this IEnumerable<DisplayData> datas, DeviceScreen screen)
        {
            return datas.Select(data => new AllDisplayOutput(data));
        }
        public static IEnumerable<ScreenOutput> ToOutput(this IEnumerable<DeviceScreen> screens)
        {
            return screens.Select(screen => new ScreenOutput(screen));
        }
    }

    public class CurrentDisplayOutput
    {
        DisplayData _data;
        DeviceScreen _screen;

        public CurrentDisplayOutput(DisplayData data, DeviceScreen screen)
        {
            _data = data;
            _screen = screen;
        }

        public int Number { get { return (_screen == null) ? 0 : _screen.Number; } }
        public string AdapterString { get { return (_screen == null) ? "" : _screen.Adapter.DeviceString; } }
        public string MonitorString { get { return (_screen == null) ? "" : _screen.Monitor.DeviceString; } }

        public int Width { get { return _data.Width; } }
        public int Height { get { return _data.Height; } }
        public int ColorDepth { get { return _data.ColorDepth; } }
        public int ScreenRefreshRate { get { return _data.ScreenRefreshRate; } }

        public string GetDisplayText()
        {
            return _data.GetDisplayText();
        }
    }

    public class AllDisplayOutput
    {
        DisplayData _data;

        public AllDisplayOutput(DisplayData data)
        {
            _data = data;
        }

        public int Width { get { return _data.Width; } }
        public int Height { get { return _data.Height; } }
        public int ColorDepth { get { return _data.ColorDepth; } }
        public int ScreenRefreshRate { get { return _data.ScreenRefreshRate; } }
        public bool IsCurrent { get { return _data.IsCurrent; } }

        public string GetDisplayText()
        {
            return _data.GetDisplayText();
        }
    }

    public class ScreenOutput
    {
        DeviceScreen _screen;

        public int Number { get { return _screen.Number; } }
        public string AdapterString { get { return _screen.Adapter.DeviceString; } }
        public string MonitorString { get { return _screen.Monitor.DeviceString; } }
        public bool IsPrimary { get { return _screen.IsPrimary; } }

        public ScreenOutput(DeviceScreen screen)
        {
            _screen = screen;
        }

        public string GetDisplayText()
        {
            return AdapterString + " => " + MonitorString;
        }
    }
}
