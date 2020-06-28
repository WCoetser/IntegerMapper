using System;
using System.Collections.Generic;

namespace Trl.IntegerMapper.EqualityComparerIntegerMapper
{
    public class EqualityComparerMapper<T> : IIntegerMapper<T>
    {
        private readonly Dictionary<T, ulong> _mappedValues;
        private readonly Dictionary<ulong, T> _inverseMappedValues;

        public ulong MappedObjectsCount { get; private set; }

        public EqualityComparerMapper(IEqualityComparer<T> equalityComparer)
        => (MappedObjectsCount, _mappedValues, _inverseMappedValues) 
            = (MapConstants.FirstMappableInteger, new Dictionary<T, ulong>(equalityComparer), new Dictionary<ulong, T>());

        public ulong Map(T inputValue)
        {
            if (_mappedValues.TryGetValue(inputValue, out ulong returnValue))
            {
                return returnValue;
            }
            returnValue = MappedObjectsCount;
            _mappedValues.Add(inputValue, returnValue);
            _inverseMappedValues.Add(MappedObjectsCount, inputValue);
            MappedObjectsCount++;
            return returnValue;
        }

        public T ReverseMap(ulong mappedValue)
        =>  (mappedValue, mappedValue >= MappedObjectsCount) switch
            {
                (MapConstants.NullOrEmpty, false) => default,
                (_, true) => throw new Exception($"Value has not been mapped: {mappedValue}"),
                (_, false) => _inverseMappedValues[mappedValue]
            };

        public void Clear()
        {
            MappedObjectsCount = MapConstants.FirstMappableInteger;
            _mappedValues.Clear();
            _inverseMappedValues.Clear();
        }

        public bool TryGetMappedValue(T inputValue, out ulong? mappedValue)
        {
            ulong retMappedValue;
            var retFound = _mappedValues.TryGetValue(inputValue, out retMappedValue);
            mappedValue = retMappedValue switch
            {
                0 => null,
                _ => retMappedValue
            };
            return retFound;
        }

    }
}
