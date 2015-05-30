using Xunit;

namespace ByteSizeLib.Tests
{
    public class CreatingMethods
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            double byteSize = 1125899906842624;

            // Act
            var result = new ByteSize(byteSize);

            // Assert
            Assert.Equal(byteSize * 8, result.Bits);
            Assert.Equal(byteSize, result.Bytes);
            Assert.Equal(byteSize / 1024, result.KiloBytes);
            Assert.Equal(byteSize / 1024 / 1024, result.MegaBytes);
            Assert.Equal(byteSize / 1024 / 1024 / 1024, result.GigaBytes);
            Assert.Equal(byteSize / 1024 / 1024 / 1024 / 1024, result.TeraBytes);
            Assert.Equal(1, result.PetaBytes);
        }

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

        [Fact]
        public void FromKiloBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromKiloBytes(value);

            // Assert
            Assert.Equal(1536, result.Bytes);
            Assert.Equal(1.5, result.KiloBytes);
        }

        [Fact]
        public void FromMegaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromMegaBytes(value);

            // Assert
            Assert.Equal(1572864, result.Bytes);
            Assert.Equal(1.5, result.MegaBytes);
        }

        [Fact]
        public void FromGigaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromGigaBytes(value);

            // Assert
            Assert.Equal(1610612736, result.Bytes);
            Assert.Equal(1.5, result.GigaBytes);
        }

        [Fact]
        public void FromTeraBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromTeraBytes(value);

            // Assert
            Assert.Equal(1649267441664, result.Bytes);
            Assert.Equal(1.5, result.TeraBytes);
        }

        [Fact]
        public void FromPetaBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromPetaBytes(value);

            // Assert
            Assert.Equal(1688849860263936, result.Bytes);
            Assert.Equal(1.5, result.PetaBytes);
        }
    }
}
