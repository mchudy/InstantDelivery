namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "RegistrationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "RegistrationNumber", c => c.String());
        }
    }
}
