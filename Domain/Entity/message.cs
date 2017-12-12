namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("netlammou.message")]
    public partial class message
    {
        public int id { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        public DateTime? date { get; set; }

        [Column(TypeName = "bit")]
        public bool deleted { get; set; }

        [Column("object")]
        [StringLength(255)]
        public string _object { get; set; }

        public int? user_id { get; set; }

        public virtual user user { get; set; }
    }
}
