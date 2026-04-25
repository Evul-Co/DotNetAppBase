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
using System.Collections.Generic;
using System.Diagnostics;
using DotNetAppBase.Std.Exceptions.Assert.Debugs;

namespace DotNetAppBase.Std.Exceptions.Assert;

public static class XDebug
{
    private static readonly List<IKnowledgeableExceptions> KnowledgeableExceptionsList;

    static XDebug()
    {
        KnowledgeableExceptionsList = new List<IKnowledgeableExceptions>();
    }

    [Conditional("DEBUG")]
    public static void OnException(Exception exception)
    {
        if (exception == null)
        {
            return;
        }

        Debug.Assert(true, GetExceptionMessage(exception));
    }

    public static void RegisterKnowledgeableExceptions(IKnowledgeableExceptions knowledgeableExceptions)
    {
        if (KnowledgeableExceptionsList.Contains(knowledgeableExceptions))
        {
            return;
        }

        KnowledgeableExceptionsList.Add(knowledgeableExceptions);
    }

    private static string GetExceptionMessage(Exception exception)
    {
        foreach (var knowledgeableExceptionse in KnowledgeableExceptionsList)
        {
            if (knowledgeableExceptionse.DoYouKnowAboutMessage(exception, out var message))
            {
                return message;
            }
        }

        return exception.Message;
    }
}