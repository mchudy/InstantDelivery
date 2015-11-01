using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            HasKey(t => t.EmployeeId);
            Property(t => t.EmployeeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FirstName).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.Gender).IsRequired();
            Property(t => t.DateOfBirth);
            Property(t => t.PlaceOfBirth);
            Property(t => t.PhoneNumber);
            Property(t => t.PlaceOfResidence.City);
            Property(t => t.PlaceOfResidence.Country);
            Property(t => t.PlaceOfResidence.Number);
            Property(t => t.PlaceOfResidence.PostalCode);
            Property(t => t.PlaceOfResidence.State);
            Property(t => t.PlaceOfResidence.Street);
            Property(t => t.Email);
            Property(t => t.Pesel);
            Property(t => t.Comments);
            Property(t => t.MotherMaidenName);
            Property(t => t.MotherName);
            Property(t => t.FatherName);
            Property(t => t.Salary).IsRequired();
            Property(t => t.HireDate);
            Property(t => t.Citizenship);

            ToTable("Employees");
        }
    }

}