using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaffFrontend.Controllers
{
    public class AccountController : Controller
    {

        private IHttpClientFactory _client { get; }
        public AccountController(IHttpClientFactory client)
        {
            this._client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel login, [FromQuery] string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var client = _client.CreateClient();

                var discoveryServer = await client.GetDiscoveryDocumentAsync("https://localhost:44373/");
                var token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = discoveryServer.TokenEndpoint,
                    ClientId = "staff_frontend",
                    ClientSecret = "steff_frontand_vrey_secret_hopefuly_nobody_figuras_this_crep_out_or_at_laest_its_long_enough",
                    UserName = login.Email,
                    Password = login.Password
                });

                if (token.IsError)
                {
                    ModelState.AddModelError("", "Invalid credentials.");
                    return View(login);
                }

                var userInfo = await client.GetUserInfoAsync(new UserInfoRequest
                {
                    Address = discoveryServer.UserInfoEndpoint,
                    Token = token.AccessToken
                });

                if (userInfo.IsError)
                {
                    ModelState.AddModelError("", "Invalid credentials.");
                    return View(login);
                }

                var claimsIdentity = new ClaimsIdentity(userInfo.Claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var tokensToStore = new AuthenticationToken[]
                {
                    new AuthenticationToken{Name = "access_token", Value=token.AccessToken}
                };

                var authProperties = new AuthenticationProperties();
                authProperties.StoreTokens(tokensToStore);

                await HttpContext.SignInAsync("Cookies", claimsPrincipal, authProperties);

                return LocalRedirect(returnUrl ?? "/");
            }
            return View(login);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl = null)
        {
            await HttpContext.SignOutAsync("Cookies");
            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
