using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Supplier
    {
        [Display(Name = "Supplier ID")]
        public int SupplierId { get; set; }

        [Display(Name = "Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Postal Address")]
        public string SupplierAddress { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string SupplierEmail { get; set; }

        [Display(Name = "Phone")]
        [Phone]
        public string SupplierContactNumber { get; set; }
    }
}
