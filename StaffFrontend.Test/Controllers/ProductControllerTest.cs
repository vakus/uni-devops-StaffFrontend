using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Models.Product;
using StaffFrontend.Proxies;
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
        private Mock<IProductProxy> productMock;
        private Mock<IReviewProxy> reviewMock;

        [TestInitialize]
        public void initTest()
        {
            products = new List<Product>() {
                new Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99m, Available = false},
                new Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99m, Available = true },
                new Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99m, Available = true }
            };

            productMock = new Mock<IProductProxy>(MockBehavior.Strict);
            reviewMock = new Mock<IReviewProxy>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task GetProducts_NoFilters()
        {
            productMock.Setup(m => m.GetProducts(null, null, null, null)).ReturnsAsync(products);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Index(null, null, null, null);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProducts(null, null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterName()
        {
            productMock.Setup(m => m.GetProducts("Lorem Ipsum", null, null, null)).ReturnsAsync(products);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Index("Lorem Ipsum", null, null, null);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProducts("Lorem Ipsum", null, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterVisible()
        {
            productMock.Setup(m => m.GetProducts(null, true, null, null)).ReturnsAsync(products);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Index(null, true, null, null);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProducts(null, true, null, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterMinPrice()
        {
            productMock.Setup(m => m.GetProducts(null, null, 10, null)).ReturnsAsync(products);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Index(null, null, 10, null);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProducts(null, null, 10, null), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_FilterMaxPrice()
        {
            productMock.Setup(m => m.GetProducts(null, null, null, 10)).ReturnsAsync(products);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Index(null, null, null, 10);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProducts(null, null, null, 10), Times.Once);
        }


        [TestMethod]
        public async Task GetProduct()
        {
            productMock.Setup(m => m.GetProduct(1)).ReturnsAsync(products[0]);

            List<Review> reviews = new List<Review>()
            {
                new Review()
                {
                    reviewId = 1,
                    productId = 1,
                    createTime = DateTime.Now,
                    hidden = false,
                    reviewContent = "Ipsum Lorem",
                    reviewRating = 5,
                    userId = 1,
                    userName = "Giorno"
                }
            };

            reviewMock.Setup(m => m.GetReviews(1, null)).ReturnsAsync(reviews);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Details(1);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProduct(1), Times.Once);
            reviewMock.Verify();
            reviewMock.Verify(m => m.GetReviews(1, null), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct()
        {
            productMock.Setup(m => m.AddProduct(products[0])).Returns(Task.Run(() => { })) ;

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Create(products[0]);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.AddProduct(products[0]), Times.Once);
        }

        [TestMethod]
        public async Task EditProductInitial()
        {
            productMock.Setup(m => m.GetProduct(products[0].ID)).ReturnsAsync(products[0]);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Edit(products[0].ID);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProduct(products[0].ID), Times.Once);
        }

        [TestMethod]
        public async Task EditProductPost()
        {
            productMock.Setup(m => m.UpdateProduct(products[0])).Returns(Task.Run(() => { }));

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Edit(products[0].ID, products[0]);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.UpdateProduct(products[0]), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductInitial()
        {
            productMock.Setup(m => m.GetProduct(products[0].ID)).ReturnsAsync(products[0]);

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Delete(products[0].ID);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.GetProduct(products[0].ID), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProductPost()
        {
            productMock.Setup(m => m.DeleteProduct(products[0].ID)).Returns(Task.Run(() => { }));

            var productController = new ProductController(productMock.Object, reviewMock.Object);

            var result = await productController.Delete(products[0].ID);

            Assert.IsNotNull(result);

            productMock.Verify();
            productMock.Verify(m => m.DeleteProduct(products[0].ID), Times.Once);
        }


    }
}