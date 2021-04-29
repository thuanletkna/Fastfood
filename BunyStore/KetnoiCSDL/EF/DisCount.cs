namespace KetnoiCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DisCount")]
    public partial class DisCount
    {
        public int id { get; set; }

        [StringLength(80)]
        public string Title { get; set; }

        [StringLength(500)]
        public string DiscountDetail { get; set; }

        [Column(TypeName = "text")]
        public string Img { get; set; }

        public DateTime? DayStart { get; set; }

        public DateTime? DayEnd { get; set; }

        [Column("Discount")]
        public int? Discount1 { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        public bool? Hide { get; set; }

        public int? Location { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreateOrUpdate { get; set; }
    }
}
