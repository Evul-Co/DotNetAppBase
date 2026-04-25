using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Enums;

public enum EValidationKind
{
    [Display(ResourceType = typeof(DbEnums), Name = "EValidationKind_Error")]
    Error,

    [Display(ResourceType = typeof(DbEnums), Name = "EValidationKind_Warning")]
    Warning
}