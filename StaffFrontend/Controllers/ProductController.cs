using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Proxies;
using StaffFrontend.Models;
using Microsoft.AspNetCore.Authorization;
using StaffFrontend.Models.Product;

namespace StaffFrontend.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class ProductController : Controller
    {

        private readonly IProductProxy _product;
        private readonly IReviewProxy _review;

        public ProductController(IProductProxy productProxy, IReviewProxy reviewProxy)
        {
            _product = productProxy;
            _review = reviewProxy;
        }
        [HttpGet("/products")]
        // GET: /products
        public async Task<ActionResult> Index(string name, bool? visible, decimal? minprice, decimal? maxprice, string sortby)
        {
            List<Product> products;
            try
            {
                products = await _product.GetProducts(name, visible, minprice, maxprice);
            }
            catch (SystemException)
            {
                products = new List<Product>();
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }

            //sort items
            if (!String.IsNullOrEmpty(sortby))
            {
                if (sortby == "ID")
                {
                    return View(products.OrderBy(o => o.ID).ToList());
                }
                else if (sortby == "Name")
                {
                    return View(products.OrderBy(o => o.Name).ToList());
                }
                else if (sortby == "Price")
                {
                    return View(products.OrderBy(o => o.Price).ToList());
                }
                else if (sortby == "Stock Level")
                {
                    return View(products.OrderBy(o => o.Supply).ToList());
                }
            }
            return View(products);
        }

        [HttpGet("/products/view/{itemid}")]
        // GET: /products/view/5
        public async Task<ActionResult> Details(int itemid)
        {

            ProductDetailsDTO product = new ProductDetailsDTO();
            try
            {
                product.product = await _product.GetProduct(itemid);

                if (product.product == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                product.product = new Product();
            }

            try
            {
                product.reviews = await _review.GetReviews(itemid, null);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load review data from remote service.");
            }
            return View(product);
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
        public async Task<ActionResult> Create([Bind("Name,Description,Price,Supply,Available")] Product prod)
        {
            try
            {
                await _product.AddProduct(prod);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to send data to remote service. Please try again.");
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/products/edit/{itemid}")]
        // GET: /products/edit/5
        public async Task<ActionResult> Edit(int itemid)
        {
            Product prod;
            try
            {
                prod = await _product.GetProduct(itemid);

                if (prod == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                prod = new Product();
            }

            return View(prod);
        }

        // POST: /products/edit/5
        [HttpPost("/products/edit/{itemid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Name,Description,Price,Supply,Available")] Product prod)
        {
            prod.ID = id;
            try
            {
                await _product.UpdateProduct(prod);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to send data to remote service. Please try again.");
                return View(prod);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/products/delete/{itemid}")]
        // GET: products/delete/5
        public async Task<ActionResult> Delete(int itemid)
        {
            Product prod;
            try
            {
                prod = await _product.GetProduct(itemid);

                if (prod == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                prod = new Product();
            }

            return View(prod);
        }

        // POST: products/delete/5
        [HttpPost("/products/delete/{itemid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int itemid, IFormCollection collection)
        {
            try
            {
                await _product.DeleteProduct(itemid);
                await _review.DeleteByProductId(itemid);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                return View(new Product());
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
