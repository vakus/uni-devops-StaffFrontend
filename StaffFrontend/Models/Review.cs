using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Review
    {
        public int id { set; get; }

        public string review { get; set; }

        public int rating { get; set; }

        public bool hidden { get; set; }

        public int itemid { get; set; }
    }
}
