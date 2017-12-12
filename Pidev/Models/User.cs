using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pidev.Models
{
    public class User
    {

        public int id { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string adress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string mailAdress { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int phoneNumber { get; set; }
        public string picture { get; set; }
    }
}