using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Reviews
{
    [TestClass]
    public class Index
    {
        private List<Review> reviews;

        private Mock<IReviewProxy> mockReview;

        private ReviewController controller;

        [TestInitialize]
        public void initialize()
        {

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
                    reviewId = 4,
                    reviewContent = "",
                    reviewRating = 5,
                    productId = 1,
                    hidden = true,
                }
            };

            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new ReviewController(mockReview.Object);
        }

        [TestMethod]
        public async Task Index_Parameter_Null()
        {
            mockReview.Setup(s => s.GetReviews(1, null)).ReturnsAsync(reviews);

            var response = await controller.Index(1, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            Assert.IsTrue(reviews.SequenceEqual((IEnumerable<Review>)responseOk.Model));

            mockReview.Verify();
            
            mockReview.Verify(s => s.GetReviews(1, null), Times.Once);
        }


        [TestMethod]
        public async Task Index_Parameter_minPrice()
        {
            mockReview.Setup(s => s.GetReviews(1, null)).ReturnsAsync(reviews.FindAll(p => p.productId == 1));

            var response = await controller.Index(1, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            var model = (IEnumerable<Review>)responseOk.Model;
            foreach(Review product in model)
            {
                Assert.IsTrue(product.productId == 1);
            }

            mockReview.Verify();
            
            mockReview.Verify(s => s.GetReviews(1, null), Times.Once);
        }


        [TestMethod]
        public async Task Index_Parameter_maxPrice()
        {
            mockReview.Setup(s => s.GetReviews(1, null)).ReturnsAsync(reviews.FindAll(p => p.productId == 1));

            var response = await controller.Index(1, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            var model = (IEnumerable<Review>)responseOk.Model;
            foreach (Review product in model)
            {
                Assert.IsTrue(product.productId == 1);
            }

            mockReview.Verify();
            
            mockReview.Verify(s => s.GetReviews(1, null), Times.Once);
        }

        [TestMethod]
        public async Task Index_Throw_Parameter_Null()
        {
            mockReview.Setup(s => s.GetReviews(1, null)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Index(1, null);
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockReview.Verify();
            
            mockReview.Verify(s => s.GetReviews(1, null), Times.Once);
        }
    }
}