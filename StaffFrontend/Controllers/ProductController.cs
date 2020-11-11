using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Data;
using StaffFrontend.Models;

namespace StaffFrontend.Controllers
{
    public class ProductController : Controller
    {

        private IProductProxy _product;

        public ProductController(IProductProxy productProxy)
        {
            _product = productProxy;
        }
        [HttpGet("/products")]
        // GET: /products
        public async Task<ActionResult> Index(string? name, bool? visible, double? minprice, double? maxprice)
        {
            return View(await _product.GetProducts(name, visible, minprice, maxprice));
        }

        [HttpGet("/products/view/{itemid}")]
        // GET: /products/view/5
        public async Task<ActionResult> Details(int itemid)
        {
            Product prod = await _product.GetProduct(itemid);

            if (prod == null)
            {
                return NotFound();
            }

            return View(prod);
        }

        [HttpGet("/products/new")]
        // GET: /products/new
        public ActionResult Create()
        {
            return View();
        }


        // POST: /products/new
        [HttpPost("/products/new")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,description,price")] Product prod)
        {
            await _product.AddProduct(prod);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/products/edit/{itemid}")]
        // GET: /products/edit/5
        public async Task<ActionResult> Edit(int itemid)
        {
            Product prod = await _product.GetProduct(itemid);

            if (prod == null)
            {
                return NotFound();
            }

            return View(prod);
        }

        // POST: /products/edit/5
        [HttpPost("/products/edit/{itemid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("id,name,description,price")] Product prod)
        {
            await _product.UpdateProduct(prod);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/products/delete/{itemid}")]
        // GET: products/delete/5
        public async Task<ActionResult> Delete(int itemid)
        {
            return View(await _product.GetProduct(itemid));
        }

        // POST: products/delete/5
        [HttpPost("/products/delete/{itemid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int itemid, IFormCollection collection)
        {
            await _product.DeleteProduct(itemid);
            return RedirectToAction(nameof(Index));
        }
    }
}
