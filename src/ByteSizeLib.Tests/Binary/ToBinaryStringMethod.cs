using Xunit;

namespace ByteSizeLib.Tests.BinaryByteSizeTests
{
    public class ToBinaryStringMethod
    {

        [Fact]
        public void ReturnsDefaultRepresenation()
        {
            // Arrange
            var b = ByteSize.FromKiloBytes(10);

            // Act
            var result = b.ToBinaryString();

            // Assert
            Assert.Equal("9.77 KiB", result);
        }
	}
}
