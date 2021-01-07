using StaffFrontend.Models.Restock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.RestockProxy
{
    public class RestockProxyRemote : IRestockProxy
    {
        public Task<List<Restock>> GetRestocks(string accountName, int? supplierId, bool? approved)
        {
            throw new NotImplementedException();
        }

        public Task<List<Supplier>> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public Task<List<SupplierProduct>> GetSuppliersProducts(int id)
        {
            throw new NotImplementedException();
        }
    }
}
