using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies.ResupplyProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies
{
    [TestClass]
    public class RestockProxyLocalTest
    {

        private List<Restock> restocks;
        private RestockProxyLocal rpl;

        [TestInitialize]
        public void initTest()
        {
            restocks = new List<Restock>()
            {
                new Restock(){ restockId = "1", supplierId = "1", sProductId = "1", quantity = 1, price = 4.99m, approved=false, date=DateTime.MinValue},
                new Restock(){ restockId = "2", supplierId = "1", sProductId = "3", quantity = 2, price = 2499.98m, approved=true, date=DateTime.Now}
            };

            rpl = new RestockProxyLocal(restocks);
        }

        [TestMethod]
        public async Task RestockProxy_GetRestocks()
        {
            Assert.IsTrue(restocks.SequenceEqual(await rpl.GetRestocks()));
        }

        [TestMethod]
        public async Task RestockProxy_GetRestock()
        {
            Assert.AreEqual(restocks.First(r => r.restockId == "1"), await rpl.GetRestock(1));
        }

        [TestMethod]
        public async Task RestockProxy_UpdateRestock()
        {
            Restock r = restocks.First();
            r.approved = true;
            r.date = DateTime.Now;
            await rpl.UpdateRestock(r);
            Assert.AreEqual(r, await rpl.GetRestock(int.Parse(r.restockId)));
        }

        [TestMethod]
        public async Task RestockProxy_UpdateRestock_Invalid()
        {
            Restock r = new Restock() { restockId = "123456789", supplierId = "1", sProductId = "1", quantity = 1, price = 4.99m, approved = true, date = DateTime.Now };
            await rpl.UpdateRestock(r);
            Assert.IsNull(await rpl.GetRestock(int.Parse(r.restockId)));
        }

        [TestMethod]
        public async Task RestockProxy_DeleteRestock()
        {
            string key = restocks.First().restockId;
            await rpl.DeleteRestock(int.Parse(key));
            Assert.IsNull(await rpl.GetRestock(int.Parse(key)));
        }

        [TestMethod]
        public async Task RestockProxy_CreateRestock()
        {
            Restock r = new Restock()
            {
                restockId = "3",
                supplierId = "1",
                sProductId = "2",
                quantity = 2000,
                price = 99.99m,
                approved = true,
                date = DateTime.Now
            };

            await rpl.CreateRestock(r);
            Assert.AreEqual(r, await rpl.GetRestock(int.Parse(r.restockId)));
        }
    }
}