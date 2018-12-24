using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSize.Tests.BinaryByteSizeTests
{
    public class ToStringMethod
    {
        [Fact]
        public void ReturnsLargestMetricSuffix()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10.5);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal(10.5.ToString("0.0 KiB"), result);
        }

        [Fact]
        public void ReturnsDefaultNumberFormat()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10.5);

            // Act
            var result = b.ToString("KiB");

            // Assert
            Assert.Equal(10.5.ToString("0.0 KiB"), result);
        }

        [Fact]
        public void ReturnsProvidedNumberFormat()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10.1234);

            // Act
            var result = b.ToString("#.#### KiB");

            // Assert
            Assert.Equal(10.1234.ToString("0.0000 KiB"), result);
        }

        [Fact]
        public void ReturnsBits()
        {
            // Arrange
            var b = BinaryByteSize.FromBits(10);

            // Act
            var result = b.ToString("##.#### b");

            // Assert
            Assert.Equal("10 b", result);
        }

        [Fact]
        public void ReturnsBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromBytes(10);

            // Act
            var result = b.ToString("##.#### B");

            // Assert
            Assert.Equal("10 B", result);
        }

        [Fact]
        public void ReturnsKibiBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10);

            // Act
            var result = b.ToString("##.#### KiB");

            // Assert
            Assert.Equal("10 KiB", result);
        }

        [Fact]
        public void ReturnsMebiBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromMebiBytes(10);

            // Act
            var result = b.ToString("##.#### MiB");

            // Assert
            Assert.Equal("10 MiB", result);
        }

        [Fact]
        public void ReturnsGibiBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromGibiBytes(10);

            // Act
            var result = b.ToString("##.#### GiB");

            // Assert
            Assert.Equal("10 GiB", result);
        }

        [Fact]
        public void ReturnsTebiBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromTebiBytes(10);

            // Act
            var result = b.ToString("##.#### TiB");

            // Assert
            Assert.Equal("10 TiB", result);
        }

        [Fact]
        public void ReturnsPebiBytes()
        {
            // Arrange
            var b = BinaryByteSize.FromPebiBytes(10);

            // Act
            var result = b.ToString("##.#### PiB");

            // Assert
            Assert.Equal("10 PiB", result);
        }

        [Fact]
        public void ReturnsSelectedFormat()
        {
            // Arrange
            var b = BinaryByteSize.FromTebiBytes(10);

            // Act
            var result = b.ToString("0.0 TiB");

            // Assert
            Assert.Equal(10.ToString("0.0 TiB"), result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZero()
        {
            // Arrange
            var b = BinaryByteSize.FromMebiBytes(.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("512 KiB", result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZeroForNegativeValues()
        {
            // Arrange
            var b = BinaryByteSize.FromMebiBytes(-.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("-512 KiB", result);
        }

        [Fact]
        public void ReturnsLargestMetricSuffixUsingCurrentCulture()
        {
            var originalCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");

            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10000);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal("9,77 MiB", result);

            CultureInfo.CurrentCulture = originalCulture;
        }

        [Fact]
        public void ReturnsLargestMetricSuffixUsingSpecifiedCulture()
        {
            // Arrange
            var b = BinaryByteSize.FromKibiBytes(10000);

            // Act
            var result = b.ToString("#.#", new CultureInfo("fr-FR"));

            // Assert
            Assert.Equal("9,8 MiB", result);
		}

		[Fact]
		public void ReturnsCultureSpecificFormat()
		{
			// Arrange
			var b = BinaryByteSize.FromKibiBytes(10.5);

			// Act
			var deCulture = new CultureInfo("de-DE");
			var result = b.ToString("0.0 KiB", deCulture);

			// Assert
			Assert.Equal(10.5.ToString("0.0 KiB", deCulture), result);
		}

        [Fact]
		public void ReturnsZeroBits()
		{
			// Arrange
			var b = BinaryByteSize.FromBits(0);

			// Act
			var result = b.ToString();

			// Assert
			Assert.Equal("0 b", result);
		}
	}
}
