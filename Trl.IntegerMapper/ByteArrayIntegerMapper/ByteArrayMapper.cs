using System.Linq;
using Trl.IntegerMapper.ByteEnumerableIntegerMapper;

namespace Trl.IntegerMapper.ByteArrayIntegerMapper
{
    public class ByteArrayMapper : IIntegerMapper<byte[]>
    {
        private readonly ByteEnumerableMapper _byteEnumerableMapper = new ByteEnumerableMapper();

        public ulong Map(byte[] inputValue) => _byteEnumerableMapper.Map(inputValue);

        public byte[] ReverseMap(ulong mappedValue) => _byteEnumerableMapper.ReverseMap(mappedValue).ToArray();

        public void Clear() => _byteEnumerableMapper.Clear();

        public ulong MappedObjectsCount => _byteEnumerableMapper.MappedObjectsCount;
    }
}
