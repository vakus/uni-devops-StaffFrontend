using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.CustomerProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Customer
{
    [TestClass]
    public class Index
    {

        private List<Models.Customers.Customer> customers;

        private Mock<ICustomerProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private CustomerController controller;

        [TestInitialize]
        public void initialize()
        {
            customers = new List<Models.Customers.Customer>() {
                new Models.Customers.Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false, isDeleted=false},
                new Models.Customers.Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true, isDeleted=false },
                new Models.Customers.Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true, isDeleted=false },
                new Models.Customers.Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false, isDeleted=false },
                new Models.Customers.Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true, isDeleted=true }
            };

            mockCustomer = new Mock<ICustomerProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new CustomerController(mockCustomer.Object, mockReview.Object);
        }

        [TestMethod]
        public async Task Index_Parameter_Null()
        {
            mockCustomer.Setup(s => s.GetCustomers(true)).ReturnsAsync(customers.Where(c => !c.isDeleted).ToList());

            var response = await controller.Index(null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            foreach (Models.Customers.Customer customer in (IEnumerable<Models.Customers.Customer>)responseOk.Model)
            {
                Assert.IsFalse(customer.isDeleted);
            }

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetCustomers(true), Times.Once);
        }

        [TestMethod]
        public async Task Index_Parameter_True()
        {
            mockCustomer.Setup(s => s.GetCustomers(true)).ReturnsAsync(customers.Where(c => !c.isDeleted).ToList());

            var response = await controller.Index(true);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            foreach (Models.Customers.Customer customer in (IEnumerable<Models.Customers.Customer>)responseOk.Model)
            {
                Assert.IsFalse(customer.isDeleted);
            }

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetCustomers(true), Times.Once);
        }

        [TestMethod]
        public async Task Index_Parameter_False()
        {
            mockCustomer.Setup(s => s.GetCustomers(false)).ReturnsAsync(customers.Where(c => c.isDeleted).ToList());

            var response = await controller.Index(false);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            foreach (Models.Customers.Customer customer in (IEnumerable<Models.Customers.Customer>)responseOk.Model)
            {
                Assert.IsTrue(customer.isDeleted);
            }

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetCustomers(false), Times.Once);
        }

        [TestMethod]
        public async Task Index_Throw_Parameter_Null()
        {
            mockCustomer.Setup(s => s.GetCustomers(true)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Index(null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetCustomers(true), Times.Once);
        }

        [TestMethod]
        public async Task Index_Throw_Parameter_True()
        {
            mockCustomer.Setup(s => s.GetCustomers(true)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Index(true);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);


            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetCustomers(true), Times.Once);
        }

        [TestMethod]
        public async Task Index_Throw_Parameter_False()
        {
            mockCustomer.Setup(s => s.GetCustomers(false)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Index(false);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetCustomers(false), Times.Once);
        }
    }
}