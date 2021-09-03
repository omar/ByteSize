using System.Globalization;
using Xunit;

namespace ByteSizeLib.Tests.Binary
{
    public class ToBinaryStringMethod
    {

        [Fact]
        public void ReturnsDefaultRepresentation()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10);

            // Act
            var result = b.ToBinaryString(CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("9.77 KiB", result);
        }

        [Fact]
        public void ReturnsDefaultRepresentationCurrentCulture()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10);
            var s = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            // Act
            var result = b.ToBinaryString(CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal($"9{s}77 KiB", result);
        }
    }
}
