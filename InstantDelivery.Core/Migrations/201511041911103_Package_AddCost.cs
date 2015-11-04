namespace InstantDelivery.Core.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Package_AddCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Length", c => c.Double(nullable: false));
            AddColumn("dbo.Packages", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Packages", "Height", c => c.Double(nullable: false));
            AlterColumn("dbo.Packages", "Width", c => c.Double(nullable: false));
            //DropColumn("dbo.Packages", "Depth");
        }

        public override void Down()
        {
            //AddColumn("dbo.Packages", "Depth", c => c.Int(nullable: false));
            AlterColumn("dbo.Packages", "Width", c => c.Int(nullable: false));
            AlterColumn("dbo.Packages", "Height", c => c.Int(nullable: false));
            DropColumn("dbo.Packages", "Cost");
            DropColumn("dbo.Packages", "Length");
        }
    }
}
