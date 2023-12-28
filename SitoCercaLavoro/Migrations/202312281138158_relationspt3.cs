namespace SitoCercaLavoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationspt3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidature", "idProfili", c => c.Int(nullable: false));

        }
        
        public override void Down()
        {

        }
    }
}
