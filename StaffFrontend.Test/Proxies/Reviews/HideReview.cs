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
    class HideReview
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
            for(int id = 1; id <= TestData.GetReviews().Count; id++)
            {
                await cpl.HideReview(id);
                Assert.IsTrue((await cpl.GetReview(id)).hidden);
            }
        }
    }
}
