using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Enums;

public enum EModoOperacao
{
    [Browsable(false)]
    None = 0,

    [Display(ResourceType = typeof(DbEnums), Name = "EModoOperacao_Brasil")]
    Brasil = 1,

    [Display(ResourceType = typeof(DbEnums), Name = "EModoOperacao_Paraguai")]
    Paraguai = 2
}