using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models.Restock;
using StaffFrontend.Proxies.RestockProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Restocks
{
    [TestClass]
    public class ViewOrders
    {
        private List<Supplier> suppliers;
        private List<Restock> restocks;

        private Mock<IRestockProxy> mockRestock;

        private RestockController controller;

        [TestInitialize]
        public void initialize()
        {
            mockRestock = new Mock<IRestockProxy>(MockBehavior.Strict);

            controller = new RestockController(mockRestock.Object);
        }


        [TestMethod]
        public async Task ViewOrder_Parameters_Null()
        {

        }
    }
}
