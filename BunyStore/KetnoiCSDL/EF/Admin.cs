namespace KetnoiCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        public bool? Hide { get; set; }

        public int? Location { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreateOrUpdate { get; set; }
    }
}
