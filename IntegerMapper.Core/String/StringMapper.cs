using System.Text;
using IntegerMapper.Core.ByteEnumerable;

namespace IntegerMapper.Core.String
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
    }
}
