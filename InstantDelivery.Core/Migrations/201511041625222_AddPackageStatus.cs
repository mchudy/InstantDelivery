namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPackageStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Status");
        }
    }
}
