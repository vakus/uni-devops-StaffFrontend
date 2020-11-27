using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.AuthorizationProxy
{
    public class AuthorizationProxyLocal : IAuthorizationProxy
    {
        public Task<AuthorizationLoginResult> Login(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
