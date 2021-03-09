using Xunit;

namespace ByteSizeLib.Tests.BinaryByteSizeTests
{
    public class ToStringMethod
    {
        [Fact]
        public void ReturnsKibiBytes()
        {
            // Arrange
            var b = ByteSize.FromKibiBytes(10);

            // Act
            var result = b.ToString("##.#### KiB");

            // Assert
            Assert.Equal("10 KiB", result);
        }

        [Fact]
        public void ReturnsMebiBytes()
        {
            // Arrange
            var b = ByteSize.FromMebiBytes(10);

            // Act
            var result = b.ToString("##.#### MiB");

            // Assert
            Assert.Equal("10 MiB", result);
        }

        [Fact]
        public void ReturnsGibiBytes()
        {
            // Arrange
            var b = ByteSize.FromGibiBytes(10);

            // Act
            var result = b.ToString("##.#### GiB");

            // Assert
            Assert.Equal("10 GiB", result);
        }

        [Fact]
        public void ReturnsTebiBytes()
        {
            // Arrange
            var b = ByteSize.FromTebiBytes(10);

            // Act
            var result = b.ToString("##.#### TiB");

            // Assert
            Assert.Equal("10 TiB", result);
        }

        [Fact]
        public void ReturnsPebiBytes()
        {
            // Arrange
            var b = ByteSize.FromPebiBytes(10);

            // Act
            var result = b.ToString("##.#### PiB");

            // Assert
            Assert.Equal("10 PiB", result);
        }

        [Fact]
        public void ReturnsSelectedFormat()
        {
            // Arrange
            var b = ByteSize.FromTebiBytes(10);

            // Act
            var result = b.ToString("0.0 TiB");

            // Assert
            Assert.Equal(10.ToString("0.0 TiB"), result);
        }
    }
}
