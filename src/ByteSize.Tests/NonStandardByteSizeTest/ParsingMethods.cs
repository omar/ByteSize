using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSize.Tests.NonStandardByteSizeTest
{
    public class ParsingMethods
    {
        // Base parsing functionality
        [Fact]
        public void Parse()
        {
            string val = "1020KB";
            var expected = NonStandardByteSize.FromKiloBytes(1020);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KB";
            var expected = NonStandardByteSize.FromKiloBytes(1020);

            NonStandardByteSize resultByteSize;
            var resultBool = NonStandardByteSize.TryParse(val, out resultByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultByteSize);
        }

        [Fact]
        public void ParseDecimalMB()
        {
            string val = "100.5MB";
            var expected = NonStandardByteSize.FromMegaBytes(100.5);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            NonStandardByteSize resultByteSize;
            var resultBool = NonStandardByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new NonStandardByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            NonStandardByteSize resultByteSize;
            var resultBool = NonStandardByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new NonStandardByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KB";

            NonStandardByteSize resultByteSize;
            var resultBool = NonStandardByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new NonStandardByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KB ";
            var expected = NonStandardByteSize.FromKiloBytes(100);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() =>
                {
                    NonStandardByteSize.Parse(val);
                });
        }


        // Parse method throws exceptions
        [Fact]
        public void ParseThrowsOnInvalid()
        {
            string badValue = "Unexpected Value";

            Assert.Throws<FormatException>(() =>
                {
                    NonStandardByteSize.Parse(badValue);
                });
        }

        [Fact]
        public void ParseThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    NonStandardByteSize.Parse(null);
                });
        }


        // Various magnitudes
        [Fact]
        public void ParseBits()
        {
            string val = "1b";
            var expected = NonStandardByteSize.FromBits(1);
            
            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseBytes()
        {
            string val = "1B";
            var expected = NonStandardByteSize.FromBytes(1);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseKB()
        {
            string val = "1020KB";
            var expected = NonStandardByteSize.FromKiloBytes(1020);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMB()
        {
            string val = "1000MB";
            var expected = NonStandardByteSize.FromMegaBytes(1000);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGB()
        {
            string val = "805GB";
            var expected = NonStandardByteSize.FromGigaBytes(805);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTB()
        {
            string val = "100TB";
            var expected = NonStandardByteSize.FromTeraBytes(100);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePB()
        {
            string val = "100PB";
            var expected = NonStandardByteSize.FromPetaBytes(100);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseCultureNumberSeparator()
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            string val = "1.500,5 MB";
            var expected = NonStandardByteSize.FromMegaBytes(1500.5);

            var result = NonStandardByteSize.Parse(val);

            Assert.Equal(expected, result);
            
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
