using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using StaffFrontend.Proxies;

namespace StaffFrontend.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class ReviewController : Controller
    {
        private readonly IReviewProxy _review;
        public ReviewController(IReviewProxy _review)
        {
            this._review = _review;
        }

        [HttpGet("/reviews/{itemid}")]
        // GET: /reviews/5
        public async Task<ActionResult> Index(int itemid, [FromQuery] bool? hidden)
        {
            List<Review> reviews;
            try
            {
                //default to visible comments if hidden doesnt have value
                if (!hidden.HasValue || !hidden.Value)
                {
                    reviews = await _review.GetReviews(itemid, null);
                }
                else
                {
                    reviews = await _review.GetHiddenReviews(itemid, null);
                }
            }
            catch (SystemException)
            {
                reviews = new List<Review>();
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
            }
            return View(reviews);
        }

        [HttpGet("/reviews/view/{reviewid}")]
        // GET: /reviews/view/5
        public async Task<ActionResult> Details(int reviewid)
        {
            Review review;
            try
            {
                review = await _review.GetReview(reviewid);

                if (review == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                review = new Review();
            }
            return View(review);
        }

        [HttpGet("/reviews/edit/{reviewid}")]
        // GET: /reviews/edit/5
        public async Task<ActionResult> Edit(int reviewid)
        {
            Review review;
            try
            {
                review = await _review.GetReview(reviewid);

                if (review == null)
                {
                    return NotFound();
                }
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to load data from remote service. Please try again.");
                review = new Review();
            }
            return View(review);
        }

        [HttpPost("/reviews/edit/{reviewid}")]
        // POST: /reviews/edit/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("reviewid,content,rating,hidden,itemid,createTime")] Review review)
        {
            try
            {
                Review rev = await _review.GetReview(review.reviewId);
                rev.hidden = review.hidden;
                await _review.UpdateReview(rev);
            }
            catch (SystemException)
            {
                ModelState.AddModelError("", "Unable to send data to remote service. Please try again.");
                return View(new Review());
            }
            return RedirectToAction(nameof(Index), new { itemid = review.productId });
        }
    }
}
