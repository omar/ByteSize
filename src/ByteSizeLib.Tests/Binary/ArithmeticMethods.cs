using Xunit;

namespace ByteSizeLib.Tests.Binary
{
    public class ArithmeticMethods
    {
        [Fact]
        public void AddKibiBytesMethod()
        {
            var size = ByteSize.FromKibiBytes(2).AddKibiBytes(2);

            Assert.Equal(4 * 1024 * 8, size.Bits);
            Assert.Equal(4 * 1024, size.Bytes);
            Assert.Equal(4, size.KibiBytes);
        }

        [Fact]
        public void AddMebiBytesMethod()
        {
            var size = ByteSize.FromMebiBytes(2).AddMebiBytes(2);

            Assert.Equal(4 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4 * 1024 * 1024, size.Bytes);
            Assert.Equal(4 * 1024, size.KibiBytes);
            Assert.Equal(4, size.MebiBytes);
        }

        [Fact]
        public void AddGibiBytesMethod()
        {
            var size = ByteSize.FromGibiBytes(2).AddGibiBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024, size.KibiBytes);
            Assert.Equal(4d * 1024, size.MebiBytes);
            Assert.Equal(4d, size.GibiBytes);
        }

        [Fact]
        public void AddTebiBytesMethod()
        {
            var size = ByteSize.FromTebiBytes(2).AddTebiBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.KibiBytes);
            Assert.Equal(4d * 1024 * 1024, size.MebiBytes);
            Assert.Equal(4d * 1024, size.GibiBytes);
            Assert.Equal(4d, size.TebiBytes);
        }

        [Fact]
        public void AddPebiBytesMethod()
        {
            var size = ByteSize.FromPebiBytes(2).AddPebiBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024, size.KibiBytes);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.MebiBytes);
            Assert.Equal(4d * 1024 * 1024, size.GibiBytes);
            Assert.Equal(4d * 1024, size.TebiBytes);
            Assert.Equal(4d, size.PebiBytes);
        }
    }
}
