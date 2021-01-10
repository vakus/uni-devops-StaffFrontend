using StaffFrontend.Models.Restock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.RestockProxy
{
    public interface IRestockProxy
    {
        public Task<List<Supplier>> GetSuppliers();

        public Task<List<Restock>> GetRestocks(int? id, string accountName, int? supplierId, bool? approved);

        public Task<List<SupplierProduct>> GetSuppliersProducts(int id);

        public Task CreateRestock(int supplierid, string accountName, int productid, int quantity);

        public Task ApproveRestock(int restockid, string accountName, string CardNumber);

        public Task RejectRestock(int restockid);
    }
}