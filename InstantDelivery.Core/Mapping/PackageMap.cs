using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core.Mapping
{
    public class PackageMap : EntityTypeConfiguration<Package>
    {
        public PackageMap()
        {
            HasKey(t => t.PackageId);
            Property(t => t.PackageId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ShippingAddress.City);
            Property(t => t.ShippingAddress.Country);
            Property(t => t.ShippingAddress.Number);
            Property(t => t.ShippingAddress.PostalCode);
            Property(t => t.ShippingAddress.State);
            Property(t => t.ShippingAddress.Street);
            Property(t => t.Weight);
            Property(t => t.Height);
            Property(t => t.Width);
            Property(t => t.Depth);

            ToTable("Packages");
        }
    }
}