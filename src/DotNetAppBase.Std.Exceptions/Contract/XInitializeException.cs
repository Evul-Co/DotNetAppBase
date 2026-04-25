using System.ComponentModel;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Exceptions.Contract;

public class XInitializeException : XException
{
    [Localizable(false)]
    protected XInitializeException(string message) : base(message) { }

    [Localizable(false)]
    public static XInitializeException Create(string message) => new XInitializeException(message);

    public static XInitializeException CreateForNullReference(object initializingOject, string requireMemberName)
        => new XInitializeException(
            $"The type {initializingOject.GetType().Name} is initialized, but the member '{requireMemberName}' is required and NULL!");

    public static XInitializeException InvalidInheritance(object initializingOject, string inheritanceMethodName)
        => new XInitializeException(
            $"The type {initializingOject.GetType().Name} don't supports the initialization from this member '{inheritanceMethodName}', try use the overloaded!");

    public static XInitializeException PropertyButRequireMemberIsNull(object initializingPropertyOf, string propertyName, string requireMemberName)
        => new XInitializeException(
            $"The type {initializingPropertyOf.GetType().Name} had a property {propertyName} initialized, but the member '{requireMemberName}' is required and NULL!");

    public static XInitializeException Reinitialize(string message) => new XInitializeException(message);

    public static XInitializeException Reinitialize(object obj) => new XInitializeException($"The type {obj.GetType().Name} is initialized, you can't do this again!");
}