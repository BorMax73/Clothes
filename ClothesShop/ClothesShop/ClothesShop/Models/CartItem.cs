namespace ClothesShop.Models
{
	public class CartItem
	{
		public Product Product { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public int Quantity { get; set; }
	}
}
