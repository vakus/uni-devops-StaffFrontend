using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies.ProductProxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Products
{
    [TestClass]
    public class AddProduct
    {
        private IProductProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new ProductProxyLocal(TestData.GetProducts());
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
            await cpl.AddProduct(prod);
            Assert.IsTrue((await cpl.GetProduct(4)).Equals(prod));
        }
    }
}
