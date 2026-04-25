using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        public static class Docs
        {
            public static string Cnpj(string value) => Mask.Format(ValidationDataFormats.DocCnpjMask, value);
            public static string Cpf(string value) => Mask.Format(ValidationDataFormats.DocCpfMask, value);
            public static string DocumentoID(string value) => value.Length <= 11 ? Cpf(value) : Cnpj(value);
        }
    }
}