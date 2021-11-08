using Xunit;

namespace ByteSizeLib.Tests.Binary
{
    public class ParsingMethods
    {
        [Fact]
        public void ParseKiB()
        {
            string val = "1020KiB";
            var expected = ByteSize.FromKibiBytes(1020);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseMiB()
        {
            string val = "1000MiB";
            var expected = ByteSize.FromMebiBytes(1000);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseGiB()
        {
            string val = "805GiB";
            var expected = ByteSize.FromGibiBytes(805);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseTiB()
        {
            string val = "100TiB";
            var expected = ByteSize.FromTebiBytes(100);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParsePiB()
        {
            string val = "100PiB";
            var expected = ByteSize.FromPebiBytes(100);

            var result = ByteSize.Parse(val);

            Assert.Equal(expected, result);
        }
    }
}
