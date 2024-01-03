namespace SitoCercaLavoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rompitutto : DbMigration
    {
        public override void Up()
        {
         AddColumn("dbo.Profili", "Candidature_IdCandidatura", c => c.Int());
        }

        public override void Down()
        {
        }
         
    }
}
