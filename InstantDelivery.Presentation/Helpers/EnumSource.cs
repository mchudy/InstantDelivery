using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Markup;

namespace InstantDelivery.Helpers
{
    /// <summary>
    /// Umożliwia wykorzystanie typu wyliczeniowego w kontrolkach typu <see cref="ComboBox"/>
    /// </summary>
    public class EnumSource : MarkupExtension
    {
        private readonly Type type;

        public EnumSource(Type type)
        {
            this.type = type;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(type)
                .Cast<object>()
                .Select(e => new { Value = e, Name = ((Enum)e).GetDescription() });
        }
    }
}
