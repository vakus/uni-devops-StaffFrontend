using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Customer
{
    [TestClass]
    public class Edit
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
                new Models.Customers.Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true, isDeleted=false }
            };

            mockCustomer = new Mock<ICustomerProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new CustomerController(mockCustomer.Object, mockReview.Object);
        }

        [TestMethod]
        public async Task Pre_Edit_Parameters_Valid()
        {
            foreach(Models.Customers.Customer customer in customers)
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ReturnsAsync(customers.Find(c => c.id == customer.id));

                var response = await controller.Edit(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                Assert.AreEqual(customer, responseOk.Model);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Pre_Edit_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337};
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ReturnsAsync(customers.Find(c => c.id == id));

                var response = await controller.Edit(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Pre_Edit_Parameters_Valid_Throws()
        {
            foreach (Models.Customers.Customer customer in customers)
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Edit(customer.id);
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
        public async Task Pre_Edit_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Edit(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }



        [TestMethod]
        public async Task Post_Edit_Parameters_Valid()
        {
            Models.Customers.Customer customer = customers[2];
            customer.address = "1 Annamark Pass";

            mockCustomer.Setup(s => s.UpdateCustomer(customer)).Returns(Task.Run(() => { }));

            var response = await controller.Edit(customer);
            Assert.IsNotNull(response);
            var responseOk = response as RedirectToActionResult;
            Assert.IsNotNull(responseOk);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateCustomer(customer), Times.Once);
        }


        [TestMethod]
        public async Task Post_Edit_Parameters_Invalid()
        {
            Models.Customers.Customer customer = customers[2];
            customer.id = 20;
            customer.address = "1 Annamark Pass";

            mockCustomer.Setup(s => s.UpdateCustomer(customer)).Returns(Task.Run(()=> { }));

            var response = await controller.Edit(customer);
            Assert.IsNotNull(response);
            var responseOk = response as RedirectToActionResult;
            Assert.IsNotNull(responseOk);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateCustomer(customer), Times.Once);
        }


        [TestMethod]
        public async Task Post_Edit_Parameters_Valid_Throws()
        {
            Models.Customers.Customer customer = customers[2];
            customer.address = "1 Annamark Pass";

            mockCustomer.Setup(s => s.UpdateCustomer(customer)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Edit(customer);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateCustomer(customer), Times.Once);
        }

        [TestMethod]
        public async Task Post_Edit_Parameters_Invalid_Throws()
        {
            Models.Customers.Customer customer = customers[2];
            customer.id = 20;
            customer.address = "1 Annamark Pass";

            mockCustomer.Setup(s => s.UpdateCustomer(customer)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Edit(customer);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateCustomer(customer), Times.Once);
        }
    }
}
