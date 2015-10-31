using InstantDelivery.Core.Entities;
using System.Data.Entity;

namespace InstantDelivery.Core
{
    public class InstantDeliveryContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}