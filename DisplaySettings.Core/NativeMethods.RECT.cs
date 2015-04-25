// Source: https://github.com/ajdotnet/display-settings

using System.Diagnostics;

namespace DisplaySettings.Core
{
    static partial class NativeMethods
    {
        [DebuggerDisplay("{Left},{Top} / {Right},{Bottom}")]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
