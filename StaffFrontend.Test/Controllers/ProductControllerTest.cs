using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Data;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.Test.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        // FIXME: this test only checks if mock was called only once, but doesnt check the data returned by it
        public async Task GetProducts_NoFilters()
        {
            List<Product> products = new List<Product>() {
                new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false},
                new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true },
                new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true }
            };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProducts(null, null, null, null)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index(null, null, null, null);

            Assert.IsNotNull(result);

            /*
            var objResult = result as OkResult;
            Assert.IsNotNull(objResult);
            var productsResult = objResult as IEnumerable<Product>;
            Assert.IsNotNull(productsResult);
            var productsResultList = productsResult.ToList();
            Assert.IsNotNull(productsResultList);
            for(int x = 0; x < products.Count; x++)
            {
                Assert.AreEqual(products[x].id, productsResultList[x].id);
                Assert.AreEqual(products[x].name, productsResultList[x].name);
                Assert.AreEqual(products[x].description, productsResultList[x].description);
                Assert.AreEqual(products[x].price, productsResultList[x].price);
                Assert.AreEqual(products[x].visible, productsResultList[x].visible);
            }*/

            mock.Verify();
            mock.Verify(m => m.GetProducts(null, null, null, null), Times.Once);
        }

        [TestMethod]
        // FIXME: this test only checks if mock was called only once, but doesnt check the data returned by it
        public async Task GetProducts_FilterName()
        {
            List<Product> products = new List<Product>() {
                new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false},
                new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true },
                new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true }
            };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProducts("Lorem Ipsum", null, null, null)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index("Lorem Ipsum", null, null, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProducts("Lorem Ipsum", null, null, null), Times.Once);
        }
    }
}