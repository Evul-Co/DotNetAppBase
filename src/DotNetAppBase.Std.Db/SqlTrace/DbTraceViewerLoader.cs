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

using System.IO;
using System.Linq;
using System.Reflection;

namespace DotNetAppBase.Std.Db.SqlTrace;

public static class DbTraceViewerLoader
{
    public const string AssemblyPattern = "DotNetAppBase.Std.Db.TraceViewer*.dll";

    private static IDbTraceViewerRegister _register;

    public static void Load()
    {
        if (_register != null)
        {
            return;
        }

        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), AssemblyPattern, SearchOption.TopDirectoryOnly);

        foreach (var assembly in files.Select(Assembly.LoadFile))
        {
            _register = assembly.CreateInstance("DbTraceViewerRegister") as IDbTraceViewerRegister;
            if (_register != null)
            {
                DbTraceProvider.Instance.SqlTrace += _register.DbTraceHandler;
            }
        }
    }

    public static void Unload()
    {
        if (_register == null)
        {
            return;
        }

        _register.Dispose();

        DbTraceProvider.Instance.SqlTrace -= _register.DbTraceHandler;
    }
}