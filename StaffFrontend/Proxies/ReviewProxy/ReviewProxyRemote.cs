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

        public async Task<List<Review>> GetReviews(int? itemid, int? customerid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemid },
                { "customer-id", customerid }
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
