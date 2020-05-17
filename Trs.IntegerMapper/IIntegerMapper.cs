namespace Trs.IntegerMapper
{
    /// <summary>
    /// Maps any object to any object to a monotonically consecutively increasing unsigned integer.
    /// Numbers are assigned from <see cref="MapConstants.FirstMappableInteger"/>.
    /// No numbers are skipped or "wasted"
    /// <see cref="MapConstants.NullOrEmpty"/> is used for null or empty values.
    /// </summary>
    /// <typeparam name="TIn">The type to be mapped.</typeparam>
    public interface IIntegerMapper<TIn>
    {
        /// <summary>
        /// Maps an input value to a monotonically consecutively increasing unsigned integer.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        ulong Map(TIn inputValue);

        /// <summary>
        /// Gets the input value that was associated with an integer using <see cref="Map(TIn)"/>
        /// </summary>
        /// <param name="mappedValue">The output of <see cref="Map(TIn)"/>.</param>
        /// <returns></returns>
        TIn ReverseMap(ulong mappedValue);

        /// <summary>
        /// Gets the number of objects that have been mapped.
        /// Null and empty collections must always be mapped to 0,
        /// therefore there will always al least be 1 element in an <see cref="IntegerMapper"/>.
        /// </summary>
        ulong MappedObjectsCount { get; }

        /// <summary>
        /// Clears all values stored and resets counts.
        /// </summary>
        void Clear();
    }
}
