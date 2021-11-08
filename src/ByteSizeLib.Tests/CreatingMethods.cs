using Xunit;

namespace ByteSizeLib.Tests
{
    public class CreatingMethods
    {
        [Fact]
        public void FromBitsMethod()
        {
            // Arrange
            long value = 8;

            // Act
            var result = ByteSize.FromBits(value);

            // Assert
            Assert.Equal(8, result.Bits);
            Assert.Equal(1, result.Bytes);
        }

        [Fact]
        public void FromBitsFraction()
        {
            // Arrange
            long value = 12;

            // Act
            var result = ByteSize.FromBits(value);

            // Assert
            Assert.Equal(12, result.Bits);
            Assert.Equal(1.5, result.Bytes);
        }

        [Fact]
        public void FromBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromBytes(value);

            // Assert
            Assert.Equal(12, result.Bits);
            Assert.Equal(1.5, result.Bytes);
        }
    }
}
