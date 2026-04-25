namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Data;

public class RecordValue : IPresentableValue
{
    public object Tag { get; set; }
    public string DefaultDisplay { get; set; }

    public decimal Value { get; set; }
}