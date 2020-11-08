using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Supplier
    {
        public int id { get; set; }

        public string name { get; set; }

        public List<int> items { get; set; }
    }
}
