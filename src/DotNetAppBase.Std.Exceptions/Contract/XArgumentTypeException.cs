using System;
using System.ComponentModel;
using DotNetAppBase.Std.Exceptions.Assert;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Exceptions.Contract;

public class XArgumentTypeException : XException
{
    private const string DefaultMessage = "The argument {0} is incompatible with expected type!\n\nWait.Type: {1}\nReceive.Type: {2}";

    protected XArgumentTypeException(string argumentName, Type wait, Type received = null)
        : base(string.Format(DefaultMessage, argumentName, wait.FullName, GetTypeName(received)))
    {
        XContract.ArgIsNotNull(argumentName, nameof(argumentName));
        XContract.ArgIsNotNull(wait, nameof(wait));
        XContract.ArgIsNotNull(received, nameof(received));

        ArgumentName = argumentName;
        Wait = wait;
        Received = received;
    }

    public string ArgumentName { get; }

    public Type Received { get; }

    public Type Wait { get; }

    public static XArgumentTypeException Create(string argumentName, Type wait)
    {
        if (argumentName == null)
        {
            throw new ArgumentNullException(nameof(argumentName));
        }

        if (wait == null)
        {
            throw new ArgumentNullException(nameof(wait));
        }

        return new XArgumentTypeException(argumentName, wait, typeof(NullReference));
    }

    public static XArgumentTypeException CreateFromObject([Localizable(false)] string argumentName, Type wait, object received)
    {
        if (argumentName == null)
        {
            throw new ArgumentNullException(nameof(argumentName));
        }

        if (wait == null)
        {
            throw new ArgumentNullException(nameof(wait));
        }

        return new XArgumentTypeException(argumentName, wait, received?.GetType() ?? typeof(NullReference));
    }

    public static XArgumentTypeException CreateFromType(string argumentName, Type wait, Type received)
    {
        if (argumentName == null)
        {
            throw new ArgumentNullException(nameof(argumentName));
        }

        if (wait == null)
        {
            throw new ArgumentNullException(nameof(wait));
        }

        if (received == null)
        {
            throw new ArgumentNullException(nameof(received));
        }

        return new XArgumentTypeException(argumentName, wait, received);
    }

    // ReSharper disable LocalizableElement
    private static string GetTypeName(Type type) => type == typeof(NullReference) ? "[Object is NULL]" : type.FullName;
    // ReSharper restore LocalizableElement

    private static class NullReference { }
}