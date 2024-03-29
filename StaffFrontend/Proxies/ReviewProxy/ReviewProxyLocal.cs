﻿using StaffFrontend.Models;
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
                new Review() { reviewId=1, reviewContent="Love this! Especially when it suddenly goes *quack*!", reviewRating=5, productId=2, hidden=false},
                new Review() { reviewId=2, reviewContent="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam arcu leo, porta quis suscipit id, varius sed urna. Nulla ut. ", reviewRating=3, productId=1, hidden=false},
                new Review() { reviewId=3, reviewContent="dont like", reviewRating=1, productId=3, hidden=false},
                new Review() { reviewId=4, reviewContent="Adorable!", reviewRating=4, productId=2, hidden=false},
                new Review() { reviewId=5, reviewContent="", reviewRating=1, productId=1, hidden=true}
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

        public Task DeletePII(int customerid)
        {
            return Task.Run(() =>
            {
                for(int x = 0; x < reviews.Count; x++)
                {
                    if(reviews[x].userId == customerid)
                    {
                        reviews[x].userName = "";
                    }
                }
            });
        }

        public Task<List<Review>> GetHiddenReviews(int? itemId, int? customerId)
        {
            return Task.FromResult(reviews.FindAll(r =>
                ((itemId.HasValue && r.productId == itemId)
                || (customerId.HasValue && r.userId == customerId.Value)
                || (!itemId.HasValue && !customerId.HasValue))
                && r.hidden
            ));
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
            return Task.FromResult(reviews.FindAll(r =>
                ((itemId.HasValue && r.productId == itemId)
                || (customerId.HasValue && r.userId == customerId.Value)
                || (!itemId.HasValue && !customerId.HasValue))
                && !r.hidden
            ));
        }

        public Task HideReview(int reviewid)
        {
            return Task.Run(() => {
                if (reviews.Where(r => r.reviewId == reviewid).Count() != 0)
                {
                    Review review = reviews.FirstOrDefault(r => r.reviewId == reviewid);
                    reviews.Remove(review);
                    review.hidden = true;
                    reviews.Add(review);
                }
            });
        }

        public Task UnhideReview(int reviewid)
        {
            return Task.Run(() => {
                if (reviews.Where(r => r.reviewId == reviewid).Count() != 0)
                {
                    Review review = reviews.FirstOrDefault(r => r.reviewId == reviewid);
                    reviews.Remove(review);
                    review.hidden = false;
                    reviews.Add(review);
                }
            });
        }
    }
}
