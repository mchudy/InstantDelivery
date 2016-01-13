using InstantDelivery.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace InstantDelivery.Domain
{
    /// <summary>
    /// Kontekst danych
    /// </summary>
    public class InstantDeliveryContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Konstruktor kontekstu bazy danych.
        /// </summary>
        public InstantDeliveryContext()
            : base("DbConnection")
        {
        }

        /// <summary>
        /// Tabela pracowników.
        /// </summary>
        public virtual IDbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Tabela pojazdów.
        /// </summary>
        public virtual IDbSet<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Tabela przesyłek.
        /// </summary>
        public virtual IDbSet<Package> Packages { get; set; }

        /// <summary>
        /// Tabela modeli pojazdów.
        /// </summary>
        public virtual IDbSet<VehicleModel> VehicleModels { get; set; }

        /// <summary>
        /// Tabela zdarzeń związanych z paczkami
        /// </summary>
        public virtual IDbSet<PackageEvent> PackageEvents { get; set; }
    }
}