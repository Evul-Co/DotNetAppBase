using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DotNetAppBase.Std.Library.ComponentModel.Types;

public interface IXTypeDescriptor
{
    string ModelDisplayName { get; }
    Type ModelType { get; }
    IEnumerable<PropertyDescriptor> Properties { get; }
    PropertyDescriptor GetDefaultLookupProperty();
    object GetDefaultLookupPropertyValue(object obj);

    string GetModelDisplayName(object obj);

    PropertyDescriptor GetPrimaryKeyProperty();
    object GetPrimaryKeyPropertyValue(object obj);
    PropertyDescriptor GetPropertyDescriptor(string propertyName);

    string GetPropertyDisplayName(PropertyDescriptor propertyDescription, bool formatRequiredAsHtml = true);

    PropertyInfo GetPropertyInfo(string propertyName);
    bool IsPropertyRequired(PropertyDescriptor propertyDescriptor);
}

// ReSharper disable UnusedTypeParameter
public interface IXTypeDescriptor<TModel> : IXTypeDescriptor { }
// ReSharper restore UnusedTypeParameter

public interface IXVMTypeDescriptor<TModel> : IXTypeDescriptor<TModel> { }