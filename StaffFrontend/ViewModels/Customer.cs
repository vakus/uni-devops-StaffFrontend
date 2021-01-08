using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.ViewModels
{
    public class Customer
    {
        [Display(Name="Customer Id")]
        public int id { get; set; }

        public string firstname { get; set; }

        public string surname { get; set; }

        public string address { get; set; }

        public string contact { get; set; }

        public bool canPurchase { get; set; }

        public bool isDeleted { get; set; }
    }
}
