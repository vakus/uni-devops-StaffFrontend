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

        private Mock<ICustomerProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private CustomerController controller;

        [TestInitialize]
        public void initialize()
        {
            mockCustomer = new Mock<ICustomerProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new CustomerController(mockCustomer.Object, mockReview.Object);
        }

        [TestMethod]
        public async Task Index_Parameter_Null()
        {
            mockCustomer.Setup(s => s.GetCustomers(true)).ReturnsAsync(TestData.GetCustomers().Where(c => !c.isDeleted).ToList());

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
            mockCustomer.Setup(s => s.GetCustomers(true)).ReturnsAsync(TestData.GetCustomers().Where(c => !c.isDeleted).ToList());

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
            mockCustomer.Setup(s => s.GetCustomers(false)).ReturnsAsync(TestData.GetCustomers().Where(c => c.isDeleted).ToList());

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