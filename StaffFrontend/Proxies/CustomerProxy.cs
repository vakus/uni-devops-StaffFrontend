using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{
    public interface ICustomerProxy
    {
        Task<List<Customer>> GetCustomers();

        Task<Customer> GetCustomer(int customerid);

        Task UpdateCustomer(Customer customer);
    }

    public class CustomerProxyLocal : ICustomerProxy
    {

        private List<Customer> customers;

        public CustomerProxyLocal()
        {
            customers = new List<Customer>();

            customers.Add(new Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false });
            customers.Add(new Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true });
            customers.Add(new Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true });
            customers.Add(new Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false });
            customers.Add(new Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true });
        }

        public CustomerProxyLocal(List<Customer> customers)
        {
            this.customers = customers;
        }

        public Task<Customer> GetCustomer(int customerid)
        {
            return Task.FromResult(customers.Find(customer => customer.id == customerid));
        }

        public Task<List<Customer>> GetCustomers()
        {
            return Task.FromResult(customers);
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
        public async Task<Customer> GetCustomer(int customerid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }

}
