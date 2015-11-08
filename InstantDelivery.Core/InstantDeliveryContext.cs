using InstantDelivery.Core.Entities;
using System.Data.Entity;

namespace InstantDelivery.Core
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