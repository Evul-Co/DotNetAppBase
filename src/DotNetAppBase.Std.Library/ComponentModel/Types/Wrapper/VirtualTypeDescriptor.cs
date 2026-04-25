using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DotNetAppBase.Std.Library.ComponentModel.Types.Wrapper;

public class VirtualTypeDescriptor : object, ICustomTypeDescriptor
{
    private readonly Dictionary<string, VirtualPropertyDescriptor> _data;

    public VirtualTypeDescriptor()
    {
        _data = new Dictionary<string, VirtualPropertyDescriptor>();
    }

    public IEnumerable<string> PropertiesNames => _data.Keys;

    public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(this, true);

    public string GetClassName() => TypeDescriptor.GetClassName(this, true);

    public string GetComponentName() => TypeDescriptor.GetComponentName(this, true);

    public TypeConverter GetConverter() => TypeDescriptor.GetConverter(this, true);

    public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(this, true);

    public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty(this, true);

    public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor(this, editorBaseType, true);

    public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents(this, true);

    public EventDescriptorCollection GetEvents(Attribute[] attributes) => TypeDescriptor.GetEvents(attributes, true);

    public PropertyDescriptorCollection GetProperties() => new PropertyDescriptorCollection(_data.Values.Cast<PropertyDescriptor>().ToArray());

    public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => GetProperties();

    public object GetPropertyOwner(PropertyDescriptor pd) => this;

    public void Add(
        string propertyName, string displayName, Type dataType, object value,
        string category, Attribute[] attributes = null)
    {
        var newPropertyDescriptor = new VirtualPropertyDescriptor(
            propertyName, value, dataType, displayName, category, this);

        newPropertyDescriptor.AddNewAttributes(attributes);

        _data.Add(propertyName, newPropertyDescriptor);
    }

    public object GetValue(string propertyName) => _data[propertyName].Value;
}