﻿using Xunit;

namespace ByteSizeLib.Tests.Binary
{
    public class CreatingMethods
    {
        [Fact]
        public void Constructor()
        {
            // Arrange
            double bytes = 1125899906842624;

            // Act
            var result = new ByteSize(bytes);

            // Assert
            Assert.Equal(bytes * 8, result.Bits);
            Assert.Equal(bytes, result.Bytes);
            Assert.Equal(bytes / 1024, result.KibiBytes);
            Assert.Equal(bytes / 1024 / 1024, result.MebiBytes);
            Assert.Equal(bytes / 1024 / 1024 / 1024, result.GibiBytes);
            Assert.Equal(bytes / 1024 / 1024 / 1024 / 1024, result.TebiBytes);
            Assert.Equal(1, result.PebiBytes);
        }

        [Fact]
        public void FromKibiBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromKibiBytes(value);

            // Assert
            Assert.Equal(1536, result.Bytes);
            Assert.Equal(1.5, result.KibiBytes);
        }

        [Fact]
        public void FromMebiBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromMebiBytes(value);

            // Assert
            Assert.Equal(1572864, result.Bytes);
            Assert.Equal(1.5, result.MebiBytes);
        }

        [Fact]
        public void FromGibiBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromGibiBytes(value);

            // Assert
            Assert.Equal(1610612736, result.Bytes);
            Assert.Equal(1.5, result.GibiBytes);
        }

        [Fact]
        public void FromTebiBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromTebiBytes(value);

            // Assert
            Assert.Equal(1649267441664, result.Bytes);
            Assert.Equal(1.5, result.TebiBytes);
        }

        [Fact]
        public void FromPebiBytesMethod()
        {
            // Arrange
            double value = 1.5;

            // Act
            var result = ByteSize.FromPebiBytes(value);

            // Assert
            Assert.Equal(1688849860263936, result.Bytes);
            Assert.Equal(1.5, result.PebiBytes);
        }
    }
}
