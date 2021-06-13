using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.CustomerProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Customer
{
    [TestClass]
    public class Details
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
        public async Task Details_Parameters_Valid()
        {
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ReturnsAsync(TestData.GetCustomers().Find(c => c.id == customer.id));
                mockReview.Setup(s => s.GetReviews(null, customer.id)).ReturnsAsync(TestData.GetReviews().FindAll(r => r.userId == customer.id));

                var response = await controller.Details(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                var model = (Models.Customers.CustomerDetailsDTO)responseOk.Model;

                Assert.AreEqual(customer.id, model.Customer.id);
                Assert.AreEqual(customer.firstname, model.Customer.firstname);
                Assert.AreEqual(customer.surname, model.Customer.surname);
                Assert.AreEqual(customer.address, model.Customer.address);
                Assert.AreEqual(customer.contact, model.Customer.contact);
                Assert.AreEqual(customer.canPurchase, model.Customer.canPurchase);
                Assert.AreEqual(customer.isDeleted, model.Customer.isDeleted);

                foreach (Review review in model.Reviews)
                {
                    Assert.AreEqual(customer.id, review.userId);
                }

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(customer.id), Times.Once);
                mockReview.Verify(s => s.GetReviews(null, customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ReturnsAsync(TestData.GetCustomers().Find(c => c.id == id));
                mockReview.Setup(s => s.GetReviews(null, id)).ReturnsAsync(TestData.GetReviews().FindAll(r => r.userId == id));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Valid_Throws()
        {
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.GetReviews(null, customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.GetReviews(null, id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }
    }
}