namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoContratto")]
    public partial class TipoContratto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoContratto()
        {
            Annunci = new HashSet<Annunci>();
            Esperienze = new HashSet<Esperienze>();
        }

        [Key]
        public int idContratto { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeContratto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Annunci> Annunci { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Esperienze> Esperienze { get; set; }
    }
}
