namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String());
            AlterColumn("dbo.VehicleModels", "Model", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleModels", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String(nullable: false));
        }
    }
}
