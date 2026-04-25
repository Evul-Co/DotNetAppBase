using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Key;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;

public static class BehaviorIdentifier
{
    public static bool IsPropertyRequired(PropertyDescriptor propertyDescriptor)
    {
        var attributes = propertyDescriptor.Attributes;
        var required = attributes.OfType<RequiredAttribute>().Any()
                       || attributes.OfType<XRequiredAttribute>().Any();

        if (!required)
        {
            if (XHelper.Types.IsNullable(propertyDescriptor.PropertyType))
            {
                return false;
            }

            required = attributes.OfType<XForeignKeyAttribute>().Any(att => !att.UpdatedProgrammatically);
        }

        return required;
    }

    public static class Fk
    {
        public static bool Is(PropertyInfo propertyInfo) => Is(propertyInfo, out _);

        public static bool Is(PropertyInfo propertyInfo, out string navPropertyName)
        {
            var att = XHelper.Reflections.Attributes.Get<XForeignKeyAttribute>(propertyInfo);

            if (att == null)
            {
                navPropertyName = null;
                return false;
            }

            navPropertyName = att.NavigationPropertyName ?? Entity.Metadata.GetNavigationPropertyName(propertyInfo.Name);

            return true;
        }

        public static bool Is(PropertyDescriptor propertyDescriptor, out string navPropertyName)
        {
            var att = XHelper.Reflections.Attributes.Get<XForeignKeyAttribute>(propertyDescriptor);

            if (att == null)
            {
                navPropertyName = null;
                return false;
            }

            navPropertyName = att.NavigationPropertyName ?? Entity.Metadata.GetNavigationPropertyName(propertyDescriptor.Name);

            return true;
        }

        public static bool TryGetNavigationProperty(Type modelType, string fkPropertyName, out PropertyInfo navPropertyInfo, Type navPropertyType = null)
        {
            var navPropertyName = Entity.Metadata.GetNavigationPropertyName(fkPropertyName);
            navPropertyInfo = XHelper.Reflections.Properties.Get(modelType, navPropertyName, false);

            if (navPropertyInfo != null && navPropertyType != null)
            {
                if (!XHelper.Types.Is(navPropertyInfo.PropertyType, navPropertyType))
                {
                    navPropertyInfo = null;
                }
            }

            return navPropertyInfo != null;
        }

        public static bool TryGetNavigationProperty(Type modelType, PropertyInfo fkPropertyInfo, out PropertyInfo navPropertyInfo, Type navPropertyType = null)
        {
            if (!Is(fkPropertyInfo, out var navPropertyName))
            {
                navPropertyInfo = null;

                return false;
            }

            navPropertyInfo = XHelper.Reflections.Properties.Get(modelType, navPropertyName, false);

            if (navPropertyInfo != null && navPropertyType != null)
            {
                if (!XHelper.Types.Is(navPropertyInfo.PropertyType, navPropertyType))
                {
                    navPropertyInfo = null;
                }
            }

            return navPropertyInfo != null;
        }

        public static bool TryGetNavigationProperty(Type modelType, PropertyDescriptor fkPropertyDescriptor, out PropertyInfo navPropertyInfo, Type navPropertyType = null)
        {
            if (!Is(fkPropertyDescriptor, out var navPropertyName))
            {
                navPropertyInfo = null;

                return false;
            }

            navPropertyInfo = XHelper.Reflections.Properties.Get(modelType, navPropertyName, false);

            if (navPropertyInfo != null && navPropertyType != null)
            {
                if (!XHelper.Types.Is(navPropertyInfo.PropertyType, navPropertyType))
                {
                    navPropertyInfo = null;
                }
            }

            return navPropertyInfo != null;
        }
    }
}