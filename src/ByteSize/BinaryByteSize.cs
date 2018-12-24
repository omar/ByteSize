using System;
using System.Globalization;

namespace ByteSize
{
    /// <summary>
    /// Represents a binary byte size value (1 KiB = 1024 B).
    /// Uses 3 letter abbreviations (KiB, MiB, GiB, TiB, PiB).
    /// Follows the IEC standard.
    /// </summary>
    public struct BinaryByteSize : IComparable<BinaryByteSize>, IEquatable<BinaryByteSize>
    {         
        public static readonly BinaryByteSize MinValue = BinaryByteSize.FromBits(long.MinValue);
        public static readonly BinaryByteSize MaxValue = BinaryByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8;
        public const long BytesInKibiByte = 1_024;
        public const long BytesInMebiByte = 1_048_576;
        public const long BytesInGibiByte = 1_073_741_824;
        public const long BytesInTebiByte = 1_099_511_627_776;
        public const long BytesInPebiByte = 1_125_899_906_842_624;

        public const string BitSymbol = "b";
        public const string ByteSymbol = "B";
        public const string KibiByteSymbol = "KiB";
        public const string MebiByteSymbol = "MiB";
        public const string GibiByteSymbol = "GiB";
        public const string TebiByteSymbol = "TiB";
        public const string PebiByteSymbol = "PiB";

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KibiBytes => Bytes / BytesInKibiByte;
        public double MebiBytes => Bytes / BytesInMebiByte;
        public double GibiBytes => Bytes / BytesInGibiByte;
        public double TebiBytes => Bytes / BytesInTebiByte;
        public double PebiBytes => Bytes / BytesInPebiByte;

        public string LargestWholeNumberSymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.PebiBytes) >= 1)
                    return BinaryByteSize.PebiByteSymbol;

                if (Math.Abs(this.TebiBytes) >= 1)
                    return BinaryByteSize.TebiByteSymbol;

                if (Math.Abs(this.GibiBytes) >= 1)
                    return BinaryByteSize.GibiByteSymbol;

                if (Math.Abs(this.MebiBytes) >= 1)
                    return BinaryByteSize.MebiByteSymbol;

                if (Math.Abs(this.KibiBytes) >= 1)
                    return BinaryByteSize.KibiByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return BinaryByteSize.ByteSymbol;

                return BinaryByteSize.BitSymbol;
            }
        }

        public double LargestWholeNumberValue
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.PebiBytes) >= 1)
                    return this.PebiBytes;

                if (Math.Abs(this.TebiBytes) >= 1)
                    return this.TebiBytes;

                if (Math.Abs(this.GibiBytes) >= 1)
                    return this.GibiBytes;

                if (Math.Abs(this.MebiBytes) >= 1)
                    return this.MebiBytes;

                if (Math.Abs(this.KibiBytes) >= 1)
                    return this.KibiBytes;

                if (Math.Abs(this.Bytes) >= 1)
                    return this.Bytes;

                return this.Bits;
            }
        }

        public BinaryByteSize(double BinaryByteSize)
            : this()
        {
            // Get ceiling because bits are whole units
            Bits = (long)Math.Ceiling(BinaryByteSize * BitsInByte);

            Bytes = BinaryByteSize;
        }

        public static BinaryByteSize FromBits(long value)
        {
            return new BinaryByteSize(value / (double)BitsInByte);
        }

        public static BinaryByteSize FromBytes(double value)
        {
            return new BinaryByteSize(value);
        }

        public static BinaryByteSize FromKibiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInKibiByte);
        }

        public static BinaryByteSize FromMebiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInMebiByte);
        }

        public static BinaryByteSize FromGibiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInGibiByte);
        }

        public static BinaryByteSize FromTebiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInTebiByte);
        }

        public static BinaryByteSize FromPebiBytes(double value)
        {
            return new BinaryByteSize(value * BytesInPebiByte);
        }

        /// <summary>
        /// Converts the value of the current object to a string.
        /// The prefix symbol (bit, byte, kilo, mebi, gibi, tebi) used is the
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

            if (has("PiB"))
                return output(this.PebiBytes);
            if (has("TiB"))
                return output(this.TebiBytes);
            if (has("GiB"))
                return output(this.GibiBytes);
            if (has("MiB"))
                return output(this.MebiBytes);
            if (has("KiB"))
                return output(this.KibiBytes);

            // Byte and Bit symbol must be case-sensitive
            if (format.IndexOf(BinaryByteSize.ByteSymbol) != -1)
                return output(this.Bytes);

            if (format.IndexOf(BinaryByteSize.BitSymbol) != -1)
                return output(this.Bits);

            return string.Format("{0} {1}", this.LargestWholeNumberValue.ToString(format, provider), this.LargestWholeNumberSymbol);
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            BinaryByteSize other;
            if (value is BinaryByteSize)
                other = (BinaryByteSize)value;
            else
                return false;

            return Equals(other);
        }

        public bool Equals(BinaryByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(BinaryByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public BinaryByteSize Add(BinaryByteSize bs)
        {
            return new BinaryByteSize(this.Bytes + bs.Bytes);
        }

        public BinaryByteSize AddBits(long value)
        {
            return this + FromBits(value);
        }

        public BinaryByteSize AddBytes(double value)
        {
            return this + BinaryByteSize.FromBytes(value);
        }

        public BinaryByteSize AddKibiBytes(double value)
        {
            return this + BinaryByteSize.FromKibiBytes(value);
        }

        public BinaryByteSize AddMebiBytes(double value)
        {
            return this + BinaryByteSize.FromMebiBytes(value);
        }

        public BinaryByteSize AddGibiBytes(double value)
        {
            return this + BinaryByteSize.FromGibiBytes(value);
        }

        public BinaryByteSize AddTebiBytes(double value)
        {
            return this + BinaryByteSize.FromTebiBytes(value);
        }

        public BinaryByteSize AddPebiBytes(double value)
        {
            return this + BinaryByteSize.FromPebiBytes(value);
        }

        public BinaryByteSize Subtract(BinaryByteSize bs)
        {
            return new BinaryByteSize(this.Bytes - bs.Bytes);
        }

        public static BinaryByteSize operator +(BinaryByteSize b1, BinaryByteSize b2)
        {
            return new BinaryByteSize(b1.Bytes + b2.Bytes);
        }

        public static BinaryByteSize operator ++(BinaryByteSize b)
        {
            return new BinaryByteSize(b.Bytes + 1);
        }

        public static BinaryByteSize operator -(BinaryByteSize b)
        {
            return new BinaryByteSize(-b.Bytes);
        }

        public static BinaryByteSize operator -(BinaryByteSize b1, BinaryByteSize b2)
        {
            return new BinaryByteSize(b1.Bytes - b2.Bytes);
        }

        public static BinaryByteSize operator --(BinaryByteSize b)
        {
            return new BinaryByteSize(b.Bytes - 1);
        }

        public static bool operator ==(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(BinaryByteSize b1, BinaryByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        public static BinaryByteSize Parse(string s)
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
                case "kib":
                    return FromKibiBytes(number);

                case "mib":
                    return FromMebiBytes(number);

                case "gib":
                    return FromGibiBytes(number);

                case "tib":
                    return FromTebiBytes(number);

                case "pib":
                    return FromPebiBytes(number);
                
                default:
                    throw new FormatException($"Bytes of magnitude '{ sizePart }' is not supported.");
            }
        }

        public static bool TryParse(string s, out BinaryByteSize result)
        {
            try 
            {
                result = Parse(s);
                return true;
            }
            catch
            {
                result = new BinaryByteSize();
                return false;
            }
        }
    }
}
