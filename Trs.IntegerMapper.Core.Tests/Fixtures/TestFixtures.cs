namespace Trs.IntegerMapper.Core.Tests.Fixtures
{
    public static class TestFixtures
    {
        public static TestData<byte[]>[] GetTestDataForByteArrays()
            => new[]
            {
                new TestData<byte[]> { Input = new byte[] { 0x01 }, ExpectedOutput = MapConstants.FirstMappableInteger },
                new TestData<byte[]> { Input = new byte[] { 0x01, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 1 },
                new TestData<byte[]> { Input = new byte[] { 0x02, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 2 },
                new TestData<byte[]> { Input = new byte[] { 0x03, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 3 },
                new TestData<byte[]> { Input = new byte[] { 0x03, 0x02, 0x01 }, ExpectedOutput = MapConstants.FirstMappableInteger + 4 },
                new TestData<byte[]> { Input = new byte[] { 0x03, 0x02, 0x02 }, ExpectedOutput = MapConstants.FirstMappableInteger + 5 },
            };

        public static TestData<string>[] GetTestDataForString()
            => new[]
            {
                new TestData<string> { Input = "a", ExpectedOutput = MapConstants.FirstMappableInteger },
                new TestData<string> { Input = "ab", ExpectedOutput = MapConstants.FirstMappableInteger + 1 },
                new TestData<string> { Input = "bb", ExpectedOutput = MapConstants.FirstMappableInteger + 2 },
                new TestData<string> { Input = "cb", ExpectedOutput = MapConstants.FirstMappableInteger + 3 },
                new TestData<string> { Input = "cba", ExpectedOutput = MapConstants.FirstMappableInteger + 4 },
                new TestData<string> { Input = "cbb", ExpectedOutput = MapConstants.FirstMappableInteger + 5 },
            };
    }
}
