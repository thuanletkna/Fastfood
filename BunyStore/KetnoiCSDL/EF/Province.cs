namespace KetnoiCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Province")]
    public partial class Province
    {
        public Province()
        {
            //Customer = new HashSet<User>();
            //Producer = new HashSet<Producer>();
            Store = new HashSet<Store>();
        }
        public int id { get; set; }

        [StringLength(50)]
        public string nameProvince { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        public bool? Hide { get; set; }

        public int? Location { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateCreateOrUpdate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       /// public virtual ICollection<User> Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // public virtual ICollection<Producer> Producer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Store> Store { get; set; }
    }
}
