using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models.Product
{
    public class ProductDetailsDTO
    {

        public Product product { get; set; }

        public List<Review> reviews { get; set; }
    }
}
