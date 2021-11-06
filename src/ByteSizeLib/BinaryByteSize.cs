using System;
using System.Globalization;

namespace ByteSizeLib
{
    public partial struct ByteSize
    {
        /// <summary>Number of bytes in 1 kibibyte.</summary>
        public const long BytesInKibiByte = 1_024;

        /// <summary>Number of bytes in 1 mebibyte.</summary>
        public const long BytesInMebiByte = 1_048_576;
        
        /// <summary>Number of bytes in 1 gibibyte.</summary>
        public const long BytesInGibiByte = 1_073_741_824;
        
        /// <summary>Number of bytes in 1 tebibyte.</summary>
        public const long BytesInTebiByte = 1_099_511_627_776;
        
        /// <summary>Number of bytes in 1 pebibyte.</summary>
        public const long BytesInPebiByte = 1_125_899_906_842_624;

        /// <summary>Kibibyte symbol.</summary>
        public const string KibiByteSymbol = "KiB";
        
        /// <summary>Mebibyte symbol.</summary>
        public const string MebiByteSymbol = "MiB";
        
        /// <summary>Gibibyte symbol.</summary>
        public const string GibiByteSymbol = "GiB";
        
        /// <summary>Tebibyte symbol.</summary>
        public const string TebiByteSymbol = "TiB";
        
        /// <summary>Pebibyte symbol.</summary>
        public const string PebiByteSymbol = "PiB";

        /// <summary>Gets the number of kibibytes represented by this object.</summary>
        public double KibiBytes => Bytes / BytesInKibiByte;
        
        /// <summary>Gets the number of mebibytes represented by this object.</summary>
        public double MebiBytes => Bytes / BytesInMebiByte;
        
        /// <summary>Gets the number of gibibytes represented by this object.</summary>
        public double GibiBytes => Bytes / BytesInGibiByte;
        
        /// <summary>Gets the number of tebibytes represented by this object.</summary>
        public double TebiBytes => Bytes / BytesInTebiByte;
        
        /// <summary>Gets the number of pebibytes represented by this object.</summary>
        public double PebiBytes => Bytes / BytesInPebiByte;

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of kibibytes (1 KiB = 1024 B).</param>
        public static ByteSize FromKibiBytes(double value)
        {
            return new ByteSize(value * BytesInKibiByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of mebibytes (1 MiB = 1024 KiB).</param>
        public static ByteSize FromMebiBytes(double value)
        {
            return new ByteSize(value * BytesInMebiByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of gibibytes (1 GiB = 1024 MiB).</param>
        public static ByteSize FromGibiBytes(double value)
        {
            return new ByteSize(value * BytesInGibiByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of tebibytes (1 TiB = 1024 GiB).</param>
        public static ByteSize FromTebiBytes(double value)
        {
            return new ByteSize(value * BytesInTebiByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of pebibytes (1 PiB = 1024 TiB).</param>
        public static ByteSize FromPebiBytes(double value)
        {
            return new ByteSize(value * BytesInPebiByte);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of kibibytes (1 KiB = 1024 B).</param>
        public ByteSize AddKibiBytes(double value)
        {
            return this + ByteSize.FromKibiBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of mebibytes (1 MiB = 1024 KiB).</param>
        public ByteSize AddMebiBytes(double value)
        {
            return this + ByteSize.FromMebiBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of gibibytes (1 GiB = 1024 MiB).</param>
        public ByteSize AddGibiBytes(double value)
        {
            return this + ByteSize.FromGibiBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of tebibytes (1 TiB = 1024 GiB).</param>
        public ByteSize AddTebiBytes(double value)
        {
            return this + ByteSize.FromTebiBytes(value);
        }

        /// <inheritdoc cref="Add" />
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

        /// <summary>
        /// Converts the value of the current object to a binary byte string.
        /// The prefix symbol (bit, byte, kibi, mebi, etc.) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// Use <see cref="ByteSize.ToString()"/> for decimal string representation.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        public string ToBinaryString(IFormatProvider formatProvider)
        {
            return this.ToString("0.##", formatProvider, useBinaryByte: true);
        }
    }
}
