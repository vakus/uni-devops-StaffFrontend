using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models
{
    public class Review
    {
        [Display(Name = "Review ID")]
        public int reviewId { set; get; }

        [Display(Name = "User ID")]
        public int userId { get; set; }

        [Display(Name = "Username")]
        public string userName { get; set; }

        [Display(Name = "Review")]
        public string reviewContent { get; set; }

        [Display(Name = "Rating")]
        public int reviewRating { get; set; }

        [Display(Name = "Hidden")]
        public bool hidden { get; set; }

        [Display(Name = "Product ID")]
        public int productId { get; set; }
    }
}
