namespace Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("netlammou.basket")]
    public partial class basket
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idProduct { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idVolenteer { get; set; }

        public int quantity { get; set; }

        public double totalPrice { get; set; }

        [Column(TypeName = "bit")]
        public bool validCommande { get; set; }

        public virtual user user { get; set; }

        public virtual product product { get; set; }
    }
}
