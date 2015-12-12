namespace InstantDelivery.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PackageHistory_OnDeleteSetNull : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[PackageHistory] DROP CONSTRAINT [FK_dbo.PackageHistory_dbo.Packages_Package_Id]");
            Sql(@"ALTER TABLE[dbo].[PackageHistory] ADD CONSTRAINT[FK_dbo.PackageHistory_dbo.Packages_Package_Id] FOREIGN KEY([Package_Id])
                    REFERENCES[dbo].[Packages]([Id])
                    ON DELETE SET NULL");

            Sql(@"ALTER TABLE [dbo].[PackageHistory] DROP CONSTRAINT [FK_dbo.PackageHistory_dbo.Employees_Employee_Id]");
            Sql(@"ALTER TABLE[dbo].[PackageHistory] ADD CONSTRAINT[FK_dbo.PackageHistory_dbo.Employees_Employee_Id] FOREIGN KEY([Employee_Id])
                    REFERENCES[dbo].[Employees]([Id])
                    ON DELETE SET NULL");
        }

        public override void Down()
        {

        }
    }
}
