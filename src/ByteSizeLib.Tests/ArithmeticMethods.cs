using System.Linq;
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

        [Fact]
        public void MultiplyOperator()
        {
            var a = ByteSize.FromBytes(2);
            var b = ByteSize.FromBytes(2);
            var actual = a * b;

            Assert.Equal(4, actual.Bytes);
        }

        [Fact]
        public void DivideOperator()
        {
            var a = ByteSize.FromBytes(4);
            var b = ByteSize.FromBytes(2);
            var actual = a / b;

            Assert.Equal(2, actual.Bytes);
        }

        [Fact]
        public void MaxValueBits()
        {
            var size = ByteSize.FromBits(long.MaxValue);

            Assert.Equal(long.MaxValue, size.Bits);
        }

        [Fact]
        public void Sum()
        {
            var sizes = new[] { ByteSize.FromBytes(2), ByteSize.FromBytes(3) };

            Assert.Equal(ByteSize.FromBytes(5), sizes.Sum());
        }
    }
}
