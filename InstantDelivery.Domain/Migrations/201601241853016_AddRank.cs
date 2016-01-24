namespace InstantDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Rank");
        }
    }
}
