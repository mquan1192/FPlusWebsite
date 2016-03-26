namespace fpluswebsite2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hotel")]
    public partial class Hotel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hotel()
        {
            Cleanings = new HashSet<Cleaning>();
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "ホテル")]
        public string Name { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "住所")]
        public string Address { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "値段")]
        public decimal? Price { get; set; }

        [StringLength(50)]
        [Display(Name = "ポスト")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "顧客")]
        public string Owner { get; set; }

        [StringLength(17)]
        [Display(Name = "顧客電話番号")]
        public string OwnerPhone { get; set; }

        [StringLength(70)]
        [Display(Name = "顧客住所")]
        public string OwnerAdrress { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "支払日")]
        public DateTime? OwnerPayday { get; set; }

        [Display(Name = "メモ")]
        public string Memo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cleaning> Cleanings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
