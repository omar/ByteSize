using Xunit;

namespace ByteSize.Tests.DecimalByteSizeTest
{
    public class CreatingMethods
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            double bytes = 1000000000000000;

            // Act
            var result = new DecimalByteSize(bytes);

            // Assert
            Assert.Equal(bytes * 8, result.Bits);
            Assert.Equal(bytes, result.Bytes);
            Assert.Equal(bytes / 1000, result.KiloBytes);
            Assert.Equal(bytes / 1000 / 1000, result.MegaBytes);
            Assert.Equal(bytes / 1000 / 1000 / 1000, result.GigaBytes);
            Assert.Equal(bytes / 1000 / 1000 / 1000 / 1000, result.TeraBytes);
            Assert.Equal(1, result.PetaBytes);
        }

        [Fact]
        public void FromBitsMethod()
        {
            // Arrange
            long value = 8;

            // Act
            var result = DecimalByteSize.FromBits(value);

            // Assert
            Assert.Equal(8, result.Bits);
            Assert.Equal(1, result.Bytes);
        }

        [Fact]
        public void FromBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = DecimalByteSize.FromBytes(value);

            // Assert
            Assert.Equal(12, result.Bits);
            Assert.Equal(1.5, result.Bytes);
        }

        [Fact]
        public void FromKiloBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = DecimalByteSize.FromKiloBytes(value);

            // Assert
            Assert.Equal(1500, result.Bytes);
            Assert.Equal(1.5, result.KiloBytes);
        }

        [Fact]
        public void FromMegaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = DecimalByteSize.FromMegaBytes(value);

            // Assert
            Assert.Equal(1500000, result.Bytes);
            Assert.Equal(1.5, result.MegaBytes);
        }

        [Fact]
        public void FromGigaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = DecimalByteSize.FromGigaBytes(value);

            // Assert
            Assert.Equal(1500000000, result.Bytes);
            Assert.Equal(1.5, result.GigaBytes);
        }

        [Fact]
        public void FromTeraBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = DecimalByteSize.FromTeraBytes(value);

            // Assert
            Assert.Equal(1500000000000, result.Bytes);
            Assert.Equal(1.5, result.TeraBytes);
        }

        [Fact]
        public void FromPetaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = DecimalByteSize.FromPetaBytes(value);

            // Assert
            Assert.Equal(1500000000000000, result.Bytes);
            Assert.Equal(1.5, result.PetaBytes);
        }
    }
}
