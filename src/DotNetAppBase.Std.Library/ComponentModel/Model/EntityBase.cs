using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using DotNetAppBase.Std.Library.ComponentModel.Model.Business;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;
using DotNetAppBase.Std.Library.ComponentModel.Model.Utilities;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Key;

namespace DotNetAppBase.Std.Library.ComponentModel.Model;

[DebuggerDisplay("{" + nameof(DisplaySmall) + "}")]
public abstract class EntityBase : Entity, IEntity, IDataErrorInfo
{
    public virtual string DisplayFull => $"{ID}";

    public virtual string DisplaySmall => $"{ID}";

    public virtual string Display => DisplaySmall;

    string IDataErrorInfo.Error
    {
        get
        {
            var validationResult = EntityValidator.Validate(this);

            return !validationResult.HasViolations ? null : validationResult.ToString();
        }
    }

    string IDataErrorInfo.this[string columnName]
    {
        get
        {
            var validationResult = EntityValidator.ValidateProperty(this, columnName);

            return !validationResult.HasViolations ? null : validationResult.ToString();
        }
    }

    [Display(Name = EntityBaseDisplayPattern.DefaultDisplayNameID), XPrimaryKey]
    public abstract int ID { get; set; }

    public new class Metadata
    {
        [Display(Name = EntityBaseDisplayPattern.DefaultDisplayNameID), XPrimaryKey]
        public int ID { get; set; }
    }

    public class MetadataWithLookup
    {
        [Display(Name = EntityBaseDisplayPattern.DefaultDisplayNameID), XPrimaryKey, LookupDisplay(0)]
        public int ID { get; set; }
    }
}