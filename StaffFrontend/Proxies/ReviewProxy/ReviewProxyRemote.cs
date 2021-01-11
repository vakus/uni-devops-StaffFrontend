using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ReviewProxy
{
    public class ReviewProxyRemote : IReviewProxy
    {

        HttpClient client;
        IConfigurationSection _config;

        public ReviewProxyRemote(HttpClient client, IConfiguration config)
        {
            this.client = client;
            _config = config.GetSection("ReviewMicroservice");
        }

        public async Task DeleteByProductId(int productid)
        {

            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "product-id", productid }
            };

            var response = await Utils.Request(client, _config.GetSection("DeleteByProductId"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task DeletePII(int customerid)
        {

            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "customer-id", customerid }
            };

            var response = await Utils.Request(client, _config.GetSection("DeletePII"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task<List<Review>> GetHiddenReviews(int? itemId, int? customerId)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemId },
                { "customer-id", customerId }
            };

            var response = await Utils.Request(client, _config.GetSection("GetHiddenReviews"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                List<Review> reviews = await response.Content.ReadAsAsync<List<Review>>();
                for(int x = 0; x < reviews.Count; x++)
                {
                    reviews[x].hidden = true;
                }
                return reviews;
            }
        }

        public async Task<double> GetRating(int itemid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemid }
            };

            var response = await Utils.Request(client, _config.GetSection("GetRating"), values);

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


            var response = await Utils.Request(client, _config.GetSection("GetReview"), values);

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

        public async Task<List<Review>> GetReviews(int? itemid, int? customerid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemid },
                { "customer-id", customerid }
            };

            var response = await Utils.Request(client, _config.GetSection("GetReviews"), values);

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

        public async Task HideReview(int reviewid)
        {

            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "review-id", reviewid }
            };

            var response = await Utils.Request(client, _config.GetSection("HideReview"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task UnhideReview(int reviewid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "review-id", reviewid }
            };

            var response = await Utils.Request(client, _config.GetSection("UnhideReview"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }
    }
}
