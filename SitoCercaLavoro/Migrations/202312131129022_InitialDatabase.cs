namespace SitoCercaLavoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annunci",
                c => new
                    {
                        IdAnnuncio = c.Int(nullable: false, identity: true),
                        NomeAnnuncio = c.String(nullable: false, maxLength: 50),
                        Retribuzione = c.String(maxLength: 50),
                        Descrizione = c.String(nullable: false),
                        Categoria = c.String(nullable: false, maxLength: 50),
                        SedeLavoro = c.String(nullable: false, maxLength: 50),
                        Luogo = c.String(nullable: false, maxLength: 100),
                        TipoContratto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnnuncio)
                .ForeignKey("dbo.TipoContratto", t => t.TipoContratto)
                .Index(t => t.TipoContratto);
            
            CreateTable(
                "dbo.Candidature",
                c => new
                    {
                        IdCandidatura = c.Int(nullable: false, identity: true),
                        IdAnnuncio = c.Int(nullable: false),
                        Curriculum = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(),
                        Stato = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCandidatura)
                .ForeignKey("dbo.Annunci", t => t.IdAnnuncio)
                .Index(t => t.IdAnnuncio);
            
            CreateTable(
                "dbo.TipoContratto",
                c => new
                    {
                        idContratto = c.Int(nullable: false, identity: true),
                        NomeContratto = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.idContratto);
            
            CreateTable(
                "dbo.Esperienze",
                c => new
                    {
                        IdEsperienza = c.Int(nullable: false, identity: true),
                        Qualifica = c.String(nullable: false, maxLength: 50),
                        Contratto = c.Int(nullable: false),
                        NomeAzienda = c.String(nullable: false, maxLength: 50),
                        Localita = c.String(nullable: false, maxLength: 50),
                        SedeLavoro = c.String(nullable: false, maxLength: 50),
                        DataInizio = c.DateTime(nullable: false, storeType: "date"),
                        DataFine = c.DateTime(storeType: "date"),
                        IdProfilo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEsperienza)
                .ForeignKey("dbo.Profili", t => t.IdProfilo)
                .ForeignKey("dbo.TipoContratto", t => t.Contratto)
                .Index(t => t.Contratto)
                .Index(t => t.IdProfilo);
            
            CreateTable(
                "dbo.Profili",
                c => new
                    {
                        IdProfilo = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Cognome = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Telefono = c.String(nullable: false, maxLength: 50),
                        Foto = c.String(maxLength: 50),
                        Presentazione = c.String(),
                        IdUtente = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProfilo)
                .ForeignKey("dbo.Utenti", t => t.IdUtente)
                .Index(t => t.IdUtente);
            
            CreateTable(
                "dbo.AttestatiCertificazioni",
                c => new
                    {
                        IdCertificazione = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(),
                        IdProfilo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCertificazione)
                .ForeignKey("dbo.Profili", t => t.IdProfilo)
                .Index(t => t.IdProfilo);
            
            CreateTable(
                "dbo.Competenze",
                c => new
                    {
                        IdCompetenza = c.Int(nullable: false, identity: true),
                        NomeCompetenza = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(),
                        IdProfilo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCompetenza)
                .ForeignKey("dbo.Profili", t => t.IdProfilo)
                .Index(t => t.IdProfilo);
            
            CreateTable(
                "dbo.Formazione",
                c => new
                    {
                        IdFormazione = c.Int(nullable: false, identity: true),
                        Scuola = c.String(nullable: false, maxLength: 50),
                        TitoloStudio = c.String(nullable: false, maxLength: 50),
                        NomeStudio = c.String(nullable: false, maxLength: 50),
                        DataInizio = c.DateTime(storeType: "date"),
                        DataFine = c.DateTime(nullable: false, storeType: "date"),
                        Votazione = c.Int(),
                        IdProfilo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdFormazione)
                .ForeignKey("dbo.Profili", t => t.IdProfilo)
                .Index(t => t.IdProfilo);
            
            CreateTable(
                "dbo.Lingue",
                c => new
                    {
                        IdLingua = c.Int(nullable: false, identity: true),
                        Lingua = c.String(nullable: false, maxLength: 50),
                        Conoscenza = c.String(nullable: false, maxLength: 50),
                        IdProfilo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLingua)
                .ForeignKey("dbo.Profili", t => t.IdProfilo)
                .Index(t => t.IdProfilo);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        IdUtente = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Ruolo = c.String(maxLength: 50),
                        Indirizzo = c.String(nullable: false, maxLength: 50),
                        CodiceFiscale = c.String(maxLength: 16),
                        IsAzienda = c.Boolean(),
                        PartitaIva = c.String(maxLength: 10, fixedLength: true),
                        Vcode = c.String(),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUtente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Esperienze", "Contratto", "dbo.TipoContratto");
            DropForeignKey("dbo.Profili", "IdUtente", "dbo.Utenti");
            DropForeignKey("dbo.Lingue", "IdProfilo", "dbo.Profili");
            DropForeignKey("dbo.Formazione", "IdProfilo", "dbo.Profili");
            DropForeignKey("dbo.Esperienze", "IdProfilo", "dbo.Profili");
            DropForeignKey("dbo.Competenze", "IdProfilo", "dbo.Profili");
            DropForeignKey("dbo.AttestatiCertificazioni", "IdProfilo", "dbo.Profili");
            DropForeignKey("dbo.Annunci", "TipoContratto", "dbo.TipoContratto");
            DropForeignKey("dbo.Candidature", "IdAnnuncio", "dbo.Annunci");
            DropIndex("dbo.Lingue", new[] { "IdProfilo" });
            DropIndex("dbo.Formazione", new[] { "IdProfilo" });
            DropIndex("dbo.Competenze", new[] { "IdProfilo" });
            DropIndex("dbo.AttestatiCertificazioni", new[] { "IdProfilo" });
            DropIndex("dbo.Profili", new[] { "IdUtente" });
            DropIndex("dbo.Esperienze", new[] { "IdProfilo" });
            DropIndex("dbo.Esperienze", new[] { "Contratto" });
            DropIndex("dbo.Candidature", new[] { "IdAnnuncio" });
            DropIndex("dbo.Annunci", new[] { "TipoContratto" });
            DropTable("dbo.Utenti");
            DropTable("dbo.Lingue");
            DropTable("dbo.Formazione");
            DropTable("dbo.Competenze");
            DropTable("dbo.AttestatiCertificazioni");
            DropTable("dbo.Profili");
            DropTable("dbo.Esperienze");
            DropTable("dbo.TipoContratto");
            DropTable("dbo.Candidature");
            DropTable("dbo.Annunci");
        }
    }
}
