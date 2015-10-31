namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeAddress : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_City", newName: "ShipAddressCity");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Street", newName: "ShipAddressStreet");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Number", newName: "ShipAddressNumber");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_PostalCode", newName: "ShipAddressPostalCode");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_State", newName: "ShipAddressState");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Country", newName: "ShipAddressCountry");
            AddColumn("dbo.Employees", "PlaceOfResidence_City", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_Street", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_Number", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_PostalCode", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_State", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfResidence_Country", c => c.String());
            AddColumn("dbo.Employees", "Comments", c => c.String());
            AddColumn("dbo.Employees", "MotherMaidenName", c => c.String());
            AddColumn("dbo.Employees", "MotherName", c => c.String());
            AddColumn("dbo.Employees", "FatherName", c => c.String());
            AddColumn("dbo.Employees", "Citizenship", c => c.String());
            DropColumn("dbo.Employees", "PlaceOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "PlaceOfBirth", c => c.String());
            DropColumn("dbo.Employees", "Citizenship");
            DropColumn("dbo.Employees", "FatherName");
            DropColumn("dbo.Employees", "MotherName");
            DropColumn("dbo.Employees", "MotherMaidenName");
            DropColumn("dbo.Employees", "Comments");
            DropColumn("dbo.Employees", "PlaceOfResidence_Country");
            DropColumn("dbo.Employees", "PlaceOfResidence_State");
            DropColumn("dbo.Employees", "PlaceOfResidence_PostalCode");
            DropColumn("dbo.Employees", "PlaceOfResidence_Number");
            DropColumn("dbo.Employees", "PlaceOfResidence_Street");
            DropColumn("dbo.Employees", "PlaceOfResidence_City");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressCountry", newName: "ShippingAddress_Country");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressState", newName: "ShippingAddress_State");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressPostalCode", newName: "ShippingAddress_PostalCode");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressNumber", newName: "ShippingAddress_Number");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressStreet", newName: "ShippingAddress_Street");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressCity", newName: "ShippingAddress_City");
        }
    }
}
