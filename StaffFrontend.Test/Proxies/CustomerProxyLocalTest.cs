using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.Test
{
    [TestClass]
    public class CustomerProxyLocalTest
    {
        [TestMethod]
        public async Task CustomerProxy_GetCustomers()
        {
            //create the data
            List<Customer> customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true }
            };

            //setup the proxy
            CustomerProxyLocal cpl = new CustomerProxyLocal(customers);

            //check data
            Assert.AreEqual(await cpl.GetCustomers(), customers);
        }

        [TestMethod]
        public async Task CustomerProxy_GetCustomer_ValidID()
        {
            //create the data
            List<Customer> customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true }
            };

            //setup the proxy

            CustomerProxyLocal cpl = new CustomerProxyLocal(customers);

            //check data

            Assert.AreEqual(customers.First(p => p.id == 1), await cpl.GetCustomer(1));
            Assert.AreEqual(customers.First(p => p.id == 2), await cpl.GetCustomer(2));
            Assert.AreEqual(customers.First(p => p.id == 3), await cpl.GetCustomer(3));
            Assert.AreEqual(customers.First(p => p.id == 4), await cpl.GetCustomer(4));
            Assert.AreEqual(customers.First(p => p.id == 5), await cpl.GetCustomer(5));
        }

        [TestMethod]
        public async Task CustomerProxy_GetCustomer_InvalidID()
        {
            //create the data
            List<Customer> customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true }
            };

            //setup the proxy

            CustomerProxyLocal cpl = new CustomerProxyLocal(customers);

            //check data

            Assert.IsNull(await cpl.GetCustomer(0));
            Assert.IsNull(await cpl.GetCustomer(6));
            Assert.IsNull(await cpl.GetCustomer(-3));
        }

        [TestMethod]
        public async Task CustomerProxy_UpdateCustomer_ValidData()
        {
            //create the data
            List<Customer> customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true }
            };

            //setup the proxy

            CustomerProxyLocal cpl = new CustomerProxyLocal(customers);

            //update data

            Customer customer = customers.FirstOrDefault(c => c.id == 1);
            customer.firstname = "Bob";
            customer.canPurchase = true;

            await cpl.UpdateCustomer(customer);

            Assert.AreEqual(customer, await cpl.GetCustomer(1));

            //check if rest hasnt been affected
            Assert.AreEqual(customers.First(c => c.id == 2), await cpl.GetCustomer(2));
            Assert.AreEqual(customers.First(c => c.id == 3), await cpl.GetCustomer(3));
            Assert.AreEqual(customers.First(c => c.id == 4), await cpl.GetCustomer(4));
            Assert.AreEqual(customers.First(c => c.id == 5), await cpl.GetCustomer(5));
        }

        [TestMethod]
        public async Task CustomerProxy_UpdateCustomer_MaliciousData()
        {
            //create the data
            List<Customer> customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true }
            };

            //setup the proxy

            CustomerProxyLocal cpl = new CustomerProxyLocal(customers);

            //update data

            Customer customer = customers.FirstOrDefault(c => c.id == 1);
            customer.id = 10;
            customer.firstname = "Bob";
            customer.canPurchase = true;

            await cpl.UpdateCustomer(customer);

            Assert.AreEqual(customer, await cpl.GetCustomer(10));

            //check if rest hasnt been affected
            Assert.AreEqual(customers.First(c => c.id == 2), await cpl.GetCustomer(2));
            Assert.AreEqual(customers.First(c => c.id == 3), await cpl.GetCustomer(3));
            Assert.AreEqual(customers.First(c => c.id == 4), await cpl.GetCustomer(4));
            Assert.AreEqual(customers.First(c => c.id == 5), await cpl.GetCustomer(5));
            Assert.IsNull(await cpl.GetCustomer(1));
        }
    }
}
