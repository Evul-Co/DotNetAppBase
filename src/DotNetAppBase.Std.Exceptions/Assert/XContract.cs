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