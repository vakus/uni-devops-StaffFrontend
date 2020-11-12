using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StaffFrontend.Models;
using StaffFrontend.Proxies;

namespace StaffFrontend.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewProxy _review;
        public ReviewController(IReviewProxy _review)
        {
            this._review = _review;
        }

        [HttpGet("/reviews/{itemid}")]
        // GET: /reviews/5
        public async Task<ActionResult> Index(int itemid)
        {
            return View(await _review.GetReviews(itemid));
        }

        [HttpGet("/reviews/view/{reviewid}")]
        // GET: /reviews/view/5
        public async Task<ActionResult> Details(int reviewid)
        {
            return View(await _review.GetReview(reviewid));
        }

        [HttpGet("/reviews/edit/{reviewid}")]
        // GET: /reviews/edit/5
        public async Task<ActionResult> Edit(int reviewid)
        {
            return View(await _review.GetReview(reviewid));
        }

        [HttpPost("/reviews/edit/{reviewid}")]
        // POST: /reviews/edit/5
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("reviewid,content,rating,hidden,itemid,createTime")] Review review)
        {
            Review rev = await _review.GetReview(review.reviewid);
            rev.hidden = review.hidden;
            await _review.UpdateReview(rev);
            return RedirectToAction(nameof(Index), new { itemid = review.itemid });
        }
    }
}
