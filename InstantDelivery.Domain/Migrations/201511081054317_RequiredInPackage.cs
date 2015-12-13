using System.Data.Entity.Migrations;

namespace InstantDelivery.Domain.Migrations
{
    /// <summary>
    /// Ta migracja pozwala na ustawienei walidacji na encji paczki w bazie danych.
    /// </summary>
    public partial class RequiredInPackage : DbMigration
    {
        /// <summary>
        /// Dodaje walidacje na encji paczki w bazie danych.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Employees", "PlaceOfResidence_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Packages", "ShippingAddress_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "PlaceOfResidence_City", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "PlaceOfResidence_Street", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "PlaceOfResidence_Number", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "PlaceOfResidence_PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "PlaceOfResidence_State", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "PlaceOfResidence_Country", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "ShippingAddress_City", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "ShippingAddress_Street", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "ShippingAddress_Number", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "ShippingAddress_PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "ShippingAddress_State", c => c.String(nullable: false));
            AlterColumn("dbo.Packages", "ShippingAddress_Country", c => c.String(nullable: false));
        }
        /// <summary>
        /// Cofa wprowadzone przez migracjê zmiany.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Packages", "ShippingAddress_Country", c => c.String());
            AlterColumn("dbo.Packages", "ShippingAddress_State", c => c.String());
            AlterColumn("dbo.Packages", "ShippingAddress_PostalCode", c => c.String());
            AlterColumn("dbo.Packages", "ShippingAddress_Number", c => c.String());
            AlterColumn("dbo.Packages", "ShippingAddress_Street", c => c.String());
            AlterColumn("dbo.Packages", "ShippingAddress_City", c => c.String());
            AlterColumn("dbo.Employees", "PlaceOfResidence_Country", c => c.String());
            AlterColumn("dbo.Employees", "PlaceOfResidence_State", c => c.String());
            AlterColumn("dbo.Employees", "PlaceOfResidence_PostalCode", c => c.String());
            AlterColumn("dbo.Employees", "PlaceOfResidence_Number", c => c.String());
            AlterColumn("dbo.Employees", "PlaceOfResidence_Street", c => c.String());
            AlterColumn("dbo.Employees", "PlaceOfResidence_City", c => c.String());
            DropColumn("dbo.Packages", "ShippingAddress_Id");
            DropColumn("dbo.Employees", "PlaceOfResidence_Id");
        }
    }
}
