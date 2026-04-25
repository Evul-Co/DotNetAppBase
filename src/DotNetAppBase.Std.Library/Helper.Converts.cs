using System;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Converts
    {
        public static T ConvertTo<T>(object value)
        {
            if (Obj.IsNull(value))
            {
                return default;
            }

            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return (T) Convert.ChangeType(value, t.GetGenericArguments()[0]);
            }

            return (T) Convert.ChangeType(value, t);
        }
    }
}