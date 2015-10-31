using System;
using System.ComponentModel;
using System.Reflection;

namespace InstantDelivery.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            MemberInfo[] memberInfo = type.GetMember(@enum.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return @enum.ToString();
        }

    }
}
