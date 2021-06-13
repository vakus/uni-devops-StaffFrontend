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
    public class GetHiddenReviews
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
            Assert.IsTrue(TestData.GetReviews().Where(r => r.hidden).ToList().SequenceEqual(await cpl.GetHiddenReviews(null, null)));
        }

        [Ignore]
        [TestMethod]
        public async Task ReviewProxy_GetReviews_ItemId()
        {
            Assert.IsTrue(TestData.GetReviews().Where(r => r.productId == 1 && r.hidden).ToList().SequenceEqual(await cpl.GetHiddenReviews(1, null)));
        }

        [Ignore]
        [TestMethod]
        public async Task ReviewProxy_GetReviews_UserId()
        {
            Assert.IsTrue(TestData.GetReviews().Where(r => r.userId == 1 && r.hidden).ToList().SequenceEqual(await cpl.GetHiddenReviews(null, 1)));
        }
    }
}