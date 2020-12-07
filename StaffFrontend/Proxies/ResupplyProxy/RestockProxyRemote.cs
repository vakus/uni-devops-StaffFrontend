using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ResupplyProxy
{
    public class RestockProxyRemote : IRestockProxy
    {

        private IHttpClientFactory _clientFactory;
        private IConfigurationSection _config;
        public RestockProxyRemote(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config.GetSection("RestockMicroservice");
        }

        public async Task CreateRestock(Restock restock)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", restock.restockId },
                { "restock-supplier-id", restock.supplierId },
                { "restock-product-id", restock.sProductId },
                { "restock-product-quantity", restock.quantity },
                { "restock-price", restock.price },
                { "restock-date", restock.date },
                { "restock-approved", restock.approved }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("CreateRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task DeleteRestock(int restockId)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", restockId }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("DeleteRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task<List<Restock>> GetRestocks()
        {

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetRestocks"), new Dictionary<string, object> { });

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<List<Restock>>();
            }
        }

        public async Task<Restock> GetRestock(int restockId)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", restockId }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<Restock>();
            }
        }

        public async Task UpdateRestock(Restock restock)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", restock.restockId },
                { "restock-supplier-id", restock.supplierId },
                { "restock-product-id", restock.sProductId },
                { "restock-product-quantity", restock.quantity },
                { "restock-price", restock.price },
                { "restock-date", restock.date },
                { "restock-approved", restock.approved }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("EditRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }
    }
}
