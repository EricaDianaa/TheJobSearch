namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Esperienze")]
    public partial class Esperienze
    {
        [Key]
        public int IdEsperienza { get; set; }

        [Required]
        [StringLength(50)]
        public string Qualifica { get; set; }

        public int Contratto { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeAzienda { get; set; }

        [Required]
        [StringLength(50)]
        public string Localita { get; set; }

        [Required]
        [StringLength(50)]
        public string SedeLavoro { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataInizio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataFine { get; set; }

        public int IdProfilo { get; set; }

        public virtual Profili Profili { get; set; }

        public virtual TipoContratto TipoContratto { get; set; }
    }
}
