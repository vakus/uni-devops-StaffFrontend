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

        private IProductProxy _productProxy;

        public ProductController(IProductProxy productProxy)
        {
            _productProxy = productProxy;
        }

        [HttpGet("/products")]
        // GET: /products
        public ActionResult Index()
        {
            return View(_productProxy.GetProducts());
        }

        [HttpGet("/products/view/{itemid}")]
        // GET: /products/view/5
        public ActionResult Details(int itemid)
        {
            Product prod = _productProxy.GetProduct(itemid);

            if (prod == null)
            {
                return NotFound();
            }

            return View(_productProxy.GetProduct(itemid));
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
        public ActionResult Create([Bind("name,description,price")] Product prod)
        {
            _productProxy.AddProduct(prod);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/products/edit/{itemid}")]
        // GET: /products/edit/5
        public ActionResult Edit(int itemid)
        {
            Product prod = _productProxy.GetProduct(itemid);

            if (prod == null)
            {
                return NotFound();
            }

            return View(prod);
        }

        // POST: /products/edit/5
        [HttpPost("/products/edit/{itemid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("id,name,description,price")] Product prod)
        {
            _productProxy.UpdateProduct(prod);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/products/delete/{itemid}")]
        // GET: products/delete/5
        public ActionResult Delete(int itemid)
        {
            return View(_productProxy.GetProduct(itemid));
        }

        // POST: products/delete/5
        [HttpPost("/products/delete/{itemid}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int itemid, IFormCollection collection)
        {
            _productProxy.DeleteProduct(itemid);
            return RedirectToAction(nameof(Index));
        }
    }
}
