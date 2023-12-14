namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Formazione")]
    public partial class Formazione
    {
        [Key]
        public int IdFormazione { get; set; }

        [Required]
        [StringLength(50)]
        public string Scuola { get; set; }

        [Required]
        [StringLength(50)]
        public string TitoloStudio { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeStudio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataInizio { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataFine { get; set; }

        public int? Votazione { get; set; }

        public int IdProfilo { get; set; }

        public virtual Profili Profili { get; set; }
    }
}
