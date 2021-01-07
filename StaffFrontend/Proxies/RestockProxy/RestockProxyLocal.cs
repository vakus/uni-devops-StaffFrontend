using StaffFrontend.Models.Restock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.RestockProxy
{
    public class RestockProxyLocal : IRestockProxy
    {
        private readonly List<Supplier> suppliers;
        private readonly List<List<SupplierProduct>> supplierProducts;
        private readonly List<Restock> restocks;

        public RestockProxyLocal()
        {
            suppliers = new List<Supplier>()
            {
                new Supplier(){
                    supplierID=1,
                    supplierName="test",
                    webaddress="example.com"
                }
            };

            supplierProducts = new List<List<SupplierProduct>>
            {
                new List<SupplierProduct>
                {
                    new SupplierProduct(){
                        id=1,
                        ean="string",
                        brandId=1,
                        brandName="apple",
                        categoryId=1,
                        categoryName="phone",
                        name="iphone 13",
                        description="revolutionary now without screen",
                        price=1399.99m,
                        inStock=true,
                        expectedRestock=DateTime.Now
                    }
                }
            };

            restocks = new List<Restock>
            {
                new Restock()
                {
                    Id = 1,
                    AccountName = "John",
                    Approved = false,
                    ProductEan = "A",
                    ProductID = 1,
                    ProductName = "iphone 13",
                    Qty = 2,
                    SupplierID = 1,
                    TotalPrice = 2799.98m
                },

                new Restock()
                {
                    Id = 1,
                    AccountName = "John",
                    Approved = true,
                    ProductEan = "A",
                    ProductID = 1,
                    ProductName = "iphone 13",
                    Qty = 1,
                    SupplierID = 1,
                    TotalPrice = 1399.99m
                }
            };
        }

        public RestockProxyLocal(
            List<Supplier> suppliers,
            List<List<SupplierProduct>> supplierProducts,
            List<Restock> restocks)
        {
            this.suppliers = suppliers;
            this.supplierProducts = supplierProducts;
            this.restocks = restocks;
        }

        public Task<List<Restock>> GetRestocks(string accountName, int? supplierId, bool? approved)
        {
            return Task.FromResult(restocks.Where(r =>
                (String.IsNullOrEmpty(accountName) || r.AccountName == accountName)
                && (!supplierId.HasValue || r.SupplierID == supplierId.Value)
                && (!approved.HasValue || r.Approved == approved.Value)).ToList()
            );
        }

        public Task<List<Supplier>> GetSuppliers()
        {
            return Task.FromResult(suppliers);
        }

        public Task<List<SupplierProduct>> GetSuppliersProducts(int id)
        {
            return Task.FromResult(supplierProducts[id]);
        }
    }
}
