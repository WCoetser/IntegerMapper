namespace Trs.IntegerMapper.Tests.Fixtures
{
    public class TestData<T>
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public T Input { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public ulong ExpectedOutput { get; set; }
    }
}
