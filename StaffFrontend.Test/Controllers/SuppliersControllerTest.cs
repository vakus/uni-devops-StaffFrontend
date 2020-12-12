using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers
{
    [TestClass]
    public class SuppliersControllerTest
    {
        private List<Supplier> suppliers;
        private Mock<ISupplierProxy> supplierMock;

        [TestInitialize]
        public void initialize()
        {
            suppliers = new List<Supplier>
            {
                new Supplier()
                {
                    SupplierId = 1,
                    SupplierName = "tees",
                    SupplierAddress = "middlesbrough",
                    SupplierContactNumber = "123456789",
                    SupplierEmail = "tees@example.com"
                }
            };

            supplierMock = new Mock<ISupplierProxy>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetSuppliers()
        {
            supplierMock.Setup(m => m.GetSuppliers()).ReturnsAsync(suppliers);

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Index();

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.GetSuppliers(), Times.Once);
        }

        [TestMethod]
        public async Task GetSupplier()
        {
            supplierMock.Setup(m => m.GetSupplier(1)).ReturnsAsync(suppliers.First());

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Details(1);

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.GetSupplier(1), Times.Once);
        }

        [TestMethod]
        public void Create_Initial()
        {

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = supplierController.Create();

            Assert.IsNotNull(result);

            supplierMock.Verify();
        }

        [TestMethod]
        public async Task Create_Create()
        {
            Supplier s = suppliers.First();
            supplierMock.Setup(m => m.CreateSupplier(s)).Returns(Task.Run(() => { }));

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Create(s);

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.CreateSupplier(s), Times.Once);
        }

        [TestMethod]
        public async Task Edit_Initial()
        {
            supplierMock.Setup(m => m.GetSupplier(1)).ReturnsAsync(suppliers.First());

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Edit(1);

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.GetSupplier(1), Times.Once);
        }



        [TestMethod]
        public async Task Edit_Edit()
        {
            Supplier s = suppliers.First();
            supplierMock.Setup(m => m.UpdateSupplier(s)).Returns(Task.Run(() => { }));

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Edit(1, s);

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.UpdateSupplier(s), Times.Once);
        }

        [TestMethod]
        public async Task Delete_Initial()
        {
            supplierMock.Setup(m => m.GetSupplier(1)).ReturnsAsync(suppliers.First());

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Delete(1);

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.GetSupplier(1), Times.Once);
        }


        [TestMethod]
        public async Task Delete_Delete()
        {
            supplierMock.Setup(m => m.DeleteSupplier(1)).Returns(Task.Run(() => { }));

            var supplierController = new SuppliersController(supplierMock.Object);

            var result = await supplierController.Delete(1, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), null));

            Assert.IsNotNull(result);

            supplierMock.Verify();
            supplierMock.Verify(m => m.DeleteSupplier(1), Times.Once);
        }

    }
}
