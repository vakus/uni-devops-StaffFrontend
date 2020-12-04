using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using StaffFrontend.Models.Forms;
using StaffFrontend.Proxies.AuthorizationProxy;
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

        private IAuthorizationProxy authProxy { get; }
        public AccountController(IAuthorizationProxy authProxy)
        {
            this.authProxy = authProxy;
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
        public async Task<IActionResult> Login([FromForm] LoginForm login, [FromQuery] string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AuthorizationLoginResult loginResult = await authProxy.Login(login.Email, login.Password);
                    await HttpContext.SignInAsync("Cookies", loginResult.claimsPrincipal, loginResult.authProperties);

                    return LocalRedirect(returnUrl ?? "/products");
                }
                catch (SystemException)
                {
                    ModelState.AddModelError("", "Invalid Username or Password.");
                    return View(login);
                }
            }
            return View(login);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Passwd()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Passwd([FromForm] PasswordChangeForm passwd)
        {
            return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    List<string> claims = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                    await authProxy.UpdatePassword(User, passwd.Password, claims);
                }
                catch (SystemException)
                {
                    ModelState.AddModelError("", "Unable to update password.");
                    return View();
                }
            }
            return LocalRedirect("/products");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl = null)
        {
            await HttpContext.SignOutAsync("Cookies");
            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
