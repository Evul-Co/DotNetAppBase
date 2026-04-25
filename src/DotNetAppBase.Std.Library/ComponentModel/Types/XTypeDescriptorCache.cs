using System;
using System.Collections.Concurrent;

namespace DotNetAppBase.Std.Library.ComponentModel.Types;

internal class XTypeDescriptorCache
{
    private readonly ConcurrentDictionary<Type, XTypeDescriptor> _items;

    public XTypeDescriptorCache()
    {
        _items = new ConcurrentDictionary<Type, XTypeDescriptor>();
    }

    public XTypeDescriptor Get(Type type, Func<Type, XTypeDescriptor> factory) => _items.GetOrAdd(type, factory);
}