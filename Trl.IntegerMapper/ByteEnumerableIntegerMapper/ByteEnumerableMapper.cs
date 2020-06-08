using System;
using System.Collections.Generic;
using System.Linq;
using Trl.IntegerMapper.GenericSequenceIntegerMapper;

namespace Trl.IntegerMapper.ByteEnumerableIntegerMapper
{
    /// <summary>
    /// Maps equal <see cref="IEnumerable{byte}"/> to equal integers.
    /// </summary>
    public class ByteEnumerableMapper : IIntegerMapper<IEnumerable<byte>>
    {
        private readonly GenericSequenceMapper<byte> _genericSequenceMapper;

        public ByteEnumerableMapper()
            => _genericSequenceMapper = new GenericSequenceMapper<byte>(EqualityComparer<byte>.Default);

        public ulong MappedObjectsCount => _genericSequenceMapper.MappedObjectsCount;

        public void Clear() =>_genericSequenceMapper.Clear();

        public ulong Map(IEnumerable<byte> inputValue) => _genericSequenceMapper.Map(inputValue);

        public IEnumerable<byte> ReverseMap(ulong mappedValue) => _genericSequenceMapper.ReverseMap(mappedValue);
    }
}
