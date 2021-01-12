using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Product
{
    [TestClass]
    public class Delete
    {

        private List<Models.Product.Product> products;
        private List<Review> reviews;

        private Mock<IProductProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private ProductController controller;

        [TestInitialize]
        public void initialize()
        {
            products = new List<Models.Product.Product>() {

            new Models.Product.Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99m, Available = false, Supply = 2 },
            new Models.Product.Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99m, Available = true, Supply = 20 },
            new Models.Product.Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99m, Available = true, Supply = 13 }
        };


            reviews = new List<Review>()
            {
                new Review()
                {
                    userId = 1,
                    userName = "John",
                    reviewId = 1,
                    reviewContent = "good",
                    reviewRating = 4,
                    productId = 1,
                    hidden = false,
                },
                new Review()
                {
                    userId = 1,
                    userName = "John",
                    reviewId = 2,
                    reviewContent = "follow me on twitter",
                    reviewRating = 4,
                    productId = 3,
                    hidden = true,
                },
                new Review()
                {
                    userId = 1,
                    userName = "John",
                    reviewId = 3,
                    reviewContent = "good",
                    reviewRating = 5,
                    productId = 2,
                    hidden = false,
                },
                new Review()
                {
                    userId = 2,
                    userName = "Bethany",
                    reviewId = 4,
                    reviewContent = "decent",
                    reviewRating = 3,
                    productId = 1,
                    hidden = false,
                },
                new Review()
                {
                    userId = 3,
                    userName = "Brigid",
                    reviewId = 5,
                    reviewContent = "",
                    reviewRating = 5,
                    productId = 1,
                    hidden = true,
                }
            };

            mockCustomer = new Mock<IProductProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new ProductController(mockCustomer.Object, mockReview.Object);
        }



        [TestMethod]
        public async Task Pre_Delete_Parameters_Valid()
        {
            foreach (Models.Product.Product product in products)
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ReturnsAsync(products.Find(c => c.ID == product.ID));

                var response = await controller.Delete(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                Assert.AreEqual(product, responseOk.Model);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(product.ID), Times.Once);
            }
        }

        [TestMethod]
        public async Task Pre_Delete_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetProduct(id)).ReturnsAsync(products.Find(c => c.ID == id));

                var response = await controller.Delete(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Pre_Delete_Parameters_Valid_Throws()
        {
            foreach (Models.Product.Product product in products)
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(product.ID), Times.Once);
            }
        }

        [TestMethod]
        public async Task Pre_Delete_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetProduct(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetProduct(id), Times.Once);
            }
        }




        [TestMethod]
        public async Task Post_Delete_Parameters_Valid()
        {
            foreach (Models.Product.Product product in products)
            {
                mockCustomer.Setup(s => s.DeleteProduct(product.ID)).Returns(Task.Run(()=> { }));
                mockReview.Setup(s => s.DeleteByProductId(product.ID)).Returns(Task.Run(() => { }));

                var response = await controller.Delete(product.ID, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as RedirectToActionResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteProduct(product.ID), Times.Once);
                mockReview.Verify(s => s.DeleteByProductId(product.ID), Times.Once);
            }
        }

        [TestMethod]
        public async Task Post_Delete_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.DeleteProduct(id)).Returns(Task.Run(() => { }));
                mockReview.Setup(s => s.DeleteByProductId(id)).Returns(Task.Run(() => { }));

                var response = await controller.Delete(id, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as RedirectToActionResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteProduct(id), Times.Once);
                mockReview.Verify(s => s.DeleteByProductId(id), Times.Once);
            }
        }


        [TestMethod]
        public async Task Post_Delete_Parameters_Valid_Throws()
        {
            foreach (Models.Product.Product product in products)
            {
                mockCustomer.Setup(s => s.DeleteProduct(product.ID)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.DeleteByProductId(product.ID)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(product.ID, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteProduct(product.ID), Times.Once);
                mockReview.Verify(s => s.DeleteByProductId(product.ID), Times.AtMostOnce);
            }
        }

        [TestMethod]
        public async Task Post_Delete_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.DeleteProduct(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.DeleteByProductId(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Delete(id, new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()));
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.DeleteProduct(id), Times.Once);
                mockReview.Verify(s => s.DeleteByProductId(id), Times.AtMostOnce);
            }
        }
    }
}
