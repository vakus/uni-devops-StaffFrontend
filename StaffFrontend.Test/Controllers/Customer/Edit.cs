using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.CustomerProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Customer
{
    [TestClass]
    public class Edit
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
        public async Task Pre_Edit_Parameters_Valid()
        {
            foreach(Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ReturnsAsync(TestData.GetCustomers().Find(c => c.id == customer.id));

                var response = await controller.Edit(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                var model = (Models.Customers.Customer)responseOk.Model;

                Assert.AreEqual(customer.id, model.id);
                Assert.AreEqual(customer.firstname, model.firstname);
                Assert.AreEqual(customer.surname, model.surname);
                Assert.AreEqual(customer.address, model.address);
                Assert.AreEqual(customer.contact, model.contact);
                Assert.AreEqual(customer.canPurchase, model.canPurchase);
                Assert.AreEqual(customer.isDeleted, model.isDeleted);

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
                mockCustomer.Setup(s => s.GetCustomer(id)).ReturnsAsync(TestData.GetCustomers().Find(c => c.id == id));

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
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
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
            Models.Customers.Customer customer = TestData.GetCustomers()[2];
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
            Models.Customers.Customer customer = TestData.GetCustomers()[2];
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
            Models.Customers.Customer customer = TestData.GetCustomers()[2];
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
            Models.Customers.Customer customer = TestData.GetCustomers()[2];
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
