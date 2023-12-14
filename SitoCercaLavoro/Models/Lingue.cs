namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lingue")]
    public partial class Lingue
    {
        [Key]
        public int IdLingua { get; set; }

        [Required]
        [StringLength(50)]
        public string Lingua { get; set; }

        [Required]
        [StringLength(50)]
        public string Conoscenza { get; set; }

        public int IdProfilo { get; set; }

        public virtual Profili Profili { get; set; }
    }
}
