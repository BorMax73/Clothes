using ClothesShop.Models;

namespace ClothesShop.ViewModels
{
	public class ProductsViewModel : BaseViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public IEnumerable<Category> Categories { get; set; }
		public int SelectedCategoryId { get; set; }
		public string SearchString { get; set; }
		public string SortOrder { get; set; }
	}
}
