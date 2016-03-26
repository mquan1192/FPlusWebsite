namespace fpluswebsite2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cleaning")]
    public partial class Cleaning
    {
        public long Id { get; set; }

        [Display(Name = "スタッフ")]
        public int StaffId { get; set; }

        [Display(Name = "ホテル")]
        public int HotelId { get; set; }

        [Display(Name = "日付")]
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Display(Name = "到着")]
        public TimeSpan? TimeIn { get; set; }

        [Display(Name = "出発")]
        public TimeSpan? TimeOut { get; set; }

        [Display(Name = "経費")]
        [Column(TypeName = "money")]
        public decimal? Fee { get; set; }

        [Display(Name = "メモ")]
        public string Memo { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime dateStart { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime dateStop { get; set; }
        public virtual Hotel Hotel { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
