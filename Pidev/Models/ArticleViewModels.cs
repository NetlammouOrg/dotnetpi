using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pidev.Models
{
    public class ArticleViewModels
    {
        public int Id { get; set; }
        [Required]
        //[StringLength(100, ErrorMessage = "The minimum length for the title is 3 characters.", MinimumLength = 3)]
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Required ( ErrorMessage = "This field is required")]
        public string Body { get; set; }
        public DateTime Date { get; set; }  
             
        public string Image { get; set; }

     }
}