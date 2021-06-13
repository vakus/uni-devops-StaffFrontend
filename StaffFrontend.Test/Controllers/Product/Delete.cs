using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffFrontend.Controllers;
using StaffFrontend.Models;
using StaffFrontend.Proxies;
using StaffFrontend.Proxies.ProductProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StaffFrontend.test.Controllers.Product
{
    [TestClass]
    public class Delete
    {

        private Mock<IProductProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private ProductController controller;

        [TestInitialize]
        public void initialize()
        {
            mockCustomer = new Mock<IProductProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new ProductController(mockCustomer.Object, mockReview.Object);
        }



        [TestMethod]
        public async Task Pre_Delete_Parameters_Valid()
        {
            foreach (Models.Product.Product product in TestData.GetProducts())
            {
                mockCustomer.Setup(s => s.GetProduct(product.ID)).ReturnsAsync(TestData.GetProducts().Find(c => c.ID == product.ID));

                var response = await controller.Delete(product.ID);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                var model = (Models.Product.Product)responseOk.Model;

                Assert.AreEqual(product.ID, model.ID);
                Assert.AreEqual(product.Name, model.Name);
                Assert.AreEqual(product.Description, model.Description);
                Assert.AreEqual(product.Supply, model.Supply);
                Assert.AreEqual(product.Price, model.Price);
                Assert.AreEqual(product.Available, model.Available);

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
                mockCustomer.Setup(s => s.GetProduct(id)).ReturnsAsync(TestData.GetProducts().Find(c => c.ID == id));

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
            foreach (Models.Product.Product product in TestData.GetProducts())
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
            foreach (Models.Product.Product product in TestData.GetProducts())
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
            foreach (Models.Product.Product product in TestData.GetProducts())
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
