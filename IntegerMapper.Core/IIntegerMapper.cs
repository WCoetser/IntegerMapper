namespace IntegerMapper.Core
{
    /// <summary>
    /// Maps any object to any object to a monotonically increasing unsigned integer.
    /// Numbers are assigned from <see cref="MapConstants.FirstMappableInteger"/>.
    /// No numbers are skipped or "wasted"
    /// <see cref="MapConstants.NullOrEmpty"/> is used for null or empty values.
    /// </summary>
    /// <typeparam name="TIn">The type to be mapped.</typeparam>
    public interface IIntegerMapper<in TIn>
    {
        uint Map(TIn inputValue);
    }
}
