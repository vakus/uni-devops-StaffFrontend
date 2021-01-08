using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models.Customers
{
    public class CustomerDetailsDTO
    {
        public Customer Customer { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
