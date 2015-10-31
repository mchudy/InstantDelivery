using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace InstantDelivery.Core.Repositories
{
    public class EnumerationManager
    {
        public static Array GetValues(Type enumeration)
        {
            var wArray = Enum.GetValues(enumeration);
            var wFinalArray = new ArrayList();
            foreach (Enum wValue in wArray)
            {
                var fi = enumeration.GetField(wValue.ToString());
                if (null == fi) continue;
                var wBrowsableAttributes = fi.GetCustomAttributes(typeof(BrowsableAttribute), true) as BrowsableAttribute[];
                if (wBrowsableAttributes != null && wBrowsableAttributes.Length > 0)
                {
                    if (wBrowsableAttributes[0].Browsable == false)
                    {
                        continue;
                    }
                }
                var wDescriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), true) as DescriptionAttribute[];
                if (wDescriptions != null && wDescriptions.Length > 0)
                {
                    wFinalArray.Add(wDescriptions[0].Description);
                }
                else
                    wFinalArray.Add(wValue);
            }
            return wFinalArray.ToArray();
        }
    }
}