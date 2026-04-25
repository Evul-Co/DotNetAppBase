using System;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library.ComponentModel.Collection.Events;

public class EntityActionEventArgs<TEntity> : EventArgs where TEntity : class
{
    public EntityActionEventArgs(TEntity entity)
    {
        XContract.ArgIsNotNull(entity, nameof(entity));

        Entity = entity;
        Action = EEntityAction.NotInformed;
    }

    public EntityActionEventArgs(TEntity entity, EEntityAction action) : this(entity)
    {
        Action = action;
    }

    public EEntityAction Action { get; }

    public TEntity Entity { get; }
}