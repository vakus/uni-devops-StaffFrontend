using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies.ReviewProxy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StaffFrontend.test.Proxies.Reviews
{
    [TestClass]
    public
    class GetReview
    {

        private List<Review> reviews;
        private ReviewProxyLocal cpl;

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

            cpl = new ReviewProxyLocal(reviews);
        }

        [TestMethod]
        public async Task ReviewProxy_GetReview()
        {
            Assert.AreEqual(reviews.First(r => r.reviewId == 1), await cpl.GetReview(1));
            Assert.AreEqual(reviews.First(r => r.reviewId == 2), await cpl.GetReview(2));
            Assert.AreEqual(reviews.First(r => r.reviewId == 3), await cpl.GetReview(3));
            Assert.AreEqual(reviews.First(r => r.reviewId == 4), await cpl.GetReview(4));
        }
    }
}
