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
            suppliers = new List<Supplier>()
            {
                new Supplier(){
                    supplierID=1,
                    supplierName="test",
                    webaddress="example.com"
                }
            };

            restocks = new List<Restock>
            {
                new Restock()
                {
                    RestockId = 1,
                    AccountName = "John",
                    Approved = false,
                    ProductEan = "A",
                    ProductID = 1,
                    ProductName = "iphone 13",
                    Gty = 2,
                    SupplierID = 1,
                    TotalPrice = 2799.98m
                },

                new Restock()
                {
                    RestockId = 1,
                    AccountName = "John",
                    Approved = true,
                    ProductEan = "A",
                    ProductID = 1,
                    ProductName = "iphone 13",
                    Gty = 1,
                    SupplierID = 1,
                    TotalPrice = 1399.99m
                }
            };

            mockRestock = new Mock<IRestockProxy>(MockBehavior.Strict);

            controller = new RestockController(mockRestock.Object);
        }


        [TestMethod]
        public async Task ViewOrder_Parameters_Null()
        {

        }
    }
}
