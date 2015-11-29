using System.ComponentModel;

namespace InstantDelivery.Common.Enums
{
    /// <summary>
    /// Definicja reprezentacji płci
    /// </summary>
    public enum Gender
    {
        [Description("Mężczyczna")]
        Male,
        [Description("Kobieta")]
        Female
    };
}