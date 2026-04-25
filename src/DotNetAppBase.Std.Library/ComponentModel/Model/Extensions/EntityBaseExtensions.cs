using System;
using System.Linq;

// ReSharper disable CheckNamespace
public static class EntityBaseExtensions
// ReSharper restore CheckNamespace
{
    public static TResult ReadNavigateDataIfNotNull<TModel, TResult>(
        this TModel model, Func<TModel, bool> canReadFunc, Func<TModel, TResult> readFunc, TResult defaultValue)
        where TModel : class =>
        canReadFunc(model) ? readFunc(model) : defaultValue;

    public static TResult ReadNavigateDataIfNotNull<TModel, TNaviProperty, TResult>(
        this TModel model, TNaviProperty property, Func<TNaviProperty, TResult> readFunc, TResult defaultValue = default)
        where TModel : class
        where TNaviProperty : class =>
        property != null ? readFunc(property) : defaultValue;
}