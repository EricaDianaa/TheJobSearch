namespace SitoCercaLavoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relazione : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profili", "Candidature_IdCandidatura", "dbo.Candidature");
            DropIndex("dbo.Profili", new[] { "Candidature_IdCandidatura" });
            DropColumn("dbo.Profili", "Candidature_IdCandidatura");
            DropColumn("dbo.Candidature", "idProfili");
        }
        
        public override void Down()
        {
        }
    }
}
