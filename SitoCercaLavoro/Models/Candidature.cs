namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Candidature")]
    public partial class Candidature
    {
        [Key]
        public int IdCandidatura { get; set; }

        public int IdAnnuncio { get; set; }

        [Required]
        [StringLength(50)]
        public string Curriculum { get; set; }

        public string Descrizione { get; set; }

        [StringLength(50)]
        public string Stato { get; set; }

        public virtual Annunci Annunci { get; set; }
    }
}
