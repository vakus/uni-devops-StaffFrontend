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

        private List<Models.Product.Product> products;
        private List<Review> reviews;

        private Mock<IProductProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private ProductController controller;

        [TestInitialize]
        public void initialize()
        {
            products = new List<Models.Product.Product>() {
                new Models.Product.Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99m, Available = false, Supply = 2 },
                new Models.Product.Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99m, Available = true, Supply = 20 },
                new Models.Product.Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99m, Available = true, Supply = 13 }
            };


            reviews = new List<Review>()
            {
                new Review()
                {
                    userId = 1,
                    userName = "John",
                    reviewId = 1,
                    reviewContent = "good",
                    reviewRating = 4,
                    productId = 1,
                    hidden = false,
                },
                new Review()
                {
                    userId = 1,
                    userName = "John",
                    reviewId = 2,
                    reviewContent = "follow me on twitter",
                    reviewRating = 4,
                    productId = 3,
                    hidden = true,
                },
                new Review()
                {
                    userId = 1,
                    userName = "John",
                    reviewId = 3,
                    reviewContent = "good",
                    reviewRating = 5,
                    productId = 2,
                    hidden = false,
                },
                new Review()
                {
                    userId = 2,
                    userName = "Bethany",
                    reviewId = 4,
                    reviewContent = "decent",
                    reviewRating = 3,
                    productId = 1,
                    hidden = false,
                },
                new Review()
                {
                    userId = 3,
                    userName = "Brigid",
                    reviewId = 5,
                    reviewContent = "",
                    reviewRating = 5,
                    productId = 1,
                    hidden = true,
                }
            };

            mockCustomer = new Mock<IProductProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new ProductController(mockCustomer.Object, mockReview.Object);
        }

        [TestMethod]
        public async Task Index_Parameter_Null()
        {
            mockCustomer.Setup(s => s.GetProducts(null, null, null, null)).ReturnsAsync(products);

            var response = await controller.Index(null, null, null, null, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            Assert.IsTrue(products.SequenceEqual((IEnumerable<Models.Product.Product>)responseOk.Model));

            mockCustomer.Verify();
            mockReview.Verify();
            mockCustomer.Verify(s => s.GetProducts(null, null, null, null), Times.Once);
        }


        [TestMethod]
        public async Task Index_Parameter_minPrice()
        {
            mockCustomer.Setup(s => s.GetProducts(null, null, 20, null)).ReturnsAsync(products.FindAll(p => p.Price > 20));

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
            mockCustomer.Setup(s => s.GetProducts(null, null, null, 20)).ReturnsAsync(products.FindAll(p => p.Price < 20));

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