using DotNetAppBase.Std.Library.ComponentModel.Model.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

public static class ValidationSettings
{
    public static bool InParaguayMode => RestrictionFor == EModoOperacao.Paraguai;

    public static EModoOperacao RestrictionFor { get; set; }
}