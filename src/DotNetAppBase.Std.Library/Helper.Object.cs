using System;
using System.Linq;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Obj
    {
        public static bool AnyNull(params object[] args) => args.Any(o => o == null);

        public static bool AreEquals(object objA, object objB) => Equals(objA, objB);

        public static bool AreNotEquals(object objA, object objB) => !AreEquals(objA, objB);

        public static object[] AsArray(params object[] args) => args;

        public static void DoNothing(object data) { }

        public static TResult GetOrDefault<T, TResult>(T obj, Func<T, TResult> func, TResult defaultValue) where T : class
        {
            XContract.ArgIsNotNull(func, nameof(func));

            if (obj == null)
            {
                return defaultValue;
            }

            return func(obj);
        }

        public static TResult GetOrDefault<T, TResult>(T obj, Func<T, TResult> func) where T : class => GetOrDefault(obj, func, default);

        public static bool IsDbNull(object @object) => @object == DBNull.Value;

        public static bool IsNotNull(object @object) => !IsNull(@object);

        public static bool IsNull(object @object) => @object is null or DBNull;
    }
}