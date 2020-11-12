using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{
    public interface IReviewProxy
    {
        Task<List<Review>> GetReviews(int itemid);

        Task<Review> GetReview(int reviewid);

        Task UpdateReview(Review review);
    }

    public class ReviewProxyLocal : IReviewProxy
    {
        private List<Review> reviews;

        public ReviewProxyLocal()
        {
            reviews = new List<Review>() {
                new Review() { reviewid=1, content="Love this! Especially when it suddenly goes *quack*!", rating=5, itemid=2, hidden=false, createTime=new DateTime(2020, 11, 12, 1, 9, 52)},
                new Review() { reviewid=2, content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam arcu leo, porta quis suscipit id, varius sed urna. Nulla ut. ", rating=3, itemid=1, hidden=false, createTime=new DateTime(1980, 1, 1, 0, 0, 0)},
                new Review() { reviewid=3, content="dont like", rating=1, itemid=3, hidden=false, createTime=new DateTime(2020, 11, 6, 12, 32, 52)},
                new Review() { reviewid=4, content="Adorable!", rating=4, itemid=2, hidden=false, createTime=new DateTime(2020, 11, 7, 15, 42, 23)},
                new Review() { reviewid=5, content="", rating=1, itemid=1, hidden=true, createTime=new DateTime(2020, 10, 12, 13, 24, 42)}
            };
        }

        public ReviewProxyLocal(List<Review> reviews)
        {
            this.reviews = reviews;
        }

        public Task<Review> GetReview(int reviewid)
        {
            return Task.FromResult(reviews.Find(r => r.reviewid == reviewid));
        }

        public Task<List<Review>> GetReviews(int itemid)
        {
            return Task.FromResult(reviews.FindAll(r => r.itemid == itemid));
        }

        public Task UpdateReview(Review review)
        {
            return Task.Run(() => {
                reviews.RemoveAll(r => r.reviewid == review.reviewid);
                reviews.Add(review);
            });
        }
    }

    public class ReviewProxyRemote : IReviewProxy
    {
        public Task<Review> GetReview(int reviewid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetReviews(int itemid)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
