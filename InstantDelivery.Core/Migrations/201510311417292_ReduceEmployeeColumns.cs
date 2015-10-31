namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceEmployeeColumns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleModels",
                c => new
                    {
                        VehicleModelId = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        Payload = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleModelId);
            
            AddColumn("dbo.Employees", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "PlaceOfBirth", c => c.String());
            AddColumn("dbo.Employees", "Email", c => c.String());
            AddColumn("dbo.Employees", "Pesel", c => c.String());
            AddColumn("dbo.Vehicles", "VehicleModel_VehicleModelId", c => c.Int());
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false));
            CreateIndex("dbo.Vehicles", "VehicleModel_VehicleModelId");
            AddForeignKey("dbo.Vehicles", "VehicleModel_VehicleModelId", "dbo.VehicleModels", "VehicleModelId");
            DropColumn("dbo.Employees", "Sex");
            DropColumn("dbo.Vehicles", "CarryWeight");
            DropColumn("dbo.Vehicles", "Brand");
            DropColumn("dbo.Vehicles", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Model", c => c.String());
            AddColumn("dbo.Vehicles", "Brand", c => c.String());
            AddColumn("dbo.Vehicles", "CarryWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Employees", "Sex", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vehicles", "VehicleModel_VehicleModelId", "dbo.VehicleModels");
            DropIndex("dbo.Vehicles", new[] { "VehicleModel_VehicleModelId" });
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
            DropColumn("dbo.Vehicles", "VehicleModel_VehicleModelId");
            DropColumn("dbo.Employees", "Pesel");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "PlaceOfBirth");
            DropColumn("dbo.Employees", "Gender");
            DropTable("dbo.VehicleModels");
        }
    }
}
