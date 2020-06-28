using Xunit;
using Trl.IntegerMapper.StringIntegerMapper;
using Trl.IntegerMapper.Tests.Fixtures;

namespace Trl.IntegerMapper.Tests
{
    [Collection("StringMapper tests for mapping strings to integers")]
    public class StringMapperTests
    {
        private readonly StringMapper _mapper;

        public StringMapperTests()
        {
            _mapper = new StringMapper();
        }

        [Fact]
        public void DefaultContainerShouldContainEmptyCase()
        {
            // Assert
            Assert.Equal(1u, _mapper.MappedObjectsCount);
            Assert.Equal(string.Empty, _mapper.ReverseMap(0));
        }

        [Fact]
        public void ShouldGetExistingValueWithoutAddingNewData()
        {
            // Arrange
            var initial1 = _mapper.Map("abc");

            // Act
            ulong existingObjectCountBefore = _mapper.MappedObjectsCount;
            ulong? existing1, existing2;
            var found1 = _mapper.TryGetMappedValue("abc", out existing1);
            var found2 = _mapper.TryGetMappedValue("456", out existing2);
            ulong existingObjectCountAfter = _mapper.MappedObjectsCount;

            // Assert
            Assert.Equal(existingObjectCountBefore, existingObjectCountAfter);
            Assert.True(found1);
            Assert.False(found2);
            Assert.Equal(initial1, existing1.Value);
            Assert.Null(existing2);
        }

        [Fact]
        public void ShouldMapNullAndEmptyToZero()
        {
            // Act
            var r1 = _mapper.Map(null);
            var r2 = _mapper.Map(string.Empty);

            // Assert
            Assert.Equal(MapConstants.NullOrEmpty, r1);
            Assert.Equal(MapConstants.NullOrEmpty, r2);
        }

        [Fact]
        public void ShouldNotMapWhitespaceToZero()
        {
            // Act
            var r = _mapper.Map("\t");

            // Assert
            Assert.NotEqual(MapConstants.NullOrEmpty, r);
        }

        [Fact]
        public void ShouldMapValueToFirstAssignableInteger()
        {
            // Act
            var r = _mapper.Map("a");

            // Assert
            Assert.Equal(MapConstants.FirstMappableInteger, r);
        }

        [Fact]
        public void ShouldRepeatedlyMapSameValuesToSameInputs()
        {
            // Arrange
            var testCases = TestFixtures.GetTestDataForString();

            foreach (var testCase in testCases)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Act
                    var r = _mapper.Map(testCase.Input);

                    // Assert
                    Assert.Equal(testCase.ExpectedOutput, r);
                }
            }
            Assert.Equal(testCases.Length + 1, (int)_mapper.MappedObjectsCount);
        }

        [Fact]
        public void ShouldDoInverseMapForZero()
        {
            // Act            
            var rInverse = _mapper.ReverseMap(MapConstants.NullOrEmpty);

            // Assert
            Assert.Equal(rInverse, string.Empty);
        }

        [Fact]
        public void ShouldDoInverseMapForValues()
        {
            // Arrange
            var testData = TestFixtures.GetTestDataForString();

            foreach (var test in testData)
            {
                // Act
                var r = _mapper.Map(test.Input);
                var rInverse = _mapper.ReverseMap(r);

                // Assert
                Assert.Equal(test.Input, rInverse);
            }
        }

        [Fact]
        public void ShouldClear()
        {
            // Arrange
            _mapper.Map("testing 123");

            // Act
            _mapper.Clear();

            // Assert
            Assert.Equal(1u, _mapper.MappedObjectsCount); // should only contain empty case
        }
    }
}
