using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Customers;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.CustomerProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Customers
{
    [TestClass]
    public class GetCustomer
    {
        private ICustomerProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new CustomerProxyLocal(TestData.GetCustomers());
        }


        [TestMethod]
        public async Task CustomerProxy_GetCustomer_ValidID()
        {
            //check data
            foreach(Customer customer in TestData.GetCustomers())
            {

                Assert.AreEqual(customer.id, TestData.GetCustomers().First(p => p.id == customer.id).id);
                Assert.AreEqual(customer.firstname, TestData.GetCustomers().First(p => p.id == customer.id).firstname);
                Assert.AreEqual(customer.surname, TestData.GetCustomers().First(p => p.id == customer.id).surname);
                Assert.AreEqual(customer.address, TestData.GetCustomers().First(p => p.id == customer.id).address);
                Assert.AreEqual(customer.contact, TestData.GetCustomers().First(p => p.id == customer.id).contact);
                Assert.AreEqual(customer.canPurchase, TestData.GetCustomers().First(p => p.id == customer.id).canPurchase);
                Assert.AreEqual(customer.isDeleted, TestData.GetCustomers().First(p => p.id == customer.id).isDeleted);
            }
        }

        [TestMethod]
        public async Task CustomerProxy_GetCustomer_InvalidID()
        {
            List<int> ids = new List<int>() { 0, 6, -3, 531, 420, 69 };
            foreach(int id in ids)
            {
                Assert.IsNull(await cpl.GetCustomer(id));
            }
        }
    }
}
