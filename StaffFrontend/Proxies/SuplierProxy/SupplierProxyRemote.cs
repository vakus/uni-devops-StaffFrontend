using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.SuplierProxy
{

    public class SupplierProxyRemote : ISupplierProxy
    {
        public Task DeleteSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> GetSupplier(int supplierid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Supplier>> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplier(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
