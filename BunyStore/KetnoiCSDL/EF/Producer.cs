namespace KetnoiCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producer")]
    public partial class Producer
    {
        public int id { get; set; }

        //public int? id_Province { get; set; }

        [StringLength(80)]
        public string ProducerName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        public bool? Hide { get; set; }

        public int? Location { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreateOrUpdate { get; set; }
        //public virtual Province Province { get; set; }
    }
}
