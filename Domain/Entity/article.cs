namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    [Table("netlammou.article")]
    public partial class article
    {
        public int id { get; set; }

        [StringLength(255)]
        public string body { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [Column(TypeName = "bit")]
        public bool favourite { get; set; }

        [StringLength(255)]
        public string image { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string other { get; set; }

        public int? user_id { get; set; }

        public virtual user user { get; set; }
    }
}
