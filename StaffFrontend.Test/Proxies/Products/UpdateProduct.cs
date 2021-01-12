using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies.ProductProxy;
using System.Linq;
using System.Threading.Tasks;


namespace StaffFrontend.test.Proxies.Products
{
    [TestClass]
    public class UpdateProduct
    {
        private ProductProxyLocal cpl;


        [TestInitialize]
        public void initTest()
        {

            cpl = new ProductProxyLocal(TestData.GetProducts());
        }

        [TestMethod]
        public async Task ProductProxy_UpdateProduct()
        {
            Product p = TestData.GetProducts().First();
            p.Price = 4.99m;
            await cpl.UpdateProduct(p);
            Assert.IsTrue((await cpl.GetProduct(1)).Equals(p));

            var product = await cpl.GetProduct(1);

            Assert.AreEqual(p.ID, product.ID);
            Assert.AreEqual(p.Name, product.Name);
            Assert.AreEqual(p.Description, product.Description);
            Assert.AreEqual(p.Supply, product.Supply);
            Assert.AreEqual(p.Price, product.Price);
            Assert.AreEqual(p.Available, product.Available);
        }
    }
}
