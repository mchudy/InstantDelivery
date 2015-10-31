namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Vehicle_VehicleId", c => c.Int());
            CreateIndex("dbo.Employees", "Vehicle_VehicleId");
            AddForeignKey("dbo.Employees", "Vehicle_VehicleId", "dbo.Vehicles", "VehicleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Vehicle_VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Employees", new[] { "Vehicle_VehicleId" });
            DropColumn("dbo.Employees", "Vehicle_VehicleId");
        }
    }
}
