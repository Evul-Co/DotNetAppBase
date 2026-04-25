using System.ComponentModel.DataAnnotations;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Business;

public interface IEntity
{
    [Display(Name = "Apresentação")]
    string Display { get; }

    [Display(Name = "Código")]
    int ID { get; set; }
}