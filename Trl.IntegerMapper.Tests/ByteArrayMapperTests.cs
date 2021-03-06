﻿using System;
using System.Linq;
using Xunit;
using Trl.IntegerMapper.ByteEnumerableIntegerMapper;
using Trl.IntegerMapper.ByteArrayIntegerMapper;
using Trl.IntegerMapper.Tests.Fixtures;

namespace Trl.IntegerMapper.Tests
{
    [Collection("ByteArrayMapper tests for mapping byte arrays to integers")]
    public class ByteArrayMapperTests
    {
        private readonly ByteArrayMapper _mapper;

        public ByteArrayMapperTests()
        {
            _mapper = new ByteArrayMapper();
        }


        [Fact]
        public void DefaultContainerShouldContainEmptyCase()
        {
            // Assert
            Assert.Equal(1u, _mapper.MappedObjectsCount);
            Assert.Equal(Array.Empty<byte>(), _mapper.ReverseMap(0));
        }

        [Fact]
        public void ShouldMapNullOrEmptyToZero()
        {
            // Act
            var r1 = _mapper.Map(null);
            var r2 = _mapper.Map(Array.Empty<byte>());

            // Assert
            Assert.Equal(MapConstants.NullOrEmpty, r1);
            Assert.Equal(MapConstants.NullOrEmpty, r2);
        }

        [Fact]
        public void ShouldMapValueToFirstAssignableInteger()
        {
            // Act
            var r = _mapper.Map(new byte[] { 0x01 });

            // Assert
            Assert.Equal(MapConstants.FirstMappableInteger, r);
        }

        [Fact]
        public void ShouldRepeatedlyMapSameValuesToSameInputs()
        {
            // Arrange            
            var testData = TestFixtures.GetTestDataForByteArrays();
            foreach (var testCase in testData)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Act
                    var r = _mapper.Map(testCase.Input);

                    // Assert
                    Assert.Equal(testCase.ExpectedOutput, r);
                }
            }

            Assert.Equal(testData.Length + 1, (int)_mapper.MappedObjectsCount);
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
            var testData = TestFixtures.GetTestDataForByteArrays();

            foreach (var testCase in testData)
            {
                // Act
                var r = _mapper.Map(testCase.Input);
                var rInverse = _mapper.ReverseMap(r);

                // Assert
                Assert.True(Enumerable.SequenceEqual(testCase.Input, rInverse));
            }
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

        [Fact]
        public void ShouldGetExistingValueWithoutAddingNewData()
        {
            // Arrange
            var initial1 = _mapper.Map(new byte[] { 0x00, 0x01, 0x02 });

            // Act
            ulong existingObjectCountBefore = _mapper.MappedObjectsCount;
            ulong? existing1, existing2;
            var found1 = _mapper.TryGetMappedValue(new byte[] { 0x00, 0x01, 0x02 }, out existing1);
            var found2 = _mapper.TryGetMappedValue(new byte[] { 0x00, 0x01 }, out existing2);
            ulong existingObjectCountAfter = _mapper.MappedObjectsCount;

            // Assert
            Assert.Equal(existingObjectCountBefore, existingObjectCountAfter);
            Assert.True(found1);
            Assert.False(found2);
            Assert.Equal(initial1, existing1.Value);
            Assert.Null(existing2);
        }

    }
}
