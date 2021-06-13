using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies.ReviewProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StaffFrontend.test.Proxies.Reviews
{
    [TestClass]
    public class GetReviews
    {
        private ReviewProxyLocal cpl;

        [TestInitialize]

        public void initTest()
        {

            cpl = new ReviewProxyLocal(TestData.GetReviews());
        }
        [Ignore]
        [TestMethod]
        public async Task ReviewProxy_GetReviews()
        {
            Assert.IsTrue(TestData.GetReviews().Where(r => !r.hidden).ToList().SequenceEqual(await cpl.GetReviews(null, null)));
        }
        [Ignore]
        [TestMethod]
        public async Task ReviewProxy_GetReviews_ItemId()
        {
            Assert.IsTrue(TestData.GetReviews().Where(r => r.productId == 1).ToList().SequenceEqual(await cpl.GetReviews(1, null)));
        }
        [Ignore]
        [TestMethod]
        public async Task ReviewProxy_GetReviews_UserId()
        {
            Assert.IsTrue(TestData.GetReviews().Where(r => r.userId == 1).ToList().SequenceEqual(await cpl.GetReviews(null, 1)));
        }
    }
}