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
    public class GetRating
    {
        private ReviewProxyLocal cpl;

        [TestInitialize]

        public void initTest()
        {
            cpl = new ReviewProxyLocal(TestData.GetReviews());
        }

        [TestMethod]
        public async Task ReviewProxy_GetRating()
        {
            Assert.AreEqual(5d, await cpl.GetRating(2));
            Assert.AreEqual(4d, await cpl.GetRating(1));
        }

        [TestMethod]
        public async Task ReviewProxy_GetRating_Invalid()
        {
            Assert.AreEqual(0, await cpl.GetRating(5));
        }

    }
}
