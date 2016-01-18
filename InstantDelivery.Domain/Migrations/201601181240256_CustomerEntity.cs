namespace InstantDelivery.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CustomerEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Gender = c.Int(nullable: false),
                    DateOfBirth = c.DateTime(),
                    PhoneNumber = c.String(),
                    PlaceOfResidence_City = c.String(),
                    PlaceOfResidence_Street = c.String(),
                    PlaceOfResidence_Number = c.String(),
                    PlaceOfResidence_PostalCode = c.String(),
                    PlaceOfResidence_State = c.String(),
                    PlaceOfResidence_Country = c.String(),
                    Email = c.String(),
                    Pesel = c.String(),
                    User_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);

            AddColumn("dbo.Packages", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Packages", "Customer_Id");
            AddForeignKey("dbo.Packages", "Customer_Id", "dbo.Customers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Customers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Packages", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Packages", new[] { "Customer_Id" });
            DropIndex("dbo.Customers", new[] { "User_Id" });
            DropColumn("dbo.Packages", "Customer_Id");
            DropTable("dbo.Customers");
        }
    }
}
