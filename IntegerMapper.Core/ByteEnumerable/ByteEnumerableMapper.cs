using System.Collections.Generic;

namespace IntegerMapper.Core.ByteEnumerable
{
    /// <summary>
    /// Maps equal <see cref="IEnumerable{byte}"/> to equal integers.
    /// </summary>
    public class ByteEnumerableMapper : IIntegerMapper<IEnumerable<byte>>
    {
        /// <summary>
        /// The collection of known mapped byte arrays
        /// </summary>
        private readonly ByteEnumerableMapperNode _nodes = new ByteEnumerableMapperNode();

        /// <summary>
        /// Next integer to assign to an input value.
        /// </summary>
        private uint _nextAssignableInteger = MapConstants.FirstMappableInteger;

        public uint Map(IEnumerable<byte>? byteIterator)
        {
            var byteInputEnumerator = byteIterator?.GetEnumerator();
            if (byteIterator == null || !(byteInputEnumerator!.MoveNext()))
            {
                return MapConstants.NullOrEmpty;
            }

            var currentNode = _nodes;
            do
            {
                if (currentNode.NextNodes[byteInputEnumerator.Current] == null)
                {
                    currentNode.NextNodes[byteInputEnumerator.Current] = new ByteEnumerableMapperNode();
                }
                currentNode = currentNode.NextNodes[byteInputEnumerator.Current];
            } while (byteInputEnumerator.MoveNext());

            if (!currentNode.MappedValue.HasValue)
            {
                currentNode.MappedValue = _nextAssignableInteger;
                _nextAssignableInteger++;
            }

            return currentNode.MappedValue.Value;
        }
    }
}
