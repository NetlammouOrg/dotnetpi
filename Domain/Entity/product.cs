namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("netlammou.product")]
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            basket = new HashSet<basket>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string category { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string image { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public double? price { get; set; }

        public int quantity { get; set; }

        public int? ngo_id { get; set; }

        [StringLength(255)]
        public string picture { get; set; }

        [StringLength(255)]
        public string Categorie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<basket> basket { get; set; }

        public virtual user user { get; set; }
    }
}
