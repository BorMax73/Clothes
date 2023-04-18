using ClothesShop.Data;
using ClothesShop.Models;
using ClothesShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClothesShop.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationContext _context;

		public ProductController(ApplicationContext context)
		{
			_context = context;
		}
		public IActionResult Index(int? categoryId, string sortOrder, string searchString, int priceRange = 0)
		{
			// Отримання даних з корзини
			var cart = new Cart();
			if (HttpContext.Session.GetString("Cart") != null)
			{
				var jsonCart = HttpContext.Session.GetString("Cart");
				cart = JsonConvert.DeserializeObject<Cart>(jsonCart);
				
			}
			

			// Отримуємо всі категорії
			var categories = _context.Categories.ToList();

			// Отримуємо всі продукти
			var products = _context.Products.Include(p => p.Category).Include(x=>x.Images).ToList();

			//Сортуємо за ціною
			if (priceRange >= 0 & priceRange <=5)
			{
				switch (priceRange)
				{
					case 1:
						products = products.Where(p => p.Price >= 0 && p.Price <= 50).ToList();
						break;
					case 2:
						products = products.Where(p => p.Price > 50 && p.Price <= 100).ToList();
						break;
					case 3:
						products = products.Where(p => p.Price > 100 && p.Price <= 150).ToList();
						break;
					case 4:
						products = products.Where(p => p.Price > 150 && p.Price <= 200).ToList();
						break;
					case 5:
						products = products.Where(p => p.Price > 200).ToList();
						break;
				}
			}

			// Фільтруємо за категорією, якщо вона обрана
			if (categoryId.HasValue)
			{
				products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
			}
			else
			{
				categoryId = 0;
			}
			// Фільтруємо за рядком пошуку, якщо він заданий
			if (!string.IsNullOrEmpty(searchString))
			{
				products = products.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString)).ToList();
			}

			// Сортуємо продукти
			switch (sortOrder)
			{
				case "name_desc":
					products = products.OrderByDescending(p => p.Name).ToList();
					break;
				case "price":
					products = products.OrderBy(p => p.Price).ToList();
					break;
				case "price_desc":
					products = products.OrderByDescending(p => p.Price).ToList();
					break;
				default:
					products = products.OrderBy(p => p.Name).ToList();
					break;
			}

			// Створюємо ViewModel з продуктами та категоріями
			var viewModel = new ProductsViewModel
			{
				Products = products,
				Categories = categories,
				SelectedCategoryId = (int)categoryId,
				SearchString = searchString,
				SortOrder = sortOrder,
				Cart = cart
			};

			// Повертаємо відображення з ViewModel
			return View(viewModel);
		}
		public IActionResult Details(int id)
		{
			// Отримання даних з корзини
			var cart = new Cart();
			if (HttpContext.Session.GetString("Cart") != null)
			{
				var jsonCart = HttpContext.Session.GetString("Cart");
				cart = JsonConvert.DeserializeObject<Cart>(jsonCart);

			}

			var product = _context.Products.Include(x=>x.Images)
											.FirstOrDefault(p => p.Id == id);
			var size = _context.ProductSizes.Where(x=> x.ProductId == id).Include(x=>x.Size).ToList();
			var color = _context.ProductColors.Where(x => x.ProductId == id).Include(x=>x.Color).ToList();
			if (product == null)
			{
				return NotFound();
			}
			var viewModel = new DetailsViewModel()
			{
				Product = product,
				Colors = color,
				Sizes = size,

			};
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult AddToCart(int productId, string Size, string Color, int quantity)
		{
			// отримуємо товар за його Id
			var product = _context.Products.FirstOrDefault(p => p.Id == productId);

			if (product != null)
			{
				var cart = new Cart();
				// отримуємо поточну корзину
				var cartString = HttpContext.Session.GetString("Cart");
				if (cartString != null)
				{
					cart = JsonConvert.DeserializeObject<Cart>(cartString);
					cart.AddItem(product, Size, Color, quantity);
				}
				else
				{
					cart = new Cart();
					cart.AddItem(product, Size, Color, quantity);
				}
				// зберігаємо корзину в сесії
				HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

			}

			return RedirectToAction("Index");
		}

	}

}
