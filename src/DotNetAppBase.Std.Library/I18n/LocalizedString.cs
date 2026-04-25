using System;
using System.Globalization;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Library.I18n;

public class LocalizableString
{
    private readonly string _propertyName;

    private Func<string> _cachedResult;

    private string _propertyValue;
    private Type _resourceType;

    public LocalizableString(string propertyName)
    {
        _propertyName = propertyName;
    }

    public Type ResourceType
    {
        get => _resourceType;
        set
        {
            if (_resourceType == value)
            {
                return;
            }

            ClearCache();

            _resourceType = value;
        }
    }

    public string Value
    {
        get => _propertyValue;
        set
        {
            if (_propertyValue == value)
            {
                return;
            }

            ClearCache();

            _propertyValue = value;
        }
    }

    public string GetLocalizableValue()
    {
        if (_cachedResult != null)
        {
            return _cachedResult();
        }

        // If the property value is null, then just cache that value
        // If the resource type is null, then property value is literal, so cache it
        if (_propertyValue == null || _resourceType == null)
        {
            _cachedResult = () => _propertyValue;
        }
        else
        {
            // Get the property from the resource type for this resource key
            var property = _resourceType.GetProperty(_propertyValue);

            // We need to detect bad configurations so that we can throw exceptions accordingly
            var badlyConfigured = false;

            // Make sure we found the property and it's the correct type, and that the type itself is public
            if (!_resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
            {
                badlyConfigured = true;
            }
            else
            {
                // Ensure the getter for the property is available as public static
                var getter = property.GetGetMethod();
                if (getter == null || !(getter.IsPublic && getter.IsStatic))
                {
                    badlyConfigured = true;
                }
            }

            // If the property is not configured properly, then throw a missing member exception
            if (badlyConfigured)
            {
                var exceptionMessage = string.Format(CultureInfo.CurrentCulture, _propertyName, _resourceType.FullName, _propertyValue);

                _cachedResult = () => throw new XInvalidOperationException(exceptionMessage);
            }
            else
            {
                // We have a valid property, so cache the resource
                _cachedResult = () => (string) property.GetValue(null, null);
            }
        }

        // Return the cached result
        return _cachedResult();
    }

    private void ClearCache()
    {
        _cachedResult = null;
    }
}