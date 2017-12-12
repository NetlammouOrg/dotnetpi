namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("netlammou.comment")]
    public partial class comment
    {
        public int id { get; set; }

        public DateTime? date { get; set; }

        [Column("object")]
        [StringLength(255)]
        public string _object { get; set; }

        [Column(TypeName = "bit")]
        public bool reported { get; set; }

        public int? topic_id { get; set; }

        public virtual topic topic { get; set; }
    }
}
