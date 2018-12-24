using Xunit;

namespace ByteSize.Tests.NonStandardByteSizeTest
{
    public class CreatingMethods
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            double bytes = 1125899906842624;

            // Act
            var result = new NonStandardByteSize(bytes);

            // Assert
            Assert.Equal(bytes * 8, result.Bits);
            Assert.Equal(bytes, result.Bytes);
            Assert.Equal(bytes / 1024, result.KiloBytes);
            Assert.Equal(bytes / 1024 / 1024, result.MegaBytes);
            Assert.Equal(bytes / 1024 / 1024 / 1024, result.GigaBytes);
            Assert.Equal(bytes / 1024 / 1024 / 1024 / 1024, result.TeraBytes);
            Assert.Equal(1, result.PetaBytes);
        }

        [Fact]
        public void FromBitsMethod()
        {
            // Arrange
            long value = 8;

            // Act
            var result = NonStandardByteSize.FromBits(value);

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
            var result = NonStandardByteSize.FromBytes(value);

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
            var result = NonStandardByteSize.FromKiloBytes(value);

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
            var result = NonStandardByteSize.FromMegaBytes(value);

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
            var result = NonStandardByteSize.FromGigaBytes(value);

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
            var result = NonStandardByteSize.FromTeraBytes(value);

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
            var result = NonStandardByteSize.FromPetaBytes(value);

            // Assert
            Assert.Equal(1688849860263936, result.Bytes);
            Assert.Equal(1.5, result.PetaBytes);
        }
    }
}
