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
        private Mock<IReviewProxy> mockReview;

        private ReviewController controller;

        [TestInitialize]
        public void initialize()
        {
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new ReviewController(mockReview.Object);
        }

        [TestMethod]
        public async Task Details_Parameters_Valid()
        {
            foreach (Review review in TestData.GetReviews())
            {
                mockReview.Setup(s => s.GetReview(review.reviewId)).ReturnsAsync(TestData.GetReviews().Find(r => r.reviewId == review.reviewId));

                var response = await controller.Details(review.reviewId);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);



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
                mockReview.Setup(s => s.GetReview(id)).ReturnsAsync(TestData.GetReviews().Find(r => r.reviewId == id));

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
            foreach (Review review in TestData.GetReviews())
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