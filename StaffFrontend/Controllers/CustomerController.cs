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
            List<Customer> customers;
            try
            {
                customers = await _customer.GetCustomers(excludeDeleted.GetValueOrDefault(true));
            }
            catch (SystemException)
            {
                customers = new List<Customer>();
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            return View(customers);
        }

        [HttpGet("/customers/view/{userid}")]
        // GET: /customers/view/5
        public async Task<ActionResult> Details(int userid)
        {
            Customer cust;
            try
            {
                cust = await _customer.GetCustomer(userid);

                if (cust == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                cust = new Customer();
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            return View(cust);
        }

        [HttpGet("/customers/edit/{userid}")]
        // GET: /customers/edit/5
        public async Task<ActionResult> Edit(int userid)
        {
            Customer cust;
            try
            {
                cust = await _customer.GetCustomer(userid);

                if (cust == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                cust = new Customer();
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            return View(cust);
        }

        [HttpPost("/customers/edit/{userid}")]
        // POST: /customers/edit/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("id,firstname,surname,address,contact,canPurchase")] Customer cust)
        {
            try
            {
                await _customer.UpdateCustomer(cust);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to send data to remote service. Please try again");
                return View(cust);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/customers/delete/{userid}")]
        // GET: /customers/delete/5
        public async Task<ActionResult> Delete(int userid)
        {
            Customer cust;
            try
            {
                cust = await _customer.GetCustomer(userid);

                if (cust == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                cust = new Customer();
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            return View(cust);
        }

        [HttpPost("/customers/delete/{userid}")]
        // POST: /customers/delete/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int userid, IFormCollection collection)
        {
            try
            {
                await _customer.DeleteCustomer(userid);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                return View(new Customer());
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
