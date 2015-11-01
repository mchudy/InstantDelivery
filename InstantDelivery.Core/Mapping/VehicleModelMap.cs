using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core.Mapping
{
    public class VehicleModelMap : EntityTypeConfiguration<VehicleModel>
    {
        public VehicleModelMap()
        {
            HasKey(t => t.VehicleModelId);
            Property(t => t.VehicleModelId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Brand);
            Property(t => t.Model);
            Property(t => t.Payload);
            Property(t => t.AvailableSpaceX);
            Property(t => t.AvailableSpaceY);
            Property(t => t.AvailableSpaceZ);

            ToTable("VehicleModels");
        }
    }
}