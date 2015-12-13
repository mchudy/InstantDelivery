using System.ComponentModel;

namespace InstantDelivery.Common.Enums
{
    /// <summary>
    /// Definicja roli jaką może przyjąć użytkownik
    /// </summary>
    public enum Role
    {
        [Description("Administrator")]
        Admin,

        [Description("Kurier")]
        Courier,

        [Description("Pracownik administracyjny")]
        AdministrativeEmployee
    }
}