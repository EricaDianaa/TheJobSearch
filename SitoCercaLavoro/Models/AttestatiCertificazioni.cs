namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttestatiCertificazioni")]
    public partial class AttestatiCertificazioni
    {
        [Key]
        public int IdCertificazione { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public string Descrizione { get; set; }

        public int IdProfilo { get; set; }

        public virtual Profili Profili { get; set; }
    }
}
