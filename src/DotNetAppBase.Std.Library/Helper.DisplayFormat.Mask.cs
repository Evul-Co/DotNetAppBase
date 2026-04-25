using System;
using System.ComponentModel;
using System.Globalization;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        public static class Mask
        {
            public static string Format(string mask, string text, CultureInfo culture = null) => Format(mask, text, out _, out _, culture);

            public static string Format(string mask, string text, out MaskedTextResultHint hint, out int hintPosition, CultureInfo culture = null)
            {
                if (text == null)
                {
                    text = string.Empty;
                }

                if (culture == null)
                {
                    culture = CultureInfo.InvariantCulture;
                }

                var provider = new MaskedTextProvider(mask, culture, true);

                // Format and return the string
                provider.Set(text, out hintPosition, out hint);

                // Positive hint results are successful
                if (hint > 0)
                {
                    return provider.ToString();
                }

                // Return the text as-is if it didn't fit the mask
                return text;
            }
        }
    }
}