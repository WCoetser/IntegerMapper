using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trl.IntegerMapper.GenericSequenceIntegerMapper
{
    /// <summary>
    /// Maps sequences of type <see cref="T"/> into a tree structure for 
    /// efficient lookup.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericSequenceMapper<TItem> : IIntegerMapper<IEnumerable<TItem>>
    {
        private readonly IEqualityComparer<TItem> _subnodeEqualityComparer;
        private GenericSequenceNode<TItem> _root;
        private readonly Dictionary<ulong, GenericSequenceNode<TItem>> _inverseLookup;

        public GenericSequenceMapper(IEqualityComparer<TItem> equalityComparer)
        {
            MappedObjectsCount = MapConstants.FirstMappableInteger;
            _subnodeEqualityComparer = equalityComparer;
            _root = new GenericSequenceNode<TItem>(default, null, equalityComparer);
            _inverseLookup = new Dictionary<ulong, GenericSequenceNode<TItem>>();
        }

        public ulong MappedObjectsCount { get; private set; }

        public void Clear()
        {
            MappedObjectsCount = MapConstants.FirstMappableInteger;
            _inverseLookup.Clear();
            _root = null;
        }

        public ulong Map(IEnumerable<TItem> inputSequence)
        {
            if (inputSequence == null || !inputSequence.Any())
            {
                return MapConstants.NullOrEmpty;
            }

            GenericSequenceNode<TItem> current = _root;
            var enumerator = inputSequence.GetEnumerator();
            ulong mapValue = 0;
            bool hasNext;
            do
            {
                hasNext = enumerator.MoveNext();
                if (!hasNext)
                {
                    if (current.AssignedInteger.HasValue)
                    {
                        mapValue = current.AssignedInteger.Value;
                    }
                    else
                    {
                        mapValue = MappedObjectsCount++;
                        current.AssignedInteger = mapValue;
                        _inverseLookup.Add(mapValue, current);
                    }
                }
                else
                {
                    var item = enumerator.Current;
                    if (!current.NextValues.TryGetValue(item, out GenericSequenceNode<TItem> next))
                    {
                        next = new GenericSequenceNode<TItem>(item, current, _subnodeEqualityComparer);
                        current.NextValues.Add(item, next);
                    }
                    current = next;
                }
            }
            while (hasNext);
            return mapValue;
        }

        public IEnumerable<TItem> ReverseMap(ulong mappedValue)
        {
            if (mappedValue == MapConstants.NullOrEmpty)
            {
                return Enumerable.Empty<TItem>();
            }
            GenericSequenceNode<TItem> current;
            if (!_inverseLookup.TryGetValue(mappedValue, out current))
            {
                throw new Exception($"Unmapped value: {mappedValue}");
            }
            var sequence = new LinkedList<TItem>();
            while (current.Parent != null)
            {
                sequence.AddLast(current.Value);
                current = current.Parent;
            }
            return sequence.Reverse();
        }
    }
}
