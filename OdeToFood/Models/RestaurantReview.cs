﻿using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
	public class RestaurantReview
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		[Range(1, 10)]
		[Required]
		public int Rating { get; set; }
		[Required]
		[StringLength(1024)]
		public string Body { get; set; }
		public int RestaurantId { get; set; }
		public string ReviewerName { get; set; }
	}
}