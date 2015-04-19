// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using System.Management.Automation;

namespace DisplaySettings.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "DisplayDevices")]
    public class GetDisplayDevicesCommand : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var list = DisplayLogic.QueryDeviceScreens().ToOutput();
            foreach (var entry in list)
                WriteObject(entry);
        }
    }
}
