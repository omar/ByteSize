using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.ComponentModel;

namespace ByteSizeLib.Tests
{
    public class ByteSizeTypeConverterTests
    {
        [Fact]
        public void ConvertsToString()
        {
            var converter = TypeDescriptor.GetConverter(typeof(ByteSize));
            Assert.NotNull(converter);
            var bs = ByteSize.FromBytes(1024);
            var actual =converter.ConvertToString(bs);
            var expected = "1 KiB";
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(variant2TypesTestData))]
        public void ConvertsFromString(string input, ByteSize expected)
        {
            var converter = TypeDescriptor.GetConverter(typeof(ByteSize));
            Assert.NotNull(converter);
            var actualO = converter.ConvertFromString(input);
            var actual = Assert.IsType<ByteSize>(actualO);
            Assert.Equal(expected, actual);
        }

        public static readonly IEnumerable<object[]> variant2TypesTestData = new List<object[]>
        {
            new object []{
                "1 KiB",
                ByteSize.FromKibiBytes(1),
            },

            new object []{
                "1KiB",
                ByteSize.FromKibiBytes(1),
            },

            new object []{
                "1MiB",
                ByteSize.FromMebiBytes(1),
            },
            new object []{
                "1GiB",
                ByteSize.FromGibiBytes(1),
            },
        };

    }
}
