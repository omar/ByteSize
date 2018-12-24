using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSize.Tests.DecimalByteSizeTest
{
    public class ParsingMethods
    {
        // Base parsing functionality
        [Fact]
        public void Parse()
        {
            string val = "1020KB";
            var expected = DecimalByteSize.FromKiloBytes(1020);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KB";
            var expected = DecimalByteSize.FromKiloBytes(1020);

            DecimalByteSize resultByteSize;
            var resultBool = DecimalByteSize.TryParse(val, out resultByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultByteSize);
        }

        [Fact]
        public void ParseDecimalMB()
        {
            string val = "100.5MB";
            var expected = DecimalByteSize.FromMegaBytes(100.5);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            DecimalByteSize resultByteSize;
            var resultBool = DecimalByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new DecimalByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            DecimalByteSize resultByteSize;
            var resultBool = DecimalByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new DecimalByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KB";

            DecimalByteSize resultByteSize;
            var resultBool = DecimalByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new DecimalByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KB ";
            var expected = DecimalByteSize.FromKiloBytes(100);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() =>
                {
                    DecimalByteSize.Parse(val);
                });
        }


        // Parse method throws exceptions
        [Fact]
        public void ParseThrowsOnInvalid()
        {
            string badValue = "Unexpected Value";

            Assert.Throws<FormatException>(() =>
                {
                    DecimalByteSize.Parse(badValue);
                });
        }

        [Fact]
        public void ParseThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    DecimalByteSize.Parse(null);
                });
        }


        // Various magnitudes
        [Fact]
        public void ParseBits()
        {
            string val = "1b";
            var expected = DecimalByteSize.FromBits(1);
            
            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseBytes()
        {
            string val = "1B";
            var expected = DecimalByteSize.FromBytes(1);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseKB()
        {
            string val = "1020KB";
            var expected = DecimalByteSize.FromKiloBytes(1020);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMB()
        {
            string val = "1000MB";
            var expected = DecimalByteSize.FromMegaBytes(1000);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGB()
        {
            string val = "805GB";
            var expected = DecimalByteSize.FromGigaBytes(805);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTB()
        {
            string val = "100TB";
            var expected = DecimalByteSize.FromTeraBytes(100);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePB()
        {
            string val = "100PB";
            var expected = DecimalByteSize.FromPetaBytes(100);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseCultureNumberSeparator()
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            string val = "1.500,5 MB";
            var expected = DecimalByteSize.FromMegaBytes(1500.5);

            var result = DecimalByteSize.Parse(val);

            Assert.Equal(expected, result);
            
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
