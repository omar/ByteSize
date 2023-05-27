using System.Globalization;
using Xunit;

namespace ByteSizeLib.Tests.Decimal
{
    public class ParsingMethods
    {
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
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            string val = "1.500,5 MB";
            var expected = ByteSize.FromMegaBytes(1500.5);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);

            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
