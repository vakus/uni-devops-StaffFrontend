﻿using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using StaffFrontend.Models.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ProductProxy
{

    public interface IProductProxy
    {
        Task<List<Product>> GetProducts(string name, bool? visible, decimal? minprice, decimal? maxprice);

        Task<Product> GetProduct(int itemid);

        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int itemid);
    }
}