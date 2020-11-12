using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Review
    {
        public int reviewid { set; get; }

        public string content { get; set; }

        public int rating { get; set; }

        public bool hidden { get; set; }

        public int itemid { get; set; }

        public DateTime createTime { get; set; }
    }
}
