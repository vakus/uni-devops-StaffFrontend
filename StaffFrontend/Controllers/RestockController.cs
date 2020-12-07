using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using StaffFrontend.Proxies.ResupplyProxy;

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
        [HttpGet("/restock")]
        // GET: RestockController
        public async Task<ActionResult> Index()
        {
            return View(await restockProxy.GetRestocks());
        }

        [HttpGet("/restock/view/{id}")]
        // GET: RestockController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await restockProxy.GetRestock(id));
        }

        [HttpGet("/restock/new")]
        // GET: RestockController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestockController/Create
        [HttpPost("/restock/new")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] Restock restock)
        {
            try
            {
                await restockProxy.CreateRestock(restock);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("/restock/edit/{id}")]
        // GET: RestockController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await restockProxy.GetRestock(id));
        }

        // POST: RestockController/Edit/5
        [HttpPost("/restock/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromForm] Restock restock)
        {
            try
            {
                restock.restockId = id.ToString();
                await restockProxy.UpdateRestock(restock);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("/restock/delete/{id}")]
        // GET: RestockController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await restockProxy.GetRestock(id));
        }

        // POST: RestockController/Delete/5
        [HttpPost("/restock/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await restockProxy.DeleteRestock(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
