using StaffFrontend.Models;
using StaffFrontend.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ProductProxy
{
    public class ProductProxyLocal : IProductProxy
    {

        private List<Product> products;

        public ProductProxyLocal()
        {
            products = new List<Product>();

            products.Add(new Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99m, Available = false, Supply = 2 });
            products.Add(new Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99m, Available = true, Supply = 20 });
            products.Add(new Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99m, Available = true, Supply = 13 });
        }

        public ProductProxyLocal(List<Product> products)
        {
            this.products = products;
        }

        public Task<List<Product>> GetProducts(string name, bool? visible, decimal? minprice, decimal? maxprice)
        {
            return Task.FromResult(products.FindAll(product =>
            (name == null || product.Name.Contains(name))
            && (!visible.HasValue || product.Available == visible.Value)
            && (!minprice.HasValue || product.Price >= minprice.Value)
            && (!maxprice.HasValue || product.Price <= maxprice.Value)));
        }

        public Task<Product> GetProduct(int itemid)
        {
            return Task.FromResult(products.Find(p => p.ID == itemid));
        }

        public Task AddProduct(Product product)
        {
            return Task.Run(() =>
            {
                foreach (Product prod in products)
                {
                    if (prod.ID >= product.ID)
                    {
                        product.ID = prod.ID + 1;
                    }
                }
                products.Add(product);
            });
        }

        public Task UpdateProduct(Product product)
        {
            return Task.Run(() =>
            {
                DeleteProduct(product.ID);
                products.Add(product);
            });

        }

        public Task DeleteProduct(int itemid)
        {
            return Task.Run(() =>
            {
                products.Remove(products.Find(p => p.ID == itemid));
            });
        }
    }
}
