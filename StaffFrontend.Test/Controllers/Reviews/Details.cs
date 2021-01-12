using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Reviews
{
    [TestClass]
    public class Details
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
                    reviewId = 5,
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
        public async Task Details_Parameters_Valid()
        {
            foreach (Review review in reviews)
            {
                mockReview.Setup(s => s.GetReview(review.reviewId)).ReturnsAsync(reviews.Find(r => r.reviewId == review.reviewId));

                var response = await controller.Details(review.reviewId);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                Assert.AreEqual(review, responseOk.Model);

                
                mockReview.Verify();
                mockReview.Verify(s => s.GetReview(review.reviewId), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockReview.Setup(s => s.GetReview(id)).ReturnsAsync(reviews.Find(r => r.reviewId == id));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockReview.Verify();
                mockReview.Verify(s => s.GetReview(id), Times.AtMostOnce);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Valid_Throws()
        {
            foreach (Review review in reviews)
            {
                mockReview.Setup(s => s.GetReview(review.reviewId)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(review.reviewId);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                
                mockReview.Verify();
                mockReview.Verify(s => s.GetReview(review.reviewId), Times.AtMostOnce);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockReview.Setup(s => s.GetReview(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);

                
                mockReview.Verify();
                mockReview.Verify(s => s.GetReview(id), Times.AtMostOnce);
            }
        }
    }
}