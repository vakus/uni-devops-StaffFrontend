using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Models.Restock;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.RestockProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Restocks
{
    [TestClass]
    public class Index
    {

        private List<Supplier> suppliers;

        private Mock<IRestockProxy> mockRestock;

        private RestockController controller;

        [TestInitialize]
        public void initialize()
        {
            suppliers = new List<Supplier>()
            {
                new Supplier(){
                    supplierID=1,
                    supplierName="test",
                    webaddress="example.com"
                }
            };

            mockRestock = new Mock<IRestockProxy>(MockBehavior.Strict);

            controller = new RestockController(mockRestock.Object);
        }

        [TestMethod]
        public async Task Index_Parameter_Null()
        {
            mockRestock.Setup(s => s.GetSuppliers()).ReturnsAsync(suppliers);

            var response = await controller.Index();
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);
            Assert.IsTrue(suppliers.SequenceEqual((IEnumerable<Supplier>)responseOk.Model));

            mockRestock.Verify();
            mockRestock.Verify(s => s.GetSuppliers(), Times.Once);
        }

        [TestMethod]
        public async Task Index_Throw_Parameter_Null()
        {
            mockRestock.Setup(s => s.GetSuppliers()).ThrowsAsync(new SystemException("Could not receive data from remote service"));

            var response = await controller.Index();
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            mockRestock.Verify();
            mockRestock.Verify(s => s.GetSuppliers(), Times.Once);
        }
    }
}