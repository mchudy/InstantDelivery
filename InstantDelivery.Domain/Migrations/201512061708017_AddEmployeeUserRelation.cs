namespace InstantDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Ta migracja umo¿liwia stworzenie relacji pracownik - u¿ytkownik.
    /// </summary>
    public partial class AddEmployeeUserRelation : DbMigration
    {
        /// <summary>
        /// Tworzy relacjê pracownik - u¿ytkownik.
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Employees", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "User_Id");
            AddForeignKey("dbo.Employees", "User_Id", "dbo.AspNetUsers", "Id");
        }
        /// <summary>
        /// Cofa wprowadzone przez migracjê zmiany.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "User_Id" });
            DropColumn("dbo.Employees", "User_Id");
        }
    }
}
