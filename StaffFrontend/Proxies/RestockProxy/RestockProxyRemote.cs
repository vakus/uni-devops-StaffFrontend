using Microsoft.Extensions.Configuration;
using StaffFrontend.Models.Restock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.RestockProxy
{
    public class RestockProxyRemote : IRestockProxy
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfigurationSection _config;

        public RestockProxyRemote(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config.GetSection("RestockMicroservice");
        }

        public async Task ApproveRestock(int restockid, string accountName, string CardNumber)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", restockid },
                { "account-name", accountName },
                { "card-number", CardNumber }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("ApproveRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task CreateRestock(int supplierid, string accountName, int productid, int quantity)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "supplier-id", supplierid },
                { "account-name", accountName },
                { "product-id", productid },
                { "product-quantity", quantity }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("CreateRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task<List<Restock>> GetRestocks(int? id, string accountName, int? supplierId, bool? approved)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", id },
                { "account-name", accountName },
                { "supplier-id", supplierId },
                { "approved", approved }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetRestocks"), values);

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

        public async Task<List<Supplier>> GetSuppliers()
        {
            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetSuppliers"), new Dictionary<string, object>());

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<List<Supplier>>();
            }
        }

        public async Task<List<SupplierProduct>> GetSuppliersProducts(int id)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "supplier-id", id }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetSuppliersProducts"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<List<SupplierProduct>>();
            }
        }

        public async Task RejectRestock(int restockid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "restock-id", restockid }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("RejectRestock"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }
    }
}
