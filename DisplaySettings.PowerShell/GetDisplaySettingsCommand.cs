// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
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

        #endregion

        #region processing

        protected override void ProcessRecord()
        {
            if (this.All == true)
            {
                var list = Display.QueryAllDisplaySettings();
                foreach (DisplayData data in list)
                    WriteObject(data);
            }
            else
            {
                var current = Display.QueryCurrentDisplaySettings();
                WriteObject(current);
            }
        }

        #endregion
    }
}
