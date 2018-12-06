using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSizeLib.Tests.BinaryByteSizeTests
{
    public class ParsingMethods
    {
        // Base parsing functionality
        [Fact]
        public void Parse()
        {
            string val = "1020KiB";
            var expected = BinaryByteSize.FromKibiBytes(1020);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KiB";
            var expected = BinaryByteSize.FromKibiBytes(1020);

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultBinaryByteSize);
        }

        [Fact]
        public void ParseDecimalMiB()
        {
            string val = "100.5MiB";
            var expected = BinaryByteSize.FromMebiBytes(100.5);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new BinaryByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new BinaryByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KiB";

            BinaryByteSize resultBinaryByteSize;
            var resultBool = BinaryByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new BinaryByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KiB ";
            var expected = BinaryByteSize.FromKibiBytes(100);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() =>
                {
                    BinaryByteSize.Parse(val);
                });
        }


        // Parse method throws exceptions
        [Fact]
        public void ParseThrowsOnInvalid()
        {
            string badValue = "Unexpected Value";

            Assert.Throws<FormatException>(() =>
                {
                    BinaryByteSize.Parse(badValue);
                });
        }

        [Fact]
        public void ParseThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    BinaryByteSize.Parse(null);
                });
        }


        // Various magnitudes
        [Fact]
        public void ParseBits()
        {
            string val = "1b";
            var expected = BinaryByteSize.FromBits(1);
            
            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseBytes()
        {
            string val = "1B";
            var expected = BinaryByteSize.FromBytes(1);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseKiB()
        {
            string val = "1020KiB";
            var expected = BinaryByteSize.FromKibiBytes(1020);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMiB()
        {
            string val = "1000MiB";
            var expected = BinaryByteSize.FromMebiBytes(1000);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGiB()
        {
            string val = "805GiB";
            var expected = BinaryByteSize.FromGibiBytes(805);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTiB()
        {
            string val = "100TiB";
            var expected = BinaryByteSize.FromTebiBytes(100);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePiB()
        {
            string val = "100PiB";
            var expected = BinaryByteSize.FromPebiBytes(100);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseCultureNumberSeparator()
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            string val = "1.500,5 MiB";
            var expected = BinaryByteSize.FromMebiBytes(1500.5);

            var result = BinaryByteSize.Parse(val);

            Assert.Equal(expected, result);
            
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
