﻿using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
	public class ReviewsController : Controller
	{
		OdeToFoodDb _db = new OdeToFoodDb();
		[ChildActionOnly]
		public ActionResult BestReview()
		{
			var best = from r in _review
					   orderby r.Rating descending
					   select r;
			return PartialView("_Review", best.First());
		}
		public ActionResult LatestReviews()
		{
			var model = from r in _review
						orderby r.Country
						select r;
			return View(model);
		}
		// GET: Reviews
		public ActionResult Index([Bind(Prefix = "id")] int restaurantId)
		{
			var restaurant = _db.Restaurants.Find(restaurantId);
			if (restaurant != null)
			{
				return View(restaurant);
			}
			return HttpNotFound();
		}
		protected override void Dispose(bool disposing)
		{
			if (_db != null)
			{
				_db.Dispose();
			}
			base.Dispose(disposing);
		}

		[HttpGet]
		public ActionResult Create(int restaurantId)
		{
			var model = _db.Restaurants.Find(restaurantId);
			ViewBag.Name = model.Name;
			ViewBag.restaurantId = model.Id;
			return View();
		}
		[HttpPost]
		public ActionResult Create(RestaurantReview review)
		{
			if (ModelState.IsValid)
			{
				_db.Reviews.Add(review);
				_db.SaveChanges();
				return RedirectToAction("Index", new { id = review.RestaurantId });
			}
			return View();
		}
		// GET: Reviews/Details/5

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var model = _db.Reviews.Find(id);
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit([Bind(Exclude = "ReviewerName")]EditReviewViewModel review)
		{
			if (ModelState.IsValid)
			{
				var editable_review = _db.Reviews.Find(review.Id);
				editable_review.Body = review.Body;
				editable_review.Rating = review.Rating;
				_db.Entry(editable_review).State = EntityState.Modified;
				_db.SaveChanges();
				return RedirectToAction("Index", new { id = editable_review.RestaurantId });
			}
			return View(review);
		}
		static List<RestaurantReview> _review = new List<RestaurantReview>
		{
			new RestaurantReview
			{
				Id = 1,
				Name = "Cinamon Club",
				City = "London",
				Country = "UK",
				Rating = 10,
			},
			new RestaurantReview
			{
				Id = 1,
				Name = "Obamos",
				City = "Mmmmmmmm",
				Country = "USA",
				Rating = 6,
			},
			new RestaurantReview
			{
				Id = 1,
				Name = "YES",
				City = "Maybe",
				Country = "Definetly",
				Rating = 9,
			}
		};
		//public ActionResult Index([Bind(Prefix = "id")]
    }
}
