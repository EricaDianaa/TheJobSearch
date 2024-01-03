namespace SitoCercaLavoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class riparatuttoprova : DbMigration
    {
        public override void Up()
        {

            
            DropColumn("dbo.Profili", "Candidature_IdCandidatura");
        }
        
        public override void Down()
        {
        }
    }
}
