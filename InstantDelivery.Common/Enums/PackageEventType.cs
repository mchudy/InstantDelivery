using System.ComponentModel;

namespace InstantDelivery.Common.Enums
{
    /// <summary>
    /// Rodzaj zdarzenia w procesie dostarczenia paczki
    /// </summary>
    public enum PackageEventType
    {
        [Description("Paczka zarejestrowana")]
        Registered,

        [Description("Paczka przekazana kurierowi")]
        HandedToCourier,

        [Description("Pozostawione awizo")]
        NoticeLeft,

        [Description("Paczka dostarczona")]
        Delivered,
    }
}