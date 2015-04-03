// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using System.Management.Automation;

namespace DisplaySettingsPS
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// - parameter all: SwitchParameter as datatype
    /// - output formatting
    ///     - format datei wird bei "interactive" verwendet, nicht im script
    ///     - "format-list * " tut im script auch nicht
    /// </remarks>
    [Cmdlet(VerbsCommon.Get, "DisplaySettings")]
    public class GetDisplaySettingsCommand : PSCmdlet
    {
        [Parameter(HelpMessage =
           "provide this parameter to show a complete list of available screen settings; " +
           "otherwise only the current screen setting is reported.")]
        public SwitchParameter All { get; set; }

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
    }
}
