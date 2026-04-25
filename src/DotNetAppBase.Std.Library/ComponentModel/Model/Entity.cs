using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DotNetAppBase.Std.Library.ComponentModel.Model;

[Serializable]
public class Entity : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class Metadata
    {
        public const string DefaultPrimaryKeyColumnName = "ID";

        public static string GetKeyFormated(int value) => XHelper.DisplayFormat.Model.AsKey(value);

        public static string GetNavigationPropertyName(string foreignKeyName) => foreignKeyName.Replace(DefaultPrimaryKeyColumnName, string.Empty);
    }
}