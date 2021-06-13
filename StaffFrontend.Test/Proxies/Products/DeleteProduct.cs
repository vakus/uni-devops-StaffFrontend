using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies.ProductProxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Products
{
    [TestClass]
    public class DeleteProduct
    {
        private IProductProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new ProductProxyLocal(TestData.GetProducts());
        }
        [TestMethod]
        public async Task ProductProxy_DeleteProduct()
        {
            await cpl.DeleteProduct(1);
            Assert.IsNull(await cpl.GetProduct(1));
        }
    }
}
