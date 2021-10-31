namespace ByteSizeLib
{
    public partial struct ByteSize
    {
        public const long BytesInKiloByte = 1_000;
        public const long BytesInMegaByte = 1_000_000;
        public const long BytesInGigaByte = 1_000_000_000;
        public const long BytesInTeraByte = 1_000_000_000_000;
        public const long BytesInPetaByte = 1_000_000_000_000_000;

        public const string KiloByteSymbol = "KB";
        public const string MegaByteSymbol = "MB";
        public const string GigaByteSymbol = "GB";
        public const string TeraByteSymbol = "TB";
        public const string PetaByteSymbol = "PB";

        public double KiloBytes => Bytes / BytesInKiloByte;
        public double MegaBytes => Bytes / BytesInMegaByte;
        public double GigaBytes => Bytes / BytesInGigaByte;
        public double TeraBytes => Bytes / BytesInTeraByte;
        public double PetaBytes => Bytes / BytesInPetaByte;

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of kilobytes (1 KB = 1000 B).</param>
        public static ByteSize FromKiloBytes(double value)
        {
            return new ByteSize(value * BytesInKiloByte);
        }

        /// <inheritdoc [cref="ByteSize.FromKiloBytes"]
        /// <param name="value">Number of megabytes (1 MB = 1000 KB).</param>
        public static ByteSize FromMegaBytes(double value)
        {
            return new ByteSize(value * BytesInMegaByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of gigabytes (1 GB = 1000 MB).</param>
        public static ByteSize FromGigaBytes(double value)
        {
            return new ByteSize(value * BytesInGigaByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of terabytes (1 TB = 1000 GB).</param>
        public static ByteSize FromTeraBytes(double value)
        {
            return new ByteSize(value * BytesInTeraByte);
        }

        /// <summary>
        /// Initializes a new instance of the ByteSize structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="value">Number of terabytes (1 PB = 1000 TB).</param>
        public static ByteSize FromPetaBytes(double value)
        {
            return new ByteSize(value * BytesInPetaByte);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of kilobytes (1 KB = 1000 B).</param>
        public ByteSize AddKiloBytes(double value)
        {
            return this + ByteSize.FromKiloBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of megabytes (1 MB = 1000 KB).</param>
        public ByteSize AddMegaBytes(double value)
        {
            return this + ByteSize.FromMegaBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of gigabytes (1 GB = 1000 MB).</param>
        public ByteSize AddGigaBytes(double value)
        {
            return this + ByteSize.FromGigaBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of terabytes (1 TB = 1000 GB).</param>
        public ByteSize AddTeraBytes(double value)
        {
            return this + ByteSize.FromTeraBytes(value);
        }

        /// <summary>
        /// Returns a new ByteSize object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="value">Number of petabytes (1 PB = 1000 TB).</param>
        public ByteSize AddPetaBytes(double value)
        {
            return this + ByteSize.FromPetaBytes(value);
        }
    }
}
