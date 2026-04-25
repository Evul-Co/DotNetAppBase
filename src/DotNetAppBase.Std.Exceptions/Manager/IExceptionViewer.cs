using System;
using System.Net.Mail;

namespace DotNetAppBase.Std.Exceptions.Manager;

public interface IExceptionConfig
{
    void CaptureScreen(string fileName);

    void Execute(Exception exception, Guid protocolID, MailMessage message);

    void RegisterManager(ExceptionManager exceptionManager);

    void UnregisterManager(ExceptionManager exceptionManager);
}