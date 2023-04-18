namespace ClothesShop.Models
{
	public class Cart
	{
		public List<CartItem> items = new List<CartItem>();

		public IReadOnlyCollection<CartItem> Items => items.AsReadOnly();

		public void AddItem(Product product, string Size, string Color, int quantity)
		{
			var item = items.FirstOrDefault(i => i.Product.Id == product.Id);

			if (item != null)
			{
				item.Quantity += quantity;
			}
			else
			{
				items.Add(new CartItem
				{
					Product= product,
					Size=Size,
					Color=Color,
					Quantity = quantity
				});
			}
		}

		public void RemoveItem(int productId)
		{
			items.RemoveAll(i => i.Product.Id == productId);
		}

		public decimal GetTotal()
		{
			return items.Sum(i => i.Product.Price * i.Quantity);
		}

		public void Clear()
		{
			items.Clear();
		}
	}
}

