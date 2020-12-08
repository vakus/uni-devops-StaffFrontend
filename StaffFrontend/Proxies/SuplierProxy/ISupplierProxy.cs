using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{
    public interface ISupplierProxy
    {

        Task<List<Supplier>> GetSuppliers();

        Task<Supplier> GetSupplier(int supplierid);

        Task UpdateSupplier(Supplier supplier);

        Task DeleteSupplier(int supplierid);

        Task CreateSupplier(Supplier supplier);
    }

}