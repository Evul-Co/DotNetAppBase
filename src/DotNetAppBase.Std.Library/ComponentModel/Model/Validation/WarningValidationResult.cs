using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

public class WarningValidationResult : ValidationResult
{
    public WarningValidationResult(string warningMessage) : base(warningMessage) { }

    public WarningValidationResult(string warningMessage, IEnumerable<string> memberNames) : base(warningMessage, memberNames) { }

    protected WarningValidationResult(ValidationResult validationResult) : base(validationResult) { }
}