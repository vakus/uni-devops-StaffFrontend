using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.ProductProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Product
{
    [TestClass]
    public class Edit
    {
        private Mock<IProductProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private ProductController controller;

        [TestInitialize]
        public void initialize()
        {
            mockCustomer = new Mock<IProductProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new ProductController(mockCustomer.Object, mockReview.Object);
        }

        [TestMethod]
        public async Task Pre_Edit_Parameters_Valid()
        {
            foreach(Models.Product.Product product in TestData.GetProducts())
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ReturnsAsync(TestData.GetProducts().Find(c => c.ID == product.ID));

                var response = await controller.Edit(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                var model = (Models.Product.Product)responseOk.Model;

                Assert.AreEqual(product.ID, model.ID);
                Assert.AreEqual(product.Name, model.Name);
                Assert.AreEqual(product.Description, model.Description);
                Assert.AreEqual(product.Supply, model.Supply);
                Assert.AreEqual(product.Price, model.Price);
                Assert.AreEqual(product.Available, model.Available);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(product.ID), Times.Once);
            }
        }

        [TestMethod]
        public async Task Pre_Edit_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337};
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetProduct(id)).ReturnsAsync(TestData.GetProducts().Find(c => c.ID == id));

                var response = await controller.Edit(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Pre_Edit_Parameters_Valid_Throws()
        {
            foreach (Models.Product.Product product in TestData.GetProducts())
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Edit(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(product.ID), Times.Once);
            }
        }

        [TestMethod]
        public async Task Pre_Edit_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetProduct(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Edit(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(id), Times.Once);
            }
        }



        [TestMethod]
        public async Task Post_Edit_Parameters_Valid()
        {
            Models.Product.Product product = TestData.GetProducts()[2];
            product.Description = "Updated";

            mockCustomer.Setup(s => s.UpdateProduct(product)).Returns(Task.Run(() => { }));

            var response = await controller.Edit(product.ID, product);
            Assert.IsNotNull(response);
            var responseOk = response as RedirectToActionResult;
            Assert.IsNotNull(responseOk);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateProduct(product), Times.Once);
        }


        [TestMethod]
        public async Task Post_Edit_Parameters_Invalid()
        {
            Models.Product.Product product = TestData.GetProducts()[2];
            product.ID = 20;
            product.Description = "Updated";

            mockCustomer.Setup(s => s.UpdateProduct(product)).Returns(Task.Run(()=> { }));

            var response = await controller.Edit(product.ID, product);
            Assert.IsNotNull(response);
            var responseOk = response as RedirectToActionResult;
            Assert.IsNotNull(responseOk);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateProduct(product), Times.Once);
        }


        [TestMethod]
        public async Task Post_Edit_Parameters_Valid_Throws()
        {
            Models.Product.Product product = TestData.GetProducts()[2];
            product.Description = "Updated";

            mockCustomer.Setup(s => s.UpdateProduct(product)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Edit(product.ID, product);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateProduct(product), Times.Once);
        }

        [TestMethod]
        public async Task Post_Edit_Parameters_Invalid_Throws()
        {
            Models.Product.Product product = TestData.GetProducts()[2];
            product.ID = 20;
            product.Description = "Updated";

            mockCustomer.Setup(s => s.UpdateProduct(product)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Edit(product.ID, product);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.UpdateProduct(product), Times.Once);
        }
    }
}
