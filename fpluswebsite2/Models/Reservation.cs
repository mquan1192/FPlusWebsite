namespace fpluswebsite2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reservation")]
    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            Pickups = new HashSet<Pickup>();
            //TimeIn = DateTime.Now;
            //TimeOut = DateTime.Now;
        }

        public long Id { get; set; }

        public int? HotelId { get; set; }

        [StringLength(50)]
        [Display(Name = "ゲスト名前")]
        public string GuestName { get; set; }

        [Display(Name = "人数")]
        public byte? NumberOfGuest { get; set; }

        [Display(Name = "ゲスト情報")]
        public string GuestInfo { get; set; }

        [Display(Name = "到着日")]
        [DataType(DataType.Date)]
        public DateTime? Checkin { get; set; }

        //[DataType(DataType.Time)]
        [Display(Name = "到着時間")]
        public string TimeIn { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "出発日")]
        public DateTime? Checkout { get; set; }
        //[DataType(DataType.Time)]
        [Display(Name = "出発時間")]
        public string TimeOut { get; set; }

        [Display(Name = "日数")]
        public byte? DayStay { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "予約金額")]
        public decimal? Deposit { get; set; }


        [Display(Name = "メモ")]
        public string Memo { get; set; }

        public virtual Hotel Hotel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pickup> Pickups { get; set; }

    }
}
