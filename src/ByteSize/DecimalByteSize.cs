using System;
using System.Globalization;

namespace ByteSize
{
    /// <summary>
    /// Represents a decimal byte size value (1 KB = 1000 B).
    /// Uses 2 letter abbreviations (KB, MB, GB, TB, PB).
    /// Follows the IEC standard.
    /// </summary>
    public struct DecimalByteSize : IComparable<DecimalByteSize>, IEquatable<DecimalByteSize>
    {
        public static readonly DecimalByteSize MaxValue = DecimalByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8;
        public const long BytesInKiloByte = 1_000;
        public const long BytesInMegaByte = 1_000_000;
        public const long BytesInGigaByte = 1_000_000_000;
        public const long BytesInTeraByte = 1_000_000_000_000;
        public const long BytesInPetaByte = 1_000_000_000_000_000;

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
                    return DecimalByteSize.PetaByteSymbol;

                if (Math.Abs(this.TeraBytes) >= 1)
                    return DecimalByteSize.TeraByteSymbol;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return DecimalByteSize.GigaByteSymbol;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return DecimalByteSize.MegaByteSymbol;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return DecimalByteSize.KiloByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return DecimalByteSize.ByteSymbol;

                return DecimalByteSize.BitSymbol;
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

        public DecimalByteSize(double bytes)
            : this()
        {
            // Get ceiling because bits are whole units
            Bits = (long)Math.Ceiling(bytes * BitsInByte);

            Bytes = bytes;
        }

        public static DecimalByteSize FromBits(long value)
        {
            return new DecimalByteSize(value / (double)BitsInByte);
        }

        public static DecimalByteSize FromBytes(double value)
        {
            return new DecimalByteSize(value);
        }

        public static DecimalByteSize FromKiloBytes(double value)
        {
            return new DecimalByteSize(value * BytesInKiloByte);
        }

        public static DecimalByteSize FromMegaBytes(double value)
        {
            return new DecimalByteSize(value * BytesInMegaByte);
        }

        public static DecimalByteSize FromGigaBytes(double value)
        {
            return new DecimalByteSize(value * BytesInGigaByte);
        }

        public static DecimalByteSize FromTeraBytes(double value)
        {
            return new DecimalByteSize(value * BytesInTeraByte);
        }

        public static DecimalByteSize FromPetaBytes(double value)
        {
            return new DecimalByteSize(value * BytesInPetaByte);
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
            if (format.IndexOf(DecimalByteSize.ByteSymbol) != -1)
                return output(this.Bytes);

            if (format.IndexOf(DecimalByteSize.BitSymbol) != -1)
                return output(this.Bits);

            return string.Format("{0} {1}", this.LargestWholeNumberValue.ToString(format, provider), this.LargestWholeNumberSymbol);
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            DecimalByteSize other;
            if (value is DecimalByteSize)
                other = (DecimalByteSize)value;
            else
                return false;

            return Equals(other);
        }

        public bool Equals(DecimalByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(DecimalByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public DecimalByteSize Add(DecimalByteSize bs)
        {
            return new DecimalByteSize(this.Bytes + bs.Bytes);
        }

        public DecimalByteSize AddBits(long value)
        {
            return this + FromBits(value);
        }

        public DecimalByteSize AddBytes(double value)
        {
            return this + DecimalByteSize.FromBytes(value);
        }

        public DecimalByteSize AddKiloBytes(double value)
        {
            return this + DecimalByteSize.FromKiloBytes(value);
        }

        public DecimalByteSize AddMegaBytes(double value)
        {
            return this + DecimalByteSize.FromMegaBytes(value);
        }

        public DecimalByteSize AddGigaBytes(double value)
        {
            return this + DecimalByteSize.FromGigaBytes(value);
        }

        public DecimalByteSize AddTeraBytes(double value)
        {
            return this + DecimalByteSize.FromTeraBytes(value);
        }

        public DecimalByteSize AddPetaBytes(double value)
        {
            return this + DecimalByteSize.FromPetaBytes(value);
        }

        public DecimalByteSize Subtract(DecimalByteSize bs)
        {
            return new DecimalByteSize(this.Bytes - bs.Bytes);
        }

        public static DecimalByteSize operator +(DecimalByteSize b1, DecimalByteSize b2)
        {
            return new DecimalByteSize(b1.Bytes + b2.Bytes);
        }

        public static DecimalByteSize operator ++(DecimalByteSize b)
        {
            return new DecimalByteSize(b.Bytes + 1);
        }

        public static DecimalByteSize operator -(DecimalByteSize b)
        {
            return new DecimalByteSize(-b.Bytes);
        }

        public static DecimalByteSize operator -(DecimalByteSize b1, DecimalByteSize b2)
        {
            return new DecimalByteSize(b1.Bytes - b2.Bytes);
        }

        public static DecimalByteSize operator --(DecimalByteSize b)
        {
            return new DecimalByteSize(b.Bytes - 1);
        }

        public static bool operator ==(DecimalByteSize b1, DecimalByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(DecimalByteSize b1, DecimalByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(DecimalByteSize b1, DecimalByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(DecimalByteSize b1, DecimalByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(DecimalByteSize b1, DecimalByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(DecimalByteSize b1, DecimalByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        public static DecimalByteSize Parse(string s)
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

        public static bool TryParse(string s, out DecimalByteSize result)
        {
            try 
            {
                result = Parse(s);
                return true;
            }
            catch
            {
                result = new DecimalByteSize();
                return false;
            }
        }
    }
}
