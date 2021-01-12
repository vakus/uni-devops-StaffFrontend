using StaffFrontend.Models;
using StaffFrontend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.CustomerProxy
{
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
            return Task.Run(async () =>
            {
                Customer customer = await GetCustomer(customerid);

                if(customer == null)
                {
                    return;
                }

                customer.surname = "REDACTED";
                customer.firstname = "REDACTED";
                customer.address = "REDACTED";
                customer.contact = "REDACTED";
                customer.canPurchase = false;
                customer.isDeleted = true;

                customers.RemoveAll(cust => cust.id == customer.id);
                customers.Add(customer);
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
}
