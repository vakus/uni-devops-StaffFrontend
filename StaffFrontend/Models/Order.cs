using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Order
    {

        public int id { get; set; }

        public List<int> items { get; set; }

        public int customerid { get; set; }

        public DateTime orderTime { get; set; }
    }
}
