using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.SuplierProxy
{

    public class SupplierProxyLocal : ISupplierProxy
    {
        List<Supplier> suppliers;

        public SupplierProxyLocal()
        {
            suppliers = new List<Supplier>(){
                new Supplier(){ SupplierId=1, SupplierName="Teesside", SupplierAddress="teeside", SupplierContactNumber="69420-177013", SupplierEmail="t33ss1d3@t33ss1d3.ac.co.uk.com.gov.uk"},
                new Supplier(){ SupplierId=2, SupplierName="idk co", SupplierAddress="london", SupplierContactNumber="123456789", SupplierEmail="email@email.co.uk"}
            };
        }

        public SupplierProxyLocal(List<Supplier> suppliers)
        {
            this.suppliers = suppliers;
        }

        public Task DeleteSupplier(int supplierid)
        {
            return Task.Run(() => {
                suppliers.RemoveAll(s => s.SupplierId == supplierid);
            });
        }

        public Task<Supplier> GetSupplier(int supplierid)
        {
            return Task.FromResult(suppliers.Find(s => s.SupplierId == supplierid));
        }

        public Task<List<Supplier>> GetSuppliers()
        {
            return Task.FromResult(suppliers);
        }

        public Task UpdateSupplier(Supplier supplier)
        {
            return Task.Run(() => {
                suppliers.RemoveAll(s => s.SupplierId == supplier.SupplierId);
                suppliers.Add(supplier);
            });
        }
    }
}
