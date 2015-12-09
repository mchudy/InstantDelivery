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
        public InstantDeliveryContext()
            : base("LocalDbConnection")
        {
        }

        public virtual IDbSet<Employee> Employees { get; set; }
        public virtual IDbSet<Vehicle> Vehicles { get; set; }
        public virtual IDbSet<Package> Packages { get; set; }
        public virtual IDbSet<VehicleModel> VehicleModels { get; set; }
        public virtual IDbSet<PackageEvent> PackageEvents { get; set; }
    }
}