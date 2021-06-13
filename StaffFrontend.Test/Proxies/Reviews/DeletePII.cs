using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies.ReviewProxy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies.Reviews
{
    [TestClass]
    public class DeletePII
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
            List<int> ids = new List<int>() { 1, 2, 3};
            foreach(int id in ids)
            {
                await cpl.DeletePII(id);
                foreach (Review review in (await cpl.GetReviews(null, id)))
                {
                    if (review.userId == id)
                    {
                        Assert.AreEqual("", review.userName);
                    }
                }
            }
        }
    }
}
