# Overview

_Integer Mapper_ maps data structures to monotonically increasing consecutive integers starting from 0, with 0 being reserved for `null` or any collection that is empty. This applies to strings as well, with the empty string being mapped to 0.

Integer Mapper is useful for implementing hashing and equality in the sense that everything is always mapped to a predictable value. The value can serve as a hash code, but that can also directly be used to compare two objects.

It is also possible to get the original data back because the mapped integers preserve equality. This makes it useful for things like caching, or algorithms that need to represent complex data as a simple integer that is mapped to an array address.

# Usage

The core interface for mapping values is `IIntegerMapper`. All mappers implement this interface.

For example, to map `string` and then get the original string back from the assigned `uint`:

```C#
IIntegerMapper<string> mapper = new StringMapper();

mapper.Map("One");
mapper.Map("Two");
ulong lastValue = mapper.Map("Three");

for (uint i = MapConstants.FirstMappableInteger; i <= lastValue; i++)
{
    string originalValue = mapper.ReverseMap(i);
    Console.WriteLine($"{i} => {originalValue}");
}

// Output:
// 1 => One
// 2 => Two
// 3 => Three
```

Integer Mapper also supports `byte[]` and `IEnumberable<byte>`, ex.:

```C#
IIntegerMapper<byte[]> mapper1 = new ByteArrayMapper();
IIntegerMapper<IEnumerable<byte>> mapper2 = new ByteEnumerableMapper();
```

# Licence

Integer Mapper is released under the MIT open source licence. See LICENCE.txt in this repository for the full text.
