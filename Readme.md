# Overview

_Integer Mapper_ maps data structures to monotonically increasing integers starting from 0, with 0 being reserved for `null` or any collection that is empty. This applies to strings as well, with the empty string being remapped to 0.

Integer Mapper is useful for implementing hashing and equality in the sense that everything is always mapped to a predictable value that can serve as a hash code, but that can also directly be used to compare two objects.

# Usage

The core interface for mapping values is `IIntegerMapper`. All mappers implement this interface.

For example, to map `string`:

```C#
IIntegerMapper<string> mapper = new StringMapper();
HashSet<uint> cache = new HashSet<uint>();

// First process ...
var i1 = mapper.Map("abc");
cache.Add(i1);

// Second process later on ...
var i2 = mapper.Map(string.Concat("ab","c"));
cache.Add(i2);

// prints "1"
Console.WriteLine(cache.Count);
```

Or to map `byte[]`:

```C#
IIntegerMapper<byte[]> mapper = new ByteEnumerableMapper();
HashSet<uint> cache = new HashSet<uint>();

// First process ...
var i1 = mapper.Map(new byte[] { 0x01,0x02,0x03 });
cache.Add(i1);

// Second process later on ...
var i2 = mapper.Map(new byte[] { 0x01, 0x02, 0x03 });
cache.Add(i2);

// At this point we only know of one buffer
Console.WriteLine(cache.Count);
```

# License

Integer Mapper is released under the MIT open source license. See LICENCE.txt in this repository for the full text.
