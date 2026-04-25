using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Data
    {
        public static class Validations
        {
            public static bool AreIntegerNumbers(string input) => ValidationDataFormats.AreIntegerNumbers(input);

            public static bool AreNumbers(string input) => ValidationDataFormats.AreNumbers(input);

            public static bool IsCep(string input) => ValidationDataFormats.IsCep(input);

            public static bool IsCnpj(string input) => ValidationDataFormats.IsCnpj(input);

            public static bool IsCpf(string input) => ValidationDataFormats.IsCpf(input);

            public static bool IsEmail(string input) => ValidationDataFormats.IsEmail(input);

            public static bool IsGuid(string input) => ValidationDataFormats.IsGuid(input);

            public static bool IsHora(string input) => ValidationDataFormats.IsHora(input);

            public static bool IsIccID(string input) => ValidationDataFormats.IsIccID(input);

            public static bool IsIPv4(string input) => ValidationDataFormats.IsIPv4(input);

            public static bool IsLatitude(string input) => ValidationDataFormats.IsLatitude(input);

            public static bool IsLoginPatternLength6To60(string input) => ValidationDataFormats.IsLoginPatternLength6To60(input);

            public static bool IsLongitude(string input) => ValidationDataFormats.IsLongitude(input);

            public static bool IsPlaca(string input) => ValidationDataFormats.IsPlaca(input);

            public static bool IsRuc(string input) => ValidationDataFormats.IsRuc(input);

            public static bool IsTelefone(string input, bool accept9Digits = true) => ValidationDataFormats.IsTelefone(input, accept9Digits);

            public static bool IsUrl(string input) => ValidationDataFormats.IsUrl(input);
        }
    }
}