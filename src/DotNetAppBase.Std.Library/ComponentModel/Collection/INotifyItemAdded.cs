using System;
using DotNetAppBase.Std.Library.ComponentModel.Collection.Events;

namespace DotNetAppBase.Std.Library.ComponentModel.Collection;

public interface INotifyItemAdded<TItem> where TItem : class
{
    event EventHandler<EntityActionEventArgs<TItem>> ItemsChanged;
}