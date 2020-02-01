using System.Linq;
using System.Text;
using IntegerMapper.Core.ByteEnumerableIntegerMapper;

namespace IntegerMapper.Core.StringIntegerMapper
{
    /// <summary>
    /// Maps equal strings to equal integers.
    /// </summary>
    public class StringMapper : IIntegerMapper<string>
    {
        private readonly ByteEnumerableMapper _integerMapper = new ByteEnumerableMapper();

        public uint Map(string? inputValue)
        {
            if (string.IsNullOrEmpty(inputValue))
            {
                return MapConstants.NullOrEmpty;
            }
            else
            {
                byte[] byteString = Encoding.UTF8.GetBytes(inputValue);
                return _integerMapper.Map(byteString);
            }
        }

        public string ReverseMap(uint mappedValue)
        {
            if (mappedValue == MapConstants.NullOrEmpty)
            {
                return string.Empty;
            }
            else
            {
                byte[] stringAsByteArray = _integerMapper.ReverseMap(mappedValue).ToArray();
                return Encoding.UTF8.GetString(stringAsByteArray);
            }
        }
    }
}
