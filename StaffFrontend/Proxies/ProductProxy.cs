using Microsoft.EntityFrameworkCore.Internal;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Data
{

    public interface IProductProxy
    {
        List<Product> GetProducts(string? name, bool? visible, double? minprice, double? maxprice);

        Product GetProduct(int itemid);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(int itemid);
    }

    public class ProductProxyLocal : IProductProxy
    {

        private List<Product> products;

        public ProductProxyLocal()
        {
            products = new List<Product>();

            products.Add(new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false});
            products.Add(new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true});
            products.Add(new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true});
        }

        public ProductProxyLocal(List<Product> products)
        {
            this.products = products;
        }

        public List<Product> GetProducts(string? name, bool? visible, double? minprice, double? maxprice)
        {
            return products.FindAll(product => (name == null || product.name.Contains(name)) && (visible == null || product.visible == visible) && (minprice == null || product.price >= minprice) && (maxprice == null || product.price <= maxprice));
        }

        public Product GetProduct(int itemid)
        {
            return products.Find(p => p.id == itemid);
        }

        public void AddProduct(Product product)
        {

            foreach(Product prod in products)
            {
                if(prod.id >= product.id)
                {
                    product.id = prod.id + 1;
                }
            }
            products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            DeleteProduct(product.id);
            products.Add(product);

        }

        public void DeleteProduct(int itemid)
        {
            products.Remove(products.Find(p => p.id == itemid));
        }
    }

    public class ProductProxyRemote : IProductProxy
    {
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int itemid)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int itemid)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(string? name, bool? visible, double? minprice, double? maxprice)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct( Product product)
        {
            throw new NotImplementedException();
        }
    }
}