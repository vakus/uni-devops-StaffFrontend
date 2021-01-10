using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies.ProductProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.Test.Proxies
{
    [TestClass]
    public class ProductProxyLocalTest
    {

        private List<Product> products;
        private ProductProxyLocal ppl;


        [TestInitialize]
        public void initTest()
        {
            products = new List<Product>()
            {
                new Product(){ID=1, Name="Duck", Description="Rubber ducky", Price=99.99m, Available=true, Supply=20},
                new Product(){ID=2, Name="Raspberry PI", Description="very smoll pc", Price=20.00m, Available=true, Supply=4},
                new Product(){ID=3, Name="Arduino", Description="microcontroller", Price=40.00m, Available=false, Supply=0}
            };

            ppl = new ProductProxyLocal(products);
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts()
        {
            Assert.IsTrue(products.SequenceEqual(await ppl.GetProducts(null, null, null, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_Name()
        {
            List<Product> p = new List<Product>()
            {
                products.First()
            };
            Assert.IsTrue(p.SequenceEqual(await ppl.GetProducts("Duck", null, null, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_Visible()
        {

            List<Product> p = new List<Product>()
            {
                products.Last()
            };
            Assert.IsTrue(p.SequenceEqual(await ppl.GetProducts(null, false, null, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_MinPrice()
        {
            List<Product> p = new List<Product>()
            {
                products.First(),
                products.Last()
            };
            Assert.IsTrue(p.SequenceEqual(await ppl.GetProducts(null, null, 30, null)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_MaxPrice()
        {
            List<Product> p = products.GetRange(1, 2);
            Assert.IsTrue(p.SequenceEqual(await ppl.GetProducts(null, null, null, 50)));
        }

        [TestMethod]
        public async Task ProductProxy_GetProduct()
        {
            Assert.IsTrue((await ppl.GetProduct(1)).Equals(products.Find(p => p.ID == 1)));
            Assert.IsTrue((await ppl.GetProduct(2)).Equals(products.Find(p => p.ID == 2)));
            Assert.IsTrue((await ppl.GetProduct(3)).Equals(products.Find(p => p.ID == 3)));
            Assert.IsNull(await ppl.GetProduct(5));
            Assert.IsNull(await ppl.GetProduct(-2));
        }

        [TestMethod]
        public async Task ProductProxy_AddProduct()
        {
            Product prod = new Product()
            {
                ID = 4,
                Name = "OnePlus One",
                Description = "First phone from OnePlus",
                Available = false,
                Price = 999.99m,
                Supply = 13
            };
            await ppl.AddProduct(prod);
            Assert.IsTrue((await ppl.GetProduct(4)).Equals(prod));
        }

        [TestMethod]
        public async Task ProductProxy_UpdateProduct()
        {
            Product p = products.First();
            p.Price = 4.99m;
            await ppl.UpdateProduct(p);
            Assert.IsTrue((await ppl.GetProduct(1)).Equals(p));
        }

        [TestMethod]
        public async Task ProductProxy_DeleteProduct()
        {
            await ppl.DeleteProduct(1);
            Assert.IsNull(await ppl.GetProduct(1));
        }
    }
}