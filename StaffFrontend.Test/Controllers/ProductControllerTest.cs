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
    // FIXME: test in this file only check if mock was called right amount of times, but doesnt check the data returned by the controller.
    public class ProductControllerTest
    {
        [TestMethod]
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

        [TestMethod]
        public async Task GetProducts_FilterVisible()
        {
            List<Product> products = new List<Product>() {
                new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false},
                new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true },
                new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true }
            };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
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
            List<Product> products = new List<Product>() {
                new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false},
                new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true },
                new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true }
            };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
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
            List<Product> products = new List<Product>() {
                new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false},
                new Product() { id = 2, name = "Duck", description = "Sometimes makes quack sound", price = 99.99, visible = true },
                new Product() { id = 3, name = "IPhone 13 pro max ultra plus 6G no screen edition", description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", price = 1399.99, visible = true }
            };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
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
            Product product = new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProduct(1)).ReturnsAsync(product);

            var productController = new ProductController(mock.Object);

            var result = await productController.Details(1);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProduct(1), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct()
        {
            Product product = new Product() { name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.AddProduct(product)).Returns(Task.Run(() => { })) ;

            var productController = new ProductController(mock.Object);

            var result = await productController.Create(product);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.AddProduct(product), Times.Once);
        }

        [TestMethod]
        public async Task EditProductInitial()
        {
            Product product = new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProduct(product.id)).ReturnsAsync(product);

            var productController = new ProductController(mock.Object);

            var result = await productController.Edit(product.id);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProduct(product.id), Times.Once);
        }

        [TestMethod]
        public async Task EditProductPost()
        {
            Product product = new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.UpdateProduct(product)).Returns(Task.Run(() => { }));

            var productController = new ProductController(mock.Object);

            var result = await productController.Edit(product);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.UpdateProduct(product), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductInitial()
        {
            Product product = new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.GetProduct(product.id)).ReturnsAsync(product);

            var productController = new ProductController(mock.Object);

            var result = await productController.Delete(product.id);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.GetProduct(product.id), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductPost()
        {
            Product product = new Product() { id = 1, name = "Lorem Ipsum", description = "Lorem Ipsum", price = 5.99, visible = false };

            var mock = new Mock<IProductProxy>(MockBehavior.Strict);
            mock.Setup(m => m.DeleteProduct(product.id)).Returns(Task.Run(() => { }));

            var productController = new ProductController(mock.Object);

            var result = await productController.Delete(product.id, null);

            Assert.IsNotNull(result);

            mock.Verify();
            mock.Verify(m => m.DeleteProduct(product.id), Times.Once);
        }


    }
}