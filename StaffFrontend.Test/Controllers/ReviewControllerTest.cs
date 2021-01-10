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

namespace StaffFrontend.test.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {

        private List<Review> reviews;
        private Mock<IReviewProxy> reviewMock;

        [TestInitialize]
        public void initialize()
        {
            reviews = new List<Review>
            {
                new Review(){
                    reviewId = 1,
                    userId = 1,
                    userName = "john",
                    productId = 1,
                    reviewContent = "pretty decent",
                    reviewRating = 4,
                    createTime = DateTime.Now,
                    hidden = false
                }
            };

            reviewMock = new Mock<IReviewProxy>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetReviews()
        {
            reviewMock.Setup(m => m.GetReviews(1, null)).ReturnsAsync(reviews);

            var reviewController = new ReviewController(reviewMock.Object);

            var result = await reviewController.Index(1, false);

            Assert.IsNotNull(result);

            reviewMock.Verify();
            reviewMock.Verify(m => m.GetReviews(1, null), Times.Once);
        }

        [TestMethod]
        public async Task GetReview()
        {
            reviewMock.Setup(m => m.GetReview(1)).ReturnsAsync(reviews.Find(r => r.reviewId == 1));

            var reviewController = new ReviewController(reviewMock.Object);

            var result = await reviewController.Details(1);

            Assert.IsNotNull(result);

            reviewMock.Verify();
            reviewMock.Verify(m => m.GetReview(1), Times.Once);
        }
    }
}