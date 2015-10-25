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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BuildAddressModel(modelBuilder);
        }

        private static void BuildAddressModel(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types<Package>()
                .Configure(ctc => ctc.Property(cust => cust.ShippingAddress.Street).HasColumnName("ShipAddressStreet"));
            modelBuilder.Types<Package>()
                .Configure(ctc => ctc.Property(cust => cust.ShippingAddress.City).HasColumnName("ShipAddressCity"));
            modelBuilder.Types<Package>()
                .Configure(ctc => ctc.Property(cust => cust.ShippingAddress.Country).HasColumnName("ShipAddressCountry"));
            modelBuilder.Types<Package>()
                .Configure(ctc => ctc.Property(cust => cust.ShippingAddress.Number).HasColumnName("ShipAddressNumber"));
            modelBuilder.Types<Package>()
                .Configure(ctc => ctc.Property(cust => cust.ShippingAddress.PostalCode).HasColumnName("ShipAddressPostalCode"));
            modelBuilder.Types<Package>()
                .Configure(ctc => ctc.Property(cust => cust.ShippingAddress.State).HasColumnName("ShipAddressState"));
        }
    }
}