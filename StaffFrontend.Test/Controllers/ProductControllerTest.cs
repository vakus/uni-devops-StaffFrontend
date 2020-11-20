using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Proxies;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.Test.Controllers
{
    [TestClass]
    // FIXME: test in this file only check if mock was called right amount of times, but doesnt check the data returned by the controller.
    public class ProductControllerTest
    {

        private List<Product> products;
        private Mock<IProductProxy> mock;

        [TestInitialize]
        public void initTest()
        {
            products = new List<Product>() {
                new Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99, Available = false},
                new Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99, Available = true },
                new Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99, Available = true }
            };

            mock = new Mock<IProductProxy>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetProducts_NoFilters()
        {
            mock.Setup(m => m.GetProducts(null, null, null, null)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index(null, null, null, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProducts(null, null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterName()
        {
            mock.Setup(m => m.GetProducts("Lorem Ipsum", null, null, null)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index("Lorem Ipsum", null, null, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProducts("Lorem Ipsum", null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterVisible()
        {
            mock.Setup(m => m.GetProducts(null, true, null, null)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index(null, true, null, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProducts(null, true, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterMinPrice()
        {
            mock.Setup(m => m.GetProducts(null, null, 10, null)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index(null, null, 10, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProducts(null, null, 10, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterMaxPrice()
        {
            mock.Setup(m => m.GetProducts(null, null, null, 10)).ReturnsAsync(products);

            var productController = new ProductController(mock.Object);

            var result = await productController.Index(null, null, null, 10);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProducts(null, null, null, 10), Times.Once);
        }


        [TestMethod]
        public async Task GetProduct()
        {
            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProduct(1)).ReturnsAsync(products[0]);

            var productController = new ProductController(mock.Object);

            var result = await productController.Details(1);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProduct(1), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct()
        {
            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.AddProduct(products[0])).Returns(Task.Run(() => { })) ;

            var productController = new ProductController(mock.Object);

            var result = await productController.Create(products[0]);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.AddProduct(products[0]), Times.Once);
        }

        [TestMethod]
        public async Task EditProductInitial()
        {
            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProduct(products[0].ID)).ReturnsAsync(products[0]);

            var productController = new ProductController(mock.Object);

            var result = await productController.Edit(products[0].ID);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProduct(products[0].ID), Times.Once);
        }

        [TestMethod]
        public async Task EditProductPost()
        {
            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.UpdateProduct(products[0])).Returns(Task.Run(() => { }));

            var productController = new ProductController(mock.Object);

            var result = await productController.Edit(products[0]);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.UpdateProduct(products[0]), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductInitial()
        {
            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProduct(products[0].ID)).ReturnsAsync(products[0]);

            var productController = new ProductController(mock.Object);

            var result = await productController.Delete(products[0].ID);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProduct(products[0].ID), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductPost()
        {
            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.DeleteProduct(products[0].ID)).Returns(Task.Run(() => { }));

            var productController = new ProductController(mock.Object);

            var result = await productController.Delete(products[0].ID, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.DeleteProduct(products[0].ID), Times.Once);
        }


    }
}