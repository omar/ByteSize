using System.Globalization;
using System.Threading;
using Xunit;

namespace ByteSizeLib.Tests
{
    public class ToStringMethod
    {
        [Fact]
        public void ReturnsLargestMetricSuffix()
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
        public void ReturnsKiloBytes()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10);

            // Act
            var result = b.ToString("##.#### KB");

            // Assert
            Assert.Equal("10 KB", result);
        }

        [Fact]
        public void ReturnsMegaBytes()
        {
            // Arrange
            var b = ByteSize.FromMegaBytes(10);

            // Act
            var result = b.ToString("##.#### MB");

            // Assert
            Assert.Equal("10 MB", result);
        }

        [Fact]
        public void ReturnsGigaBytes()
        {
            // Arrange
            var b = ByteSize.FromGigaBytes(10);

            // Act
            var result = b.ToString("##.#### GB");

            // Assert
            Assert.Equal("10 GB", result);
        }

        [Fact]
        public void ReturnsTeraBytes()
        {
            // Arrange
            var b = ByteSize.FromTeraBytes(10);

            // Act
            var result = b.ToString("##.#### TB");

            // Assert
            Assert.Equal("10 TB", result);
        }

        [Fact]
        public void ReturnsPetaBytes()
        {
            // Arrange
            var b = ByteSize.FromPetaBytes(10);

            // Act
            var result = b.ToString("##.#### PB");

            // Assert
            Assert.Equal("10 PB", result);
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
        public void ReturnsLargestMetricPrefixLargerThanZero()
        {
            // Arrange
            var b = ByteSize.FromMegaBytes(.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("512 KB", result);
        }

        [Fact]
        public void ReturnsLargestMetricPrefixLargerThanZeroForNegativeValues()
        {
            // Arrange
            var b = ByteSize.FromMegaBytes(-.5);

            // Act
            var result = b.ToString("#.#");

            // Assert
            Assert.Equal("-512 KB", result);
        }

        [Fact]
        public void ReturnsLargestMetricSuffixUsingCurrentCulture()
        {
#if NETCOREAPP1_1
            var originalCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
#else
            var originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
#endif

            // Arrange
            var b = ByteSize.FromKiloBytes(10000);

            // Act
            var result = b.ToString();

            // Assert
            Assert.Equal("9,77 MB", result);

#if NETCOREAPP1_1
            CultureInfo.CurrentCulture = originalCulture;
#else
            Thread.CurrentThread.CurrentCulture = originalCulture;
#endif
        }

        [Fact]
        public void ReturnsLargestMetricSuffixUsingSpecifiedCulture()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10000);

            // Act
#if NETCOREAPP1_1
            var result = b.ToString("#.#", new CultureInfo("fr-FR"));
#else
            var result = b.ToString("#.#", CultureInfo.CreateSpecificCulture("fr-FR"));
#endif

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
		public void ReturnsZeroBits()
		{
			// Arrange
			var b = ByteSize.FromBits(0);

			// Act
			var result = b.ToString();

			// Assert
			Assert.Equal("0 b", result);
		}
	}
}
