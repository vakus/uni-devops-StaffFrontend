using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies.ProductProxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Products
{
    [TestClass]
    public class GetProduct
    {
        private IProductProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new ProductProxyLocal(TestData.GetProducts());
        }

        [TestMethod]
        public async Task ValidID()
        {

            var model = await cpl.GetProduct(1);
            var expected = TestData.GetProducts().Find(s => s.ID == 1);
            Assert.AreEqual(expected.ID, model.ID);
            Assert.AreEqual(expected.Name, model.Name);
            Assert.AreEqual(expected.Description, model.Description);
            Assert.AreEqual(expected.Supply, model.Supply);
            Assert.AreEqual(expected.Price, model.Price);
            Assert.AreEqual(expected.Available, model.Available);

            model = await cpl.GetProduct(2);
            expected = TestData.GetProducts().Find(s => s.ID == 2);
            Assert.AreEqual(expected.ID, model.ID);
            Assert.AreEqual(expected.Name, model.Name);
            Assert.AreEqual(expected.Description, model.Description);
            Assert.AreEqual(expected.Supply, model.Supply);
            Assert.AreEqual(expected.Price, model.Price);
            Assert.AreEqual(expected.Available, model.Available);

            model = await cpl.GetProduct(3);
            expected = TestData.GetProducts().Find(s => s.ID == 3);
            Assert.AreEqual(expected.ID, model.ID);
            Assert.AreEqual(expected.Name, model.Name);
            Assert.AreEqual(expected.Description, model.Description);
            Assert.AreEqual(expected.Supply, model.Supply);
            Assert.AreEqual(expected.Price, model.Price);
            Assert.AreEqual(expected.Available, model.Available);
        }

        [TestMethod]
        public async Task InvalidID()
        {
            Assert.IsNull(await cpl.GetProduct(5));
            Assert.IsNull(await cpl.GetProduct(-2));
        }
    }
}
