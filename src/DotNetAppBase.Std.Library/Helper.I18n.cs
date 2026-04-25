using System;
using System.Globalization;
using System.Threading;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    // ReSharper disable InconsistentNaming
    public static class I18n
        // ReSharper restore InconsistentNaming
    {
        public enum ELanguage
        {
            Invariant = 0,
            Brazil = 1,
            Paraguay = 2
        }

        private static ELanguage _currentLanguage;
        private static EventHandler _currentLanguageChanged;

        static I18n()
        {
            _currentLanguage = ELanguage.Brazil;

            InternalCurrentLanguageChanged();
        }

        public static CultureInfo CurrentCulture { get; private set; }

        public static ELanguage CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                if (_currentLanguage != value)
                {
                    XContract.IsEnumValid(nameof(CurrentLanguage), value);

                    _currentLanguage = value;

                    InternalCurrentLanguageChanged();

                    _currentLanguageChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static string NumberDecimalSeparator => CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static event EventHandler CurrentLanguageChanged
        {
            add
            {
                _currentLanguageChanged += value;

                if (_currentLanguage == ELanguage.Invariant)
                {
                    return;
                }

                value(typeof(I18n), EventArgs.Empty);
            }
            remove => _currentLanguageChanged -= value;
        }

        private static CultureInfo IdentifyCulture(ELanguage currentLanguage)
        {
            switch (currentLanguage)
            {
                case ELanguage.Brazil:
                    return new CultureInfo("pt-BR");

                case ELanguage.Paraguay:
                    return new CultureInfo("es-PY");

                default:
                    return CultureInfo.InvariantCulture;
            }
        }

        private static void InternalCurrentLanguageChanged()
        {
            CurrentCulture = IdentifyCulture(_currentLanguage);

            CultureInfo.CurrentCulture = CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = CurrentCulture;

            CultureInfo.DefaultThreadCurrentCulture = CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CurrentCulture;
        }
    }
}