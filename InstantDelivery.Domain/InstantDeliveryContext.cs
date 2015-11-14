using System.Data.Entity;
using InstantDelivery.Domain.Entities;

namespace InstantDelivery.Domain
{
    /// <summary>
    /// Kontekst danych
    /// </summary>
    public class InstantDeliveryContext : DbContext
    {
        public virtual IDbSet<Employee> Employees { get; set; }
        public virtual IDbSet<Vehicle> Vehicles { get; set; }
        public virtual IDbSet<Package> Packages { get; set; }
        public virtual IDbSet<VehicleModel> VehicleModels { get; set; }
    }
}