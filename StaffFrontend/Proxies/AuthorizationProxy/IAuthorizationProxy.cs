using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.AuthorizationProxy
{
    public interface IAuthorizationProxy
    {

        public Task<AuthorizationLoginResult> Login(string username, string password);

        public Task UpdatePassword(ClaimsPrincipal User, string password, IList<string> roles);
    }
}