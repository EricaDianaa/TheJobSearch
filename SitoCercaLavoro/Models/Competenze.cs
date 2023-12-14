namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Competenze")]
    public partial class Competenze
    {
        [Key]
        public int IdCompetenza { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeCompetenza { get; set; }

        public string Descrizione { get; set; }

        public int IdProfilo { get; set; }

        public virtual Profili Profili { get; set; }
    }
}
