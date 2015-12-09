using InstantDelivery.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstantDelivery.Domain.Entities
{
    /// <summary>
    /// Reprezentuje zdarzenie w procesie dostarczania paczek (np. rejestrację
    /// paczki lub jej dostarczenie)
    /// </summary>
    [Table("PackageHistory")]
    public class PackageEvent : Entity
    {
        /// <summary>
        /// Paczka, której dotyczy dane zdarzenie
        /// </summary>
        public virtual Package Package { get; set; }

        /// <summary>
        /// Pracownik odpowiedzialny za zdarzenie
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Data i czas zdarzenia
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Rodzaj zdarzenia
        /// </summary>
        public PackageEventType EventType { get; set; }
    }
}
