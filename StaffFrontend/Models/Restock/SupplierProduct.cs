using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models.Restock
{
    public class SupplierProduct
    {
        public int id { get; set; }
        public string ean { get; set; }

        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public bool inStock { get; set; }
        public DateTime expectedRestock { get; set; }
    }
}
