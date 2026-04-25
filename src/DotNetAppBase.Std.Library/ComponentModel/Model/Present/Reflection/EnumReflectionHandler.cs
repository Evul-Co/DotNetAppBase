using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Reflection;

public class EnumReflectionHandler
{
    static EnumReflectionHandler()
    {
        Instance = new EnumReflectionHandler();
    }

    protected EnumReflectionHandler() { }

    public static EnumReflectionHandler Instance { get; }

    public event Action<(EnumDisplayAttribute, Enum)> ExecuteProcess;

    public void Process(EnumDisplayAttribute enumDisplayAttribute, Enum inherit)
    {
        ExecuteProcess?.Invoke((enumDisplayAttribute, inherit));
    }

    public void Registre(Action<(EnumDisplayAttribute, Enum)> handle)
    {
        ExecuteProcess += handle;
    }
}