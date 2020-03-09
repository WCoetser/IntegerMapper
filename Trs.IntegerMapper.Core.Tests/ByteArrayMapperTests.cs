using System;
using Xunit;
using System.Linq;

using Trs.IntegerMapper.Core.ByteEnumerableIntegerMapper;
using Trs.IntegerMapper.Core.ByteArrayIntegerMapper;
using Trs.IntegerMapper.Core.Tests.Fixtures;

namespace Trs.IntegerMapper.Core.Tests
{
    [Collection("ByteArrayMapper tests for mapping byte enumerables to integers")]
    public class ByteArrayMapperTests
    {
        [Fact]
        public void ShouldMapNullOrEmptyToZero()
        {
            // Arrange
            var mapper = new ByteArrayMapper();

            // Act
            var r1 = mapper.Map(null);
            var r2 = mapper.Map(Array.Empty<byte>());

            // Assert
            Assert.Equal(MapConstants.NullOrEmpty, r1);
            Assert.Equal(MapConstants.NullOrEmpty, r2);
        }

        [Fact]
        public void ShouldMapValueToFirstAssignableInteger()
        {
            // Arrange
            var mapper = new ByteArrayMapper();

            // Act
            var r = mapper.Map(new byte[] { 0x01 });

            // Assert
            Assert.Equal(MapConstants.FirstMappableInteger, r);
        }

        [Fact]
        public void ShouldRepeatedlyMapSameValuesToSameInputs()
        {
            // Arrange
            var mapper = new ByteArrayMapper();
            var testData = TestFixtures.GetTestDataForByteArrays();

            foreach (var testCase in testData)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Act
                    var r = mapper.Map(testCase.Input);

                    // Assert
                    Assert.Equal(testCase.ExpectedOutput, r);
                }
            }
        }

        [Fact]
        public void ShouldDoInverseMapForZero()
        {
            // Arrange
            var mapper = new ByteArrayMapper();
            var mappedValue = mapper.Map(Array.Empty<byte>());

            // Act
            var retrievedValue = mapper.ReverseMap(mappedValue);

            // Assert
            Assert.Empty(retrievedValue);
        }

        [Fact]
        public void ShouldDoInverseMapForValues()
        {
            // Arrange
            var mapper = new ByteEnumerableMapper();
            var testData = TestFixtures.GetTestDataForByteArrays();

            foreach (var testCase in testData)
            {
                // Act
                var r = mapper.Map(testCase.Input);
                var rInverse = mapper.ReverseMap(r);

                // Assert
                Assert.True(Enumerable.SequenceEqual(testCase.Input, rInverse));
            }
        }
    }
}
