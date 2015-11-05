namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
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
                        Vehicle_VehicleId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_VehicleId)
                .Index(t => t.Vehicle_VehicleId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
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
                        Employee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.PackageId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .Index(t => t.Employee_EmployeeId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false),
                        VehicleModel_VehicleModelId = c.Int(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.VehicleModels", t => t.VehicleModel_VehicleModelId)
                .Index(t => t.VehicleModel_VehicleModelId);
            
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        VehicleModelId = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        Payload = c.Double(nullable: false),
                        AvailableSpaceX = c.Double(nullable: false),
                        AvailableSpaceY = c.Double(nullable: false),
                        AvailableSpaceZ = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehicleModel_VehicleModelId", "dbo.VehicleModels");
            DropForeignKey("dbo.Packages", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Vehicles", new[] { "VehicleModel_VehicleModelId" });
            DropIndex("dbo.Packages", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Employees", new[] { "Vehicle_VehicleId" });
            DropTable("dbo.VehicleModels");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Packages");
            DropTable("dbo.Employees");
        }
    }
}
