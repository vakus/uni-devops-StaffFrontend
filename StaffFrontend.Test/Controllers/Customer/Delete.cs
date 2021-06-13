using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.CustomerProxy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Customer
{
    [TestClass]
    public class Delete
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
        public async Task Pre_Delete_Parameters_Valid()
        {
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ReturnsAsync(TestData.GetCustomers().Find(c => c.id == customer.id));

                var response = await controller.Delete(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                Assert.AreEqual(customer.id, ((Models.Customers.Customer)responseOk.Model).id);
                Assert.AreEqual(customer.firstname, ((Models.Customers.Customer)responseOk.Model).firstname);
                Assert.AreEqual(customer.surname, ((Models.Customers.Customer)responseOk.Model).surname);
                Assert.AreEqual(customer.address, ((Models.Customers.Customer)responseOk.Model).address);
                Assert.AreEqual(customer.contact, ((Models.Customers.Customer)responseOk.Model).contact);
                Assert.AreEqual(customer.canPurchase, ((Models.Customers.Customer)responseOk.Model).canPurchase);
                Assert.AreEqual(customer.isDeleted, ((Models.Customers.Customer)responseOk.Model).isDeleted);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Pre_Delete_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ReturnsAsync(TestData.GetCustomers().Find(c => c.id == id));

                var response = await controller.Delete(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Pre_Delete_Parameters_Valid_Throws()
        {
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(customer.id);
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
        public async Task Pre_Delete_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(id);
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
        public async Task Post_Delete_Parameters_Valid()
        {
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.DeleteCustomer(customer.id)).Returns(Task.Run(()=> { }));
                mockReview.Setup(s => s.DeletePII(customer.id)).Returns(Task.Run(() => { }));

                var response = await controller.Delete(customer.id, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as RedirectToActionResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteCustomer(customer.id), Times.Once);
                mockReview.Verify(s => s.DeletePII(customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Post_Delete_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.DeleteCustomer(id)).Returns(Task.Run(() => { }));
                mockReview.Setup(s => s.DeletePII(id)).Returns(Task.Run(() => { }));

                var response = await controller.Delete(id, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as RedirectToActionResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteCustomer(id), Times.Once);
                mockReview.Verify(s => s.DeletePII(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Post_Delete_Parameters_Valid_Throws()
        {
            foreach (Models.Customers.Customer customer in TestData.GetCustomers())
            {
                mockCustomer.Setup(s => s.DeleteCustomer(customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.DeletePII(customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(customer.id, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteCustomer(customer.id), Times.Once);
                mockReview.Verify(s => s.DeletePII(customer.id), Times.AtMostOnce);
            }
        }

        [TestMethod]
        public async Task Post_Delete_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.DeleteCustomer(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.DeletePII(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(id, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteCustomer(id), Times.Once);
                mockReview.Verify(s => s.DeletePII(id), Times.AtMostOnce);
            }
        }
    }
}
