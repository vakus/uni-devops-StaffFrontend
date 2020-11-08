using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Restock
    {
        public int id { get; set; }

        public bool confirmed { get; set; }

        public DateTime orderTime { get; set; }
    }
}
