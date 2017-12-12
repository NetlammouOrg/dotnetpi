using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pidev.Models
{
    public class EventModel
    {


        public int id { get; set; }

      
        public bool actif { get; set; }

        public DateTime? date { get; set; }

      
        public string description { get; set; }

        public int desiredVolenteers { get; set; }

       
        public string name { get; set; }

       
        public string type { get; set; }

       
    }
}
