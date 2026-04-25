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