#region License

// Copyright(c) 2020 GrappTec
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace DotNetAppBase.Std.Library.Log;

[Localizable(false)]
public static class NativeLogCenter
{
    public enum ELogLevel
    {
        Debug,
        Error,
        Fatal,
        Info,
        Warn,
        Profile
    }

    public static bool EnableDebugger { get; set; }

    public static bool EnableTrace { get; set; }

    public static void Add(Exception exception)
    {
        var exceptionMessage = string.Empty;

        ExtractExceptionMessage(exception, ref exceptionMessage, 1);

        Add(ELogLevel.Error, "Exception Catch: {1}{0}", exceptionMessage, Environment.NewLine);
    }

    public static void Add(ELogLevel level, string message, params object[] args)
    {
        if (string.IsNullOrEmpty(message))
        {
            message = "NULL";
        }

        var fMessage = string.Format(DateTime.Now.ToString(CultureInfo.InvariantCulture) + ": " + message, args);

        if (EnableDebugger)
        {
            Debugger.Log((int) level, level.ToString(), fMessage);
        }

        if (EnableTrace)
        {
            Trace.WriteLine(fMessage, level.ToString());
        }
    }

    private static void ExtractExceptionMessage(Exception exception, ref string outMessage, byte level)
    {
        outMessage += $"Level ({level}): {exception.Message}{Environment.NewLine}";
        if (exception.InnerException != null)
        {
            ExtractExceptionMessage(exception.InnerException, ref outMessage, (byte) (level + 1));
        }
    }
}