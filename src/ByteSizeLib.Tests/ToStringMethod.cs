using System.Globalization;
using Xunit;

namespace ByteSizeLib.Tests
{
    public class ToStringMethod
    {
        [Fact]
        public void ReturnsLargestSuffix()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10.5);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal(10.5.ToString("0.0 KB"), result);
        }

        [Fact]
        public void ReturnsDefaultNumberFormat()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10.5);

            // Act
            var result = b.ToString("KB");

            // Assert
            Assert.Equal(10.5.ToString("0.0 KB"), result);
        }

        [Fact]
        public void ReturnsProvidedNumberFormat()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10.1234);

            // Act
            var result = b.ToString("#.#### KB");

            // Assert
            Assert.Equal(10.1234.ToString("0.0000 KB"), result);
        }

        [Fact]
        public void ReturnsBits()
        {
            // Arrange
            var b = ByteSize.FromBits(10);

            // Act
            var result = b.ToString("##.#### b");

            // Assert
            Assert.Equal("10 b", result);
        }

        [Fact]
        public void ReturnsBytes()
        {
            // Arrange
            var b = ByteSize.FromBytes(10);

            // Act
            var result = b.ToString("##.#### B");

            // Assert
            Assert.Equal("10 B", result);
        }

        [Fact]
        public void ReturnsSelectedFormat()
        {
            // Arrange
            var b = ByteSize.FromTeraBytes(10);

            // Act
            var result = b.ToString("0.0 TB");

            // Assert
            Assert.Equal(10.ToString("0.0 TB"), result);
        }

        [Fact]
        public void ReturnsLargestPrefixLargerThanZero()
        {
            // Arrange
            var b = ByteSize.FromMegaBytes(.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("500 KB", result);
        }

        [Fact]
        public void ReturnsLargestPrefixLargerThanZeroForNegativeValues()
        {
            // Arrange
            var b = ByteSize.FromMegaBytes(-.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("-500 KB", result);
        }

        [Fact]
        public void ReturnsLargestSuffixUsingCurrentCulture()
        {
            var originalCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

            // Arrange
            var b = ByteSize.FromKiloBytes(9770);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal("9,77 MB", result);

            CultureInfo.CurrentCulture = originalCulture;
        }

        [Fact]
        public void ReturnsLargestSuffixUsingSpecifiedCulture()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(9800);

            // Act
            var result = b.ToString("#.#", new CultureInfo("fr-FR"));

            // Assert
            Assert.Equal("9,8 MB", result);
        }

        [Fact]
        public void ReturnsCultureSpecificFormat()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10.5);

            // Act
            var deCulture = new CultureInfo("de-DE");
            var result = b.ToString("0.0 KB", deCulture);

            // Assert
            Assert.Equal(10.5.ToString("0.0 KB", deCulture), result);
        }

        [Fact]
        public void ReturnsZeroBytes()
        {
            // Arrange
            var b = ByteSize.FromBytes(0);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal("0 B", result);
        }

        [Fact]
        public void StringInterpolationWithFormat()
        {
            // Arrange
            var b = ByteSize.FromBytes(12);

            // Act
            var result = $"{b:0.0}";

            // Assert
            Assert.Equal($"{12.0:0.0} B", result);
        }

        [Fact]
        public void StringInterpolationWithoutFormat()
        {
            // Arrange
            var b = ByteSize.FromBytes(12);

            // Act
            var result = $"{b}";

            // Assert
            Assert.Equal("12 B", result);
        }

        [Fact]
        public void StringInterpolationDefaultFormat()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(1.1234);

            // Act
            var result = $"{b}";

            // Assert
            Assert.Equal($"{1.12} KB", result);
        }
    }
}
