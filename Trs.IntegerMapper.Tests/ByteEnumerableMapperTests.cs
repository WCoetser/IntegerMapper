using System;
using Xunit;
using Trs.IntegerMapper.ByteEnumerableIntegerMapper;

namespace Trs.IntegerMapper.Tests
{
    [Collection("ByteEnumerableMapperTests tests for mapping byte enumerables to integers")]
    public class ByteEnumerableMapperTests
    {
        [Fact]
        public void DefaultContainerShouldContainEmptyCase()
        {
            // Act
            var mapper = new ByteEnumerableMapper();

            // Assert
            Assert.Equal(1u, mapper.MappedObjectsCount);
            Assert.Equal(Array.Empty<byte>(), mapper.ReverseMap(0));
        }
    }
}
