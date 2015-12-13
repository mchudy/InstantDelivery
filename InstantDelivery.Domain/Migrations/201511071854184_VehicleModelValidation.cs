using System.Data.Entity.Migrations;

namespace InstantDelivery.Domain.Migrations
{
    /// <summary>
    /// Ta migracja umo¿liwia stworzenie walidacji dla encji modelu widoku w bazie danych.
    /// </summary>
    public partial class VehicleModelValidation : DbMigration
    {
        /// <summary>
        /// Ustawia walidacje w bazie danych dla encji modelu pojazdu.
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleModels", "Model", c => c.String(nullable: false));
        }
        /// <summary>
        /// Cofa wprowadzone przez migracjê zmiany.
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.VehicleModels", "Model", c => c.String());
            AlterColumn("dbo.VehicleModels", "Brand", c => c.String());
        }
    }
}
