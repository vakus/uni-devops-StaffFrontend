using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Product
    {
        [Display(Name = "Product ID")]
        public int id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public double price { get; set; }

        public bool visible { get; set; }
    }
}
