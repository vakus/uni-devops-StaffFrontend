using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using StaffFrontend.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ProductProxy
{
    public class ProductProxyRemote : IProductProxy
    {

        HttpClient client;
        IConfigurationSection _config;

        public ProductProxyRemote(HttpClient client, IConfiguration config)
        {
            this.client = client;
            _config = config.GetSection("ProductMicroservice");
        }
        public async Task AddProduct(Product product)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "product-name", product.Name },
                { "product-description", product.Description },
                { "product-price", product.Price },
                { "product-supply", product.Supply },
                { "product-available", product.Available }
            };

            var response = await Utils.Request(client, _config.GetSection("AddProduct"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task DeleteProduct(int itemid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "product-id", itemid }
            };

            var response = await Utils.Request(client, _config.GetSection("DeleteProduct"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }

        public async Task<Product> GetProduct(int itemid)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "product-id", itemid }
            };

            var response = await Utils.Request(client, _config.GetSection("GetProduct"), values);

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

            var response = await Utils.Request(client, _config.GetSection("GetProducts"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
            else
            {
                //TODO: remove filtering once its implemented in remote service
                List<Product> products = await response.Content.ReadAsAsync<List<Product>>();
                if (!String.IsNullOrEmpty(name))
                {
                    products.RemoveAll(p => !p.Name.Contains(name));
                }
                if (minprice.HasValue)
                {
                    products.RemoveAll(p => p.Price < minprice.Value);
                }
                if (maxprice.HasValue)
                {
                    products.RemoveAll(p => p.Price > maxprice.Value);
                }
                return products;
            }
        }

        public async Task UpdateProduct(Product product)
        {
            Dictionary<string, object> values = new Dictionary<string, object>
            {
                { "product-id", product.ID },
                { "product-name", product.Name },
                { "product-description", product.Description },
                { "product-price", product.Price },
                { "product-supply", product.Supply },
                { "product-available", product.Available }
            };

            var response = await Utils.Request(client, _config.GetSection("UpdateProduct"), values);

            if (!response.IsSuccessStatusCode)
            {
                //error occured can not receive information
                throw new SystemException("Could not receive data from remote service");
            }
        }
    }
}
