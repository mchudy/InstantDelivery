namespace InstantDelivery.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddPackageHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackageHistory",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(),
                    EventType = c.Int(nullable: false),
                    Employee_Id = c.Int(),
                    Package_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Package_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PackageHistory", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.PackageHistory", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.PackageHistory", new[] { "Package_Id" });
            DropIndex("dbo.PackageHistory", new[] { "Employee_Id" });
            DropTable("dbo.PackageHistory");
        }
    }
}
