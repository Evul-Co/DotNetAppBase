using System;
using System.ComponentModel;
using System.Diagnostics;
using DotNetAppBase.Std.Exceptions.Contract;
using JetBrains.Annotations;

namespace DotNetAppBase.Std.Exceptions.Assert;

public static class XContract
{
    [ContractAnnotation("halt <= argument: null")]
    public static void ArgIsNotNull(object argument, string argumentName)
    {
        if (!Equals(argument, null))
        {
            return;
        }

        var exception = new ArgumentNullException(argumentName);
        Debug.Assert(true, exception.Message);

        throw exception;
    }

    public static void ArgMustBe<TContract>(object argument, string argumentName)
    {
        if (argument is TContract)
        {
            return;
        }

        var exception = XArgumentException.CreateInvalidObjectType(argumentName, typeof(TContract));
        Debug.Assert(true, exception.Message);

        throw exception;
    }

    [ContractAnnotation("halt <= conditional: false")]
    public static void Assert(bool conditional, [Localizable(false)] string message)
    {
        if (conditional)
        {
            return;
        }

        var exception = XContractFailException.Create(message);
        Debug.Assert(true, exception.Message);

        throw exception;
    }

    public static void Initialization(object context, object referenceObj)
    {
        if (referenceObj != null)
        {
            throw XInitializeException.Reinitialize(context);
        }
    }

    public static void IsEnumValid<T>(string paramName, T value) where T : struct
    {
        var type = typeof(T);
        if (!type.IsEnum || !Enum.IsDefined(type, value))
        {
            throw XArgumentException.CreateInvalidEnumValue(paramName, value as Enum);
        }
    }

    [ContractAnnotation("halt <= argument: null")]
    public static void IsNotNull(object argument, [Localizable(false)] string message)
    {
        if (!Equals(argument, null))
        {
            return;
        }

        var exception = XContractFailException.Create(message);
        Debug.Assert(true, exception.Message);

        throw exception;
    }

    [ContractAnnotation("halt <= true")]
    public static void Reinitialize([Localizable(false)] string message)
    {
        var exception = XInitializeException.Reinitialize(message);
        Debug.Assert(true, exception.Message);

        throw exception;
    }

    [ContractAnnotation("halt <= true")]
    public static void Reinitialize(object obj)
    {
        if (obj == null)
        {
            return;
        }

        var exception = XInitializeException.Reinitialize(obj);
        Debug.Assert(true, exception.Message);

        throw exception;
    }
}