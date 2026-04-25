using System.ComponentModel.DataAnnotations;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Business.Enums;

public enum ELogicalDelete : byte
{
    [Display(Name = "Ativo")]
    Active = 1,

    [Display(Name = "Inativo")]
    Inactive = 2
}