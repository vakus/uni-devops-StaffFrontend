﻿using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ProductProxy
{
    public class ProductProxyRemote : IProductProxy
    {

        IHttpClientFactory _clientFactory;
        IConfigurationSection _config;

        public ProductProxyRemote(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config.GetSection("ProductMicroservice");
        }
        public Task AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int itemid)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(int itemid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "item-id", itemid }
            };

            string url = Utils.createUriBuilder(_config.GetSection("GetProduct"), values).ToString();

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
                return await response.Content.ReadAsAsync<Product>();
            }
        }

        public async Task<List<Product>> GetProducts(string name, bool? visible, decimal? minprice, decimal? maxprice)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "product-name", name },
                { "product-visible", visible },
                { "price-min", minprice },
                { "price-max", maxprice }
            };

            string url = Utils.createUriBuilder(_config.GetSection("GetProducts"), values).ToString();

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
                return await response.Content.ReadAsAsync<List<Product>>();
            }
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
