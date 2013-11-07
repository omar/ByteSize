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

namespace ByteSize
{
    /// <summary>
    /// Represents a byte size value.
    /// </summary>
    public struct ByteSize
    {
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

        public long Bits { get; private set; }
        public double Bytes { get; private set; }
        public double KiloBytes { get; private set; }
        public double MegaBytes { get; private set; }
        public double GigaBytes { get; private set; }
        public double TeraBytes { get; private set; }

        public ByteSize(double byteSize)
            : this()
        {
            Bits = (long)System.Math.Ceiling(byteSize * BitsInByte);

            Bytes = byteSize;
            KiloBytes = byteSize / BytesInKiloByte;
            MegaBytes = byteSize / BytesInMegaByte;
            GigaBytes = byteSize / BytesInGigaByte;
            TeraBytes = byteSize / BytesInTeraByte;
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
            
            if (GigaBytes >= 1)
            {
                value = this.GigaBytes;
                symbol = ByteSize.GigaByteSymbol;
            }
            else if (MegaBytes >= 1)
            {
                value = this.MegaBytes;
                symbol = ByteSize.MegaByteSymbol;
            }
            else if (KiloBytes >= 1)
            {
                value = this.KiloBytes;
                symbol = ByteSize.KiloByteSymbol;
            }
            else if (Bytes >= 1)
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
    }
}