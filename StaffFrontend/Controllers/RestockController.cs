using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaffFrontend.Models.Restock;
using StaffFrontend.Proxies.RestockProxy;

namespace StaffFrontend.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class RestockController : Controller
    {


        private readonly IRestockProxy restockProxy;
        public RestockController(IRestockProxy restockProxy)
        {
            this.restockProxy = restockProxy;
        }
        // GET: RestockController
        [HttpGet("/restock/")]
        public async Task<IActionResult> Index()
        {
            return View(await restockProxy.GetSuppliers());
        }

        [HttpGet("/restock/orders")]
        public async Task<IActionResult> ViewOrders([FromQuery] string accountName, [FromQuery] int? supplierid, [FromQuery] bool? approved)
        {
            return View(await restockProxy.GetRestocks(null, accountName, supplierid, approved));
        }

        [Authorize(Policy = "ManagerOnly")]
        [HttpGet("/restock/process/{id}")]
        public async Task<IActionResult> Process(int id)
        {
            return View((await restockProxy.GetRestocks(id, null, null, null))[0]);
        }

        [Authorize(Policy = "ManagerOnly")]
        [HttpPost("/restock/process/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(int id, string cardNumber, [FromQuery] bool approved)
        {
            return RedirectPermanent("/restock/orders");
        }

        [HttpGet("/restock/create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            RestockCreateDTO rc = new RestockCreateDTO();
            rc.products = new SelectList(await restockProxy.GetSuppliersProducts(id), "id", "name");
            rc.restock = new Restock();
            return View(rc);
        }

        [HttpPost("/restock/create/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string accountname, int productid, int quantity)
        {
            return RedirectPermanent("/restock/orders");
        }
    }
}