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
            List<Supplier> suppliers = new List<Supplier>();
            try
            {
                suppliers = await restockProxy.GetSuppliers();
            }
            catch(SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            return View(suppliers);
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
        public async Task<IActionResult> Process(int id, string accountName, string cardNumber, bool approved)
        {
            if (approved)
            {
                await restockProxy.ApproveRestock(id, accountName, cardNumber);
            }
            else
            {
                await restockProxy.RejectRestock(id);
            }
            return RedirectPermanent("/restock");
        }

        [HttpGet("/restock/create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            RestockCreateDTO rc = new RestockCreateDTO();
            try
            {
                rc.products = new SelectList(await restockProxy.GetSuppliersProducts(id), "id", "name");
            }
            catch (SystemException)
            {
                rc.products = new SelectList(new List<SupplierProduct>(), "id", "name");
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            rc.restock = new Restock();
            return View(rc);
        }

        [HttpPost("/restock/create/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int id,
            [FromForm(Name ="restock.AccountName")] string AccountName,
            [FromForm(Name = "restock.ProductID")] int ProductID,
            [FromForm(Name = "restock.Gty")] int Qty)
        {
            await restockProxy.CreateRestock(id, AccountName, ProductID, Qty);
            return RedirectPermanent("/restock/orders?supplierid=" + id.ToString());
        }
    }
}