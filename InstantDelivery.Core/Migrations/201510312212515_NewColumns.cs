namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Vehicle_VehicleId", c => c.Int());
            AddColumn("dbo.Packages", "Depth", c => c.Int(nullable: false));
            AddColumn("dbo.VehicleModels", "AvailableSpaceX", c => c.Double(nullable: false));
            AddColumn("dbo.VehicleModels", "AvailableSpaceY", c => c.Double(nullable: false));
            AddColumn("dbo.VehicleModels", "AvailableSpaceZ", c => c.Double(nullable: false));
            CreateIndex("dbo.Employees", "Vehicle_VehicleId");
            AddForeignKey("dbo.Employees", "Vehicle_VehicleId", "dbo.Vehicles", "VehicleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Vehicle_VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Employees", new[] { "Vehicle_VehicleId" });
            DropColumn("dbo.VehicleModels", "AvailableSpaceZ");
            DropColumn("dbo.VehicleModels", "AvailableSpaceY");
            DropColumn("dbo.VehicleModels", "AvailableSpaceX");
            DropColumn("dbo.Packages", "Depth");
            DropColumn("dbo.Employees", "Vehicle_VehicleId");
        }
    }
}
