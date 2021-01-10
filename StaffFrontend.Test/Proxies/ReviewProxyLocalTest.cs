using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies.ReviewProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.Test.Proxies
{
    [TestClass]
    public class ReviewProxyLocalTest
    {

        private List<Review> reviews;
        private ReviewProxyLocal rpl;

        [TestInitialize]

        public void initTest()
        {
            reviews = new List<Review>()
            {
                new Review(){ reviewId = 1, userId = 1, userName = "John", reviewContent = "This item is amazing", hidden=false, productId = 2, reviewRating=5},
                new Review(){ reviewId = 2, userId = 1, userName = "John", reviewContent = "This item is bad because x,y,z", hidden=false, productId=3, reviewRating=2},
                new Review(){ reviewId = 3, userId = 2, userName = "Kekyo1n", reviewContent = "This item claims it can deflect my emerald splash, but nothing can deflect my emeral splash", hidden=false, productId=1, reviewRating=1},
                new Review(){ reviewId = 4, userId = 3, userName = "Kuj0h", reviewContent = "", hidden=true, productId=2, reviewRating=1 }
            };

            rpl = new ReviewProxyLocal(reviews);
        }

        [TestMethod]
        public async Task ReviewProxy_GetReviews()
        {
            Assert.IsTrue(reviews.Where(r => !r.hidden).ToList().SequenceEqual(await rpl.GetReviews(null, null)));
        }

        [TestMethod]
        public async Task ReviewProxy_GetReviews_ItemId()
        {
            Assert.IsTrue(reviews.Where(r => r.productId == 1).ToList().SequenceEqual(await rpl.GetReviews(1, null)));
        }

        [TestMethod]
        public async Task ReviewProxy_GetReviews_UserId()
        {
            Assert.IsTrue(reviews.Where(r => r.userId == 1).ToList().SequenceEqual(await rpl.GetReviews(null, 1)));
        }

        [TestMethod]
        public async Task ReviewProxy_GetRating()
        {
            Assert.AreEqual(3d, await rpl.GetRating(2));
            Assert.AreEqual(1d, await rpl.GetRating(1));
        }

        [TestMethod]
        public async Task ReviewProxy_GetRating_Invalid()
        {
            Assert.AreEqual(0, await rpl.GetRating(5));
        }

        [TestMethod]
        public async Task ReviewProxy_GetReview()
        {
            Assert.AreEqual(reviews.First(r => r.reviewId == 1), await rpl.GetReview(1));
            Assert.AreEqual(reviews.First(r => r.reviewId == 2), await rpl.GetReview(2));
            Assert.AreEqual(reviews.First(r => r.reviewId == 3), await rpl.GetReview(3));
            Assert.AreEqual(reviews.First(r => r.reviewId == 4), await rpl.GetReview(4));
        }
    }
}
