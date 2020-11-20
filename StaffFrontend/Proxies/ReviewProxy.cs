using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{
    public interface IReviewProxy
    {
        Task<List<Review>> GetReviews(int itemid);

        Task<Review> GetReview(int reviewid);

        Task UpdateReview(Review review);

        Task<double> GetRating(int itemid);
    }

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
                return (double)(total / applicable.Count());
            });
        }

        public Task<Review> GetReview(int reviewid)
        {
            return Task.FromResult(reviews.Find(r => r.reviewId == reviewid));
        }

        public Task<List<Review>> GetReviews(int itemid)
        {
            return Task.FromResult(reviews.FindAll(r => r.productId == itemid));
        }

        public Task UpdateReview(Review review)
        {
            return Task.Run(() =>
            {
                reviews.RemoveAll(r => r.reviewId == review.reviewId);
                reviews.Add(review);
            });
        }
    }

    public class ReviewProxyRemote : IReviewProxy
    {

        IHttpClientFactory _clientFactory;
        IConfigurationSection _config;

        public ReviewProxyRemote(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config.GetSection("ReviewMicroservice");
        }

        public async Task<double> GetRating(int itemid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemid }
            };

            string url = Utils.createUriBuilder(_config.GetSection("GetRating"), values).ToString();

            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");


            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<double>();
            }
        }

        public async Task<Review> GetReview(int reviewid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "review-id", reviewid }
            };

            string url = Utils.createUriBuilder(_config.GetSection("GetReview"), values).ToString();

            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");


            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<Review>();
            }
        }

        public async Task<List<Review>> GetReviews(int itemid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemid }
            };

            string url = Utils.createUriBuilder(_config.GetSection("GetReviews"), values).ToString();

            var client = _clientFactory.CreateClient();

            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");


            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<List<Review>>();
            }
        }

        public Task UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
