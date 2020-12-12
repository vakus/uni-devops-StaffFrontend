using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaffFrontend.Models;
using StaffFrontend.Proxies.SuplierProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Proxies
{
    [TestClass]
    public class SupplierProxyLocalTest
    {

        private List<Supplier> suppliers;
        private SupplierProxyLocal spl;

        [TestInitialize]
        public void initialize()
        {
            suppliers = new List<Supplier>()
            {
                new Supplier(){ SupplierId=1, SupplierName="Teesside", SupplierAddress="teeside", SupplierContactNumber="69420-177013", SupplierEmail="t33ss1d3@t33ss1d3.ac.co.uk.com.gov.uk"},
                new Supplier(){ SupplierId=2, SupplierName="idk co", SupplierAddress="london", SupplierContactNumber="123456789", SupplierEmail="email@email.co.uk"}
            };

            spl = new SupplierProxyLocal(suppliers);
        }

        [TestMethod]
        public async Task GetSuppliers()
        {
            Assert.IsTrue(suppliers.SequenceEqual(await spl.GetSuppliers()));
        }

        [TestMethod]
        public async Task GetSupplier()
        {
            Assert.AreEqual(suppliers.Find(s => s.SupplierId == 1), await spl.GetSupplier(1));
        }

        [TestMethod]
        public async Task GetSupplier_Invalid()
        {
            Assert.IsNull(await spl.GetSupplier(20));
            Assert.IsNull(await spl.GetSupplier(0));
            Assert.IsNull(await spl.GetSupplier(-3));
        }

        [TestMethod]
        public async Task UpdateSupplier()
        {
            Supplier s = suppliers.First();
            s.SupplierAddress = "unknown";
            await spl.UpdateSupplier(s);

            Assert.AreEqual(s, await spl.GetSupplier(s.SupplierId));
        }


        [TestMethod]
        public async Task UpdateSupplier_Invalid()
        {
            Supplier s = new Supplier()
            {
                SupplierId = 20,
                SupplierName = "t33551d3",
                SupplierAddress = "t33551d3",
                SupplierContactNumber = "177013-69420",
                SupplierEmail = "t33ss1d3@t33ss1d3hax.uk"
            };

            await spl.UpdateSupplier(s);

            Assert.IsNull(await spl.GetSupplier(s.SupplierId));
        }
    }
}
