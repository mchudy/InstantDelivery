using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core.Mapping
{
    public class VehicleMap : EntityTypeConfiguration<Vehicle>
    {
        public VehicleMap()
        {
            HasKey(t => t.VehicleId);
            Property(t => t.VehicleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.RegistrationNumber);

            ToTable("Vehicles");
        }
    }
}