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
        public async Task CustomerProxy_GetCustomers()
        {
            //check data
            Assert.IsTrue(products.SequenceEqual(await cpl.GetProducts(null, null, null, null)));
        }
    }
}
