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

        public MemoizerTests()
        {
            var doubleMapper = new EqualityComparerMapper<double>(EqualityComparer<double>.Default);
            var intMapper = new EqualityComparerMapper<int>(EqualityComparer<int>.Default);
            _memoizer = new Memoizer<int, double>(intMapper, doubleMapper);
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
    }
}
