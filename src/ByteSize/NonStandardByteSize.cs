using System;
using System.Globalization;

namespace ByteSize
{
    /// <summary>
    /// Represents a non-standard byte size value (1 KB = 1024 B).
    /// Uses 2 letter abbreviations (KB, MB, GB, TB, PB).
    /// Does NOT follow the IEC standard.
    /// See <see cref="BinaryByteSize"/> and <see cref="DecimalByteSize"/>
    /// </summary>
    public struct NonStandardByteSize : IComparable<NonStandardByteSize>, IEquatable<NonStandardByteSize>
    {
        public static readonly NonStandardByteSize MaxValue = NonStandardByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8;
        public const long BytesInKiloByte = 1_024;
        public const long BytesInMegaByte = 1_048_576;
        public const long BytesInGigaByte = 1_073_741_824;
        public const long BytesInTeraByte = 1_099_511_627_776;
        public const long BytesInPetaByte = 1_125_899_906_842_624;

        public const string BitSymbol = "b";
        public const string ByteSymbol = "B";
        public const string KiloByteSymbol = "KB";
        public const string MegaByteSymbol = "MB";
        public const string GigaByteSymbol = "GB";
        public const string TeraByteSymbol = "TB";
        public const string PetaByteSymbol = "PB";

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KiloBytes => Bytes / BytesInKiloByte;
        public double MegaBytes => Bytes / BytesInMegaByte;
        public double GigaBytes => Bytes / BytesInGigaByte;
        public double TeraBytes => Bytes / BytesInTeraByte;
        public double PetaBytes => Bytes / BytesInPetaByte;

