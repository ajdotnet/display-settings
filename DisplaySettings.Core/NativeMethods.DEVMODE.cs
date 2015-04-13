// Source: https://github.com/ajdotnet/display-settings

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace DisplaySettings.Core
{
    static partial class NativeMethods
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct DEVMODE_UnionA
        {
            // union a
            [FieldOffsetAttribute(0)]
            public short dmOrientation;
            [FieldOffsetAttribute(2)]
            public short dmPaperSize;
            [FieldOffsetAttribute(4)]
            public short dmPaperLength;
            [FieldOffsetAttribute(6)]
            public short dmPaperWidth;
            [FieldOffsetAttribute(8)]
            public short dmScale;
            [FieldOffsetAttribute(10)]
            public short dmCopies;
            [FieldOffsetAttribute(12)]
            public short dmDefaultSource;
            [FieldOffsetAttribute(14)]
            public short dmPrintQuality;

            // union b
            [FieldOffsetAttribute(0)]
            public int dmPositionX;
            [FieldOffsetAttribute(4)]
            public int dmPositionY;
            [FieldOffsetAttribute(8)]
            public uint dmDisplayOrientation;
            [FieldOffsetAttribute(12)]
            public uint dmDisplayFixedOutput;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DEVMODE_UnionB
        {
            [FieldOffsetAttribute(0)]
            public DmDisplayFlags dmDisplayFlags;
            [FieldOffsetAttribute(0)]
            public uint dmNup;
        }


        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd183565(v=vs.85).aspx
        [SuppressMessage("Microsoft.Portability", "CA1900:ValueTypeFieldsShouldBePortable", MessageId = "dmFormName")]
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            public const int CCHDEVICENAME = 32;
            public const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public ushort dmSpecVersion;
            public ushort dmDriverVersion;
            public ushort dmSize;
            public ushort dmDriverExtra;
            public DmFields dmFields;

            //// union a
            //public short dmOrientation;
            //public short dmPaperSize;
            //public short dmPaperLength;
            //public short dmPaperWidth;
            //public short dmScale;
            //public short dmCopies;
            //public short dmDefaultSource;
            //public short dmPrintQuality;
            //// union b
            //public int dmPositionX;
            //public int dmPositionY;
            //public uint dmDisplayOrientation;
            //public uint dmDisplayFixedOutput;
            public DEVMODE_UnionA UnionA;

            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public ushort dmLogPixels;
            public uint dmBitsPerPel;
            public uint dmPelsWidth;
            public uint dmPelsHeight;

            //public DmDisplayFlags dmDisplayFlags;   // union a
            //public uint dmNup;                      // union b
            public DEVMODE_UnionB UnionB;

            public uint dmDisplayFrequency;

            public uint dmICMMethod;
            public uint dmICMIntent;
            public uint dmMediaType;
            public uint dmDitherType;
            public uint dmReserved1;
            public uint dmReserved2;
            public uint dmPanningWidth;
            public uint dmPanningHeight;
        }

        [Flags]
        public enum DmFields : uint
        {
            DM_ORIENTATION = 0x00000001,
            DM_PAPERSIZE = 0x00000002,
            DM_PAPERLENGTH = 0x00000004,
            DM_PAPERWIDTH = 0x00000008,
            DM_SCALE = 0x00000010,
            DM_POSITION = 0x00000020,
            DM_NUP = 0x00000040,
            DM_DISPLAYORIENTATION = 0x00000080,
            DM_COPIES = 0x00000100,
            DM_DEFAULTSOURCE = 0x00000200,
            DM_PRINTQUALITY = 0x00000400,
            DM_COLOR = 0x00000800,
            DM_DUPLEX = 0x00001000,
            DM_YRESOLUTION = 0x00002000,
            DM_TTOPTION = 0x00004000,
            DM_COLLATE = 0x00008000,
            DM_FORMNAME = 0x00010000,
            DM_LOGPIXELS = 0x00020000,
            DM_BITSPERPEL = 0x00040000,
            DM_PELSWIDTH = 0x00080000,
            DM_PELSHEIGHT = 0x00100000,
            DM_DISPLAYFLAGS = 0x00200000,
            DM_DISPLAYFREQUENCY = 0x00400000,
            DM_ICMMETHOD = 0x00800000,
            DM_ICMINTENT = 0x01000000,
            DM_MEDIATYPE = 0x02000000,
            DM_DITHERTYPE = 0x04000000,
            DM_PANNINGWIDTH = 0x08000000,
            DM_PANNINGHEIGHT = 0x10000000,
            DM_DISPLAYFIXEDOUTPUT = 0x20000000
        }

        public enum DmDisplayFlags : uint
        {
            DM_COLOR_NONINTERLACED = 0,
            DM_GRAYSCALE = 0x00000001, /* This flag is no longer valid */
            DM_INTERLACED = 0x00000002
        }

    }
}
