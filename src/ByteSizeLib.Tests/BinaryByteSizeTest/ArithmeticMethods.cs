using Xunit;

namespace ByteSizeLib.Tests.BinaryByteSizeTests
{
    public class ArithmeticMethods
    {
        [Fact]
        public void AddMethod()
        {
            var size1 = BinaryByteSize.FromBytes(1);
            var result = size1.Add(size1);

            Assert.Equal(2, result.Bytes);
            Assert.Equal(16, result.Bits);
        }

        [Fact]
        public void AddBitsMethod()
        {
            var size = BinaryByteSize.FromBytes(1).AddBits(8);

            Assert.Equal(2, size.Bytes);
            Assert.Equal(16, size.Bits);
        }

        [Fact]
        public void AddBytesMethod()
        {
            var size = BinaryByteSize.FromBytes(1).AddBytes(1);

            Assert.Equal(2, size.Bytes);
            Assert.Equal(16, size.Bits);
        }

        [Fact]
        public void AddKibiBytesMethod()
        {
            var size = BinaryByteSize.FromKibiBytes(2).AddKibiBytes(2);

            Assert.Equal(4 * 1024 * 8, size.Bits);
            Assert.Equal(4 * 1024, size.Bytes);
            Assert.Equal(4, size.KibiBytes);
        }

        [Fact]
        public void AddMebiBytesMethod()
        {
            var size = BinaryByteSize.FromMebiBytes(2).AddMebiBytes(2);

            Assert.Equal(4 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4 * 1024 * 1024, size.Bytes);
            Assert.Equal(4 * 1024, size.KibiBytes);
            Assert.Equal(4, size.MebiBytes);
        }

        [Fact]
        public void AddGibiBytesMethod()
        {
            var size = BinaryByteSize.FromGibiBytes(2).AddGibiBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024, size.KibiBytes);
            Assert.Equal(4d * 1024, size.MebiBytes);
            Assert.Equal(4d, size.GibiBytes);
        }

        [Fact]
        public void AddTebiBytesMethod()
        {
            var size = BinaryByteSize.FromTebiBytes(2).AddTebiBytes(2);

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
            var size = BinaryByteSize.FromPebiBytes(2).AddPebiBytes(2);

            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 1024 * 8, size.Bits);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024 * 1024, size.Bytes);
            Assert.Equal(4d * 1024 * 1024 * 1024 * 1024, size.KibiBytes);
            Assert.Equal(4d * 1024 * 1024 * 1024, size.MebiBytes);
            Assert.Equal(4d * 1024 * 1024, size.GibiBytes);
            Assert.Equal(4d * 1024, size.TebiBytes);
            Assert.Equal(4d, size.PebiBytes);
        }

        [Fact]
        public void SubtractMethod()
        {
            var size = BinaryByteSize.FromBytes(4).Subtract(BinaryByteSize.FromBytes(2));

            Assert.Equal(16, size.Bits);
            Assert.Equal(2, size.Bytes);
        }

        [Fact]
        public void IncrementOperator()
        {
            var size = BinaryByteSize.FromBytes(2);
            size++;

            Assert.Equal(24, size.Bits);
            Assert.Equal(3, size.Bytes);
        }


        [Fact]
        public void MinusOperatorUnary()
        {
            var size = BinaryByteSize.FromBytes(2);

            size = -size;

            Assert.Equal(-16, size.Bits);
            Assert.Equal(-2, size.Bytes);
        }

        [Fact]
        public void MinusOperatorBinary()
        {
            var size = BinaryByteSize.FromBytes(4) - BinaryByteSize.FromBytes(2);

            Assert.Equal(16, size.Bits);
            Assert.Equal(2, size.Bytes);
        }

        [Fact]
        public void DecrementOperator()
        {
            var size = BinaryByteSize.FromBytes(2);
            size--;

            Assert.Equal(8, size.Bits);
            Assert.Equal(1, size.Bytes);
        }
    }
}
