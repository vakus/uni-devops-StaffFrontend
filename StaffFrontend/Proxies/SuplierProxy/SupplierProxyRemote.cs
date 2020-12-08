using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.SuplierProxy
{

    public class SupplierProxyRemote : ISupplierProxy
    {

        private IHttpClientFactory _clientFactory;
        private IConfigurationSection _config;
        public SupplierProxyRemote(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config.GetSection("SupplierMicroservice");
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "supplier-id", supplier.SupplierId },
                { "supplier-name", supplier.SupplierName },
                { "supplier-email", supplier.SupplierEmail },
                { "supplier-phone", supplier.SupplierContactNumber },
                { "supplier-address", supplier.SupplierAddress }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("CreateSupplier"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task DeleteSupplier(int supplierid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "supplier-id", supplierid }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("DeleteSupplier"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task<Supplier> GetSupplier(int supplierid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "supplier-id", supplierid }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetSupplier"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<Supplier>();
            }
        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetSuppliers"), new Dictionary<string, object> { });

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

        public async Task UpdateSupplier(Supplier supplier)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "supplier-id", supplier.SupplierId },
                { "supplier-name", supplier.SupplierName },
                { "supplier-email", supplier.SupplierEmail },
                { "supplier-phone", supplier.SupplierContactNumber },
                { "supplier-address", supplier.SupplierAddress }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("EditSupplier"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }
    }
}
