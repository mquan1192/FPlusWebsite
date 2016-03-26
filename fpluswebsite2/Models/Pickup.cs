namespace fpluswebsite2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pickup")]
    public partial class Pickup
    {
        public long Id { get; set; }

        public long? ReservationId { get; set; }

        [Display(Name = "時間")]
        public DateTime? Time { get; set; }

        public int? StaffId { get; set; }

        [StringLength(70)]
        [Display(Name = "場所")]
        public string Place { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
