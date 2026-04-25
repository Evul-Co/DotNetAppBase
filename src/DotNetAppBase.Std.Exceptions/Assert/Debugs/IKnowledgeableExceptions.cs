using System;

namespace DotNetAppBase.Std.Exceptions.Assert.Debugs;

public interface IKnowledgeableExceptions
{
    bool DoYouKnowAboutMessage(Exception ex, out string message);
}