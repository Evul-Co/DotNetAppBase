using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XForeignKeyExtensions
// ReSharper restore CheckNamespace
{
    public static bool FkIsNotNull(this int fk) => XHelper.Models.FkIsNotNull(fk);

    public static bool FkIsNull(this int fk) => XHelper.Models.FkIsNull(fk);
}