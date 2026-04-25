using System;
using System.Data.Common;

namespace DotNetAppBase.Std.Exceptions.Base;

public class XDbException : XException
{
    protected const string Prefix = "XDbException:";

    public XDbException(Exception ex) : base(ex.Message.Replace(Prefix, string.Empty), ex)
    {
        if (!ex.Message.StartsWith(Prefix))
        {
            throw new XException(
                "A exceção XDbException requer como base uma exceção disparada pelo " +
                "banco de dados, e esta exceção será assumida como parâmetro.");
        }
    }

    protected XDbException(Exception ex, string message) : base(message.Replace(Prefix, string.Empty), ex) { }

    protected XDbException(string exception, Exception innerException = null) : base($"{Prefix}{exception}", innerException) { }

    public static string FormatMessage(Exception exception)
    {
        if (exception is DbException && exception.Message.StartsWith(Prefix))
        {
            return exception.Message.Replace(Prefix, string.Empty);
        }

        return exception.Message;
    }

    public static bool IsOne(Exception exception, out XDbException xException)
    {
        xException = null;

        if (!(exception is DbException sqlEx))
        {
            return xException != null;
        }

        if (exception.Message.StartsWith(Prefix))
        {
            xException = new XDbException(sqlEx);
        }
        else
        {
            var result = exception.Message.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            if (result.Length > 1 && result[1].StartsWith("XDbException:"))
            {
                xException = new XDbException(sqlEx, result[1].Replace("XDbException:", string.Empty));
            }
        }

        return xException != null;
    }
}