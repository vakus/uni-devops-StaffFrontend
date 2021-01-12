using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Customers;
using StaffFrontend.Proxies.CustomerProxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Customers
{
    [TestClass]
    public class GetProducts
    {
        private ICustomerProxy cpl;

        [TestInitialize]
        public void initTest()
        {

            cpl = new CustomerProxyLocal(TestData.GetCustomers());
        }

        [Ignore]
        [TestMethod]
        public async Task CustomerProxy_GetCustomers()
        {

            List<Customer> customers = await cpl.GetCustomers(true);

            for (int x = 0; x < TestData.GetCustomers().Count; x++)
            {
                Assert.AreEqual(TestData.GetCustomers()[x].id, customers[x].id);
                Assert.AreEqual(TestData.GetCustomers()[x].firstname, customers[x].firstname);
                Assert.AreEqual(TestData.GetCustomers()[x].surname, customers[x].surname);
                Assert.AreEqual(TestData.GetCustomers()[x].address, customers[x].address);
                Assert.AreEqual(TestData.GetCustomers()[x].contact, customers[x].contact);
                Assert.AreEqual(TestData.GetCustomers()[x].canPurchase, customers[x].canPurchase);
                Assert.AreEqual(TestData.GetCustomers()[x].isDeleted, customers[x].isDeleted);
            }
        }
    }
}
