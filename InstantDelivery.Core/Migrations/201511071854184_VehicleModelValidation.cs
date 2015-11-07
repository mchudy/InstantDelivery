namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleModelValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleModels", "Model", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleModels", "Model", c => c.String());
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String());
        }
    }
}
