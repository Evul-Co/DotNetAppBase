using System;
using System.ComponentModel;
using System.Configuration;
using DotNetAppBase.Std.Library.Settings;

namespace DotNetAppBase.Std.Library;

public static partial class XHelper
{
    public sealed class ConfigWrapper<TKey, TValue> where TValue : class
    {
        private readonly Func<TKey, TValue> _translate;

        internal ConfigWrapper(Func<TKey, TValue> translate)
        {
            _translate = translate;
        }

        public TValue this[TKey name] => _translate(name);

        public TValue Get(TKey key, TValue defaultValue) => this[key] ?? defaultValue;
    }

    public static class Configs
    {
        public static string GlobalCustomSettingID { get; set; }

        [Localizable(false)]
        public static SettingsBuilder CustomAppSettings(string sectionID, string settingID = null, string directory = null) => new(sectionID, settingID ?? GlobalCustomSettingID, directory);

#if !NETSTANDARD
        public static readonly ConfigWrapper<string, string> AppSettings = new(s => ConfigurationManager.AppSettings[s]);

        public static readonly ConfigWrapper<string, string> ConnStrings = new(s => ConfigurationManager.ConnectionStrings[s].ConnectionString);

        public static readonly ConfigWrapper<string, ConnectionStringSettings> ConnStringsSettings = new(s => ConfigurationManager.ConnectionStrings[s]);
#endif
    }
}