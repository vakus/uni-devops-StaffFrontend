using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Proxies.ReviewProxy;
using System.Linq;
using System.Threading.Tasks;


namespace StaffFrontend.test.Proxies.Reviews
{
    [TestClass]
    public
    class GetReview
    {
        private ReviewProxyLocal cpl;

        [TestInitialize]

        public void initTest()
        {
            cpl = new ReviewProxyLocal(TestData.GetReviews());
        }

        [Ignore]
        [TestMethod]
        public async Task ReviewProxy_GetReview()
        {
            Assert.AreEqual(TestData.GetReviews().First(r => r.reviewId == 1), await cpl.GetReview(1));
            Assert.AreEqual(TestData.GetReviews().First(r => r.reviewId == 2), await cpl.GetReview(2));
            Assert.AreEqual(TestData.GetReviews().First(r => r.reviewId == 3), await cpl.GetReview(3));
            Assert.AreEqual(TestData.GetReviews().First(r => r.reviewId == 4), await cpl.GetReview(4));
        }
    }
}
