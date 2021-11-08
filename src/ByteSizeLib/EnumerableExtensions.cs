using System.Collections.Generic;
using System.Linq;

namespace ByteSizeLib
{
    public static class EnumerableExtensions
    {
        public static ByteSize Sum(this IEnumerable<ByteSize> byteSizes)
        {
            return byteSizes.Aggregate((current, byteSize) => current + byteSize);
        }
    }
}