using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models.Restock
{
    public class Restock
    {

        [Display(Name = "Restock ID")]
        public int Id { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        [Display(Name = "Quantity")]
        public int Qty { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product EAN")]
        public string ProductEan { get; set; }

        [Display(Name = "Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Supplier ID")]
        public int SupplierID { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }
    }
}