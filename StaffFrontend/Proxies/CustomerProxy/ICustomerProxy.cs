using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using StaffFrontend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace StaffFrontend.Proxies.CustomerProxy
{
    public interface ICustomerProxy
    {
        Task<List<Customer>> GetCustomers(bool excludeDeleted);

        Task<Customer> GetCustomer(int customerid);

        Task UpdateCustomer(Customer customer);

        Task DeleteCustomer(int customerid);
    }
}
