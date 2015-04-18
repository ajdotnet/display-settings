using System.Diagnostics;

namespace DisplaySettings.Core
{
    // kombination of adapter + monitor
    // Monitor.DeviceString is what is usually recoginzed, but Adapter is relevant for API calls
    [DebuggerDisplay("#{Number} {Adapter.DeviceString} => {Monitor.DeviceString}, IsPrimary={IsPrimary}")]
    public class DeviceScreen
    {
        public DeviceData Adapter { get; private set; }
        public DeviceData Monitor { get; private set; }

        public int Number { get; set; }
        public bool IsPrimary { get { return Adapter.IsPrimary; } }

        public DeviceScreen(DeviceData adapter, DeviceData monitor)
        {
            this.Adapter = adapter;
            this.Monitor = monitor;
        }
    }
}
