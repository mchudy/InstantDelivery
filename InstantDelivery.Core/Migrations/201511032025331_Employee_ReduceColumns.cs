namespace InstantDelivery.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_ReduceColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "PlaceOfBirth");
            DropColumn("dbo.Employees", "MotherMaidenName");
            DropColumn("dbo.Employees", "Citizenship");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Citizenship", c => c.String());
            AddColumn("dbo.Employees", "MotherMaidenName", c => c.String());
            AddColumn("dbo.Employees", "PlaceOfBirth", c => c.String());
        }
    }
}
