using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models.Customers;
using StaffFrontend.Proxies.CustomerProxy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Customers
{
    [TestClass]
    public class UpdateCustomer
    {
        private ICustomerProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            cpl = new CustomerProxyLocal(TestData.GetCustomers());
        }

        [TestMethod]
        public async Task CustomerProxy_UpdateCustomer_ValidData()
        {
            //update data

            Customer customer = TestData.GetCustomers().FirstOrDefault(c => c.id == 1);
            customer.firstname = "Bob";
            customer.canPurchase = true;

            await cpl.UpdateCustomer(customer);

            var model = await cpl.GetCustomer(1);

            Assert.AreEqual(customer.id, model.id);
            Assert.AreEqual(customer.firstname, model.firstname);
            Assert.AreEqual(customer.surname, model.surname);
            Assert.AreEqual(customer.address, model.address);
            Assert.AreEqual(customer.contact, model.contact);
            Assert.AreEqual(customer.canPurchase, model.canPurchase);
            Assert.AreEqual(customer.isDeleted, model.isDeleted);
        }

        [TestMethod]
        public async Task CustomerProxy_UpdateCustomer_MaliciousData()
        {
            //update data

            Customer customer = TestData.GetCustomers().FirstOrDefault(c => c.id == 1);
            customer.id = 10;
            customer.firstname = "Bob";
            customer.canPurchase = true;

            await cpl.UpdateCustomer(customer);

            Assert.AreEqual(customer, await cpl.GetCustomer(10));

            var model = await cpl.GetCustomer(10);

            Assert.AreEqual(customer.id, model.id);
            Assert.AreEqual(customer.firstname, model.firstname);
            Assert.AreEqual(customer.surname, model.surname);
            Assert.AreEqual(customer.address, model.address);
            Assert.AreEqual(customer.contact, model.contact);
            Assert.AreEqual(customer.canPurchase, model.canPurchase);
            Assert.AreEqual(customer.isDeleted, model.isDeleted);
        }
    }
}
