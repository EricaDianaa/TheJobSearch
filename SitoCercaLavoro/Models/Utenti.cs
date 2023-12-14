namespace SitoCercaLavoro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utenti")]
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            Profili = new HashSet<Profili>();
        }

        [Key]
        public int IdUtente { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Ruolo { get; set; }

        [Required]
        [StringLength(50)]
        public string Indirizzo { get; set; }

        [StringLength(16)]
        public string CodiceFiscale { get; set; }

        public bool IsAzienda { get; set; }

        [StringLength(10)]
        public string PartitaIva { get; set; }

        public string Vcode { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profili> Profili { get; set; }
    }
}
