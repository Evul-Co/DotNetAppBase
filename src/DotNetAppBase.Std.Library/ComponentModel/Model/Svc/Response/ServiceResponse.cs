using System.Runtime.Serialization;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

public abstract class ServiceResponse
{
    [DataMember]
    public bool Fail => Status == EServiceResponse.Failed;

    [DataMember]
    public EServiceResponse Status => ValidationResult.HasViolations ? EServiceResponse.Failed : EServiceResponse.Succeeded;

    [DataMember]
    public bool Success => Status == EServiceResponse.Succeeded;

    [DataMember]
    public EntityValidationResult ValidationResult { get; internal set; }
}