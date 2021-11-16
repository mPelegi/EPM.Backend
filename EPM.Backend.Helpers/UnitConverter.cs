using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Helpers
{
    public class UnitConverter
    {
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string ToConvert(Int64 value)
        {
            if (value < 0)
            {
                return "-" + ToConvert(-value);
            }

            if (value == 0)
            {
                return "0.0 bytes";
            }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public static string ToConvert(UInt64 value)
        {
            if (value == 0)
            {
                return "0.0 bytes";
            }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public static decimal ByteToGigabyte(UInt64 value)
        {
            if (value == 0)
            {
                return 0m;
            }

            return Math.Round((value / 1073741824m), 1);
        }

        public static decimal ByteToMegabyte(UInt64 value)
        {
            if (value == 0)
            {
                return 0m;
            }

            return Math.Round((value / 1048576m), 1);
        }

        public static decimal KilobyteToMegabyte(UInt64 value)
        {
            if (value == 0)
            {
                return 0m;
            }

            return Math.Round((value / 1024m), 1);
        }

        public static decimal KilobyteToGigabyte(UInt64 value)
        {
            if (value == 0)
            {
                return 0m;
            }

            return Math.Round((value / 1048576m), 1);
        }

        public static decimal MHzToGHz(UInt32 value)
        {
            if (value == 0)
            {
                return 0m;
            }

            return Math.Round((value / 1000m), 1);
        }
    }
}
