namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("netlammou.localisation")]
    public partial class localisation
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string altitude { get; set; }

        [Required]
        [StringLength(100)]
        public string longitude { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        public int? action_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        public virtual action action { get; set; }

        public virtual user user { get; set; }
    }
}
