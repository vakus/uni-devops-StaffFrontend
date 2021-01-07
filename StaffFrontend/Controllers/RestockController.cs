using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Proxies.RestockProxy;

namespace StaffFrontend.Controllers
{
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
            return View(await restockProxy.GetRestocks(accountName, supplierid, approved));
        }
    }
}