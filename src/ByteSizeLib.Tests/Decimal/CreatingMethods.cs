using Xunit;

namespace ByteSizeLib.Tests.Decimal
{
    public class CreatingMethods
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            double bytes = 1000000000000000;

            // Act
            var result = new ByteSize(bytes);

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
        public void FromKiloBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromKiloBytes(value);

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
            var result = ByteSize.FromMegaBytes(value);

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
            var result = ByteSize.FromGigaBytes(value);

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
            var result = ByteSize.FromTeraBytes(value);

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
            var result = ByteSize.FromPetaBytes(value);

            // Assert
            Assert.Equal(1500000000000000, result.Bytes);
            Assert.Equal(1.5, result.PetaBytes);
        }
    }
}
