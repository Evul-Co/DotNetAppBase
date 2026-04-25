using System;

namespace DotNetAppBase.Std.Exceptions.Bussines;

public class XDataNotFoundException : XBussinesException
{
    private const string DefaultMessage = "The entity {0} is not found using criteria {1}";

    protected XDataNotFoundException(string entityName, string criteria) : base(string.Format(DefaultMessage, entityName, criteria)) { }

    public static XDataNotFoundException Create(string entityName, string criteria) => new XDataNotFoundException(entityName, criteria);
}