using System.ComponentModel;

namespace InstantDelivery.Common.Enums
{
    /// <summary>
    /// Definicja statusu paczki
    /// </summary>
    public enum PackageStatus
    {
        [Description("W magazynie")]
        InWarehouse,

        [Description("U klienta")]
        AtClientsLocation,

        [Description("Do odebrania")]
        ToPickUp,

        [Description("W dostawie")]
        InDelivery,

        [Description("Dostarczona")]
        Delivered,

        [Description("Awizowana")]
        NoticeLeft,
    }
}