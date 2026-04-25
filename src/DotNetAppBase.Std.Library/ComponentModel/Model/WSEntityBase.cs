using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.ComponentModel.Model.Business;
using DotNetAppBase.Std.Library.ComponentModel.Model.Business.Enums;
using DotNetAppBase.Std.Library.ComponentModel.Model.Utilities;

namespace DotNetAppBase.Std.Library.ComponentModel.Model;

public abstract class WSEntityBase : EntityBase, IEntityWorkWithSituation
{
    [Display(Name = EntityBaseDisplayPattern.DefaultDisplaySituation)]
    public abstract ELogicalDelete Situacao { get; set; }

    public new class Metadata : EntityBase.Metadata
    {
        [Display(Name = EntityBaseDisplayPattern.DefaultDisplaySituation)]
        public ELogicalDelete Situacao { get; set; }
    }

    public new class MetadataWithLookup : EntityBase.MetadataWithLookup
    {
        [Display(Name = EntityBaseDisplayPattern.DefaultDisplaySituation)]
        public ELogicalDelete Situacao { get; set; }
    }
}