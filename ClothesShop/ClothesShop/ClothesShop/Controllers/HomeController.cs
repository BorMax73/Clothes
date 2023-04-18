using ClothesShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClothesShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			// Отримання даних з корзини
			var cart = new Cart();
			if (HttpContext.Session.GetString("Cart") != null)
			{
				var jsonCart = HttpContext.Session.GetString("Cart");
				cart = JsonConvert.DeserializeObject<Cart>(jsonCart);

			}
			ViewData["Cart"] = cart;
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}