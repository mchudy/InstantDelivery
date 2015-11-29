using System.ComponentModel;

namespace InstantDelivery.Common.Enums
{
    /// <summary>
    /// Definicja statusu paczki
    /// </summary>
    public enum PackageStatus
    {
        [Description("Nowa")]
        New,

        [Description("W dostawie")]
        InDelivery,

        [Description("Dostarczona")]
        Delivered
    }
}