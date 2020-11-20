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
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int Supply { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public double Price { get; set; }

        public bool Available { get; set; }
    }
}
