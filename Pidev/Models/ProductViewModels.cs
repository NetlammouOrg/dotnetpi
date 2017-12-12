//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace Pidev.Models
//{
//    public class ProductViewModels
//    {
//        public int Id { get; set; }

//        [Required]
//        [StringLength(100, ErrorMessage = "The minimum length for the title is 3 characters.", MinimumLength = 3)]
//        [Display(Name = "Title")]
//        public string Name { get; set; }

//        [Required(ErrorMessage = "This field is required")]
//        public string Description { get; set; }

//        public string Image { get; set; }

//        public int Quantity { get; set; }

//        public string Categorie { get; set; }
//    }
//}