        public string LargestWholeNumberSymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.PetaBytes) >= 1)
                    return NonStandardByteSize.PetaByteSymbol;

                if (Math.Abs(this.TeraBytes) >= 1)
                    return NonStandardByteSize.TeraByteSymbol;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return NonStandardByteSize.GigaByteSymbol;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return NonStandardByteSize.MegaByteSymbol;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return NonStandardByteSize.KiloByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return NonStandardByteSize.ByteSymbol;

                return NonStandardByteSize.BitSymbol;
            }
        }

        public double LargestWholeNumberValue
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.PetaBytes) >= 1)
                    return this.PetaBytes;

                if (Math.Abs(this.TeraBytes) >= 1)
                    return this.TeraBytes;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return this.GigaBytes;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return this.MegaBytes;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return this.KiloBytes;

                if (Math.Abs(this.Bytes) >= 1)
                    return this.Bytes;

                return this.Bits;
            }
        }

        public NonStandardByteSize(double bytes)
            : this()
        {
            // Get ceiling because bits are whole units
            Bits = (long)Math.Ceiling(bytes * BitsInByte);

            Bytes = bytes;
        }

        public static NonStandardByteSize FromBits(long value)
        {
            return new NonStandardByteSize(value / (double)BitsInByte);
        }

        public static NonStandardByteSize FromBytes(double value)
        {
            return new NonStandardByteSize(value);
        }

        public static NonStandardByteSize FromKiloBytes(double value)
        {
            return new NonStandardByteSize(value * BytesInKiloByte);
        }

        public static NonStandardByteSize FromMegaBytes(double value)
        {
            return new NonStandardByteSize(value * BytesInMegaByte);
        }

        public static NonStandardByteSize FromGigaBytes(double value)
        {
            return new NonStandardByteSize(value * BytesInGigaByte);
        }

        public static NonStandardByteSize FromTeraBytes(double value)
        {
            return new NonStandardByteSize(value * BytesInTeraByte);
        }

        public static NonStandardByteSize FromPetaBytes(double value)
        {
            return new NonStandardByteSize(value * BytesInPetaByte);
        }

        /// <summary>
        /// Converts the value of the current object to a string.
        /// The prefix symbol (bit, byte, kilo, mega, giga, tera) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// </summary>
        public override string ToString()
        {
            return this.ToString("0.##", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (!format.Contains("#") && !format.Contains("0"))
                format = "0.## " + format;

            if (provider == null) provider = CultureInfo.CurrentCulture;

            Func<string, bool> has = s => format.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1;
            Func<double, string> output = n => n.ToString(format, provider);

            if (has("PB"))
                return output(this.PetaBytes);
            if (has("TB"))
                return output(this.TeraBytes);
            if (has("GB"))
                return output(this.GigaBytes);
            if (has("MB"))
                return output(this.MegaBytes);
            if (has("KB"))
                return output(this.KiloBytes);

            // Byte and Bit symbol must be case-sensitive
            if (format.IndexOf(NonStandardByteSize.ByteSymbol) != -1)
                return output(this.Bytes);

            if (format.IndexOf(NonStandardByteSize.BitSymbol) != -1)
                return output(this.Bits);

            return string.Format("{0} {1}", this.LargestWholeNumberValue.ToString(format, provider), this.LargestWholeNumberSymbol);
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            NonStandardByteSize other;
            if (value is NonStandardByteSize)
                other = (NonStandardByteSize)value;
            else
                return false;

            return Equals(other);
        }

        public bool Equals(NonStandardByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(NonStandardByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public NonStandardByteSize Add(NonStandardByteSize bs)
        {
            return new NonStandardByteSize(this.Bytes + bs.Bytes);
        }

        public NonStandardByteSize AddBits(long value)
        {
            return this + FromBits(value);
        }

        public NonStandardByteSize AddBytes(double value)
        {
            return this + NonStandardByteSize.FromBytes(value);
        }

        public NonStandardByteSize AddKiloBytes(double value)
        {
            return this + NonStandardByteSize.FromKiloBytes(value);
        }

        public NonStandardByteSize AddMegaBytes(double value)
        {
            return this + NonStandardByteSize.FromMegaBytes(value);
        }

        public NonStandardByteSize AddGigaBytes(double value)
        {
            return this + NonStandardByteSize.FromGigaBytes(value);
        }

        public NonStandardByteSize AddTeraBytes(double value)
        {
            return this + NonStandardByteSize.FromTeraBytes(value);
        }

        public NonStandardByteSize AddPetaBytes(double value)
        {
            return this + NonStandardByteSize.FromPetaBytes(value);
        }

        public NonStandardByteSize Subtract(NonStandardByteSize bs)
        {
            return new NonStandardByteSize(this.Bytes - bs.Bytes);
        }

        public static NonStandardByteSize operator +(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return new NonStandardByteSize(b1.Bytes + b2.Bytes);
        }

        public static NonStandardByteSize operator ++(NonStandardByteSize b)
        {
            return new NonStandardByteSize(b.Bytes + 1);
        }

        public static NonStandardByteSize operator -(NonStandardByteSize b)
        {
            return new NonStandardByteSize(-b.Bytes);
        }

        public static NonStandardByteSize operator -(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return new NonStandardByteSize(b1.Bytes - b2.Bytes);
        }

        public static NonStandardByteSize operator --(NonStandardByteSize b)
        {
            return new NonStandardByteSize(b.Bytes - 1);
        }

        public static bool operator ==(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(NonStandardByteSize b1, NonStandardByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        public static NonStandardByteSize Parse(string s)
        {
            // Arg checking
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("s", "String is null or whitespace");

            // Get the index of the first non-digit character
            s = s.TrimStart(); // Protect against leading spaces

            var num = 0;
            var found = false;

            var decimalSeparator = Convert.ToChar(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
            var groupSeparator = Convert.ToChar(NumberFormatInfo.CurrentInfo.NumberGroupSeparator);

            // Pick first non-digit number
            for (num = 0; num < s.Length; num++)
                if (!(char.IsDigit(s[num]) || s[num] == decimalSeparator || s[num] == groupSeparator))
                {
                    found = true;
                    break;
                }

            if (found == false)
                throw new FormatException($"No byte indicator found in value '{ s }'.");

            int lastNumber = num;

            // Cut the input string in half
            string numberPart = s.Substring(0, lastNumber).Trim();
            string sizePart = s.Substring(lastNumber, s.Length - lastNumber).Trim();

            // Get the numeric part
            double number;
            if (!double.TryParse(numberPart, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out number))
                throw new FormatException($"No number found in value '{ s }'.");

            // Get the magnitude part
            switch (sizePart)
            {
                case "b":
                    if (number % 1 != 0) // Can't have partial bits
                        throw new FormatException($"Can't have partial bits for value '{ s }'.");

                    return FromBits((long)number);

                case "B":
                    return FromBytes(number);
            }

            switch (sizePart.ToLowerInvariant())
            {
                case "kb":
                    return FromKiloBytes(number);

                case "mb":
                    return FromMegaBytes(number);

                case "gb":
                    return FromGigaBytes(number);

                case "tb":
                    return FromTeraBytes(number);

                case "pb":
                    return FromPetaBytes(number);
                
                default:
                    throw new FormatException($"Bytes of magnitude '{ sizePart }' is not supported.");
            }
        }

        public static bool TryParse(string s, out NonStandardByteSize result)
        {
            try 
            {
                result = Parse(s);
                return true;
            }
            catch
            {
                result = new NonStandardByteSize();
                return false;
            }
        }
    }
}
