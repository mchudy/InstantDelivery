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
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        PhoneNumber = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
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
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
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
                        CarryWeight = c.Double(nullable: false),
                        RegistrationNumber = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                    })
                .PrimaryKey(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Packages", new[] { "Employee_EmployeeId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Packages");
            DropTable("dbo.Employees");
        }
    }
}
