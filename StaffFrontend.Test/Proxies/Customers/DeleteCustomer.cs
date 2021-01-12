using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Customers;
using StaffFrontend.Proxies.CustomerProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Customers
{
    [TestClass]
    public class DeleteCustomer
    {
        private ICustomerProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new CustomerProxyLocal(TestData.GetCustomers());
        }

        [Ignore]
        [TestMethod]
        public async Task CustomerProxy_GetCustomer_ValidID()
        {
            //check data
            for (int id = 1; id <= TestData.GetCustomers().Count; id++)
            {
                await cpl.DeleteCustomer(id);
            }
            for (int id = 0; id < TestData.GetCustomers().Count; id++)
            {
                Assert.IsTrue((await cpl.GetCustomer(id)).isDeleted);
            }
        }

        [TestMethod]
        public async Task CustomerProxy_GetCustomer_InvalidID()
        {
            List<int> ids = new List<int>() { 0, 6, -3, 531, 420, 69 };
            foreach (int id in ids)
            {
                await cpl.DeleteCustomer(id);
                Assert.IsNull((await cpl.GetCustomer(id)));
            }
        }
    }
}
