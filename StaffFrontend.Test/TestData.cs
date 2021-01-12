using StaffFrontend.Models;
using StaffFrontend.Models.Customers;
using StaffFrontend.Models.Product;
using StaffFrontend.Models.Restock;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaffFrontend.test
{
    class TestData
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer>() {
                new Customer() {
                    id = 1,
                    firstname = "John",
                    surname = "Smith",
                    address = "0 Manufacturers Circle",
                    contact = "999-250-6512",
                    canPurchase = false,
                    isDeleted=false
                },
                new Customer() {
                    id = 2,
                    firstname = "Bethany",
                    surname = "Hulkes",
                    address = "0 Annamark Pass",
                    contact = "893-699-2769",
                    canPurchase = true,
                    isDeleted=false
                },
                new Customer() {
                    id = 3,
                    firstname = "Brigid",
                    surname = "Streak",
                    address = "2 Ruskin Crossing",
                    contact = "295-119-1574",
                    canPurchase = true,
                    isDeleted = false
                },
                new Customer() {
                    id = 4,
                    firstname = "Dottie",
                    surname = "Kristoffersen",
                    address = "696 Kedzie Circle",
                    contact = "426-882-2642",
                    canPurchase = false,
                    isDeleted = false
                },
                new Customer() {
                    id = 5,
                    firstname = "Denni",
                    surname = "Eccersley",
                    address = "7 Grim Point",
                    contact = "589-699-8186",
                    canPurchase = true,
                    isDeleted = true
                }
            };
        }

        public static List<Review> GetReviews()
        {
            return new List<Review>()
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
        }

        public static List<Product> GetProducts()
        {
            return new List<Product>() {
                new Product() { ID = 1, Name = "Lorem Ipsum", Description = "Lorem Ipsum", Price = 5.99m, Available = false, Supply = 2 },
                new Product() { ID = 2, Name = "Duck", Description = "Sometimes makes quack sound", Price = 99.99m, Available = true, Supply = 20 },
                new Product() { ID = 3, Name = "IPhone 13 pro max ultra plus 6G no screen edition", Description = "New Revolutionary IPhone. This year we managed to remove screen. Weights only 69g.", Price = 1399.99m, Available = true, Supply = 13 }
            };
        }

        public static List<Supplier> GetSuppliers()
        {
            return new List<Supplier>()
            {
                new Supplier(){
                    supplierID=1,
                    supplierName="test",
                    webaddress="example.com"
                }
            };
        }

        public static List<Restock> GetRestocks()
        {
            return new List<Restock>
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

        }
    }
}