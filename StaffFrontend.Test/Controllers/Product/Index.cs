using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.ProductProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Product
{
    [TestClass]
    public class Index
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
        public async Task Index_Parameter_Null()
        {
            mockCustomer.Setup(s => s.GetProducts(null, null, null, null)).ReturnsAsync(TestData.GetProducts());

            var response = await controller.Index(null, null, null, null, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            var model = (List<Models.Product.Product>)responseOk.Model;
            for (int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(TestData.GetProducts()[x].ID, model[x].ID);
                Assert.AreEqual(TestData.GetProducts()[x].Name, model[x].Name);
                Assert.AreEqual(TestData.GetProducts()[x].Description, model[x].Description);
                Assert.AreEqual(TestData.GetProducts()[x].Supply, model[x].Supply);
                Assert.AreEqual(TestData.GetProducts()[x].Price, model[x].Price);
                Assert.AreEqual(TestData.GetProducts()[x].Available, model[x].Available);
            }

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetProducts(null, null, null, null), Times.Once);
        }


        [TestMethod]
        public async Task Index_Parameter_minPrice()
        {
            mockCustomer.Setup(s => s.GetProducts(null, null, 20, null)).ReturnsAsync(TestData.GetProducts().FindAll(p => p.Price > 20));

            var response = await controller.Index(null, null, 20, null, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            var model = (IEnumerable<Models.Product.Product>)responseOk.Model;
            foreach (Models.Product.Product product in model)
            {
                Assert.IsTrue(product.Price > 20);
            }

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetProducts(null, null, 20, null), Times.Once);
        }


        [TestMethod]
        public async Task Index_Parameter_maxPrice()
        {
            mockCustomer.Setup(s => s.GetProducts(null, null, null, 20)).ReturnsAsync(TestData.GetProducts().FindAll(p => p.Price < 20));

            var response = await controller.Index(null, null, null, 20, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            var model = (IEnumerable<Models.Product.Product>)responseOk.Model;
            foreach (Models.Product.Product product in model)
            {
                Assert.IsTrue(product.Price < 20);
            }

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetProducts(null, null, null, 20), Times.Once);
        }

        [TestMethod]
        public async Task Index_Throw_Parameter_Null()
        {
            mockCustomer.Setup(s => s.GetProducts(null, null, null, null)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Index(null, null, null, null, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetProducts(null, null, null, null), Times.Once);
        }
    }
}