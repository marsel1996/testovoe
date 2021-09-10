using System;
using System.ComponentModel;
using System.Linq;

namespace test_api.Helpers
{
    public static class EnumHelper
    { 
        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

    }
}
