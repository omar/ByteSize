using Xunit;

namespace ByteSizeLib.Tests
{
    public class ArithmeticMethods
    {
        [Fact]
        public void AddMethod()
        {
            var size1 = ByteSize.FromBytes(1);
            var result = size1.Add(size1);

            Assert.Equal(2, result.Bytes);
            Assert.Equal(16, result.Bits);
        }

        [Fact]
        public void AddBitsMethod()
        {
            var size = ByteSize.FromBytes(1).AddBits(8);

            Assert.Equal(2, size.Bytes);
            Assert.Equal(16, size.Bits);
        }

        [Fact]
        public void AddBytesMethod()
        {
            var size = ByteSize.FromBytes(1).AddBytes(1);

            Assert.Equal(2, size.Bytes);
            Assert.Equal(16, size.Bits);
        }

        [Fact]
        public void AddKiloBytesMethod()
        {
            var size = ByteSize.FromKiloBytes(2).AddKiloBytes(2);

            Assert.Equal(4 * 1024 * 8, size.Bits);
            Assert.Equal(4 * 1024, size.Bytes);
            Assert.Equal(4, size.KiloBytes);
        }

        [Fact]
        public void AddMegaBytesMethod()
        {
            var size = ByteSize.FromMegaBytes(2).AddMegaBytes(2);

            Assert.Equal(4 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4 * 1024 * 1024, size.Bytes);
            Assert.Equal(4 * 1024, size.KiloBytes);
            Assert.Equal(4, size.MegaBytes);
        }

        [Fact]
        public void AddGigaBytesMethod()
        {
            var size = ByteSize.FromGigaBytes(2).AddGigaBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024, size.KiloBytes);
            Assert.Equal(4d * 1024, size.MegaBytes);
            Assert.Equal(4d, size.GigaBytes);
        }

        [Fact]
        public void AddTeraBytesMethod()
        {
            var size = ByteSize.FromTeraBytes(2).AddTeraBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.KiloBytes);
            Assert.Equal(4d * 1024 * 1024, size.MegaBytes);
            Assert.Equal(4d * 1024, size.GigaBytes);
            Assert.Equal(4d, size.TeraBytes);
        }

        [Fact]
        public void AddPetaBytesMethod()
        {
            var size = ByteSize.FromPetaBytes(2).AddPetaBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024, size.KiloBytes);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.MegaBytes);
            Assert.Equal(4d * 1024 * 1024, size.GigaBytes);
            Assert.Equal(4d * 1024, size.TeraBytes);
            Assert.Equal(4d, size.PetaBytes);
        }

        [Fact]
        public void SubtractMethod()
        {
            var size = ByteSize.FromBytes(4).Subtract(ByteSize.FromBytes(2));

            Assert.Equal(16, size.Bits);
            Assert.Equal(2, size.Bytes);
        }

        [Fact]
        public void IncrementOperator()
        {
            var size = ByteSize.FromBytes(2);
            size++;

            Assert.Equal(24, size.Bits);
            Assert.Equal(3, size.Bytes);
        }


        [Fact]
        public void MinusOperatorUnary()
        {
            var size = ByteSize.FromBytes(2);

            size = -size;

            Assert.Equal(-16, size.Bits);
            Assert.Equal(-2, size.Bytes);
        }

        [Fact]
        public void MinusOperatorBinary()
        {
            var size = ByteSize.FromBytes(4) - ByteSize.FromBytes(2);

            Assert.Equal(16, size.Bits);
            Assert.Equal(2, size.Bytes);
        }

        [Fact]
        public void DecrementOperator()
        {
            var size = ByteSize.FromBytes(2);
            size--;

            Assert.Equal(8, size.Bits);
            Assert.Equal(1, size.Bytes);
        }
    }
}
