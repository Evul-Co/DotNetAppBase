using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Enums;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Models
    {
        public const string DefDisplayAGerar = "[A Gerar]";
        public const string DefDisplayNaoInformado = "[Não Informado]";
        public const string DefDisplayIndisponivel = "[Indisponível]";

        public const int NullFk = 0;

        public static readonly DateTime LessDbDateTimeValida = new DateTime(1753, 1, 1);

        public static bool DateTimeIsValid(in DateTime dateTime) => dateTime >= LessDbDateTimeValida;

        public static bool FkIsNotNull(int fk) => !FkIsNull(fk);

        public static bool FkIsNull(int fk) => fk <= 0;

        public static EDateTimeFormat GetDateTimeFormat(PropertyDescriptor propertyDescriptor)
        {
            var format = Reflections.Attributes.GetData<IDateTimeConstraint, EDateTimeFormat>(propertyDescriptor, EDateTimeFormat.Date, attribute => attribute.Format);

            return format;
        }

        public static string GetDescription(PropertyDescriptor propertyDescriptor)
        {
            string GetDescription()
            {
                return Reflections.Attributes.GetData<DescriptionAttribute, string>(propertyDescriptor, null, attribute => attribute.Description)
                       ?? Reflections.Attributes.GetData<DisplayAttribute, string>(propertyDescriptor, null, attribute => attribute.Description)
                       ?? Reflections.Attributes.GetData<IPresentDisplay, string>(propertyDescriptor, null, attribute => attribute.Description);
            }

            return GetDescription();
        }

        public static string GetDescription(MemberInfo memberInfo)
        {
            string GetDescription()
            {
                return Reflections.Attributes.GetData<DescriptionAttribute, string>(memberInfo, null, attribute => attribute.Description)
                       ?? Reflections.Attributes.GetData<DisplayAttribute, string>(memberInfo, null, attribute => attribute.Description)
                       ?? Reflections.Attributes.GetData<IPresentDisplay, string>(memberInfo, null, attribute => attribute.Description);
            }

            return GetDescription();
        }

        public static (string Name, string Description, string GroupName)? GetDisplayData(MemberInfo fieldInfo)
        {
            var displayAttribute = Reflections.Attributes.Get<DisplayAttribute>(fieldInfo);
            if (displayAttribute != null)
            {
                return (displayAttribute.GetName(), displayAttribute.GetDescription(), displayAttribute.GetGroupName());
            }

            var presentDisplay = Reflections.Attributes.Get<IPresentDisplay>(fieldInfo);
            if (presentDisplay != null)
            {
                return (presentDisplay.Name, presentDisplay.Description, presentDisplay.GroupName);
            }

            return null;
        }

        public static string GetDisplayName(PropertyDescriptor propertyDescriptor, bool returnPropertyNameIfDidNotFindDisplay = true)
        {
            string GetDisplay()
            {
                return Reflections.Attributes.GetData<DisplayAttribute, string>(propertyDescriptor, null, attribute => attribute.GetName())
                       ?? Reflections.Attributes.GetData<IPresentDisplay, string>(propertyDescriptor, null, attribute => attribute.Name);
            }

            if (returnPropertyNameIfDidNotFindDisplay)
            {
                return propertyDescriptor.DisplayName != propertyDescriptor.Name
                    ? propertyDescriptor.DisplayName
                    : GetDisplay() ?? propertyDescriptor.Name;
            }

            return propertyDescriptor.DisplayName != propertyDescriptor.Name
                ? propertyDescriptor.DisplayName
                : GetDisplay();
        }

        public static string GetDisplayName(MemberInfo memberInfo, bool returnMemberNameIfDidNotFindDisplay = true)
        {
            string GetDisplay()
            {
                return Reflections.Attributes.GetData<DisplayAttribute, string>(memberInfo, null, attribute => attribute.GetName())
                       ?? Reflections.Attributes.GetData<IPresentDisplay, string>(memberInfo, null, attribute => attribute.Name);
            }

            if (returnMemberNameIfDidNotFindDisplay)
            {
                return GetDisplay() ?? memberInfo.Name;
            }

            return GetDisplay();
        }

        public static int GetMaxLength(PropertyDescriptor propertyDescriptor)
        {
            var length = Reflections.Attributes.GetData<IMaxLengthConstraint, int>(propertyDescriptor, -1, attribute => attribute.Value);
            if (length == -1)
            {
                length = Reflections.Attributes.GetData<MaxLengthAttribute, int>(propertyDescriptor, -1, attribute => attribute.Length);
            }

            if (length == -1)
            {
                length = Reflections.Attributes.GetData<StringLengthAttribute, int>(propertyDescriptor, -1, attribute => attribute.MaximumLength);
            }

            return Math.Max(length, 0);
        }

        public static int GetMinLength(PropertyDescriptor propertyDescriptor)
        {
            var length = Reflections.Attributes.GetData<MinLengthAttribute, int>(propertyDescriptor, -1, attribute => attribute.Length);

            if (length == -1)
            {
                length = Reflections.Attributes.GetData<StringLengthAttribute, int>(propertyDescriptor, -1, attribute => attribute.MinimumLength);
            }

            return Math.Max(length, 0);
        }
    }
}