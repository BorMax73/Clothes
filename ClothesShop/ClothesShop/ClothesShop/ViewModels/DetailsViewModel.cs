using ClothesShop.Models;

namespace ClothesShop.ViewModels
{
	public class DetailsViewModel : BaseViewModel
	{
		public Product Product { get; set; }

		public List<ProductColor> Colors { get; set; }
		public List<ProductSize> Sizes { get; set; }

	}
}
