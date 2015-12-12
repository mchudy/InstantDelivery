using System.ComponentModel;

namespace InstantDelivery.Common.Enums
{
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