// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using DisplaySettings.PowerShell.Properties;
using System;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Windows.Forms;

namespace DisplaySettings.PowerShell
{
    [Cmdlet(VerbsCommon.Set, "DisplaySettings", SupportsShouldProcess = true)]
    public class SetDisplaySettingsCommand : Cmdlet
    {
        #region properties

        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = false, HelpMessage = "The horizontal screen resolution")]
        [ValidateRange(0, (int)short.MaxValue)]
        public int Width { get; set; }

        [Parameter(Position = 1, Mandatory = true, HelpMessage = "The vertical screen resolution")]
        [ValidateRange(0, (int)short.MaxValue)]
        public int Height { get; set; }

        [Parameter(Position = 2, HelpMessage = "The color depth in bits (8, 16, 32)")]
        [ValidateRange(0, (int)byte.MaxValue)]
        [ValidateSet("8", "16", "32", "64")]
        public int ColorDepth { get; set; }

        [Parameter(Position = 3, HelpMessage = "The screen refresh rate")]
        [ValidateRange(0, (int)byte.MaxValue)]
        public int Frequency { get; set; }

        [Parameter]
        public SwitchParameter Force { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The device number")]
        [ValidateRange(0, (int)byte.MaxValue)]
        public int Device { get; set; }

        public SetDisplaySettingsCommand()
        {
            ColorDepth = -1;
            Frequency = -1;
            this.Device = -1; // use default
        }

        #endregion

        #region processing

        protected override void ProcessRecord()
        {
            // identify device...
            var deviceNumber = this.Device;
            if (deviceNumber < 0)
                deviceNumber = DisplayLogic.GetCurrentDeviceNumber();

            string deviceName = null;
            DeviceScreen screen = null;
            if (deviceNumber > 0)
            {
                screen = DisplayLogic.GetDeviceScreen(deviceNumber);
                if (screen == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.Err_DeviceNotFound, this.Device));
                }
                deviceName = screen.Adapter.DeviceName;
            }

            // proceed...
            var current = DisplayLogic.QueryCurrentDisplaySettings(deviceName);
            current.Width = this.Width;
            current.Height = this.Height;
            current.ColorDepth = this.ColorDepth < 0 ? current.ColorDepth : this.ColorDepth;
            current.ScreenRefreshRate = this.Frequency < 0 ? current.ScreenRefreshRate : this.Frequency;

            var all = DisplayLogic.QueryAllDisplaySettings(deviceName);
            var found = all.Where(dd => DisplayData.AreEqual(dd, current)).FirstOrDefault();
            if (found == null)
                throw new NotSupportedException(Resources.Err_NoMatchingDisplaySettings);

            // check the -whatif parameter
            string msg = string.Format(CultureInfo.CurrentCulture, Resources.Msg_WhatIf, found.GetDisplayText(), screen.Adapter.DeviceString, screen.Monitor.DeviceString);
            if (!ShouldProcess(msg))
                return;

            // check the users intention
            if (!this.Force)
            {
                msg = string.Format(CultureInfo.CurrentCulture, Resources.Msg_Confirm, found.GetDisplayText(), screen.Adapter.DeviceString, screen.Monitor.DeviceString);
                if (!ShouldContinue(msg, "Warning!"))
                    return;
            }

            // and finally go
            string err;
            if (DisplayLogic.ChangeSettings(found, out err, deviceName) != 0)
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.Err_SettingValues, err));
            WriteVerbose(string.Format(CultureInfo.CurrentCulture, Resources.OK_SettingValues, found.GetDisplayText(), screen.Adapter.DeviceString, screen.Monitor.DeviceString));
        }

        #endregion
    }
}
