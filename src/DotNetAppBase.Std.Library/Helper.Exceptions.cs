using System;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Exceptions
    {
        public static string GetMessageOnTopOfStack(Exception ex) => GetOnTopOfStack(ex).Message;

        public static Exception GetOnTopOfStack(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return ex;
            }

            return GetOnTopOfStack(ex.InnerException);
        }
    }
}