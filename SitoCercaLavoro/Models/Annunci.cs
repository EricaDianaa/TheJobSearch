namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Annunci")]
    public partial class Annunci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Annunci()
        {
            Candidature = new HashSet<Candidature>();
        }

        [Key]
        public int IdAnnuncio { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeAnnuncio { get; set; }

        [StringLength(50)]
        public string Retribuzione { get; set; }

        [Required]
        public string Descrizione { get; set; }

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string SedeLavoro { get; set; }

        [Required]
        [StringLength(100)]
        public string Luogo { get; set; }

        public int TipoContratto { get; set; }

        public virtual TipoContratto TipoContratto1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidature> Candidature { get; set; }
    }
}
