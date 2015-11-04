namespace InstantDelivery.Core.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Employee_ResetAddressColumnsToDefault : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Packages", name: "ShipAddressCity", newName: "ShippingAddress_City");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressStreet", newName: "ShippingAddress_Street");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressNumber", newName: "ShippingAddress_Number");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressPostalCode", newName: "ShippingAddress_PostalCode");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressState", newName: "ShippingAddress_State");
            RenameColumn(table: "dbo.Packages", name: "ShipAddressCountry", newName: "ShippingAddress_Country");
        }

        public override void Down()
        {
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Country", newName: "ShipAddressCountry");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_State", newName: "ShipAddressState");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_PostalCode", newName: "ShipAddressPostalCode");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Number", newName: "ShipAddressNumber");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_Street", newName: "ShipAddressStreet");
            RenameColumn(table: "dbo.Packages", name: "ShippingAddress_City", newName: "ShipAddressCity");
        }
    }
}
