using System;
using System.Collections.Generic;
using System.Linq;

namespace Trl.IntegerMapper.ByteEnumerableIntegerMapper
{
    /// <summary>
    /// Maps equal <see cref="IEnumerable{byte}"/> to equal integers.
    /// </summary>
    public class ByteEnumerableMapper : IIntegerMapper<IEnumerable<byte>>
    {
        /// <summary>
        /// The collection of known mapped byte arrays
        /// </summary>
        private ByteEnumerableMapperNode _rootNode;

        /// <summary>
        /// Keeps track of which integers have been mapped to which values.
        /// </summary>
        private readonly List<ByteEnumerableMapperNode> _inverseMap;

        public ByteEnumerableMapper()
        {
            MappedObjectsCount = MapConstants.FirstMappableInteger;
            _rootNode = new ByteEnumerableMapperNode(null, null);
            _inverseMap = new List<ByteEnumerableMapperNode>
            {
                _rootNode
            };
        }

        public ulong Map(IEnumerable<byte> byteIterator)
        {
            var byteInputEnumerator = byteIterator?.GetEnumerator();
            if (byteIterator == null || !byteInputEnumerator.MoveNext())
            {
                return MapConstants.NullOrEmpty;
            }

            var currentNode = _rootNode;
            do
            {
                if (currentNode.NextNodes[byteInputEnumerator.Current] == null)
                {
                    currentNode.NextNodes[byteInputEnumerator.Current] = new ByteEnumerableMapperNode(byteInputEnumerator.Current, currentNode);
                }
                currentNode = currentNode.NextNodes[byteInputEnumerator.Current];
            } while (byteInputEnumerator.MoveNext());

            if (!currentNode.MappedValue.HasValue)
            {
                currentNode.MappedValue = MappedObjectsCount;
                _inverseMap.Add(currentNode);
                MappedObjectsCount++;
            }

            return currentNode.MappedValue.Value;
        }

        public IEnumerable<byte> ReverseMap(ulong mappedValue)
        {
            if (mappedValue >= MappedObjectsCount)
            {
                throw new Exception($"Value has not been mapped: {mappedValue}");
            }
            if (mappedValue == MapConstants.NullOrEmpty)
            {
                return Array.Empty<byte>();
            }
            var reverseNode = _inverseMap[(int)mappedValue];
            return reverseNode.GetRepresentedValue().Reverse();
        }

        public void Clear()
        {
            MappedObjectsCount = MapConstants.FirstMappableInteger;
            _rootNode = new ByteEnumerableMapperNode(null, null);
            _inverseMap.Clear();
        }

        public ulong MappedObjectsCount { get; private set; }
    }
}
