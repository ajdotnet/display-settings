// Source: https://github.com/ajdotnet/display-settings

using System.Diagnostics;

namespace DisplaySettings.Core
{
    static partial class NativeMethods
    {
        [DebuggerDisplay("{X},{Y}")]
        public struct POINT
        {
            public int X;
            public int Y;
        }
    }
}
