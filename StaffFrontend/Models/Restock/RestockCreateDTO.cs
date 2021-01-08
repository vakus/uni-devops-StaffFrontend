using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Models.Restock
{
    public class RestockCreateDTO
    {
        public Restock restock { get; set; }

        public SelectList products { get; set; }
    }
}
