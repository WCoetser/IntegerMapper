using System;
using Xunit;
using Trl.IntegerMapper.ByteEnumerableIntegerMapper;

namespace Trl.IntegerMapper.Tests
{
    [Collection("ByteEnumerableMapperTests tests for mapping byte enumerables to integers")]
    public class ByteEnumerableMapperTests
    {
        private ByteEnumerableMapper _mapper;

        public ByteEnumerableMapperTests()
        {
            _mapper = new ByteEnumerableMapper();
        }

        [Fact]
        public void DefaultContainerShouldContainEmptyCase()
        {
            // Assert
            Assert.Equal(1u, _mapper.MappedObjectsCount);
            Assert.Equal(Array.Empty<byte>(), _mapper.ReverseMap(0));
        }

        [Fact]
        public void ShouldClear()
        {
            // Arrange
            _mapper.Map(new byte[] { 0x00, 0x01, 0x02 });

            // Act
            _mapper.Clear();

            // Assert
            Assert.Equal(1u, _mapper.MappedObjectsCount); // should only contain empty case
        }
    }
}
