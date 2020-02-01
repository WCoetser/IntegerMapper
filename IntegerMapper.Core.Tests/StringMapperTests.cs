using IntegerMapper.Core.String;
using Xunit;

namespace IntegerMapper.Core.Tests
{
    [Collection("StringMapper tests for mapping strings to integers")]
    public class StringMapperTests
    {
        [Fact]
        public void ShouldMapNullAndEmptyToZero()
        {
            // Arrange
            var IntegerMapper = new StringMapper();

            // Act
            var r1 = IntegerMapper.Map(null);
            var r2 = IntegerMapper.Map(string.Empty);

            // Assert
            Assert.Equal(MapConstants.NullOrEmpty, r1);
            Assert.Equal(MapConstants.NullOrEmpty, r2);
        }

        [Fact]
        public void ShouldNotMapWhitespaceToZero()
        {
            // Arrange
            var IntegerMapper = new StringMapper();

            // Act
            var r = IntegerMapper.Map("\t");

            // Assert
            Assert.NotEqual(MapConstants.NullOrEmpty, r); 
        }

        [Fact]
        public void ShouldMapValueToFirstAssignableInteger()
        {
            // Arrange
            var IntegerMapper = new StringMapper();

            // Act
            var r = IntegerMapper.Map("a");

            // Assert
            Assert.Equal(MapConstants.FirstMappableInteger, r);
        }

        [Fact]
        public void ShouldRepeatedlyMapSameValuesToSameInputs()
        {
            // Arrange
            var IntegerMapper = new StringMapper();
            var testCases = new[]
            {
                new { Input = "a", ExpectedOutput = MapConstants.FirstMappableInteger },
                new { Input = "ab", ExpectedOutput = MapConstants.FirstMappableInteger + 1 },
                new { Input = "bb", ExpectedOutput = MapConstants.FirstMappableInteger + 2 },
                new { Input = "cb", ExpectedOutput = MapConstants.FirstMappableInteger + 3 },
                new { Input = "cba", ExpectedOutput = MapConstants.FirstMappableInteger + 4 },
                new { Input = "cbb", ExpectedOutput = MapConstants.FirstMappableInteger + 5 },
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
