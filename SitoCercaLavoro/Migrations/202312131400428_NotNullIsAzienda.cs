namespace SitoCercaLavoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullIsAzienda : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utenti", "IsAzienda", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utenti", "IsAzienda", c => c.Boolean());
        }
    }
}
