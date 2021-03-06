# JetPack.Data for .NET

[![Version](https://img.shields.io/nuget/vpre/Jetsons.JetPack.Data.svg)](https://www.nuget.org/packages/Jetsons.JetPack.Data)
[![Downloads](https://img.shields.io/nuget/dt/Jetsons.JetPack.Data.svg)](https://www.nuget.org/packages/Jetsons.JetPack.Data)
[![GitHub contributors](https://img.shields.io/github/contributors/jetsons/JetPack.Data.Net.svg)](https://github.com/jetsons/JetPack.Data.Net/graphs/contributors)
[![License](https://img.shields.io/github/license/jetsons/JetPack.Data.Net.svg)](https://github.com/jetsons/JetPack.Data.Net/blob/master/LICENSE)

To use this simply grab our Nuget package `Jetsons.JetPack.Data` and add this to the top of your class:

    using Jetsons.JetPack;
	
This statement unlocks all the extension methods below. Enjoy!

This library depends on the following Nuget packages:

- Jetsons.JetPack
- Markdig
- SharpCompress

### Extensions

Extension methods for Data objects:

- any.**EncodeJSON**
- any.**EncodeMsgPack**
- string.**DecodeJSON**
- string.**DecodeCSV**
- bytes.**DecodeJSON**
- bytes.**DecodeMsgPack**

Extension methods for file I/O performed using Data objects:

- any.**SaveToJSON**
- any.**SaveToMsgPack**

Extension methods for file I/O performed using file path Strings:

- string.**LoadJSON**
- string.**LoadCSV**
- string.**ArchiveFolder**
- string.**ExtractArchive**
- string.**ExtractFileFromArchive**
- string.**GetFileFromArchive**


### JSON

Fastest and Zero Allocation JSON Serializer for C#

> **Types supported :** Primitives(int, string, etc...), Enum, Nullable<>, TimeSpan, DateTime, DateTimeOffset, Guid, Uri, Version, StringBuilder, BitArray, Type, ArraySegment<>, BigInteger, Complext, ExpandoObject , Task, Array[], Array[,], Array[,,], Array[,,,], KeyValuePair<,>, Tuple<,...>, ValueTuple<,...>, List<>, LinkedList<>, Queue<>, Stack<>, HashSet<>, ReadOnlyCollection<>, IList<>, ICollection<>, IEnumerable<>, Dictionary<,>, IDictionary<,>, SortedDictionary<,>, SortedList<,>, ILookup<,>, IGrouping<,>, ObservableCollection<>, ReadOnlyOnservableCollection<>, IReadOnlyList<>, IReadOnlyCollection<>, ISet<>, ConcurrentBag<>, ConcurrentQueue<>, ConcurrentStack<>, ReadOnlyDictionary<,>, IReadOnlyDictionary<,>, ConcurrentDictionary<,>, Lazy<>, Task<>, custom inherited ICollection<> or IDictionary<,> with paramterless constructor, IEnumerable, ICollection, IList, IDictionary and custom inherited ICollection or IDictionary with paramterless constructor(includes ArrayList and Hashtable), Exception, your own class or struct(includes anonymous type).

### MsgPack

Extremely Fast MessagePack Serializer for C#

> **Types supported :** Primitives(int, string, etc...), Enum, Nullable<>, TimeSpan, DateTime, DateTimeOffset, Nil, Guid, Uri, Version, StringBuilder, BitArray, ArraySegment<>, BigInteger, Complext, Task, Array[], Array[,], Array[,,], Array[,,,], KeyValuePair<,>, Tuple<,...>, ValueTuple<,...>, List<>, LinkedList<>, Queue<>, Stack<>, HashSet<>, ReadOnlyCollection<>, IList<>, ICollection<>, IEnumerable<>, Dictionary<,>, IDictionary<,>, SortedDictionary<,>, SortedList<,>, ILookup<,>, IGrouping<,>, ObservableCollection<>, ReadOnlyOnservableCollection<>, IReadOnlyList<>, IReadOnlyCollection<>, ISet<>, ConcurrentBag<>, ConcurrentQueue<>, ConcurrentStack<>, ReadOnlyDictionary<,>, IReadOnlyDictionary<,>, ConcurrentDictionary<,>, Lazy<>, Task<>, custom inherited ICollection<> or IDictionary<,> with paramterless constructor, IList, IDictionary and custom inherited ICollection or IDictionary with paramterless constructor(includes ArrayList and Hashtable).
