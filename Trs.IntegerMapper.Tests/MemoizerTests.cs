using System;
using System.Collections.Generic;
using System.Linq;
using Trs.IntegerMapper.EqualityComparerIntegerMapper;
using Trs.IntegerMapper.Memoization;
using Xunit;

namespace Trs.IntegerMapper.Tests
{
    public class MemoizerTests
    {
        private readonly Memoizer<int, double> _memoizer;
        private readonly EqualityComparerMapper<double> _outputMapper;
        private readonly EqualityComparerMapper<int> _inputMapper;

        public MemoizerTests()
        {
            _outputMapper = new EqualityComparerMapper<double>(EqualityComparer<double>.Default);
            _inputMapper = new EqualityComparerMapper<int>(EqualityComparer<int>.Default);
            _memoizer = new Memoizer<int, double>(_inputMapper, _outputMapper);
        }

        private double FibonacciClosedForm(int n)
        {
            // Binet's formula, wikipedia
            double phi = (1.0 + Math.Sqrt(5)) / 2.0; // golden ratio
            return (Math.Pow(phi, n) - Math.Pow(1.0 - phi, n)) / Math.Sqrt(5);
        }

        [Fact]
        public void ShouldMemoizeAndGetOutput()
        {
            // Arrange
            var testValues = Enumerable.Range(0, 10).Select(FibonacciClosedForm).ToArray();

            // Act
            for (int i = 0; i < testValues.Length; i++)
            {
                _memoizer.Memoize(i, testValues[i]);
            }

            // Assert
            for (int i = 0; i < testValues.Length; i++)
            {
                var value = _memoizer.GetOutput(i);
                Assert.Equal(testValues[i], value);
            }
        }

        [Fact]
        public void ShouldOverwritePreviousMemoizedValues()
        {
            // Arrange & Act
            _memoizer.Memoize(1, 2);
            _memoizer.Memoize(1, 3);
            var outputValue = _memoizer.GetOutput(1);

            // Assert
            Assert.Equal(3, outputValue);
        }

        [Fact]
        public void ShouldGetDefaultOutputForUnknownInput()
        {
            // Arrange & Act
            var outputValue = _memoizer.GetOutput(123);

            // Assert
            Assert.Equal(0, outputValue);
        }

        [Fact]
        public void ShouldClearAllIntegerMappersAndInputOutputMappings()
        {
            // Arrange
            _memoizer.Memoize(1, 2);
            _memoizer.Memoize(3, 4);

            // Act
            _memoizer.ClearAll();

            // Assert
            Assert.Equal(1u, _inputMapper.MappedObjectsCount); // only contains empty case
            Assert.Equal(1u, _outputMapper.MappedObjectsCount); // only contains empty case
            Assert.Equal(0, _memoizer.GetOutput(1));
            Assert.Equal(0, _memoizer.GetOutput(3));
        }
    }
}
