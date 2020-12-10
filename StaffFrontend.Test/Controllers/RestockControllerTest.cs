using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies.RestockProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Test.Controllers
{
    [TestClass]
    public class RestockControllerTest
    {

        private List<Restock> restocks;
        private Mock<IRestockProxy> restockMock;

        [TestInitialize]
        public void initialize()
        {
            restocks = new List<Restock>
            {
                new Restock()
                {
                    restockId = "1",
                    supplierId = "1",
                    sProductId = "1",
                    quantity = 5,
                    price = 4.99m,
                    date = DateTime.Now,
                    approved = false
                }
            };

            restockMock = new Mock<IRestockProxy>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetRestocks()
        {
            restockMock.Setup(m => m.GetRestocks()).ReturnsAsync(restocks);

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Index();

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.GetRestocks(), Times.Once);
        }

        [TestMethod]
        public async Task GetRestock()
        {
            restockMock.Setup(m => m.GetRestock(1)).ReturnsAsync(restocks.First());

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Details(1);

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.GetRestock(1), Times.Once);
        }

        [TestMethod]
        public async Task GetRestock_Invalid()
        {
            restockMock.Setup(m => m.GetRestock(2)).Returns(Task.FromResult<Restock>(null));

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Details(2);

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.GetRestock(2), Times.Once);
        }

        [TestMethod]
        public void CreateRestock_Initial()
        {
            var restockController = new RestockController(restockMock.Object);

            var result = restockController.Create();

            Assert.IsNotNull(result);

            restockMock.Verify();
        }

        [TestMethod]
        public async Task CreateRestock_Create()
        {
            restockMock.Setup(m => m.CreateRestock(restocks[0])).Returns(Task.Run(() => { }));

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Create(restocks[0]);

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.CreateRestock(restocks[0]), Times.Once);
        }

        [TestMethod]
        public async Task UpdateRestock_Initial()
        {
            int restockId = int.Parse(restocks[0].restockId);

            restockMock.Setup(m => m.GetRestock(restockId)).ReturnsAsync(restocks[0]);

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Edit(restockId);

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.GetRestock(restockId), Times.Once);
        }

        [TestMethod]
        public async Task UpdateRestock_Update()
        {
            restockMock.Setup(m => m.UpdateRestock(restocks[0])).Returns(Task.Run(() => { }));

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Edit(int.Parse(restocks[0].restockId), restocks[0]);

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.UpdateRestock(restocks[0]), Times.Once);
        }

        [TestMethod]
        public async Task DeleteRestock_Initial()
        {
            int restockId = int.Parse(restocks[0].restockId);

            restockMock.Setup(m => m.GetRestock(restockId)).ReturnsAsync(restocks[0]);

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Delete(restockId);

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.GetRestock(restockId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteRestock_Delete()
        {
            int restockId = int.Parse(restocks[0].restockId);

            restockMock.Setup(m => m.DeleteRestock(restockId)).Returns(Task.Run(() => { }));

            var restockController = new RestockController(restockMock.Object);

            var result = await restockController.Delete(restockId, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), null));

            Assert.IsNotNull(result);

            restockMock.Verify();
            restockMock.Verify(m => m.DeleteRestock(restockId), Times.Once);
        }
    }
}
