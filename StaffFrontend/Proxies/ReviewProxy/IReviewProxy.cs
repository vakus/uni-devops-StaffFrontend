using Microsoft.Extensions.Configuration;
using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies
{
    public interface IReviewProxy
    {
        Task<List<Review>> GetReviews(int? itemId, int? customerId);

        Task<List<Review>> GetHiddenReviews(int? itemId, int? customerId);

        Task<Review> GetReview(int reviewid);

        Task<double> GetRating(int itemid);

        Task DeleteByProductId(int productid);

        Task HideReview(int reviewid);

        Task UnhideReview(int reviewid);

    }
}