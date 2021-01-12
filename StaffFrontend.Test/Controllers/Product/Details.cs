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
    public class Details
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
        public async Task Details_Parameters_Valid()
        {
            foreach (Models.Product.Product product in TestData.GetProducts())
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ReturnsAsync(TestData.GetProducts().Find(c => c.ID == product.ID));
                mockReview.Setup(s => s.GetReviews(product.ID, null)).ReturnsAsync(TestData.GetReviews().FindAll(r => r.userId == product.ID));

                var response = await controller.Details(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                var model = (Models.Product.ProductDetailsDTO)responseOk.Model;

                Assert.AreEqual(product.ID, model.product.ID);
                Assert.AreEqual(product.Name, model.product.Name);
                Assert.AreEqual(product.Description, model.product.Description);
                Assert.AreEqual(product.Supply, model.product.Supply);
                Assert.AreEqual(product.Price, model.product.Price);
                Assert.AreEqual(product.Available, model.product.Available);

                foreach (Review review in model.reviews)
                {
                    Assert.AreEqual(product.ID, review.userId);
                }

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(product.ID), Times.Once);
                mockReview.Verify(s => s.GetReviews(product.ID, null), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetProduct(id)).ReturnsAsync(TestData.GetProducts().Find(c => c.ID == id));
                mockReview.Setup(s => s.GetReviews(id, null)).ReturnsAsync(TestData.GetReviews().FindAll(r => r.userId == id));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(id), Times.Once);
                mockReview.Verify(s => s.GetReviews(id, null), Times.AtMostOnce);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Valid_Throws()
        {
            foreach (Models.Product.Product product in TestData.GetProducts())
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.GetReviews(product.ID, null)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(product.ID), Times.Once);
                mockReview.Verify(s => s.GetReviews(product.ID, null), Times.AtMostOnce);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetProduct(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.GetReviews(id, null)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(id), Times.Once);
                mockReview.Verify(s => s.GetReviews(id, null), Times.AtMostOnce);
            }
        }
    }
}