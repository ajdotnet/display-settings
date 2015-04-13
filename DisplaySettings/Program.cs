// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using AJ.Console;
using DisplaySettings.Core;
using DisplaySettings.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DisplaySettings
{
    /// <summary>
    /// </summary>
    class Program : ConsoleApp
    {
        [STAThread]
        static int Main(string[] args)
        {
            Program app = new Program();

            int _i = 0;
            switch (_i)
            {
                case 1: args = new string[] { "/?" }; break;
                case 2: args = new string[] { "/query" }; break;
                case 3: args = new string[] { "/queryall" }; break;
                case 4: args = new string[] { "1440", "900" }; break;
            }

            return app.Run(args);
        }

        [Flags]
        enum Mode
        {
            None,
            Set = 0x01,
            Query = 0x02,
            QueryAll = 0x04,
            QueryDevices = 0x08
        }

        Mode _mode;
        int[] _setValues;
        int _deviceNumber = -1;

        protected override void ApplyArguments(string[] values)
        {
            Guard.AssertNotNull(values, "values");

            if (values.Length > 0)
            {
                _mode |= Mode.Set;

                EnsureLength(values, 2, 4, null);
                _setValues = new int[] { -1, -1, -1, -1 };
                for (int i = 0; i < values.Length; i++)
                {
                    if (!int.TryParse(values[i], out _setValues[i]))
                        ThrowInvalidArguments(string.Format(CultureInfo.CurrentCulture, Resources.Err_ArgumentNoNumber, i + 1));
                }
            }
        }

        protected override void ApplySwitch(string name, string[] values)
        {
            Guard.AssertNotNull(name, "name");
            Guard.AssertNotNull(values, "values");

            switch (name.ToUpperInvariant())
            {
                case "/DEBUG":
                    System.Diagnostics.Debugger.Break();
                    break;
                case "/QUERY":
                    _mode |= Mode.Query;
                    EnsureLength(values, 0, 0, name);
                    break;
                case "/QUERYALL":
                    _mode |= Mode.QueryAll;
                    EnsureLength(values, 0, 0, name);
                    break;
                case "/QUERYDEVICES":
                    _mode |= Mode.QueryDevices;
                    EnsureLength(values, 0, 0, name);
                    break;
                case "/DEVICE":
                    EnsureLength(values, 1, 1, name);
                    if (!int.TryParse(values[0], out _deviceNumber))
                        ThrowInvalidArguments(string.Format(CultureInfo.CurrentCulture, Resources.Err_ArgumentNoNumber, "for " + name));
                    break;
                default:
                    // we don't like what we don't know
                    ThrowUnknownSwitch(name);
                    break;
            }
        }

        protected override void ApplySwitches()
        {
            base.ApplySwitches();
            if (_mode == Mode.None)
                ThrowInvalidArguments(Resources.Err_NoArguments);
        }

        protected override void Process()
        {
            string deviceName = null;
            if (_deviceNumber > 0)
            {
                DeviceData dd = GetDevice(_deviceNumber);
                if (dd == null)
                {
                    throw new ConsoleException(string.Format(CultureInfo.CurrentCulture, Resources.Err_DeviceNotFound, _deviceNumber));
                }
                deviceName = dd.DeviceName;
            }

            if (_mode.HasFlag(Mode.Set))
            {
                var current = Display.QueryCurrentDisplaySettings(deviceName);
                current.Width = _setValues[0];
                current.Height = _setValues[1];
                current.ColorDepth = _setValues[2] < 0 ? current.ColorDepth : _setValues[2];
                current.ScreenRefreshRate = _setValues[3] < 0 ? current.ScreenRefreshRate : _setValues[3];

                var displaySettings = Display.QueryAllDisplaySettings(deviceName);
                var found = displaySettings.Where(dd => DisplayData.AreEqual(dd, current)).FirstOrDefault();
                if (found == null)
                    ThrowInvalidArguments(Resources.Err_NoMatchingDisplaySettings);

                string err;
                if (Display.ChangeSettings(found, out err, deviceName) != 0)
                    throw new ConsoleException(string.Format(CultureInfo.CurrentCulture, Resources.Err_SettingValues, err));

                if (err != null)
                    WriteLine(string.Format(CultureInfo.CurrentCulture, Resources.Err_SettingValues, err));
                else
                    WriteLine(Resources.OK_SettingValues);
            }

            if (_mode.HasFlag(Mode.Query))
            {
                var current = Display.QueryCurrentDisplaySettings(deviceName);
                PrintDisplayData(current);
            }

            if (_mode.HasFlag(Mode.QueryAll))
            {
                var displaySettings = Display.QueryAllDisplaySettings(deviceName);
                PrintDisplaySetting(displaySettings);
            }

            if (_mode.HasFlag(Mode.QueryDevices))
            {
                var devices = Devices.QueryDevices();
                PrintDevices(devices);
            }
        }

        private void PrintDisplayData(DisplayData data)
        {
            int width = Math.Max(Math.Max(Math.Max(Resources.Msg_Resolution.Length, Resources.Msg_ColorDepth.Length), Resources.Msg_RefreshRate.Length), Resources.Msg_Position.Length);
            WriteLine(Resources.Msg_Header_DisplaySettings);
            WriteLine("");
            WriteLine("    " + Resources.Msg_Resolution + ":   " + "".PadRight(width - Resources.Msg_Resolution.Length) + data.Width + " x " + data.Height);
            WriteLine("    " + Resources.Msg_ColorDepth + ":   " + "".PadRight(width - Resources.Msg_ColorDepth.Length) + data.ColorDepth + " " + Resources.Msg_ColorDepthUnit);
            WriteLine("    " + Resources.Msg_RefreshRate + ":   " + "".PadRight(width - Resources.Msg_RefreshRate.Length) + data.ScreenRefreshRate + " Hertz");
            WriteLine("    " + Resources.Msg_Position + ":   " + "".PadRight(width - Resources.Msg_Position.Length) + data.PositionX + " x " + data.PositionY);
            WriteLine("");
        }

        private void PrintDisplaySetting(IEnumerable<DisplayData> list)
        {
            int w1 = Math.Max(11, Resources.Msg_Resolution.Length);
            int w2 = Math.Max(6, Resources.Msg_ColorDepth.Length);
            int w3 = Math.Max(9, Resources.Msg_RefreshRate.Length);

            WriteLine(Resources.Msg_Header_DisplaySettings + ":");
            WriteLine("");
            WriteLine("    " + Resources.Msg_Resolution.PadLeft(w1) + "   " + Resources.Msg_ColorDepth.PadLeft(w2) + "   " + Resources.Msg_RefreshRate.PadLeft(w3));
            WriteLine("    " + "".PadRight(w1, '=') + "   " + "".PadRight(w2, '=') + "   " + "".PadRight(w3, '='));
            string line;

            foreach (DisplayData data in list)
            {
                line = string.Format(CultureInfo.InvariantCulture,
                    "    " + "".PadRight(w1 - 11) + "{0,4} x {1,4}   " + "".PadRight(w2 - 6) + "{2,2} bpp   " + "".PadRight(w3 - 9) + "{3,3} Hertz",
                    data.Width, data.Height, data.ColorDepth, data.ScreenRefreshRate);
                if (data.IsCurrent)
                    WriteLine(ShowLevel.Important, line + " " + Resources.Msg_Current);
                else
                    WriteLine(line);
            }
            WriteLine("");
        }

        private void PrintDevices(IEnumerable<DeviceData> list)
        {
            var a = list.ToArray();
            var lengthAdapter = a.Where(dd => dd.IsAdapter).Select(dd => dd.DeviceString.Length).Max();
            var lengthMonitor = a.Where(dd => dd.IsMonitor).Select(dd => dd.DeviceString.Length).Max();

            WriteLine(Resources.Msg_Header_DisplayAdapters);
            WriteLine("");
            WriteLine("    # " + Resources.Msg_Adapter.PadRight(lengthAdapter) + "    " + Resources.Msg_Monitor);
            WriteLine("    = " + "".PadRight(lengthAdapter, '=') + "    " + "".PadRight(lengthMonitor, '='));

            DeviceData adapter = null;
            int number = 1;
            foreach (var data in list)
            {
                if (data.IsAdapter)
                {
                    adapter = data;
                }
                else
                {
                    string line = "    " + number + " " +
                        adapter.DeviceString.PadRight(lengthAdapter) + " -> " +
                        data.DeviceString.PadRight(lengthMonitor);
                    if (adapter.IsPrimary)
                        WriteLine(ShowLevel.Important, line + " " + Resources.Msg_Primary);
                    else
                        WriteLine(line);
                    ++number;
                }
            }
            WriteLine("");
        }

        static DeviceData GetDevice(int number)
        {
            var devices = Devices.QueryDevices();

            DeviceData adapter = null;
            int n = 0;
            foreach (var data in devices)
            {
                if (data.IsAdapter)
                {
                    adapter = data;
                }
                else
                {
                    ++n;
                    if (n == number)
                        return adapter;
                }
            }

            return null;
        }
    }
}