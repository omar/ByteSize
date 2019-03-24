using Xunit;

namespace ByteSizeLib.Tests.Decimal
{
    public class ArithmeticMethods
    {
        [Fact]
        public void AddKiloBytesMethod()
        {
            var size = ByteSize.FromKiloBytes(2).AddKiloBytes(2);

            Assert.Equal(4 * 1000 * 8, size.Bits);
            Assert.Equal(4 * 1000, size.Bytes);
            Assert.Equal(4, size.KiloBytes);
        }

        [Fact]
        public void AddMegaBytesMethod()
        {
            var size = ByteSize.FromMegaBytes(2).AddMegaBytes(2);

            Assert.Equal(4 * 1000 * 1000 * 8, size.Bits);
            Assert.Equal(4 * 1000 * 1000, size.Bytes);
            Assert.Equal(4 * 1000, size.KiloBytes);
            Assert.Equal(4, size.MegaBytes);
        }

        [Fact]
        public void AddGigaBytesMethod()
        {
            var size = ByteSize.FromGigaBytes(2).AddGigaBytes(2);

            Assert.Equal(4d * 1000 * 1000 * 1000 * 8, size.Bits);
            Assert.Equal(4d * 1000 * 1000 * 1000, size.Bytes);
            Assert.Equal(4d * 1000 * 1000, size.KiloBytes);
            Assert.Equal(4d * 1000, size.MegaBytes);
            Assert.Equal(4d, size.GigaBytes);
        }

        [Fact]
        public void AddTeraBytesMethod()
        {
            var size = ByteSize.FromTeraBytes(2).AddTeraBytes(2);

            Assert.Equal(4d * 1000 * 1000 * 1000 * 1000 * 8, size.Bits);
            Assert.Equal(4d * 1000 * 1000 * 1000 * 1000, size.Bytes);
            Assert.Equal(4d * 1000 * 1000 * 1000, size.KiloBytes);
            Assert.Equal(4d * 1000 * 1000, size.MegaBytes);
            Assert.Equal(4d * 1000, size.GigaBytes);
            Assert.Equal(4d, size.TeraBytes);
        }

        [Fact]
        public void AddPetaBytesMethod()
        {
            var size = ByteSize.FromPetaBytes(2).AddPetaBytes(2);

            Assert.Equal(4d * 1000 * 1000 * 1000 * 1000 * 1000 * 8, size.Bits);
            Assert.Equal(4d * 1000 * 1000 * 1000 * 1000 * 1000, size.Bytes);
            Assert.Equal(4d * 1000 * 1000 * 1000 * 1000, size.KiloBytes);
            Assert.Equal(4d * 1000 * 1000 * 1000, size.MegaBytes);
            Assert.Equal(4d * 1000 * 1000, size.GigaBytes);
            Assert.Equal(4d * 1000, size.TeraBytes);
            Assert.Equal(4d, size.PetaBytes);
        }
    }
}
