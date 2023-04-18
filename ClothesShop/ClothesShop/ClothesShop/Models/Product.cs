namespace ClothesShop.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }

		public Category Category { get; set; }
		public string Gender { get; set; }
		public string Brand { get; set; }
		public List<ProductImage> Images { get; set; }
	}
	public enum Gender
	{
		Male,
		Female,
		Unisex
	}
}
