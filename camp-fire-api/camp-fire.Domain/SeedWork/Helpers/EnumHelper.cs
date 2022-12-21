using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace camp_fire.Domain.SeedWork.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Enum description'ları okur ve geriye string döner
        /// </summary>
        public static string GetDescription(this Enum @enum)
        {
            var type = @enum.GetType();
            if (!type.IsEnum) return string.Empty;

            var memberInfo = type.GetMember(@enum.ToString()).FirstOrDefault();

            var attr = memberInfo == null ? default :
                memberInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attr == null ? @enum.ToString() : attr.Description;
        }
    }
}