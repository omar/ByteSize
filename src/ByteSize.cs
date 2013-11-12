#region License, Terms and Author(s)
//
// ByteSize utility class.
// Copyright (c) 2013 Omar Khudeira. All rights reserved.
//
//  Author(s):
//
//      Omar Khudeira, http://omar.io
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;

namespace ByteSize
{
    /// <summary>
    /// Represents a byte size value.
    /// </summary>
    public struct ByteSize : IComparable<ByteSize>, IEquatable<ByteSize>
    {
        public static readonly ByteSize MinValue = ByteSize.FromBits(long.MinValue);
        public static readonly ByteSize MaxValue = ByteSize.FromBits(long.MaxValue);

        public const long BitsInByte = 8;
        public const long BytesInKiloByte = 1024;
        public const long BytesInMegaByte = 1048576;
        public const long BytesInGigaByte = 1073741824;
        public const long BytesInTeraByte = 1099511627776;

        public const string BitSymbol = "b";
        public const string ByteSymbol = "B";
        public const string KiloByteSymbol = "KB";
        public const string MegaByteSymbol = "MB";
        public const string GigaByteSymbol = "GB";
        public const string TeraByteSymbol = "TB";

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KiloBytes { get; private set; }
        public double MegaBytes { get; private set; }
        public double GigaBytes { get; private set; }
        public double TeraBytes { get; private set; }

        public ByteSize(double byteSize)
            : this()
        {
            // Get ceiling because bis are whole units
            Bits = (long)Math.Ceiling(byteSize * BitsInByte);

            Bytes = byteSize;
            KiloBytes = byteSize / BytesInKiloByte;
            MegaBytes = byteSize / BytesInMegaByte;
            GigaBytes = byteSize / BytesInGigaByte;
            TeraBytes = byteSize / BytesInTeraByte;
        }

        public static ByteSize FromBits(long value)
        {
            return new ByteSize(value / BitsInByte);
        }

        public static ByteSize FromBytes(double value)
        {
            return new ByteSize(value);
        }

        public static ByteSize FromKiloBytes(double value)
        {
            return new ByteSize(value * BytesInKiloByte);
        }

        public static ByteSize FromMegaBytes(double value)
        {
            return new ByteSize(value * BytesInMegaByte);
        }

        public static ByteSize FromGigaBytes(double value)
        {
            return new ByteSize(value * BytesInGigaByte);
        }

        public static ByteSize FromTeraBytes(double value)
        {
            return new ByteSize(value * BytesInTeraByte);
        }

        /// <summary>
        /// Converts the value of the current ByteSize object to a string.
        /// The metric prefix symbol (bit, byte, kilo, mega, giga, tera) used is
        /// the largest metric prefix such that the corresponding value is greater
        //  than or equal to one.
        /// </summary>
        public override string ToString()
        {
            string symbol;
            double value;

            if (this.TeraBytes >= 1)
            {
                value = this.TeraBytes;
                symbol = ByteSize.TeraByteSymbol;
            }
            else if (this.GigaBytes >= 1)
            {
                value = this.GigaBytes;
                symbol = ByteSize.GigaByteSymbol;
            }
            else if (this.MegaBytes >= 1)
            {
                value = this.MegaBytes;
                symbol = ByteSize.MegaByteSymbol;
            }
            else if (this.KiloBytes >= 1)
            {
                value = this.KiloBytes;
                symbol = ByteSize.KiloByteSymbol;
            }
            else if (this.Bytes >= 1)
            {
                value = this.Bytes;
                symbol = ByteSize.ByteSymbol;
            }
            else
            {
                value = this.Bits;
                symbol = ByteSize.BitSymbol;
            }
            return string.Format("{0} {1}", value, symbol);
        }

        public override bool Equals(object value)
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

        public bool Equals(ByteSize value)
        {
            return this.Bits == value.Bits;
        }

        public override int GetHashCode()
        {
            return this.Bits.GetHashCode();
        }

        public int CompareTo(ByteSize other)
        {
            return this.Bits.CompareTo(other.Bits);
        }

        public ByteSize Add(ByteSize bs)
        {
            return new ByteSize(this.Bits + bs.Bits);
        }

        public ByteSize Subtract(ByteSize bs)
        {
            return new ByteSize(this.Bits - bs.Bits);
        }

        public static ByteSize operator +(ByteSize b1, ByteSize b2)
        {
            return new ByteSize(b1.Bits + b2.Bits);
        }

        public static ByteSize operator ++(ByteSize b)
        {
            return new ByteSize(b.Bits++);
        }

        public static ByteSize operator --(ByteSize b)
        {
            return new ByteSize(b.Bits--);
        }

        public static bool operator ==(ByteSize b1, ByteSize b2)
        {
            return b1.Bits == b2.Bits;
        }

        public static bool operator !=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits != b2.Bits;
        }

        public static bool operator <(ByteSize b1, ByteSize b2)
        {
            return b1.Bits < b2.Bits;
        }

        public static bool operator <=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits <= b2.Bits;
        }

        public static bool operator >(ByteSize b1, ByteSize b2)
        {
            return b1.Bits > b2.Bits;
        }

        public static bool operator >=(ByteSize b1, ByteSize b2)
        {
            return b1.Bits >= b2.Bits;
        }
    }
}
