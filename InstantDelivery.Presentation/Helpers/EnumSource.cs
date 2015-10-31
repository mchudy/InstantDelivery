using System;
using System.Linq;
using System.Windows.Markup;

namespace InstantDelivery.Helpers
{
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
