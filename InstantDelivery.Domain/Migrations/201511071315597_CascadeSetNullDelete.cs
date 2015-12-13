using System.Data.Entity.Migrations;

namespace InstantDelivery.Domain.Migrations
{
    /// <summary>
    /// Ta migracja umo¿liwia w³¹czenie ustawiania wartoœci null w momencie usuwania relacji w bazie danych.
    /// </summary>
    public partial class CascadeSetNullDelete : DbMigration
    {
        /// <summary>
        /// W³¹cza kaskadowe ustawianie wartoœci null w momencie usuwania relacji w bazie danych.
        /// </summary>
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[Vehicles] DROP CONSTRAINT [FK_dbo.Vehicles_dbo.VehicleModels_VehicleModel_Id]");
            Sql(@"ALTER TABLE[dbo].[Vehicles] ADD CONSTRAINT[FK_dbo.Vehicles_dbo.VehicleModels_VehicleModel_Id] FOREIGN KEY([VehicleModel_Id]) 
                    REFERENCES[dbo].[VehicleModels]([Id]) 
                    ON DELETE SET NULL");

            Sql(@"ALTER TABLE [dbo].[Packages] DROP CONSTRAINT [FK_dbo.Packages_dbo.Employees_Employee_Id]");
            Sql(@"ALTER TABLE[dbo].[Packages] ADD CONSTRAINT[FK_dbo.Packages_dbo.Employees_Employee_Id] FOREIGN KEY([Employee_Id])
                    REFERENCES[dbo].[Employees]([Id])
                    ON DELETE SET NULL");

            Sql(@"ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_dbo.Employees_dbo.Vehicles_Vehicle_Id]");
            Sql(@"ALTER TABLE [dbo].[Employees] ADD CONSTRAINT [FK_dbo.Employees_dbo.Vehicles_Vehicle_Id] FOREIGN KEY([Vehicle_Id])
                    REFERENCES [dbo].[Vehicles] ([Id])
                    ON DELETE SET NULL");
        }
        /// <summary>
        /// Cofa wprowadzone przez migracjê zmiany.
        /// </summary>
        public override void Down()
        {
        }
    }
}
