using System;
using System.Globalization;

namespace ByteSizeLib
{
    public partial struct ByteSize
    {
        public const long BytesInKibiByte = 1_024;
        public const long BytesInMebiByte = 1_048_576;
        public const long BytesInGibiByte = 1_073_741_824;
        public const long BytesInTebiByte = 1_099_511_627_776;
        public const long BytesInPebiByte = 1_125_899_906_842_624;

        public const string KibiByteSymbol = "KiB";
        public const string MebiByteSymbol = "MiB";
        public const string GibiByteSymbol = "GiB";
        public const string TebiByteSymbol = "TiB";
        public const string PebiByteSymbol = "PiB";

        public double KibiBytes => Bytes / BytesInKibiByte;
        public double MebiBytes => Bytes / BytesInMebiByte;
        public double GibiBytes => Bytes / BytesInGibiByte;
        public double TebiBytes => Bytes / BytesInTebiByte;
        public double PebiBytes => Bytes / BytesInPebiByte;

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of kibibytes (1 KiB = 1024 B).</param>
        public static ByteSize FromKibiBytes(double value)
        {
            return new ByteSize(value * BytesInKibiByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of mebibytes (1 MiB = 1024 KiB).</param>
        public static ByteSize FromMebiBytes(double value)
        {
            return new ByteSize(value * BytesInMebiByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of gibibytes (1 GiB = 1024 MiB).</param>
        public static ByteSize FromGibiBytes(double value)
        {
            return new ByteSize(value * BytesInGibiByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of tebibytes (1 TiB = 1024 GiB).</param>
        public static ByteSize FromTebiBytes(double value)
        {
            return new ByteSize(value * BytesInTebiByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of pebibytes (1 PiB = 1024 TiB).</param>
        public static ByteSize FromPebiBytes(double value)
        {
            return new ByteSize(value * BytesInPebiByte);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of kibibytes (1 KiB = 1024 B).</param>
        public ByteSize AddKibiBytes(double value)
        {
            return this + ByteSize.FromKibiBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of mebibytes (1 MiB = 1024 KiB).</param>
        public ByteSize AddMebiBytes(double value)
        {
            return this + ByteSize.FromMebiBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of gibibytes (1 GiB = 1024 MiB).</param>
        public ByteSize AddGibiBytes(double value)
        {
            return this + ByteSize.FromGibiBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of tebibytes (1 TiB = 1024 GiB).</param>
        public ByteSize AddTebiBytes(double value)
        {
            return this + ByteSize.FromTebiBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of pebibytes (1 PiB = 1024 TiB).</param>
        public ByteSize AddPebiBytes(double value)
        {
            return this + ByteSize.FromPebiBytes(value);
        }

        /// <summary>
        /// Converts the value of the current object to a binary byte string.
        /// The prefix symbol (bit, byte, kibi, mebi, etc.) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// </summary>
        public string ToBinaryString()
        {
            return this.ToString("0.##", CultureInfo.CurrentCulture, useBinaryByte: true);
        }

        public string ToBinaryString(IFormatProvider formatProvider)
        {
            return this.ToString("0.##", formatProvider, useBinaryByte: true);
        }
    }
}
