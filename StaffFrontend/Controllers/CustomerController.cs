using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using StaffFrontend.Proxies;

namespace StaffFrontend.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerProxy _customer;

        public CustomerController(ICustomerProxy _proxy)
        {
            _customer = _proxy;
        }

        [HttpGet("/customers")]
        // GET: /customers
        public async Task<ActionResult> Index(bool? excludeDeleted)
        {
            return View(await _customer.GetCustomers(excludeDeleted.GetValueOrDefault(true)));
        }

        [HttpGet("/customers/view/{userid}")]
        // GET: /customers/view/5
        public async Task<ActionResult> Details(int userid)
        {
            Customer cust = await _customer.GetCustomer(userid);

            if(cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }

        [HttpGet("/customers/edit/{userid}")]
        // GET: /customers/edit/5
        public async Task<ActionResult> Edit(int userid)
        {
            return View(await _customer.GetCustomer(userid));
        }

        [HttpPost("/customers/edit/{userid}")]
        // POST: /customers/edit/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("id,firstname,surname,address,contact,canPurchase")] Customer cust)
        {
            await _customer.UpdateCustomer(cust);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/customers/delete/{userid}")]
        // GET: /customers/delete/5
        public async Task<ActionResult> Delete(int userid)
        {
            return View(await _customer.GetCustomer(userid));
        }

        [HttpPost("/customers/delete/{userid}")]
        // POST: /customers/delete/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int userid, IFormCollection collection)
        {
            await _customer.DeleteCustomer(userid);
            return RedirectToAction(nameof(Index));
        }
    }
}
