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
        private Mock<IRestockProxy> mockRestock;

        private RestockController controller;

        [TestInitialize]
        public void initialize()
        {

            mockRestock = new Mock<IRestockProxy>(MockBehavior.Strict);

            controller = new RestockController(mockRestock.Object);
        }

        [TestMethod]
        public async Task Index_Parameter_Null()
        {
            mockRestock.Setup(s => s.GetSuppliers()).ReturnsAsync(TestData.GetSuppliers());

            var response = await controller.Index();
            Assert.IsNotNull(response);
            var responseOk = response as ViewResult;
            Assert.IsNotNull(responseOk);
            Assert.IsNull(responseOk.StatusCode);

            var model = (List<Supplier>)responseOk.Model;

            for(int x = 0; x < model.Count; x++)
            {
                Assert.AreEqual(TestData.GetSuppliers()[x].supplierID, model[x].supplierID);
                Assert.AreEqual(TestData.GetSuppliers()[x].supplierName, model[x].supplierName);
                Assert.AreEqual(TestData.GetSuppliers()[x].webaddress, model[x].webaddress);
            }

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