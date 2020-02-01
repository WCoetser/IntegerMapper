using System.Linq;
using IntegerMapper.Core.ByteEnumerableIntegerMapper;

namespace IntegerMapper.Core.ByteArrayIntegerMapper
{
    public class ByteArrayMapper : IIntegerMapper<byte[]>
    {
        private readonly ByteEnumerableMapper _byteEnumerableMapper = new ByteEnumerableMapper();

        public uint Map(byte[]? inputValue) => _byteEnumerableMapper.Map(inputValue);

        public byte[] ReverseMap(uint mappedValue) => _byteEnumerableMapper.ReverseMap(mappedValue).ToArray();
    }
}
