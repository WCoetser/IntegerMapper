namespace IntegerMapper.Core.ByteEnumerable
{
    internal class ByteEnumerableMapperNode
    {
        internal uint? MappedValue;
        internal ByteEnumerableMapperNode[] NextNodes { get; private set; }

        internal ByteEnumerableMapperNode()
        {
            NextNodes = new ByteEnumerableMapperNode[byte.MaxValue + 1];
        }
    }
}
