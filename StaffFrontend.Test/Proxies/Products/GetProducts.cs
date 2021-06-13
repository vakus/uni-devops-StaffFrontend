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
        private IProductProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new ProductProxyLocal(TestData.GetProducts());
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts()
        {
            var model = await cpl.GetProducts(null, null, null, null);
            for (int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(TestData.GetProducts()[x].ID, model[x].ID);
                Assert.AreEqual(TestData.GetProducts()[x].Name, model[x].Name);
                Assert.AreEqual(TestData.GetProducts()[x].Description, model[x].Description);
                Assert.AreEqual(TestData.GetProducts()[x].Supply, model[x].Supply);
                Assert.AreEqual(TestData.GetProducts()[x].Price, model[x].Price);
                Assert.AreEqual(TestData.GetProducts()[x].Available, model[x].Available);
            }
        }

        //[Ignore]
        [TestMethod]
        public async Task ProductProxy_GetProducts_Name()
        {
            List<string> keywords = new List<string>() { "Duck", "Lorem", "IPhone" };
            foreach(string key in keywords)
            {
                List<Product> p = new List<Product>();
                foreach (Product product in TestData.GetProducts())
                {
                    if (product.Name.Contains(key))
                    {
                        p.Add(product);
                    }
                }
                var model = await cpl.GetProducts(key, null, null, null);
                var testData = TestData.GetProducts().Where(s => s.Name.Contains(key)).ToList();
                for (int x = 0; x < model.Count; x++)
                {
                    Assert.AreEqual(testData[x].ID, model[x].ID);
                    Assert.AreEqual(testData[x].Name, model[x].Name);
                    Assert.AreEqual(testData[x].Description, model[x].Description);
                    Assert.AreEqual(testData[x].Supply, model[x].Supply);
                    Assert.AreEqual(testData[x].Price, model[x].Price);
                    Assert.AreEqual(testData[x].Available, model[x].Available);
                }
            }
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_Visible()
        {
            List<Product> p = new List<Product>();
            foreach (Product product in TestData.GetProducts())
            {
                if (!product.Available)
                {
                    p.Add(product);
                }
            }
            var model = await cpl.GetProducts(null, false, null, null);
            var testData = TestData.GetProducts().Where(s => !s.Available).ToList();
            for (int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(testData[x].ID, model[x].ID);
                Assert.AreEqual(testData[x].Name, model[x].Name);
                Assert.AreEqual(testData[x].Description, model[x].Description);
                Assert.AreEqual(testData[x].Supply, model[x].Supply);
                Assert.AreEqual(testData[x].Price, model[x].Price);
                Assert.AreEqual(testData[x].Available, model[x].Available);
            }


            p = new List<Product>();
            foreach (Product product in TestData.GetProducts())
            {
                if (product.Available)
                {
                    p.Add(product);
                }
            }


            model = await cpl.GetProducts(null, true, null, null);
            testData = TestData.GetProducts().Where(s => s.Available).ToList();
            for (int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(testData[x].ID, model[x].ID);
                Assert.AreEqual(testData[x].Name, model[x].Name);
                Assert.AreEqual(testData[x].Description, model[x].Description);
                Assert.AreEqual(testData[x].Supply, model[x].Supply);
                Assert.AreEqual(testData[x].Price, model[x].Price);
                Assert.AreEqual(testData[x].Available, model[x].Available);
            }
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_MinPrice()
        {
            List<Product> p = new List<Product>();
            foreach (Product product in TestData.GetProducts())
            {
                if (product.Price > 30.00m)
                {
                    p.Add(product);
                }
            }
            var model = await cpl.GetProducts(null, null, 30.00m, null);
            var testData = TestData.GetProducts().Where(s => s.Price > 30.00m).ToList();
            for (int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(testData[x].ID, model[x].ID);
                Assert.AreEqual(testData[x].Name, model[x].Name);
                Assert.AreEqual(testData[x].Description, model[x].Description);
                Assert.AreEqual(testData[x].Supply, model[x].Supply);
                Assert.AreEqual(testData[x].Price, model[x].Price);
                Assert.AreEqual(testData[x].Available, model[x].Available);
            }
        }

        [TestMethod]
        public async Task ProductProxy_GetProducts_MaxPrice()
        {
            List<Product> p = new List<Product>();
            foreach (Product product in TestData.GetProducts())
            {
                if (product.Price < 50.00m)
                {
                    p.Add(product);
                }
            }
            var model = await cpl.GetProducts(null, true, null, 50.00m);
            var testData = TestData.GetProducts().Where(s => s.Price < 50.00m).ToList();
            for (int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(testData[x].ID, model[x].ID);
                Assert.AreEqual(testData[x].Name, model[x].Name);
                Assert.AreEqual(testData[x].Description, model[x].Description);
                Assert.AreEqual(testData[x].Supply, model[x].Supply);
                Assert.AreEqual(testData[x].Price, model[x].Price);
                Assert.AreEqual(testData[x].Available, model[x].Available);
            }
        }
    }
}