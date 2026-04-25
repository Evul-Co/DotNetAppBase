using System;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedBinary;

public class XImageAttribute : XValidationAttribute
{
    private readonly long _maxLength;

    public XImageAttribute(long maxLength)
        : base(
            EDataType.Custom,
            EValidationMode.Custom,
            // ReSharper disable LocalizableElement
            string.Format(DbMessages.XImageAttribute_XImageAttribute_A_imagem_associada_ao_campo__0__não_pode_ser_maior_que__1__MB_, "{0}", XHelper.DisplayFormat.Binary.ByteToMegabyte(maxLength)))
    // ReSharper restore LocalizableElement
    {
        _maxLength = maxLength;
    }

    public override string Mask => null;

    protected override bool InternalIsValid(object value)
    {
        var data = value.As<byte[]>();

        if (data == null)
        {
            return true;
        }

        return data.LongLength <= _maxLength;
    }
}