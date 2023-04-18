﻿namespace ClothesShop.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public string Size { get; set; }	
		public string Color { get; set; }
		public int OrderId { get; set; }

		public Product Product { get; set; }
		public Order Order
		{
			get; set;
		}
	}
}
