namespace InstantDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerTable_SenderRecipientInPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Sender", c => c.String());
            AddColumn("dbo.Packages", "Recipient", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Recipient");
            DropColumn("dbo.Packages", "Sender");
        }
    }
}
