using System;
using System.Collections.Generic;
using System.ComponentModel;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library.ComponentModel.Types.Wrapper;

public class VirtualPropertyDescriptor : PropertyDescriptor
{
    private readonly VirtualTypeDescriptor _parent;

    public VirtualPropertyDescriptor(
        string propertyName, object value, Type dataType,
        string displayName, string category, VirtualTypeDescriptor parent)
        : base(propertyName, null)
    {
        XContract.ArgIsNotNull(dataType, nameof(dataType));

        Value = value;
        PropertyType = dataType;
        DisplayName = displayName;
        Category = category;
        _parent = parent;
    }

    public override string Category { get; }

    public override Type ComponentType => typeof(VirtualTypeDescriptor);

    public override string DisplayName { get; }

    public override bool IsReadOnly => false;
    public override Type PropertyType { get; }

    public object Value { get; set; }

    public void AddNewAttributes(Attribute[] attributes)
    {
        if (attributes.IsNullOrEmpty())
        {
            return;
        }

        var newAttrs = new List<Attribute>();

        newAttrs.AddRange(AttributeArray);
        newAttrs.AddRange(attributes);

        AttributeArray = newAttrs.ToArray();
    }

    public override bool CanResetValue(object component) => true;

    public override object GetValue(object component) => Value;

    public override void ResetValue(object component) { }

    public override void SetValue(object component, object value)
    {
        Value = value;
    }

    public override bool ShouldSerializeValue(object component) => false;
}