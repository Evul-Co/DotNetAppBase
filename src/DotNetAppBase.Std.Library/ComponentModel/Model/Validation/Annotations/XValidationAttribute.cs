using System;
using System.ComponentModel;
using System.Linq;
using DotNetAppBase.Std.Library.ComponentModel.Model.Enums;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations;

public abstract class XValidationAttribute : XDataTypeAttribute
{
    public enum EValidationMode
    {
        Custom,

        MaskDataTime,
        MaskDateTimeAdvancingCaret,
        MaskNumeric,
        MaskRegEx,
        MaskRegular,
        MaskSimple
    }

    private string _maskForEditor;

    protected XValidationAttribute(EDataType dataType, EValidationMode mode, bool isComputed = false) : base(dataType, isComputed)
    {
        Mode = mode;
    }

    protected XValidationAttribute(EDataType dataType, EValidationMode mode, string errorMessage, bool isComputed = false) : base(dataType, isComputed)
    {
        Mode = mode;
        ErrorMessage = errorMessage;
    }

    public virtual bool Enabled => RestrictFor == EModoOperacao.None || RestrictFor == ValidationSettings.RestrictionFor;

    [Localizable(false)]
    public abstract string Mask { get; }

    public string MaskForEditor
    {
        get => _maskForEditor ?? MaskWithoutStartAndEnd;
        set => _maskForEditor = value;
    }

    [Localizable(false)]
    public string MaskForEditorWithoutStartAndEnd => MaskForEditor.Replace("^", "").Replace("$", "");

    [Localizable(false)]
    public string MaskWithoutStartAndEnd => Mask.Replace("^", "").Replace("$", "");

    public EValidationMode Mode { get; protected set; }

    public EModoOperacao RestrictFor { get; set; }

    protected override bool InternalIsEnabled() => Enabled;
}