// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using DisplaySettings.PowerShell.Properties;
using System;
using System.Globalization;
using System.Management.Automation;

namespace DisplaySettings.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "DisplaySettings")]
    public class GetDisplaySettingsCommand : Cmdlet
    {
        #region properties

        [Parameter(HelpMessage =
           "provide this parameter to show a complete list of available screen settings; " +
           "otherwise only the current screen setting is reported.")]
        public SwitchParameter All { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "The device number")]
        [ValidateRange(0, (int)byte.MaxValue)]
        public int Device { get; set; }

        public GetDisplaySettingsCommand()
        {
            this.Device = -1; // use default
        }

        #endregion

        #region processing

        protected override void ProcessRecord()
        {
            var deviceNumber= this.Device;
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

            if (this.All == true)
            {
                var list = DisplayLogic.QueryAllDisplaySettings(deviceName).ToOutput(screen);
                foreach (var entry in list)
                    WriteObject(entry);
            }
            else
            {
                var output= DisplayLogic.QueryCurrentDisplaySettings(deviceName).ToOutput(screen);
                WriteObject(output);
            }
        }
     
        #endregion
    }
}
