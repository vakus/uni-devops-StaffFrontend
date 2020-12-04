using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.AuthorizationProxy.Models
{
    public class UserUpdateForm
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public IList<string> Roles { get; set; }
    }
}
