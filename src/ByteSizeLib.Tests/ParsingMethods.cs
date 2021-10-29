using System;
using System.Globalization;
using Xunit;

namespace ByteSizeLib.Tests
{
    public class ParsingMethods
    {
        // Base parsing functionality
        [Fact]
        public void Parse()
        {
            string val = "1020KiB";
            var expected = ByteSize.FromKibiBytes(1020);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryParse()
        {
            string val = "1020KiB";
            var expected = ByteSize.FromKibiBytes(1020);

            ByteSize resultBinaryByteSize;
            var resultBool = ByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.True(resultBool);
            Assert.Equal(expected, resultBinaryByteSize);
        }

        [Fact]
        public void ParseDecimalMB()
        {
            string val = "100.5MB";
            var expected = ByteSize.FromMegaBytes(100.5);

            var result = ByteSize.Parse(val, CultureInfo.InvariantCulture);

            Assert.Equal(expected, result);
        }

        // Failure modes
        [Fact]
        public void TryParseReturnsFalseOnBadValue()
        {
            string val = "Unexpected Value";

            ByteSize resultBinaryByteSize;
            var resultBool = ByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new ByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingMagnitude()
        {
            string val = "1000";

            ByteSize resultBinaryByteSize;
            var resultBool = ByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new ByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseReturnsFalseOnMissingValue()
        {
            string val = "KiB";

            ByteSize resultBinaryByteSize;
            var resultBool = ByteSize.TryParse(val, out resultBinaryByteSize);

            Assert.False(resultBool);
            Assert.Equal(new ByteSize(), resultBinaryByteSize);
        }

        [Fact]
        public void TryParseWorksWithLotsOfSpaces()
        {
            string val = " 100 KiB ";
            var expected = ByteSize.FromKibiBytes(100);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePartialBits()
        {
            string val = "10.5b";

            Assert.Throws<FormatException>(() =>
                {
                    ByteSize.Parse(val, CultureInfo.InvariantCulture);
                });
        }

        [Fact]
        public void ParsePartialBitsCurrentCulture()
        {
            var s = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            string val = $"10{s}5b";

            Assert.Throws<FormatException>(() =>
            {
                ByteSize.Parse(val, CultureInfo.CurrentCulture);
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
                    ByteSize.Parse(null!);
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
        public void ParseCultureNumberSeparator()
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            string val = "1.500,5 MiB";
            var expected = ByteSize.FromMebiBytes(1500.5);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);

            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        [Fact]
        public void ParseWithCulture()
        {
            var cultureInfo = new CultureInfo("de-DE");
            string val = "1.234,5 MB";
            var expected = ByteSize.FromMegaBytes(1234.5);

            var result = ByteSize.Parse(val, cultureInfo);

            Assert.Equal(expected, result);
        }
    }
}
