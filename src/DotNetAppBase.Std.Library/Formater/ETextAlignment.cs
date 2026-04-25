using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.Formater;

public enum ETextAlignment
{
    [Display(ResourceType = typeof(DbEnums), Name = "ETextAlignment_Left")]
    Left = -1,

    [Display(ResourceType = typeof(DbEnums), Name = "ETextAlignment_Center")]
    Center = -2,

    [Display(ResourceType = typeof(DbEnums), Name = "ETextAlignment_Right")]
    Right = -3
}