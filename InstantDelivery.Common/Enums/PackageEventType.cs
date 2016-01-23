using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Common.Enums
{
    /// <summary>
    /// Rodzaj zdarzenia w procesie dostarczenia paczki
    /// </summary>
    public enum PackageEventType
    {
        [Display(Name = "Paczka zarejestrowana")]
        [Description("Paczka czeka na odebranie od nadawcy")]
        ReadyToPickFromSender,

        [Display(Name = "Paczka zarejestrowana")]
        [Description("Przydzielono kuriera do odebrania paczki od klienta")]
        CourierAssignedToPickFrom,

        [Display(Name = "Paczka zarejestrowana")]
        [Description("Paczka zarejestrowana w magazynie")]
        RegisteredInWarehouse,

        [Display(Name = "Paczka zarejestrowana")]
        [Description("Paczka przekazana kurierowi")]
        HandedToCourier,

        [Display(Name = "Pozostawione awizo")]
        [Description("Pozostawione awizo")]
        NoticeLeft,

        [Display(Name = "Paczka dostarczona")]
        [Description("Paczka dostarczona")]
        Delivered,
    }
}