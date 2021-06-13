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
    public class DeleteByProductId
    {
        private ReviewProxyLocal cpl;

        [TestInitialize]

        public void initTest()
        {

            cpl = new ReviewProxyLocal(TestData.GetReviews());
        }

        [TestMethod]
        public async Task Valid()
        {
            await cpl.DeleteByProductId(1);
            foreach(Review review in (await cpl.GetReviews(1, null)))
            {
                Assert.IsTrue(review.productId != 1);
            }
        }
    }
}