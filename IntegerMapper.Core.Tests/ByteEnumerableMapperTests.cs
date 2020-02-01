using System;
using Xunit;
using IntegerMapper.Core.ByteEnumerable;

namespace IntegerMapper.Core.Tests
{
    [Collection("ByteEnumerableMapper tests for mapping byte enumerables to integers")]
    public class ByteEnumerableMapperTests
    {
        [Fact]
        public void ShouldMapNullOrEmptyToZero()
        {
            // Arrange
            var IntegerMapper = new ByteEnumerableMapper();

            // Act
            var r1 = IntegerMapper.Map(null);
            var r2 = IntegerMapper.Map(Array.Empty<byte>());

            // Assert
            Assert.Equal(MapConstants.NullOrEmpty, r1);
            Assert.Equal(MapConstants.NullOrEmpty, r2);
        }

        [Fact]
        public void ShouldMapValueToFirstAssignableInteger()
        {
            // Arrange
            var IntegerMapper = new ByteEnumerableMapper();

            // Act
            var r = IntegerMapper.Map(new byte[] { 0x01 });

            // Assert
            Assert.Equal(MapConstants.FirstMappableInteger, r);
        }

        [Fact]
        public void ShouldRepeatedlyMapSameValuesToSameInputs()
        {
            // Arrange
            var IntegerMapper = new ByteEnumerableMapper();
            var testCases = new[]
            {
                new { Input = new byte[] { 0x01 }, ExpectedOutput = MapConstants.FirstMappableInteger },
                new { Input = new byte[] { 0x01, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 1 },
                new { Input = new byte[] { 0x02, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 2 },
                new { Input = new byte[] { 0x03, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 3 },
                new { Input = new byte[] { 0x03, 0x02, 0x01 }, ExpectedOutput = MapConstants.FirstMappableInteger + 4 },
                new { Input = new byte[] { 0x03, 0x02, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 5 },
            };

            foreach (var testCase in testCases)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Act
                    var r = IntegerMapper.Map(testCase.Input);

                    // Assert
                    Assert.Equal(testCase.ExpectedOutput, r);
                }
            }
        }
    }
}
