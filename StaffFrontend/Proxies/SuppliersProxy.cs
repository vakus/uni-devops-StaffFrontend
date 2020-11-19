using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{
    public interface ISuppliersProxy
    {

        Task<List<Supplier>> GetSuppliers();

        Task<Supplier> GetSupplier(int supplierid);

        Task UpdateSupplier(Supplier supplier);

        Task DeleteSupplier(Supplier supplier);
    }

    public class SuppliersProxyLocal : ISuppliersProxy
    {
        List<Supplier> suppliers;

        public SuppliersProxyLocal()
        {
            suppliers = new List<Supplier>(){
                new Supplier(){ id=1, name="Teesside", items=new List<int>(){ 1, 2, 3} },
                new Supplier(){ id=2, name="idk co", items=new List<int>(){ 1, 2} }
            };
        }

        public SuppliersProxyLocal(List<Supplier> suppliers)
        {
            this.suppliers = suppliers;
        }

        public Task DeleteSupplier(Supplier supplier)
        {
            return Task.Run(() => {
                suppliers.RemoveAll(s => s.id == supplier.id);
            });
        }

        public Task<Supplier> GetSupplier(int supplierid)
        {
            return Task.FromResult(suppliers.Find(s => s.id == supplierid));
        }

        public Task<List<Supplier>> GetSuppliers()
        {
            return Task.FromResult(suppliers);
        }

        public Task UpdateSupplier(Supplier supplier)
        {
            return Task.Run(() => {
                suppliers.RemoveAll(s => s.id == supplier.id);
                suppliers.Add(supplier);
            });
        }
    }

    public class SuppliersProxyRemote : ISuppliersProxy
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