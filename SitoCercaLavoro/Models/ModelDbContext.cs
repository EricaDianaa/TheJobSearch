using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SitoCercaLavoro.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Annunci> Annunci { get; set; }
        public virtual DbSet<AttestatiCertificazioni> AttestatiCertificazioni { get; set; }
        public virtual DbSet<Candidature> Candidature { get; set; }
        public virtual DbSet<Competenze> Competenze { get; set; }
        public virtual DbSet<Esperienze> Esperienze { get; set; }
        public virtual DbSet<Formazione> Formazione { get; set; }
        public virtual DbSet<Lingue> Lingue { get; set; }
        public virtual DbSet<Profili> Profili { get; set; }
        public virtual DbSet<TipoContratto> TipoContratto { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Annunci>()
                .HasMany(e => e.Candidature)
                .WithRequired(e => e.Annunci)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profili>()
                .HasMany(e => e.AttestatiCertificazioni)
                .WithRequired(e => e.Profili)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profili>()
                .HasMany(e => e.Competenze)
                .WithRequired(e => e.Profili)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profili>()
                .HasMany(e => e.Esperienze)
                .WithRequired(e => e.Profili)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profili>()
                .HasMany(e => e.Formazione)
                .WithRequired(e => e.Profili)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Profili>()
                .HasMany(e => e.Lingue)
                .WithRequired(e => e.Profili)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoContratto>()
                .HasMany(e => e.Annunci)
                .WithRequired(e => e.TipoContratto1)
                .HasForeignKey(e => e.TipoContratto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoContratto>()
                .HasMany(e => e.Esperienze)
                .WithRequired(e => e.TipoContratto)
                .HasForeignKey(e => e.Contratto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .Property(e => e.PartitaIva)
                .IsFixedLength();

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Profili)
                .WithRequired(e => e.Utenti)
                .WillCascadeOnDelete(false);
        }
    }
}
