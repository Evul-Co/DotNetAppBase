using System;
using System.ComponentModel;
using System.Linq;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Library.Files;

internal class FileExtensions : FileExtension
{
    private readonly Lazy<string> _lazyCreateValue;

    [Localizable(false)]
    public FileExtensions(string description, params FileExtension[] extensions) : base(string.Empty, EFileExtension.MultiExtensions, description)
    {
        Extensions = extensions;

        _lazyCreateValue = new Lazy<string>(
            () =>
                {
                    var extensionsAsString = Extensions.Select(extension => extension.Value).Aggregate((filter1, filter2) => filter1 + ";" + filter2);

                    return $"{Description}|{extensionsAsString}";
                });
    }

    public FileExtension[] Extensions { get; }

    public override string Value => _lazyCreateValue.Value;

    public override string ChangeExtension(string fileName) => throw new XException($"Não é possível alterar a extensão de um arquivo a partir de um {nameof(FileExtensions)}");
}