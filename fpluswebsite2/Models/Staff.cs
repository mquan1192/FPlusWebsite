namespace fpluswebsite2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            Cleanings = new HashSet<Cleaning>();
            Pickups = new HashSet<Pickup>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "名前")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "メールアドレス")]
        public string Mail { get; set; }

        [StringLength(50)]
        [Display(Name = "電話番号")]
        public string Phone { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "時給")]
        public decimal? Salary { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "給料日")]
        public DateTime? Payday { get; set; }

        [Display(Name = "メモ")]
        public string Memo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cleaning> Cleanings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pickup> Pickups { get; set; }
    }
}
