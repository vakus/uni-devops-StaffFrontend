using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace StaffFrontend.Proxies
{
    public interface ICustomerProxy
    {
        Task<List<Customer>> GetCustomers(bool excludeDeleted);

        Task<Customer> GetCustomer(int customerid);

        Task UpdateCustomer(Customer customer);

        Task DeleteCustomer(int customerid);
    }

    public class CustomerProxyLocal : ICustomerProxy
    {

        private List<Customer> customers;

        public CustomerProxyLocal()
        {
            customers = new List<Customer>();

            customers.Add(new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false, isDeleted = false });
            customers.Add(new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true, isDeleted = false });
            customers.Add(new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true, isDeleted = false });
            customers.Add(new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false, isDeleted = false });
            customers.Add(new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true, isDeleted = false });
        }

        public CustomerProxyLocal(List<Customer> customers)
        {
            this.customers = customers;
        }

        public Task DeleteCustomer(int customerid)
        {
            return Task.Run(() =>
            {
                Customer customer = GetCustomer(customerid).Result;

                customer.surname = "REDACTED";
                customer.firstname = "REDACTED";
                customer.address = "REDACTED";
                customer.contact = "REDACTED";
                customer.canPurchase = false;
                customer.isDeleted = true;

                UpdateCustomer(customer);
            });
        }

        public Task<Customer> GetCustomer(int customerid)
        {
            return Task.FromResult(customers.Find(customer => customer.id == customerid));
        }

        public Task<List<Customer>> GetCustomers(bool excludeDeleted)
        {
            return Task.FromResult(customers.FindAll(c => !c.isDeleted || !excludeDeleted));
        }

        public Task UpdateCustomer(Customer customer)
        {
            return Task.Run(() =>
            {
                customers.RemoveAll(cust => cust.id == customer.id);
                customers.Add(customer);
            });
        }
    }

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
            Dictionary<string, object> values = new Dictionary<string, object> { { "customer-id", customerid } };

            string url = Utils.createUriBuilder(_config.GetSection("GetCustomers"), values).ToString();

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
                return await response.Content.ReadAsAsync<Customer>();
            }

        }

        public async Task<List<Customer>> GetCustomers(bool excludeDeleted)
        {
            Dictionary<string, object> values = new Dictionary<string, object> { { "exclude-deleted", excludeDeleted } };

            string url = Utils.createUriBuilder(_config.GetSection("GetCustomers"), values).ToString();

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
                return await response.Content.ReadAsAsync<List<Customer>>();
            }
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }

}
