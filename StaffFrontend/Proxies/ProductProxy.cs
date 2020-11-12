using Microsoft.EntityFrameworkCore.Internal;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ProductProxyLocal : IProductProxy
    {

        private List<Product> products;

        public ProductProxyLocal()
        {
            products = new List<Product>();

            products.Add(new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false });
            products.Add(new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true });
            products.Add(new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true });
        }

        public ProductProxyLocal(List<Product> products)
        {
            this.products = products;
        }

        public Task<List<Product>> GetProducts(string name, bool? visible, double? minprice, double? maxprice)
        {
            return Task.FromResult(products.FindAll(product => 
            (name == null || product.name.Contains(name))
            && (!visible.HasValue || product.visible == visible.Value)
            && (!minprice.HasValue || product.price >= minprice.Value)
            && (!maxprice.HasValue || product.price <= maxprice.Value)));
        }

        public Task<Product> GetProduct(int itemid)
        {
            return Task.FromResult(products.Find(p => p.id == itemid));
        }

        public Task AddProduct(Product product)
        {
            return Task.Run(() =>
            {
                foreach (Product prod in products)
                {
                    if (prod.id >= product.id)
                    {
                        product.id = prod.id + 1;
                    }
                }
                products.Add(product);
            });
        }

        public Task UpdateProduct(Product product)
        {
            return Task.Run(() =>
            {
                DeleteProduct(product.id);
                products.Add(product);
            });

        }

        public Task DeleteProduct(int itemid)
        {
            return Task.Run(() =>
            {
                products.Remove(products.Find(p => p.id == itemid));
            });
        }
    }

    public class ProductProxyRemote : IProductProxy
    {
        public Task AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int itemid)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(int itemid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProducts(string name, bool? visible, double? minprice, double? maxprice)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}