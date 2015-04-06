// Source: https://github.com/ajdotnet/display-settings

using DisplaySettings.Core;
using System;
using System.Linq;
using System.Management.Automation;

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

        public SetDisplaySettingsCommand()
        {
            ColorDepth = -1;
            Frequency = -1;
        }

        #endregion

        #region processing

        protected override void ProcessRecord()
        {
            var current = Display.QueryCurrentDisplaySettings();
            current.Width = this.Width;
            current.Height = this.Height;
            current.ColorDepth = this.ColorDepth < 0 ? current.ColorDepth : this.ColorDepth;
            current.ScreenRefreshRate = this.Frequency < 0 ? current.ScreenRefreshRate : this.Frequency;

            var all = Display.QueryAllDisplaySettings();
            var found = all.Where(dd => DisplayData.AreEqual(dd, current)).FirstOrDefault();
            if (found == null)
                throw new NotSupportedException("No matching display setting found.");

            // check the -whatif parameter
            string msg = "Display settings " + found.DisplayText;
            if (!ShouldProcess(msg))
                return;

            // check the users intention
            if (!this.Force)
            {
                msg = "Set display settings to " + found.DisplayText + "?";
                if (!ShouldContinue(msg, "Warning!"))
                    return;
            }

            // and finally go
            string err;
            if (Display.ChangeSettings(found, out err) != 0)
                throw new NotSupportedException("Error setting values: " + err);
            if (err != null)
            {
                WriteWarning("Error setting values: " + err);
            }
            else
            {
                msg = "Display settings set to " + found.DisplayText;
                WriteVerbose(msg);
            }
        }
    
        #endregion
    }
}
