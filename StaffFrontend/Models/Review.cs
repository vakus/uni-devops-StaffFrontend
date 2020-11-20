using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Review
    {
        public int reviewId { set; get; }

        public int userId { get; set; }

        public string userName { get; set; }

        public string reviewContent { get; set; }

        public int reviewRating { get; set; }

        public bool hidden { get; set; }

        public int productId { get; set; }

        public DateTime createTime { get; set; }
    }
}
