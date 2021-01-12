using Microsoft.AspNetCore.Http;
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
    public class Hide
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
        public async Task Hide_Parameters_Valid()
        {
            foreach (Review review in TestData.GetReviews())
            {
                mockReview.Setup(s => s.HideReview(review.reviewId)).Returns(Task.Run(() => { }));

                var response = await controller.Hide(review.reviewId, "/");
                Assert.IsNotNull(response);
                var responseOk = response as LocalRedirectResult;
                Assert.IsNotNull(responseOk);

                mockReview.Verify();
                mockReview.Verify(s => s.HideReview(review.reviewId), Times.Once);
            }
        }

        [TestMethod]
        public async Task Hide_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockReview.Setup(s => s.HideReview(id)).Returns(Task.Run(() => { }));

                var response = await controller.Hide(id, "/");
                Assert.IsNotNull(response);
                var responseOk = response as LocalRedirectResult;
                Assert.IsNotNull(responseOk);

                mockReview.Verify();
                mockReview.Verify(s => s.HideReview(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Hide_Parameters_Valid_Throws()
        {
            foreach (Review review in TestData.GetReviews())
            {
                mockReview.Setup(s => s.HideReview(review.reviewId)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Hide(review.reviewId, "/");
                Assert.IsNotNull(response);
                var responseOk = response as LocalRedirectResult;
                Assert.IsNotNull(responseOk);

                mockReview.Verify();
                mockReview.Verify(s => s.HideReview(review.reviewId), Times.Once);
            }
        }

        [TestMethod]
        public async Task Hide_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockReview.Setup(s => s.HideReview(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Hide(id, "/");
                Assert.IsNotNull(response);
                var responseOk = response as LocalRedirectResult;
                Assert.IsNotNull(responseOk);

                mockReview.Verify();
                mockReview.Verify(s => s.HideReview(id), Times.Once);
            }
        }
    }
}
