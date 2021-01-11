using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using StaffFrontend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.CustomerProxy
{
    public class CustomerProxyRemote : ICustomerProxy
    {

        HttpClient client;
        IConfigurationSection _config;

        public CustomerProxyRemote(HttpClient client, IConfiguration config)
        {
            _config = config.GetSection("CustomerMicroservice");
            this.client = client;
        }
        public async Task DeleteCustomer(int customerid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "customer-id", customerid }
            };

            var response = await Utils.Request(client, _config.GetSection("DeleteCustomer"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task<Customer> GetCustomer(int customerid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "customer-id", customerid }
            };

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

        public async Task UpdateCustomer(Customer customer)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "customer-id", customer.id },
                { "customer-surname", customer.surname },
                { "customer-firstname", customer.firstname },
                { "customer-address", customer.address },
                { "customer-contact", customer.contact }
            };

            var response = await Utils.Request(client, _config.GetSection("UpdateCustomer"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }
    }
}
