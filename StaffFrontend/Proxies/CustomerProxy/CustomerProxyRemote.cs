﻿using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.CustomerProxy
{
    public class CustomerProxyRemote : ICustomerProxy
    {

        IHttpClientFactory _clientFactory;
        IConfigurationSection _config;

        public CustomerProxyRemote(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _config = config.GetSection("CustomerMicroservice");
            _clientFactory = clientFactory;
        }
        public Task DeleteCustomer(int customerid)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomer(int customerid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "customer-id", customerid }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetCustomers"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<Customer>();
            }

        }

        public async Task<List<Customer>> GetCustomers(bool excludeDeleted)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "exclude-deleted", excludeDeleted }
            };

            var client = _clientFactory.CreateClient();

            var response = await Utils.Request(client, _config.GetSection("GetCustomers"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                return await response.Content.ReadAsAsync<List<Customer>>();
            }
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
