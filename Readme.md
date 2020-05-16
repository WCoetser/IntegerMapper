# Overview

** NB: THIS IS PRE-RELEASE SOFTWARE **

_Integer Mapper_ maps data structures to monotonically increasing consecutive integers starting from 0, with 0 being reserved for `null` or any collection that is empty. This applies to strings as well, with the empty string being mapped to 0.

Integer Mapper is useful for implementing hashing and equality in the sense that everything is always mapped to a predictable value. The value can serve as a hash code, but that can also directly be used to compare two objects.

It is also possible to get the original data back because the mapped integers preserve equality. This makes it useful for things like caching, or algorithms that need to represent complex data as a simple integer that is mapped to an array address.

_Integer Mapper_ is also well suited to memoization, therefore it includes support for memoization. Any type that has an integer mapper associated with it could also be used save inputs and outputs for functions.

 _Trs.IntegerMapper_ is build on .NET Standard 2.1 for cross-platform compatibility.

# Using the Integer Mapper

The core interface for mapping values is `IIntegerMapper`. All mappers implement this interface.

For example, to map `string` and then get the original string back from the assigned `uint`:

```C#
IIntegerMapper<string> mapper = new StringMapper();

mapper.Map("One");
mapper.Map("Two");
mapper.Map("Three");

for (uint i = 0; i < mapper.MappedObjectsCount; i++)
{
    string originalValue = mapper.ReverseMap(i);
    Console.WriteLine($"{i} => {originalValue}");
}

// Output:
// 0 =>
// 1 => One
// 2 => Two
// 3 => Three
```

Note that all collections contain the empty case, which is mapped to 0. For example, `StringMapper` maps 0 to the empty string. This also means that all collections contain at least one element.

Integer Mapper also supports `byte[]` and `IEnumberable<byte>`, ex.:

```C#
IIntegerMapper<byte[]> mapper1 = new ByteArrayMapper();
IIntegerMapper<IEnumerable<byte>> mapper2 = new ByteEnumerableMapper();
```

# Integer Mapper with the `IEqualityComparer<T>`

Integer mapper allows you to map any .NET type with an equality comparer. 

For example, to map `int` values, the following code could be used:

```C#
var intMapper = new EqualityComparerMapper<int>(EqualityComparer<int>.Default);
```

# Using the memoizer

In order to use the memoizer, declare integer mappers for the input and output types of the function being memoized, and pass it in.

For example, if you want to calculate the first 92 Fibonacci numbers, and you are using the recursive formula, you can speed it up by using memoization:

```C#
var intMapper = new EqualityComparerMapper<long>(EqualityComparer<long>.Default);
var nullableIntMapper = new EqualityComparerMapper<long?>(EqualityComparer<long?>.Default);

var memoizer = new Memoizer<long, long?>(intMapper, nullableIntMapper);

long fast_fib(long n)
{
    var existingSolution = memoizer.GetOutput(n);
    var returnValue = (n, existingSolution.HasValue) switch
    {
        (0, _) => 0,
        (1, _) => 1,
        (_, false) => fast_fib(n - 1) + fast_fib(n - 2),
        (_, true) => existingSolution.Value
    };
    memoizer.Memoize(n, returnValue);
    return returnValue;
}

for (int i = 0; i < 92; i++)
{
    Console.WriteLine($"Fibonacci number {i + 1} is {fast_fib(i)}");
}

// Output:
// Fibonacci number 1 is 0
// Fibonacci number 2 is 1
// Fibonacci number 3 is 1
// Fibonacci number 4 is 2
// ...
// Fibonacci number 90 is 1779979416004714189
// Fibonacci number 91 is 2880067194370816120
// Fibonacci number 92 is 4660046610375530309
```

`ValueTuple` can be used for functions with multiple input parameters, ex.:

```C#
var inputEqualityComparer = EqualityComparer<ValueTuple<string, int>>.Default;
var inputMapper = new EqualityComparerMapper<ValueTuple<string, int>>(inputEqualityComparer);
var outputMapper = new EqualityComparerMapper<double>(EqualityComparer<double>.Default);

var memoizer = new Memoizer<ValueTuple<string, int>, double>(inputMapper, outputMapper);
memoizer.Memoize(("one", 1), 1.0);
```

# Installation via Nuget

See [https://www.nuget.org/packages/Trs.IntegerMapper/](https://www.nuget.org/packages/Trs.IntegerMapper/) for nuget package.

# Unit Test Code Coverage

Unit tests can be run using the `.\test.ps1` script. This will generate a code coverage report in the `.\UnitTestCoverageReport` folder using [Coverlet](https://github.com/tonerdo/coverlethttps://github.com/tonerdo/coverlet) and [ReportGenerator](https://github.com/danielpalme/ReportGenerator).

![Code Coverage](code_coverage.PNG)

# Licence

Integer Mapper is released under the MIT open source licence. See LICENCE.txt in this repository for the full text.
