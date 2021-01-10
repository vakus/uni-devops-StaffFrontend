using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ReviewProxy
{
    public class ReviewProxyLocal : IReviewProxy
    {
        private List<Review> reviews;

        public ReviewProxyLocal()
        {
            reviews = new List<Review>() {
                new Review() { reviewId=1, reviewContent="Love this! Especially when it suddenly goes *quack*!", reviewRating=5, productId=2, hidden=false, createTime=new DateTime(2020, 11, 12, 1, 9, 52)},
                new Review() { reviewId=2, reviewContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam arcu leo, porta quis suscipit id, varius sed urna. Nulla ut. ", reviewRating=3, productId=1, hidden=false, createTime=new DateTime(1980, 1, 1, 0, 0, 0)},
                new Review() { reviewId=3, reviewContent="dont like", reviewRating=1, productId=3, hidden=false, createTime=new DateTime(2020, 11, 6, 12, 32, 52)},
                new Review() { reviewId=4, reviewContent="Adorable!", reviewRating=4, productId=2, hidden=false, createTime=new DateTime(2020, 11, 7, 15, 42, 23)},
                new Review() { reviewId=5, reviewContent="", reviewRating=1, productId=1, hidden=true, createTime=new DateTime(2020, 10, 12, 13, 24, 42)}
            };
        }

        public ReviewProxyLocal(List<Review> reviews)
        {
            this.reviews = reviews;
        }

        public Task DeleteByProductId(int productid)
        {
            return Task.Run(() => {
                reviews.RemoveAll(p => p.productId == productid);
            });
        }

        public Task<double> GetRating(int itemid)
        {


            return Task.Run(() =>
            {

                List<Review> applicable = reviews.FindAll(r => r.productId == itemid);
                int total = 0;
                foreach (var review in applicable)
                {
                    total += review.reviewRating;
                }
                if (total != 0)
                {
                    return (double)(total / applicable.Count());
                }
                else
                {
                    return 0;
                }
            });
        }

        public Task<Review> GetReview(int reviewid)
        {
            return Task.FromResult(reviews.FirstOrDefault(r => r.reviewId == reviewid));
        }

        public Task<List<Review>> GetReviews(int? itemId, int? customerId)
        {
            return Task.FromResult(reviews.FindAll(r => (itemId.HasValue && r.productId == itemId) || (customerId.HasValue && r.userId == customerId.Value) || (!itemId.HasValue && !customerId.HasValue)));
        }

        public Task UpdateReview(Review review)
        {
            return Task.Run(() =>
            {
                if (reviews.Where(r => r.reviewId == review.reviewId).Count() != 0)
                {
                    reviews.RemoveAll(r => r.reviewId == review.reviewId);
                    reviews.Add(review);
                }
            });
        }
    }
}
