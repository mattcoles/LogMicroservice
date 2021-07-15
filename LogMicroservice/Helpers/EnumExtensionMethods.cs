using System;
using System.ComponentModel;

namespace LogMicroservice.Helpers
{
    /// <summary>
    /// Enum Extension method to get the enum description attribute
    /// </summary>
    public static class EnumExtensionMethod
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
