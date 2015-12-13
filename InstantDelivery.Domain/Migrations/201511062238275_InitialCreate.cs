using System.Data.Entity.Migrations;

namespace InstantDelivery.Domain.Migrations
{
    /// <summary>
    /// Ta migracja tworzy pocz¹tkow¹ instancjê bazy.
    /// </summary>
    public partial class Initial_Create : DbMigration
    {
        /// <summary>
        /// Tworzy pocz¹tkow¹ instancjê bazy.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Gender = c.Int(nullable: false),
                    DateOfBirth = c.DateTime(),
                    PhoneNumber = c.String(),
                    PlaceOfResidence_City = c.String(),
                    PlaceOfResidence_Street = c.String(),
                    PlaceOfResidence_Number = c.String(),
                    PlaceOfResidence_PostalCode = c.String(),
                    PlaceOfResidence_State = c.String(),
                    PlaceOfResidence_Country = c.String(),
                    Email = c.String(),
                    Pesel = c.String(),
                    Comments = c.String(),
                    MotherName = c.String(),
                    FatherName = c.String(),
                    Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    HireDate = c.DateTime(),
                    Vehicle_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Vehicle_Id);

            CreateTable(
                "dbo.Packages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ShippingAddress_City = c.String(),
                    ShippingAddress_Street = c.String(),
                    ShippingAddress_Number = c.String(),
                    ShippingAddress_PostalCode = c.String(),
                    ShippingAddress_State = c.String(),
                    ShippingAddress_Country = c.String(),
                    Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Height = c.Double(nullable: false),
                    Width = c.Double(nullable: false),
                    Length = c.Double(nullable: false),
                    Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Status = c.Int(nullable: false),
                    Employee_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.Employee_Id);

            CreateTable(
                "dbo.Vehicles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RegistrationNumber = c.String(nullable: false),
                    VehicleModel_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleModels", t => t.VehicleModel_Id)
                .Index(t => t.VehicleModel_Id);

            CreateTable(
                "dbo.VehicleModels",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Brand = c.String(),
                    Model = c.String(),
                    Payload = c.Double(nullable: false),
                    AvailableSpaceX = c.Double(nullable: false),
                    AvailableSpaceY = c.Double(nullable: false),
                    AvailableSpaceZ = c.Double(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }
        /// <summary>
        /// Cofa wprowadzone przez migracjê zmiany.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehicleModel_Id", "dbo.VehicleModels");
            DropForeignKey("dbo.Packages", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Vehicles", new[] { "VehicleModel_Id" });
            DropIndex("dbo.Packages", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "Vehicle_Id" });
            DropTable("dbo.VehicleModels");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Packages");
            DropTable("dbo.Employees");
        }
    }
}
