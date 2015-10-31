namespace InstantDelivery.Core.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateEmployee : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_City", newName: "ShipAddressCity");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Street", newName: "ShipAddressStreet");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Number", newName: "ShipAddressNumber");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_PostalCode", newName: "ShipAddressPostalCode");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_State", newName: "ShipAddressState");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Country", newName: "ShipAddressCountry");
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
            AddColumn("dbo.Employees", "PlaceOfResidence_City", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_Street", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_Number", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_PostalCode", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_State", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_Country", c => c.String());
            AddColumn("dbo.Employees", "Email", c => c.String());
            AddColumn("dbo.Employees", "Pesel", c => c.String());
            AddColumn("dbo.Employees", "Comments", c => c.String());
            AddColumn("dbo.Employees", "MotherMaidenName", c => c.String());
            AddColumn("dbo.Employees", "MotherName", c => c.String());
            AddColumn("dbo.Employees", "FatherName", c => c.String());
            AddColumn("dbo.Employees", "Citizenship", c => c.String());
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
            DropColumn("dbo.Employees", "Citizenship");
            DropColumn("dbo.Employees", "FatherName");
            DropColumn("dbo.Employees", "MotherName");
            DropColumn("dbo.Employees", "MotherMaidenName");
            DropColumn("dbo.Employees", "Comments");
            DropColumn("dbo.Employees", "Pesel");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "PlaceOfResidence_Country");
            DropColumn("dbo.Employees", "PlaceOfResidence_State");
            DropColumn("dbo.Employees", "PlaceOfResidence_PostalCode");
            DropColumn("dbo.Employees", "PlaceOfResidence_Number");
            DropColumn("dbo.Employees", "PlaceOfResidence_Street");
            DropColumn("dbo.Employees", "PlaceOfResidence_City");
            DropColumn("dbo.Employees", "PlaceOfBirth");
            DropColumn("dbo.Employees", "Gender");
            DropTable("dbo.VehicleModels");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressCountry", newName: "ShippingAddress_Country");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressState", newName: "ShippingAddress_State");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressPostalCode", newName: "ShippingAddress_PostalCode");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressNumber", newName: "ShippingAddress_Number");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressStreet", newName: "ShippingAddress_Street");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressCity", newName: "ShippingAddress_City");
        }
    }
}
