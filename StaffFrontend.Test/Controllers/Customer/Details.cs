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

namespace StaffFrontend.test.Controllers.Customer
{
    [TestClass]
    public class Details
    {

        private List<Models.Customers.Customer> customers;
        private List<Review> reviews;

        private Mock<ICustomerProxy> mockCustomer;
        private Mock<IReviewProxy> mockReview;

        private CustomerController controller;

        [TestInitialize]
        public void initialize()
        {
            customers = new List<Models.Customers.Customer>() {
                new Models.Customers.Customer() { id = 1, firstname = "John", surname = "Smith", address = "0 Manufacturers Circle", contact = "999-250-6512", canPurchase = false, isDeleted=false},
                new Models.Customers.Customer() { id = 2, firstname = "Bethany", surname = "Hulkes", address = "0 Annamark Pass", contact = "893-699-2769", canPurchase = true, isDeleted=false },
                new Models.Customers.Customer() { id = 3, firstname = "Brigid", surname = "Streak", address = "2 Ruskin Crossing", contact = "295-119-1574", canPurchase = true, isDeleted=false },
                new Models.Customers.Customer() { id = 4, firstname = "Dottie", surname = "Kristoffersen", address = "696 Kedzie Circle", contact = "426-882-2642", canPurchase = false, isDeleted=false },
                new Models.Customers.Customer() { id = 5, firstname = "Denni", surname = "Eccersley", address = "7 Grim Point", contact = "589-699-8186", canPurchase = true, isDeleted=false }
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

            mockCustomer = new Mock<ICustomerProxy>(MockBehavior.Strict);
            mockReview = new Mock<IReviewProxy>(MockBehavior.Strict);

            controller = new CustomerController(mockCustomer.Object, mockReview.Object);
        }

        [TestMethod]
        public async Task Details_Parameters_Valid()
        {
            foreach (Models.Customers.Customer customer in customers)
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ReturnsAsync(customers.Find(c => c.id == customer.id));
                mockReview.Setup(s => s.GetReviews(null, customer.id)).ReturnsAsync(reviews.FindAll(r => r.userId == customer.id));

                var response = await controller.Details(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);
                var model = (Models.Customers.CustomerDetailsDTO)responseOk.Model;
                Assert.AreEqual(customer, model.Customer);
                foreach(Review review in model.Reviews)
                {
                    Assert.AreEqual(customer.id, review.userId);
                }

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(customer.id), Times.Once);
                mockReview.Verify(s => s.GetReviews(null, customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ReturnsAsync(customers.Find(c => c.id == id));
                mockReview.Setup(s => s.GetReviews(null, id)).ReturnsAsync(reviews.FindAll(r => r.userId == id));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as NotFoundResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Valid_Throws()
        {
            foreach (Models.Customers.Customer customer in customers)
            {
                mockCustomer.Setup(s => s.GetCustomer(customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.GetReviews(null, customer.id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(customer.id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);
                Assert.IsNull(responseOk.StatusCode);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(customer.id), Times.Once);
            }
        }

        [TestMethod]
        public async Task Details_Parameters_Invalid_Throws()
        {
            List<int> ids = new List<int> { 0, -5, 20, 420, 69, -1337 };
            foreach (int id in ids)
            {
                mockCustomer.Setup(s => s.GetCustomer(id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));
                mockReview.Setup(s => s.GetReviews(null, id)).ThrowsAsync(new SystemException("Could not receive data from remote service"));

                var response = await controller.Details(id);
                Assert.IsNotNull(response);
                var responseOk = response as ViewResult;
                Assert.IsNotNull(responseOk);

                mockCustomer.Verify();
                mockReview.Verify();
                mockCustomer.Verify(s => s.GetCustomer(id), Times.Once);
            }
        }
    }
}