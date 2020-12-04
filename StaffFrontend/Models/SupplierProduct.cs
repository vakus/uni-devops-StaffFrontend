using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class SupplierProduct
    {
        public string SupplierId { get; set; }

        public string ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPricePounds { get; set; }
    }
}
