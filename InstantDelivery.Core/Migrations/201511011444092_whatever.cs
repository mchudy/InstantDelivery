namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatever : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String(nullable: false));
        }
    }
}
