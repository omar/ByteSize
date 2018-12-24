using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSize.Tests.NonStandardByteSizeTest
{
    public class ToStringMethod
    {
        [Fact]
        public void ReturnsLargestMetricSuffix()
        {
            // Arrange
            var b = NonStandardByteSize.FromKiloBytes(10.5);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal(10.5.ToString("0.0 KB"), result);
        }

        [Fact]
        public void ReturnsDefaultNumberFormat()
        {
            // Arrange
            var b = NonStandardByteSize.FromKiloBytes(10.5);

            // Act
            var result = b.ToString("KB");

            // Assert
            Assert.Equal(10.5.ToString("0.0 KB"), result);
        }

        [Fact]
        public void ReturnsProvidedNumberFormat()
        {
            // Arrange
            var b = NonStandardByteSize.FromKiloBytes(10.1234);

            // Act
            var result = b.ToString("#.#### KB");

            // Assert
            Assert.Equal(10.1234.ToString("0.0000 KB"), result);
        }

        [Fact]
        public void ReturnsBits()
        {
            // Arrange
            var b = NonStandardByteSize.FromBits(10);

            // Act
            var result = b.ToString("##.#### b");

            // Assert
            Assert.Equal("10 b", result);
        }

        [Fact]
        public void ReturnsBytes()
        {
            // Arrange
            var b = NonStandardByteSize.FromBytes(10);

            // Act
            var result = b.ToString("##.#### B");

            // Assert
            Assert.Equal("10 B", result);
        }

        [Fact]
        public void ReturnsKiloBytes()
        {
            // Arrange
            var b = NonStandardByteSize.FromKiloBytes(10);

            // Act
            var result = b.ToString("##.#### KB");

            // Assert
            Assert.Equal("10 KB", result);
        }

        [Fact]
        public void ReturnsMegaBytes()
        {
            // Arrange
            var b = NonStandardByteSize.FromMegaBytes(10);

            // Act
            var result = b.ToString("##.#### MB");

            // Assert
            Assert.Equal("10 MB", result);
        }

        [Fact]
        public void ReturnsGigaBytes()
        {
            // Arrange
            var b = NonStandardByteSize.FromGigaBytes(10);

            // Act
            var result = b.ToString("##.#### GB");

            // Assert
            Assert.Equal("10 GB", result);
        }

        [Fact]
        public void ReturnsTeraBytes()
        {
            // Arrange
            var b = NonStandardByteSize.FromTeraBytes(10);

            // Act
            var result = b.ToString("##.#### TB");

            // Assert
            Assert.Equal("10 TB", result);
        }

        [Fact]
        public void ReturnsPetaBytes()
        {
            // Arrange
            var b = NonStandardByteSize.FromPetaBytes(10);

            // Act
            var result = b.ToString("##.#### PB");

            // Assert
            Assert.Equal("10 PB", result);
        }

        [Fact]
        public void ReturnsSelectedFormat()
        {
            // Arrange
            var b = NonStandardByteSize.FromTeraBytes(10);

            // Act
            var result = b.ToString("0.0 TB");

            // Assert
            Assert.Equal(10.ToString("0.0 TB"), result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZero()
        {
            // Arrange
            var b = NonStandardByteSize.FromMegaBytes(.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("512 KB", result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZeroForNegativeValues()
        {
            // Arrange
            var b = NonStandardByteSize.FromMegaBytes(-.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("-512 KB", result);
        }

        [Fact]
        public void ReturnsLargestMetricSuffixUsingCurrentCulture()
        {
            var originalCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

            // Arrange
            var b = NonStandardByteSize.FromKiloBytes(10000);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal("9,77 MB", result);

            CultureInfo.CurrentCulture = originalCulture;
        }

        [Fact]
        public void ReturnsLargestMetricSuffixUsingSpecifiedCulture()
        {
            // Arrange
            var b = NonStandardByteSize.FromKiloBytes(10000);

            // Act
            var result = b.ToString("#.#", new CultureInfo("fr-FR"));

            // Assert
            Assert.Equal("9,8 MB", result);
		}

		[Fact]
		public void ReturnsCultureSpecificFormat()
		{
			// Arrange
			var b = NonStandardByteSize.FromKiloBytes(10.5);

			// Act
			var deCulture = new CultureInfo("de-DE");
			var result = b.ToString("0.0 KB", deCulture);

			// Assert
			Assert.Equal(10.5.ToString("0.0 KB", deCulture), result);
		}

        [Fact]
		public void ReturnsZeroBits()
		{
			// Arrange
			var b = NonStandardByteSize.FromBits(0);

			// Act
			var result = b.ToString();

			// Assert
			Assert.Equal("0 b", result);
		}
	}
}
