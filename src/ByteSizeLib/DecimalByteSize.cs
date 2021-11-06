namespace ByteSizeLib
{
    public partial struct ByteSize
    {
        /// <summary>Number of bytes in 1 kilobyte.</summary>
        public const long BytesInKiloByte = 1_000;
        
        /// <summary>Number of bytes in 1 megabyte.</summary>
        public const long BytesInMegaByte = 1_000_000;
        
        /// <summary>Number of bytes in 1 gigabyte.</summary>
        public const long BytesInGigaByte = 1_000_000_000;
        
        /// <summary>Number of bytes in 1 terabyte.</summary>
        public const long BytesInTeraByte = 1_000_000_000_000;
        
        /// <summary>Number of bytes in 1 petabyte.</summary>
        public const long BytesInPetaByte = 1_000_000_000_000_000;

        /// <summary>Kilobyte symbol.</summary>
        public const string KiloByteSymbol = "KB";
        
        /// <summary>Megabyte symbol.</summary>
        public const string MegaByteSymbol = "MB";
        
        /// <summary>Gigabyte symbol.</summary>
        public const string GigaByteSymbol = "GB";
        
        /// <summary>Terabyte symbol.</summary>
        public const string TeraByteSymbol = "TB";
        
        /// <summary>Petabyte symbol.</summary>
        public const string PetaByteSymbol = "PB";

        /// <summary>Gets the number of kilobytes represented by this object.</summary>
        public double KiloBytes => Bytes / BytesInKiloByte;
        /// <summary>Gets the number of megabytes represented by this object.</summary>
        public double MegaBytes => Bytes / BytesInMegaByte;
        /// <summary>Gets the number of gigabytes represented by this object.</summary>
        public double GigaBytes => Bytes / BytesInGigaByte;
        /// <summary>Gets the number of terabytes represented by this object.</summary>
        public double TeraBytes => Bytes / BytesInTeraByte;
        /// <summary>Gets the number of petabytes represented by this object.</summary>
        public double PetaBytes => Bytes / BytesInPetaByte;

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of kilobytes (1 KB = 1000 B).</param>
        public static ByteSize FromKiloBytes(double value)
        {
            return new ByteSize(value * BytesInKiloByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of megabytes (1 MB = 1000 KB).</param>
        public static ByteSize FromMegaBytes(double value)
        {
            return new ByteSize(value * BytesInMegaByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of gigabytes (1 GB = 1000 MB).</param>
        public static ByteSize FromGigaBytes(double value)
        {
            return new ByteSize(value * BytesInGigaByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of terabytes (1 TB = 1000 GB).</param>
        public static ByteSize FromTeraBytes(double value)
        {
            return new ByteSize(value * BytesInTeraByte);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of terabytes (1 PB = 1000 TB).</param>
        public static ByteSize FromPetaBytes(double value)
        {
            return new ByteSize(value * BytesInPetaByte);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of kilobytes (1 KB = 1000 B).</param>
        public ByteSize AddKiloBytes(double value)
        {
            return this + ByteSize.FromKiloBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of megabytes (1 MB = 1000 KB).</param>
        public ByteSize AddMegaBytes(double value)
        {
            return this + ByteSize.FromMegaBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of gigabytes (1 GB = 1000 MB).</param>
        public ByteSize AddGigaBytes(double value)
        {
            return this + ByteSize.FromGigaBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of terabytes (1 TB = 1000 GB).</param>
        public ByteSize AddTeraBytes(double value)
        {
            return this + ByteSize.FromTeraBytes(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">Number of petabytes (1 PB = 1000 TB).</param>
        public ByteSize AddPetaBytes(double value)
        {
            return this + ByteSize.FromPetaBytes(value);
        }
    }
}
