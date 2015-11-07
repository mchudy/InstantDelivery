namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coszmienilem1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "PackageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "PackageId", c => c.Int(nullable: false));
        }
    }
}
