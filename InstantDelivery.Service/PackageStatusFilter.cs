using System.ComponentModel;

namespace InstantDelivery.Service
{
    public enum PackageStatusFilter
    {
        [Description("Dostarczone")]
        Delivered,
        [Description("Nowe")]
        New,
        [Description("W dostawie")]
        InDelivery,
        [Description("Wszystkie")]
        All
    }
}