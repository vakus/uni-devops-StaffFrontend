using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Restock
    {
        public string restockId { get; set; }

        public string supplierId { get; set; }

        public string sProductId { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }

        public DateTime date { get; set; }

        public bool approved { get; set; }
    }
}
