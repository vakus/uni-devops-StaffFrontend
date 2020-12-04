using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.AuthorizationProxy
{
    public class AuthorizationLoginResult
    {

        public ClaimsPrincipal claimsPrincipal { get; set; }

        public AuthenticationProperties authProperties { get; set; }

    }
}