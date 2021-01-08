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
    }
}
