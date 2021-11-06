using System;
using System.Globalization;

namespace ByteSizeLib
{
    /// <summary>
    /// Represents a byte size value with support for decimal (KiloByte) and
    /// binary values (KibiByte).
    /// </summary>
    public partial struct ByteSize : IComparable<ByteSize>, IEquatable<ByteSize>, IFormattable
    {
        /// <summary>Minimum number of allowed bits in an instance.</summary>
        public static readonly ByteSize MinValue = ByteSize.FromBits(long.MinValue);
        /// <summary>Maximum number of allowed bits in an instance.</summary>
        public static readonly ByteSize MaxValue = ByteSize.FromBits(long.MaxValue);
        /// <summary>Number of bits in 1 byte.</summary>
        public const long BitsInByte = 8;
        /// <summary>Bit symbol.</summary>
        public const string BitSymbol = "b";
        /// <summary>Byte symbol.</summary>
        public const string ByteSymbol = "B";
        /// <summary>Gets the number of bits represented by this object.</summary>
        public long Bits { get; }
        /// <summary>Gets the number of bytes represented by this object.</summary>
        public double Bytes { get; }

        /// <summary>
        /// Gets the largest whole number binary symbol.
        /// Example: 
        /// <para>- 1024 KiB will return MiB</para>
        /// <para>- 1023 KiB will return KiB</para>
        /// </summary>
        public string LargestWholeNumberBinarySymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.PebiBytes) >= 1)
                    return PebiByteSymbol;

                if (Math.Abs(this.TebiBytes) >= 1)
                    return TebiByteSymbol;

                if (Math.Abs(this.GibiBytes) >= 1)
                    return GibiByteSymbol;

                if (Math.Abs(this.MebiBytes) >= 1)
                    return MebiByteSymbol;

                if (Math.Abs(this.KibiBytes) >= 1)
                    return KibiByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return ByteSymbol;

                return BitSymbol;
            }
        }

        /// <summary>
        /// Gets the largest whole number decimal symbol.
        /// Example: 
        /// <para>- 1000 KB will return MB</para>
        /// <para>- 999 KB will return KB</para>
        /// </summary>
        public string LargestWholeNumberDecimalSymbol
        {
            get
            {
                // Absolute value is used to deal with negative values
                if (Math.Abs(this.PetaBytes) >= 1)
                    return PetaByteSymbol;

                if (Math.Abs(this.TeraBytes) >= 1)
                    return TeraByteSymbol;

                if (Math.Abs(this.GigaBytes) >= 1)
                    return GigaByteSymbol;

                if (Math.Abs(this.MegaBytes) >= 1)
                    return MegaByteSymbol;

                if (Math.Abs(this.KiloBytes) >= 1)
                    return KiloByteSymbol;

                if (Math.Abs(this.Bytes) >= 1)
                    return ByteSymbol;

                return BitSymbol;
            }
        }

        /// <summary>
        /// Gets the largest whole number binary value.
        /// Example: 
        /// <para>- 1024 KiB will return 1</para>
        /// <para>- 1023 KiB will return 1023</para>
        /// </summary>
        public double LargestWholeNumberBinaryValue
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

        /// <summary>
        /// Gets the largest whole number decimal value.
        /// Example: 
        /// <para>- 1000 KB will return 1</para>
        /// <para>- 999 KB will return 999</para>
        /// </summary>
        public double LargestWholeNumberDecimalValue
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSize" /> structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="bits">Number of bits.</param>
        public ByteSize(long bits)
            : this()
        {
            Bits = bits;

            Bytes = (double)bits / BitsInByte;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteSize" /> structure to the specified
        /// number of units.
        /// </summary>
        /// <param name="bytes">Number of bytes.</param>
        public ByteSize(double bytes)
            : this()
        {
            // Get ceiling because bits are whole units
            Bits = (long)Math.Ceiling(bytes * BitsInByte);

            Bytes = bytes;
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of bits.</param>
        public static ByteSize FromBits(long value)
        {
            return new ByteSize(value);
        }

        /// <inheritdoc cref="ByteSize.ByteSize(long)" />
        /// <param name="value">Number of bytes.</param>
        public static ByteSize FromBytes(double value)
        {
            return new ByteSize(value);
        }

        /// <summary>
        /// Converts the value of the current object to a decimal byte string.
        /// The prefix symbol (bit, byte, kilo, mega, etc.) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// Use <see cref="ByteSize.ToBinaryString()"/> for binary string representation.
        /// </summary>
        public override string ToString()
        {
            return this.ToString("0.##", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the value of the current object to a decimal byte string.
        /// The prefix symbol (bit, byte, kilo, mega, etc.) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// Use <see cref="ByteSize.ToBinaryString()"/> for binary string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the value of the current object to a decimal byte string.
        /// The prefix symbol (bit, byte, kilo, mega, etc.) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// Use <see cref="ByteSize.ToBinaryString()"/> for binary string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        public string ToString(string? format, IFormatProvider? provider)
        {
            return this.ToString(format, provider, useBinaryByte: false);
        }

        /// <summary>
        /// Converts the value of the current object to a decimal byte string.
        /// The prefix symbol (bit, byte, kilo, mega, etc.) used is the
        /// largest prefix such that the corresponding value is greater than or
        /// equal to one.
        /// Use <see cref="ByteSize.ToBinaryString()"/> for binary string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="useBinaryByte"><see langword="true"/> to use binary byte values (1 KiB = 1024) instead of decimal values (1 KB = 1000 B).</param>
        public string ToString(string? format, IFormatProvider? provider, bool useBinaryByte)
        {
            if (format != null && !format.Contains("#") && !format.Contains("0"))
                format = "0.## " + format;

            if (provider == null) provider = CultureInfo.CurrentCulture;

            Func<string, bool> has = s => format != null && format.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1;
            Func<double, string> output = n => n.ToString(format, provider);

            // Binary
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

            // Decimal
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
            if (format != null && format.IndexOf(ByteSize.ByteSymbol, StringComparison.Ordinal) != -1)
                return output(this.Bytes);

            if (format != null && format.IndexOf(ByteSize.BitSymbol, StringComparison.Ordinal) != -1)
                return output(this.Bits);
            
            if (useBinaryByte)
            {
                return string.Format("{0} {1}", this.LargestWholeNumberBinaryValue.ToString(format, provider), this.LargestWholeNumberBinarySymbol);
            }
            else
            {
                return string.Format("{0} {1}", this.LargestWholeNumberDecimalValue.ToString(format, provider), this.LargestWholeNumberDecimalSymbol);
            }
        }

        /// <summary>
        /// Indicates whether an instance has equal number of bits to this instance.
        /// </summary>
        /// <param name="value">The instance to compare.</param>
        public override bool Equals(object? value)
        {
            if (value == null)
                return false;

            ByteSize other;
            if (value is ByteSize)
                other = (ByteSize)value;
            else
                return false;

            return Equals(other);
        }

        /// <summary>
        /// Indicates whether an instance has equal number of bits to this instance.
        /// </summary>
        /// <param name="value">The instance to compare.</param>
        public bool Equals(ByteSize value)
        {
            return this.Bits == value.Bits;
        }

        /// <summary>Returns the hash code for this instance.</summary>
        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        /// <summary>
        /// Compares this instance's bit count to the other instance's bit count.
        /// </summary>
        /// <param name="other">The instance to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and other. 
        /// <para>Negative return value: this instance is less than the one specified.</para>
        /// <para>Zero return value: this instance is equal to the one specified.</para>
        /// <para>Positive return value: this instance is greater than the one specified.</para>
        /// </returns>
        public int CompareTo(ByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        /// <summary>
        /// Returns a new <see cref="ByteSize" /> object whose value is the sum of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="bs">The <see cref="ByteSize" /> instance to sum.</param>
        public ByteSize Add(ByteSize bs)
        {
            return new ByteSize(this.Bytes + bs.Bytes);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">The number of bits.</param>
        public ByteSize AddBits(long value)
        {
            return this + FromBits(value);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="value">The number of bytes.</param>
        public ByteSize AddBytes(double value)
        {
            return this + ByteSize.FromBytes(value);
        }

        /// <summary>
        /// Returns a new <see cref="ByteSize" /> object whose value is the difference of the specified
        /// value and this instance.
        /// </summary>
        /// <param name="bs">The <see cref="ByteSize" /> instance to subtract.</param>
        public ByteSize Subtract(ByteSize bs)
        {
            return new ByteSize(this.Bytes - bs.Bytes);
        }

        /// <inheritdoc cref="Add" />
        /// <param name="b1">The first instance.</param>
        /// <param name="b2">The second instance.</param>
        public static ByteSize operator +(ByteSize b1, ByteSize b2)
        {
            return new ByteSize(b1.Bytes + b2.Bytes);
        }

        /// <summary>
        /// Returns an instance whose value is 1 byte greater than this instance.
        /// </summary>
        /// <param name="b">The instance to increment.</param>
        public static ByteSize operator ++(ByteSize b)
        {
            // TODO: I can't imagine this method being useful. Might want to get rid of.
            return new ByteSize(b.Bytes + 1);
        }

        /// <summary>
        /// Returns a new instance whose value is the negative value of the specified instance.
        /// </summary>
        /// <param name="b">The instance to be negated.</param>
        public static ByteSize operator -(ByteSize b)
        {
            return new ByteSize(-b.Bytes);
        }

        /// <inheritdoc cref="Subtract" />
        /// <param name="b1">The first instance.</param>
        /// <param name="b2">The second instance.</param>
        public static ByteSize operator -(ByteSize b1, ByteSize b2)
        {
            return new ByteSize(b1.Bytes - b2.Bytes);
        }

        /// <summary>
        /// Returns a new instance whose value is 1 byte less than this instance.
        /// </summary>
        /// <param name="b">The instance to decrement.</param>
        public static ByteSize operator --(ByteSize b)
        {
            // TODO: I can't imagine this method being useful. Might want to get rid of.
            return new ByteSize(b.Bytes - 1);
        }

        /// <summary>
        /// Multiply two instances.
        /// </summary>
        /// <param name="a">First instance to multiply.</param>
        /// <param name="b">Second instance to multiply.</param>
        public static ByteSize operator *(ByteSize a, ByteSize b) 
        {
            return new ByteSize(a.Bytes * b.Bytes);
        }

        /// <summary>
        /// Divides two instances.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <exception cref="DivideByZeroException">Thrown if <paramref name="b" /> is zero.</exception>
        public static ByteSize operator /(ByteSize a, ByteSize b)
        {
            if (b.Bytes == 0)
            {
                throw new DivideByZeroException();
            }
            return new ByteSize(a.Bytes / b.Bytes);
        }

        /// <summary>
        /// Indicates whether two instances have equal number of bits.
        /// </summary>
        /// <param name="b1">The first instance to compare.</param>
        /// <param name="b2">The second instance to compare.</param>
        public static bool operator ==(ByteSize b1, ByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        /// <summary>
        /// Indicates whether two instances have different number of bits.
        /// </summary>
        /// <param name="b1">The first instance to compare.</param>
        /// <param name="b2">The second instance to compare.</param>
        public static bool operator !=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        /// <summary>
        /// Indicates whether one instance has less bits than the other.
        /// </summary>
        /// <param name="b1">The first instance to compare.</param>
        /// <param name="b2">The second instance to compare.</param>
        public static bool operator <(ByteSize b1, ByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        /// <summary>
        /// Indicates whether one instance has less or equal bits than the other.
        /// </summary>
        /// <param name="b1">The first instance to compare.</param>
        /// <param name="b2">The second instance to compare.</param>
        public static bool operator <=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        /// <summary>
        /// Indicates whether one instance has more bits than the other.
        /// </summary>
        /// <param name="b1">The first instance to compare.</param>
        /// <param name="b2">The second instance to compare.</param>
        public static bool operator >(ByteSize b1, ByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        /// <summary>
        /// Indicates whether one instance has more bits or equal than the other.
        /// </summary>
        /// <param name="b1">The first instance to compare.</param>
        /// <param name="b2">The second instance to compare.</param>
        public static bool operator >=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }

        /// <summary>
        /// Converts the string representation of a binary or decimal byte to its <see cref="ByteSize" /> equivalent.
        /// </summary>
        /// <param name="s">A string that contains a <see cref="ByteSize" /> to convert.</param>
        public static ByteSize Parse(string s)
        {
            return Parse(s, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the string representation of a binary or decimal byte to its <see cref="ByteSize" /> equivalent.
        /// </summary>
        /// <param name="s">A string that contains a <see cref="ByteSize" /> to convert.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        public static ByteSize Parse(string s, IFormatProvider formatProvider)
        {
            return Parse(s, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);
        }

        /// <summary>
        /// Converts the string representation of a binary or decimal byte to its <see cref="ByteSize" /> equivalent.
        /// </summary>
        /// <param name="s">A string that contains a <see cref="ByteSize" /> to convert.</param>
        /// <param name="numberStyles">Number style of the string being parsed.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        public static ByteSize Parse(string s, NumberStyles numberStyles, IFormatProvider formatProvider)
        {
           
            // Arg checking
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("s", "String is null or whitespace");

            // Get the index of the first non-digit character
            s = s.TrimStart(); // Protect against leading spaces

            var num = 0;
            var found = false;

            var numberFormatInfo = NumberFormatInfo.GetInstance(formatProvider);
            var decimalSeparator = Convert.ToChar(numberFormatInfo.NumberDecimalSeparator);
            var groupSeparator = Convert.ToChar(numberFormatInfo.NumberGroupSeparator);

            // Pick first non-digit number
            for (num = 0; num < s.Length; num++)
                if (!(char.IsDigit(s[num]) || s[num] == decimalSeparator || s[num] == groupSeparator))
                {
                    found = true;
                    break;
                }

            if (found == false)
                throw new FormatException($"No byte indicator found in value '{s}'.");

            int lastNumber = num;

            // Cut the input string in half
            string numberPart = s.Substring(0, lastNumber).Trim();
            string sizePart = s.Substring(lastNumber, s.Length - lastNumber).Trim();

            // Get the numeric part
            double number;
            if (!double.TryParse(numberPart, numberStyles, formatProvider, out number))
                throw new FormatException($"No number found in value '{s}'.");

            // Get the magnitude part
            switch (sizePart)
            {
                case "b":
                    if (number % 1 != 0) // Can't have partial bits
                        throw new FormatException($"Can't have partial bits for value '{s}'.");

                    return FromBits((long) number);

                case "B":
                    return FromBytes(number);
            }

            switch (sizePart.ToLowerInvariant())
            {
                // Binary
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

                // Decimal
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
                    throw new FormatException($"Bytes of magnitude '{sizePart}' is not supported.");
            }
        }

        /// <summary>
        /// Converts the string representation of a binary or decimal byte to its <see cref="ByteSize" /> equivalent.
        /// Return value indicates success or failure of the parsing.
        /// </summary>
        /// <param name="s">A string that contains a <see cref="ByteSize" /> to convert.</param>
        /// <param name="result">Object reference to store the result if successful.</param>
        public static bool TryParse(string s, out ByteSize result)
        {
            try 
            {
                result = Parse(s);
                return true;
            }
            catch
            {
                result = new ByteSize();
                return false;
            }
        }

        /// <summary>
        /// Converts the string representation of a binary or decimal byte to its <see cref="ByteSize" /> equivalent.
        /// Return value indicates success or failure of the parsing.
        /// </summary>
        /// <param name="s">A string that contains a <see cref="ByteSize" /> to convert.</param>
        /// <param name="numberStyles">Number style of the string being parsed.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">Object reference to store the result if successful.</param>
        public static bool TryParse(string s, NumberStyles numberStyles, IFormatProvider formatProvider, out ByteSize result)
        {
            try
            {
                result = Parse(s, numberStyles, formatProvider);
                return true;
            }
            catch
            {
                result = new ByteSize();
                return false;
            }
        }
    }
}
