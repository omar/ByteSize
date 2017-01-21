using System;
using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSizeLib.Tests
{
    public class ParsingMethods
    {
        // Base parsing functionality
        [Fact]
        public void Parse()
        {
            string val = "1020KB";
            var expected = ByteSize.FromKiloBytes(1020);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KB";
            var expected = ByteSize.FromKiloBytes(1020);

            ByteSize resultByteSize;
            var resultBool = ByteSize.TryParse(val, out resultByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultByteSize);
        }

        [Fact]
        public void ParseDecimalMB()
        {
            string val = "100.5MB";
            var expected = ByteSize.FromMegaBytes(100.5);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            ByteSize resultByteSize;
            var resultBool = ByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new ByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            ByteSize resultByteSize;
            var resultBool = ByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new ByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KB";

            ByteSize resultByteSize;
            var resultBool = ByteSize.TryParse(val, out resultByteSize);

            Assert.False(resultBool);
            Assert.Equal(new ByteSize(), resultByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KB ";
            var expected = ByteSize.FromKiloBytes(100);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() =>
                {
                    ByteSize.Parse(val);
                });
        }


        // Parse method throws exceptions
        [Fact]
        public void ParseThrowsOnInvalid()
        {
            string badValue = "Unexpected Value";

            Assert.Throws<FormatException>(() =>
                {
                    ByteSize.Parse(badValue);
                });
        }

        [Fact]
        public void ParseThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    ByteSize.Parse(null);
                });
        }


        // Various magnitudes
        [Fact]
        public void ParseBits()
        {
            string val = "1b";
            var expected = ByteSize.FromBits(1);
            
            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseBytes()
        {
            string val = "1B";
            var expected = ByteSize.FromBytes(1);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseKB()
        {
            string val = "1020KB";
            var expected = ByteSize.FromKiloBytes(1020);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMB()
        {
            string val = "1000MB";
            var expected = ByteSize.FromMegaBytes(1000);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGB()
        {
            string val = "805GB";
            var expected = ByteSize.FromGigaBytes(805);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTB()
        {
            string val = "100TB";
            var expected = ByteSize.FromTeraBytes(100);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePB()
        {
            string val = "100PB";
            var expected = ByteSize.FromPetaBytes(100);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseCultureNumberSeparator()
        {
#if NETCOREAPP1_1
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
#else
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
#endif

            string val = "1.500,5 MB";
            var expected = ByteSize.FromMegaBytes(1500.5);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);

# if NETCOREAPP1_1
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
#else
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
#endif
        }
    }
}
