// Source: https://github.com/ajdotnet/display-settings

using AJ.Common;
using AJ.Console;
using DisplaySettings.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace DisplaySettings
{
    /// <summary>
    /// </summary>
    class Programm : ConsoleApp
    {
        [STAThread]
        static int Main(string[] args)
        {
            Programm app = new Programm();

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

        enum Mode
        {
            None,
            Set,
            Query,
            QueryAll
        }

        Mode _mode;
        int[] _setValues;

        protected override void ApplyArguments(string[] values)
        {
            Guard.AssertNotNull(values, "values");

            if (values.Length > 0)
                _mode = Mode.Set;

            if (_mode == Mode.Set)
            {
                EnsureLength(values, 2, 4, null);
                _setValues = new int[] { -1, -1, -1, -1 };
                for (int i = 0; i < values.Length; i++)
                {
                    if (!int.TryParse(values[i], out _setValues[i]))
                        ThrowInvalidArguments(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Err_ArgumentNoNumber, i + 1));
                }
            }
            else
            {
                EnsureLength(values, 0, 0, null);
            }
        }

        protected override void ApplySwitch(string name, string[] values)
        {
            Guard.AssertNotNull(name, "name");

            switch (name.ToUpperInvariant())
            {
                case "/DEBUG":
                    System.Diagnostics.Debugger.Break();
                    break;
                case "/QUERY":
                    _mode = Mode.Query;
                    EnsureLength(values, 0, 0, name);
                    break;
                case "/QUERYALL":
                    _mode = Mode.QueryAll;
                    EnsureLength(values, 0, 0, name);
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
                ThrowInvalidArguments(Properties.Resources.Err_NoArguments);
        }

        protected override void Process()
        {
            switch (_mode)
            {
                case Mode.Set:
                    var current = Display.QueryCurrentDisplaySettings();
                    current.Width = _setValues[0];
                    current.Height = _setValues[1];
                    current.ColorDepth = _setValues[2] < 0 ? current.ColorDepth : _setValues[2];
                    current.ScreenRefreshRate = _setValues[3] < 0 ? current.ScreenRefreshRate : _setValues[3];

                    var all = Display.QueryAllDisplaySettings();
                    var found = all.Where(dd => DisplayData.AreEqual(dd, current)).FirstOrDefault();
                    if (found == null)
                        ThrowInvalidArguments(Properties.Resources.Err_NoMatchingDisplaySettings);

                    string err;
                    if (Display.ChangeSettings(found, out err) != 0)
                        throw new ConsoleException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Err_SettingValues, err));

                    if (err != null)
                        WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.Err_SettingValues, err));
                    else
                        WriteLine(Properties.Resources.OK_SettingValues);
                    break;

                case Mode.Query:
                    current = Display.QueryCurrentDisplaySettings();
                    PrintDisplayData(current);
                    break;

                case Mode.QueryAll:
                    all = Display.QueryAllDisplaySettings();
                    PrintDisplaySetting(all);
                    break;

                default:
                    Guard.InvalidSwitchValue("_mode", _mode);
                    break;
            }
        }

        private void PrintDisplayData(DisplayData data)
        {
            WriteLine("Display Settings:");
            WriteLine("    Resolution:   " + data.Width + " x " + data.Height);
            WriteLine("    Color Deepth: " + data.ColorDepth + " bits per pixel");
            WriteLine("    Refresh rate: " + data.ScreenRefreshRate + " Hertz");
            WriteLine("    Devive name:  " + data.DeviceName);
        }

        private void PrintDisplaySetting(IEnumerable<DisplayData> list)
        {
            WriteLine("       Resolution   Color Deepth    Refresh Rate");
            WriteLine("    =============   ============    ============");
            string line;

            foreach (DisplayData data in list)
            {
                line = string.Format(CultureInfo.InvariantCulture,
                    "     {0,4} x {1,4}        {2,2} bpp        {3,2} Hertz",
                    data.Width, data.Height, data.ColorDepth, data.ScreenRefreshRate);
                if (data.IsCurrent)
                    WriteLine(ShowLevel.Important, line + " (current)");
                else
                    WriteLine(line);
            }
        }
    }
}