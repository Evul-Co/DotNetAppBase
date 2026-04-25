namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations;

public abstract class XMaskAttribute : XValidationAttribute
{
    protected XMaskAttribute(EDataType dataType, string mask, EValidationMode validationMode) : base(dataType, validationMode)
    {
        Mask = mask;
    }

    public override string Mask { get; }
}