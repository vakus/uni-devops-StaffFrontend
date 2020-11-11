using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.Test
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public async Task Customer()
        {

            List<Customer> customers = new List<Customer>() {
                new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false },
                new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true },
                new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true },
                new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false },
                new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true }
            };

            var mock = new Mock<ICustomerProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetCustomers()).ReturnsAsync(customers);

            var customerController = new CustomerController(mock.Object);

            
        }
    }
}