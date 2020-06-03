using System;
using System.Collections.Generic;

namespace Trl.IntegerMapper.ByteEnumerableIntegerMapper
{
    internal class ByteEnumerableMapperNode
    {
        internal ulong? MappedValue;
        internal readonly byte? AssociatedValue;
        private readonly ByteEnumerableMapperNode _parentNode;

        internal ByteEnumerableMapperNode[] NextNodes { get; private set; }

        internal ByteEnumerableMapperNode(byte? associatedValue, ByteEnumerableMapperNode parentNode)
        {
            NextNodes = new ByteEnumerableMapperNode[byte.MaxValue + 1];
            AssociatedValue = associatedValue;
            _parentNode = parentNode!;
        }

        internal IEnumerable<byte> GetRepresentedValue()
        {
            if (!AssociatedValue.HasValue)
            {
                throw new InvalidOperationException();
            }

            ByteEnumerableMapperNode currentNode = this;
            while (currentNode.AssociatedValue.HasValue)
            {
                yield return currentNode.AssociatedValue.Value;
                currentNode = currentNode._parentNode;
            }
        }
    }
}
