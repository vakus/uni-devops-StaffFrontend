using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using StaffFrontend.Proxies;

namespace StaffFrontend.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class SuppliersController : Controller
    {

        private readonly ISupplierProxy _supplierProxy;

        public SuppliersController(ISupplierProxy supplierProxy)
        {
            _supplierProxy = supplierProxy;
        }

        // GET: SuppliersController
        [HttpGet("/suppliers")]
        public ActionResult Index()
        {
            return View(_supplierProxy.GetSuppliers());
        }

        [HttpGet("/suppliers/{id}")]
        // GET: SuppliersController/Details/5
        public ActionResult Details(int id)
        {
            return View(_supplierProxy.GetSupplier(id));
        }

        [HttpGet("/suppliers/new")]
        // GET: SuppliersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuppliersController/Create
        [HttpPost("/suppliers/new")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("/suppliers/edit/{id}")]
        // GET: SuppliersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_supplierProxy.GetSupplier(id));
        }

        // POST: SuppliersController/Edit/5
        [HttpPost("/suppliers/edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromQuery] int id, [FromForm] Supplier supplier)
        {
            try
            {
                supplier.SupplierId = id;
                _supplierProxy.UpdateSupplier(supplier);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("/suppliers/delete/{id}")]
        // GET: SuppliersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_supplierProxy.GetSupplier(id));
        }

        // POST: SuppliersController/Delete/5
        [HttpPost("/suppliers/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await _supplierProxy.DeleteSupplier(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete supplier. Try again later.");
                return View();
            }
        }
    }
}
