using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies.ProductProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Products
{
    [TestClass]
    public class GetProducts
    {
        private List<Product> products;
        private IProductProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            products = new List<Product>() {
                new Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99m, Available = false, Supply = 2 },
                new Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99m, Available = true, Supply = 20 },
                new Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99m, Available = true, Supply = 13 }
            };

            cpl = new ProductProxyLocal(products);
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts()
        {
            Assert.IsTrue(products.SequenceEqual(await cpl.GetProducts(null, null, null, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_Name()
        {
            List<string> keywords = new List<string>() { "Duck", "Lorem", "IPhone" };
            foreach(string key in keywords)
            {
                List<Product> p = new List<Product>();
                foreach (Product product in products)
                {
                    if (product.Name.Contains(key))
                    {
                        p.Add(product);
                    }
                }
                Assert.IsTrue(p.SequenceEqual(await cpl.GetProducts(key, null, null, null)));
            }
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_Visible()
        {
            List<Product> p = new List<Product>();
            foreach (Product product in products)
            {
                if (!product.Available)
                {
                    p.Add(product);
                }
            }
            Assert.IsTrue(p.SequenceEqual(await cpl.GetProducts(null, false, null, null)));


            p = new List<Product>();
            foreach (Product product in products)
            {
                if (product.Available)
                {
                    p.Add(product);
                }
            }
            Assert.IsTrue(p.SequenceEqual(await cpl.GetProducts(null, true, null, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_MinPrice()
        {
            List<Product> p = new List<Product>();
            foreach (Product product in products)
            {
                if (product.Price > 30.00m)
                {
                    p.Add(product);
                }
            }
            Assert.IsTrue(p.SequenceEqual(await cpl.GetProducts(null, null, 30, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_MaxPrice()
        {
            List<Product> p = new List<Product>();
            foreach (Product product in products)
            {
                if (product.Price < 50.00m)
                {
                    p.Add(product);
                }
            }
            Assert.IsTrue(p.SequenceEqual(await cpl.GetProducts(null, null, null, 50)));
        }
    }
}
