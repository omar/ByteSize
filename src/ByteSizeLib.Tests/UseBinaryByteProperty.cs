using Xunit;

namespace ByteSizeLib.Tests
{
    public class MetricKiloProperty
    {
        [Fact]
        public void UseBinaryByteSetToTrue_ComputeMetricValues()
        {
            // Arrange
            ByteSize.UseBinaryByte = false;
            
            // Act
            var b = ByteSize.FromTeraBytes(3);

            // Assert
            Assert.Equal(3000, b.GigaBytes);
            Assert.Equal(3000000, b.MegaBytes);
            Assert.Equal(3000000000, b.KiloBytes);
            Assert.Equal(3000000000000, b.Bytes);

            // Cleanup
            ByteSize.UseBinaryByte = true;
        }
    }
}