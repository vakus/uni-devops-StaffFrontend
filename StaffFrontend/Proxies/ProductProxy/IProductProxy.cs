using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{

    public interface IProductProxy
    {
        Task<List<Product>> GetProducts(string name, bool? visible, double? minprice, double? maxprice);

        Task<Product> GetProduct(int itemid);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int itemid);
    }
}