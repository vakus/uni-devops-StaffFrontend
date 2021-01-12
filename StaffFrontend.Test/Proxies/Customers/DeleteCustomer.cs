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
        private List<Customer> customers;
        private ICustomerProxy cpl;

        [TestInitialize]
        public void initTest()
        {
            customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false, isDeleted=false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true, isDeleted=false },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true, isDeleted=false },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false, isDeleted=false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true, isDeleted=false }
            };

            cpl = new CustomerProxyLocal(customers);
        }

        [TestMethod]
        public async Task CustomerProxy_GetCustomer_ValidID()
        {
            //check data
            for (int id = 1; id <= customers.Count; id++)
            {
                await cpl.DeleteCustomer(id);
            }
            for (int id = 0; id < customers.Count; id++)
            {
                Assert.IsTrue(customers[id].isDeleted);
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
