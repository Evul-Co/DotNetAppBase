namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Enums;

public class XEnumAttribute : XDataTypeAttribute
{
    private const string Message = "O campo {0} não possui um valor válido.";

    public XEnumAttribute() : base(EDataType.Custom)
    {
        ErrorMessage = Message;
    }

    protected override bool InternalIsValid(object value) => value == null || XHelper.Enums.IsDefined(value);
}