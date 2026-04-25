using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DotNetAppBase.Std.Exceptions.Assert;
using DotNetAppBase.Std.Exceptions.Contract;
using DotNetAppBase.Std.Library.Attributes;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public partial class Reflections
    {
        public static class Properties
        {
            public const BindingFlags InstanceBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            public const BindingFlags StaticBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            public static PropertyInfo Get(Type type, string propertyName, bool ifNotFoundThrowAnException = true) => Get(type, propertyName, InstanceBindingFlags, ifNotFoundThrowAnException);

            public static PropertyInfo Get(object obj, string propertyName, bool ifNotFoundThrowAnException = true)
            {
                XContract.ArgIsNotNull(obj, nameof(obj));

                return Get(obj.GetType(), propertyName, ifNotFoundThrowAnException);
            }

            public static PropertyInfo GetStatic(Type type, string propertyName, bool ifNotFoundThrowAnException = true) => Get(type, propertyName, StaticBindingFlags, ifNotFoundThrowAnException);

            public static IEnumerable<(string PropertyName, string DisplayName, object Value, PropertyInfo PropInfo)> Pivot(object obj)
            {
                foreach (var prop in obj.GetType().GetProperties(InstanceBindingFlags))
                {
                    if (Attributes.HasAttribute<HideMemberAttribute>(obj.GetType()))
                    {
                        continue;
                    }

                    var displayName = Models.GetDisplayName(prop) ?? prop.Name;

                    yield return (prop.Name, displayName, prop.GetValue(obj), prop);
                }
            }

            public static IEnumerable<KeyValuePair<TKey, TValue>> PivotKeyValue<TKey, TValue>(
                object obj,
                Expression<Func<PropertyInfo, TKey>> keyField,
                Expression<Func<PropertyInfo, TValue>> valueField)
            {
                foreach (var prop in obj.GetType().GetProperties(InstanceBindingFlags))
                {
                    if (Attributes.HasAttribute<HideMemberAttribute>(prop))
                    {
                        continue;
                    }

                    yield return new KeyValuePair<TKey, TValue>(
                        keyField.Compile().Invoke(prop),
                        valueField.Compile().Invoke(prop)
                    );
                }
            }

            public static IEnumerable<KeyValuePair<TKey, object>> PivotPropertyAndValue<TKey>(
                object obj,
                Expression<Func<PropertyInfo, TKey>> keyField)
            {
                return PivotKeyValue(
                    obj,
                    keyField,
                    info => info.GetValue(obj));
            }

            public static T ReadValue<T>(object obj, string propertyName) => ReadValue<T>(obj, Get(obj, propertyName));

            public static T ReadValue<T>(object obj, PropertyInfo propertyInfo) => (T) propertyInfo.GetValue(obj);

            public static T ReadValueIfExists<T>(object obj, string propertyName)
            {
                var property = Get(obj, propertyName, false);
                return (T) property?.GetValue(obj);
            }

            public static void WriteValue(object obj, string propertyName, object newValue)
            {
                var property = Get(obj, propertyName);
                WriteValue(obj, property, newValue);
            }

            public static void WriteValue(object obj, PropertyInfo property, object newValue)
            {
                property.SetValue(obj, newValue);
            }

            public static void WriteValueIfExists(object obj, string propertyName, object newValue)
            {
                var property = Get(obj, propertyName, false);
                WriteValueIfExists(obj, property, newValue);
            }

            public static void WriteValueIfExists(object obj, PropertyInfo property, object newValue)
            {
                property?.SetValue(obj, newValue);
            }

            private static PropertyInfo Get(Type type, string propertyName, BindingFlags bindingFlags, bool ifNotFoundThrowAnException = true)
            {
                XContract.ArgIsNotNull(type, nameof(type));
                XContract.ArgIsNotNull(propertyName, nameof(propertyName));

                try
                {
                    var propertyInfo = type.GetProperty(propertyName, bindingFlags);
                    if (propertyInfo == null && ifNotFoundThrowAnException)
                    {
                        throw XArgumentException.Create(nameof(propertyName), $"Property '{propertyName}' not found on type {type.Name}.");
                    }

                    return propertyInfo;
                }
                catch (AmbiguousMatchException)
                {
                    return type
                        .GetProperties(InstanceBindingFlags)
                        .FirstOrDefault(info => info.Name == propertyName);
                }
            }
        }
    }
}