namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber;

public class XCurrencyAttribute : XNumberAttribute
{
    public XCurrencyAttribute() : base(EDataType.Currency, EValidationMode.MaskNumeric) { }

    public override string Mask => "c";
}