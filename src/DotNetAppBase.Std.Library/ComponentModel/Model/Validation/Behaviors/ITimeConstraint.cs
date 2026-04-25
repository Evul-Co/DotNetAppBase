using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;

public interface IDateTimeConstraint
{
    EDateTimeFormat Format { get; }
}