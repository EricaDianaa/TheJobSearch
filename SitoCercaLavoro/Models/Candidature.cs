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
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        ////public Candidature()
        //{
        //    Profili = new HashSet<Profili>();
        //}
        [Key]
        public int IdCandidatura { get; set; }

        public int IdAnnuncio { get; set; }

        [Required]
        [StringLength(50)]
        public string Curriculum { get; set; }

        public string Descrizione { get; set; }

        [StringLength(50)]
        public string Stato { get; set; }

        public int idProfili { get; set; }
        public virtual Annunci Annunci { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Profili> Profili { get; set; }
    }
}
